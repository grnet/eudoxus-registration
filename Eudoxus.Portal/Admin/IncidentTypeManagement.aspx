<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    Inherits="Eudoxus.Portal.Admin.IncidentTypeManagement" Title="Διαχείριση Υποσυστημάτων"
    CodeBehind="IncidentTypeManagement.aspx.cs" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .dxgvHeader td
        {
            font-size: 11px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:LinkButton ID="cmdRefresh" runat="server" Style="display: none;" OnClick="cmdRefresh_Click" />
    <table id="tbFilters" runat="server" width="400px" class="dv">
        <tr>
            <th colspan="2" class="popupHeader">
                Επιλογή Υποσυστήματος
            </th>
        </tr>
        <tr>
            <th style="width: 80px">
                Υποσύστημα:
            </th>
            <td style="width: 310px">
                <asp:DropDownList ID="ddlSubSystem" runat="server" OnInit="ddlSubSystem_Init" Width="300px"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlSubSystem_SelectedIndexChanged" />
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlSubSystem" />
        </Triggers>
        <ContentTemplate>
            <div style="padding: 15px 0px 5px;">
                <a id="lnkCreateIncidentType" runat="server" visible="false" class="icon-btn bg-addNewItem"
                    href="javascript:void(0)">Δημιουργία Τύπου Συμβάντος</a>
            </div>
            <dxwgv:ASPxGridView ID="gvIncidentTypes" runat="server" AutoGenerateColumns="False"
                DataSourceID="odsIncidentTypes" KeyFieldName="ID" EnableRowsCache="false" EnableCallBacks="true"
                Width="100%" DataSourceForceStandardPaging="true" OnRowCommand="gvIncidentTypes_RowCommand">
                <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε..." />
                <SettingsPager PageSize="10" Summary-Text="Σελίδα {0} από {1} ({2} Τύποι Συμβάντων)"
                    Summary-Position="Left" />
                <Styles>
                    <Cell Font-Size="11px" />
                </Styles>
                <Templates>
                    <EmptyDataRow>
                        Δεν βρέθηκαν αποτελέσματα
                    </EmptyDataRow>
                </Templates>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Α/Α" HeaderStyle-HorizontalAlign="Center"
                        CellStyle-HorizontalAlign="Center" Width="50px">
                        <DataItemTemplate>
                            <%# Container.ItemIndex + 1 %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Name" Name="Name" Caption="Όνομα" CellStyle-HorizontalAlign="Left">
                        <DataItemTemplate>
                            <%# Eval("Name")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SubSystem.Name" Name="SubSystem.Name" Caption="Υποσύστημα"
                        CellStyle-HorizontalAlign="Left">
                        <DataItemTemplate>
                            <%# Eval("SubSystem.Name")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Τύποι Αναφερόντων" CellStyle-HorizontalAlign="Left">
                        <DataItemTemplate>
                            <%# GetReporterTypes((IncidentType)Container.DataItem) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Συμβάντα που έχουν αναφερθεί" HeaderStyle-HorizontalAlign="Center"
                        CellStyle-HorizontalAlign="Center">
                        <DataItemTemplate>
                            <%# GetIncidentCount((IncidentType)Container.DataItem)%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Επεξεργασία Τύπου Συμβάντος" Width="80px">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                        <CellStyle HorizontalAlign="Center" />
                        <DataItemTemplate>
                            <a id="lnkEditIncidentType" runat="server" style="text-decoration: none" href="javascript:void(0)"
                                onclick=<%# string.Format("popUp.show('EditIncidentType.aspx?itID={0}', 'Επεξεργασία Τύπου Συμβάντος', cmdRefresh);", Eval("ID"))%>>
                                <img src="/_img/iconEdit.png" alt="Επεξεργασία Τύπου Συμβάντος" />
                            </a>
                            <asp:LinkButton ID="lnkDeleteIncidentType" runat="server" CommandName="DeleteIncidentType"
                                CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Είστε σίγουροι ότι θέλετε να διαγράψετε το συγκρεκριμένο τύπο συμβάντος;');"
                                Style="text-decoration: none;" ToolTip="Διαγραφή Τύπου Συμβάντος" Visible='<%# GetIncidentCount((IncidentType)Container.DataItem) == 0 %>'>
                                    <img src="/_img/iconDelete.png" alt="Διαγραφή Τύπου Συμβάντος" /></asp:LinkButton>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsIncidentTypes" runat="server" TypeName="Eudoxus.Portal.DataSources.IncidentTypes"
        SelectMethod="FindIncidentTypes" SelectCountMethod="CountIncidentTypes" EnablePaging="true"
        SortParameterName="sortExpression" OnSelecting="odsIncidentTypes_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dxpc:ASPxPopupControl ID="dxpcPopup" runat="server" ClientInstanceName="devExPopup"
        Width="800" Height="610" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        AllowDragging="true" CloseAction="CloseButton">
    </dxpc:ASPxPopupControl>

    <script type="text/javascript">
        function cmdRefresh() {
            <%= this.Page.ClientScript.GetPostBackEventReference(this.cmdRefresh, string.Empty) %>;
        }
    </script>

</asp:Content>
