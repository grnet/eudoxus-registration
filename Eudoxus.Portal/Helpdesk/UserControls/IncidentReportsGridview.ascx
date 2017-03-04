<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncidentReportsGridview.ascx.cs"
    Inherits="Eudoxus.Portal.Helpdesk.UserControls.IncidentReportsGridview" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.2" Namespace="DevExpress.Web.ASPxCallbackPanel"
    TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v9.2" Namespace="DevExpress.Web.ASPxPanel"
    TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2.Export, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>
<%@ Import Namespace="Eudoxus.BusinessModel" %>
<style type="text/css">
    .dxgvHeader td
    {
        font-size: 11px;
    }
    .img-btn
    {
        border: dashed 1px gray;
        padding: 2px 0px 4px 0px;
        outline: none;
    }
    .img-btn:hover
    {
        background-color: lightgray;
    }
</style>
<% if (DesignMode) { %>
<script src="/_js/jquery.js" type="text/javascript"></script>
<script src="/_js/jquery-impromptu.1.8.js" type="text/javascript"></script>
<% }%>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/_js/jquery-impromptu.1.8.js" />
    </Scripts>
</asp:ScriptManagerProxy>
<script type="text/javascript">
    var currentElement = null;
    $.fn.extend({
        dropIn: function (speed, callback) {
            var $t = $(this);

            if ($t.css("display") == "none") {
                eltop = $t.css('top');
                elouterHeight = $t.outerHeight(true);

                $t.css({ top: -elouterHeight + 100, display: 'block' }).animate({ top: eltop }, speed, 'swing', callback);
            }
        }
    });
    function showPopup(elem, id, oldStatus) {
        var status = oldStatus;

        currentElement = $(elem);
        var updatedStatus = currentElement.find('img').attr('rel');
        if (updatedStatus && updatedStatus != '')
            status = updatedStatus;

        var txt = $('#popTemplate').html();
        var regex = null;
        if (status == 1)
            regex = /<option value="1">.+<\/option>/;
        else if (status == 2)
            regex = /<option value="2">.+<\/option>/;
        else if (status == 3)
            regex = /<option value="3">.+<\/option>/;
        txt = txt.replace(regex, '')
        $.prompt(txt, { speed: 'fast', callback: beginChangeStatus, show: 'dropIn', buttons: { 'Αλλαγή Κατάστασης': id, Ακύρωση: false} });
        return false;
    }

    function beginChangeStatus(v, m, f) {
        if (v == undefined || v == false)
            return;

        var userContext = {};
        userContext.doRefresh = false;
        if (typeof (isValueChanged) !== 'undefined')
            userContext.doRefresh = isValueChanged;
        var newStatus = m.find('#newStatus').val();
        userContext.newStatus = newStatus;
        PageMethods.ChangeStatus(v, newStatus, onCompleted, onFailed, userContext);
    }

    function onFailed(args) {

    }

    function onCompleted(args, userContext) {
        if (args != null) {
            if (args != "error") {

                if (userContext.doRefresh) {
                    cmdRefresh();
                }
                else {
                    currentElement.html(args.replace('>', 'rel="' + userContext.newStatus + '" >'));
                }
            }
            else {
                $.prompt('Η αλλαγή δεν πραγματοποιήθηκε. Παρακαλούμε δοκιμάστε ξανά αργότερα.');
            }
        }
        else {
            $.prompt('Η αλλαγή δεν πραγματοποιήθηκε. Παρακαλούμε δοκιμάστε ξανά αργότερα.');
        }
    }
    
</script>
<div style="display: none" id="popTemplate">
    Νέα Κατάσταση:
    <select id="newStatus" name="newStatus">
        <option value="1">Εκκρεμεί</option>
        <option value="2">Έχει απαντηθεί</option>
        <option value="3">Έχει κλείσει</option>
    </select>
</div>
<dxwgv:ASPxGridView ID="gvIncidentReports" runat="server" AutoGenerateColumns="False"
    DataSourceID="odsIncidentReports" KeyFieldName="ID" EnableRowsCache="false" EnableCallBacks="true"
    Width="100%" DataSourceForceStandardPaging="true" OnRowCommand="gvIncidentReports_RowCommand"
    EnableViewState="false" ViewStateMode="Disabled">
    <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε..." />
    <SettingsPager PageSize="10" Summary-Text="Σελίδα {0} από {1} ({2} Αναφορές)" Summary-Position="Left" />
    <Styles>
        <Cell Font-Size="11px" />
    </Styles>
    <Templates>
        <EmptyDataRow>
            Δεν βρέθηκαν αποτελέσματα
        </EmptyDataRow>
    </Templates>
    <Columns>
        <dxwgv:GridViewDataTextColumn FieldName="CreatedAt" Name="CreatedAt" Caption="Ημ/νία Αναφοράς"
            CellStyle-Wrap="False" CellStyle-HorizontalAlign="Left" SortIndex="0" SortOrder="Descending" Width="100px">
            <DataItemTemplate>
                <%# Eval("CreatedAt")!=null?((DateTime)Eval("CreatedAt")).ToString("dd/MM/yyyy HH:mm") : "" %><br />
                Χρήστης:
                <%# Eval("CreatedBy")%>
                <asp:PlaceHolder ID="phUpdatedAt" runat="server" Visible='<%# Eval("UpdatedBy") != null%>'>
                    <br />
                    <br />
                    <span style="font-size: 11px; font-weight: bold">Τροποποίηση</span><br />
                    <%# Eval("UpdatedAt") != null ? ((DateTime)Eval("UpdatedAt")).ToString("dd/MM/yyyy HH:mm") : ""%><br />
                    Χρήστης:
                    <%# Eval("UpdatedBy")%>
                </asp:PlaceHolder>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="Reporter.ReporterType" Name="Reporter.ReporterType"
            Caption="Στοιχεία Αναφοράς" CellStyle-HorizontalAlign="Left" Settings-AllowSort="False">
            <DataItemTemplate>
                <%# GetIncidentTypeDetails((IncidentReport)Container.DataItem)%>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="ReporterName" Name="ReporterName" Caption="Στοιχεία Ατόμου Επικοινωνίας"
            CellStyle-HorizontalAlign="Left" Settings-AllowSort="False">
            <DataItemTemplate>
                <%# Eval("ReporterName")%><br />
                <%# Eval("ReporterPhone")%><br />
                <%# Eval("ReporterEmail")%>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn Name="SpecialDetailsOfReporter" Caption="Ειδικά Στοιχεία Αναφέροντος"
            CellStyle-HorizontalAlign="Left" Settings-AllowSort="False">
            <DataItemTemplate>
                <%# GetReporterDetails(Eval("Reporter"))%>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="ReportStatusInt" Name="ReportStatus" Caption="Κατάσταση"
            CellStyle-HorizontalAlign="Center" Settings-AllowSort="False">
            <DataItemTemplate>
                <a href="#" class="cmd-btn" onclick='return showPopup(this,<%# Eval("ID")+ ","+ (Eval("ReportStatus")!=null ? ((enReportStatus)Eval("ReportStatus")).ToString("D") : "")%>)'>
                    <%# Eval("ReportStatus")!=null ? ((enReportStatus)Eval("ReportStatus")).GetIcon() : ""%></a>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="ReportText" Name="ReportText" Caption="Κείμενο Αναφοράς"
            CellStyle-HorizontalAlign="Left" Settings-AllowSort="False" />
        <dxwgv:GridViewDataTextColumn FieldName="LastPost.PostText" Name="LastPost.PostText"
            Caption="Τελευταία Απάντηση" CellStyle-HorizontalAlign="Left" Settings-AllowSort="False">
            <DataItemTemplate>
                <asp:PlaceHolder ID="phDispatchSentAt" runat="server" Visible='<%# Eval("LastPost.LastDispatch.DispatchSentBy") != null%>'>
                    <span style="font-size: 11px; font-weight: bold">Ημ/νία Αποστολής</span><br />
                    <%# Eval("LastPost.LastDispatch.DispatchSentBy") != null ? ((DateTime)Eval("LastPost.LastDispatch.DispatchSentAt")).ToString("dd/MM/yyyy HH:mm") : ""%><br />
                    Χρήστης:
                    <%# Eval("LastPost.LastDispatch.DispatchSentBy")%><br />
                    <br />
                    <%# Eval("LastPost.PostText")%>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phNoDispatchSent" runat="server" Visible='<%# Eval("LastPost.LastDispatch.DispatchSentBy") == null && Eval("LastPost.PostText") != null%>'>
                    <%# Eval("LastPost.PostText") %>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phNoAnswerExists" runat="server" Visible='<%# Eval("LastPost.PostText") == null%>'>
                    &nbsp; </asp:PlaceHolder>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="HandlerTypeInt" Name="HandlerType" Caption="Χειρισμός Από"
            HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" Width="100px"
            Settings-AllowSort="False">
            <DataItemTemplate>
                <table>
                    <tr>
                        <td>
                            <a id="lnkEditIncidentReportHandlerInput" runat="server" href="javascript:void(0)"
                                onclick=<%# string.Format("popUp.show('EditIncidentReportHandler.aspx?irID={0}','Επεξεργασία Χειριστή Συμβάντος', cmdRefresh)", Eval("ID")) %>>
                                <img src="/_img/iconReportEdit.png" alt="Επεξεργασία Χειριστή Συμβάντος" />
                            </a>
                        </td>
                        <td>
                            <%# GetHandlerDetails((IncidentReport)Container.DataItem)%>
                        </td>
                    </tr>
                </table>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn Name="Commands" CellStyle-Wrap="False" Settings-AllowSort="False">
        </dxwgv:GridViewDataTextColumn>
    </Columns>
</dxwgv:ASPxGridView>
<dxpc:ASPxPopupControl ID="dxpcPopup" runat="server" ClientInstanceName="devExPopup"
    Width="800" Height="610" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    AllowDragging="true" CloseAction="CloseButton">
</dxpc:ASPxPopupControl>
