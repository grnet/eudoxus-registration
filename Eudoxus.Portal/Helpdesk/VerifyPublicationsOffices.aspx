<%@ Page Language="C#" MasterPageFile="~/Helpdesk/Helpdesk.Master" AutoEventWireup="true"
    Inherits="Eudoxus.Portal.Helpdesk.VerifyPublicationsOffices" Title="Πιστοποίηση Γραφείων Δημοσιευμάτων"
    CodeBehind="VerifyPublicationsOffices.aspx.cs" %>

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
<%@ Register Src="~/UserControls/EmailForm.ascx" TagName="EmailForm" TagPrefix="my" %>
<%@ Import Namespace="Eudoxus.BusinessModel" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .dxgvHeader td
        {
            font-size: 11px;
        }
        .dxeRadioButtonList td.dxe
        {
            padding: 2px 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:LinkButton ID="cmdRefresh" runat="server" Style="display: none;" OnClick="cmdRefresh_Click" />
    <table width="940px" class="dv">
        <tr>
            <th colspan="6" class="popupHeader">
                Φίλτρα Αναζήτησης
            </th>
        </tr>
        <tr>
            <th style="width: 100px">
                Πιστοποιημένος:
            </th>
            <td style="width: 150px">
                <asp:DropDownList ID="ddlVerificationStatus" runat="server" Width="100%" TabIndex="1">
                    <asp:ListItem Text="-- αδιάφορο --" Value="-1" Selected="True" />
                    <asp:ListItem Text="Όχι" Value="0" />
                    <asp:ListItem Text="Ναι" Value="1" />
                    <asp:ListItem Text="Δεν μπορεί να πιστοποιηθεί" Value="2" />
                </asp:DropDownList>
            </td>
            <th style="width: 140px">
                Αρ. Βεβαίωσης:
            </th>
            <td style="width: 150px">
                <asp:TextBox ID="txtCertificationNumber" runat="server" TabIndex="4" Width="95%" />
            </td>
            <th style="width: 80px">
                Ίδρυμα:
            </th>
            <td style="width: 300px">
                <asp:DropDownList ID="ddlInstitution" runat="server" TabIndex="7" Width="100%" OnInit="ddlInstitution_Init" />
            </td>
        </tr>
        <tr>
            <th style="width: 100px">
                Ενεργοποιημένος:
            </th>
            <td style="width: 150px">
                <asp:DropDownList ID="ddlActivationStatus" runat="server" Width="100%" TabIndex="2">
                    <asp:ListItem Text="-- αδιάφορο --" Value="-1" Selected="True" />
                    <asp:ListItem Text="Όχι" Value="0" />
                    <asp:ListItem Text="Ναι" Value="1" />
                </asp:DropDownList>
            </td>
            <th style="width: 140px">
                Ημ/νία Βεβαίωσης:
            </th>
            <td style="width: 200px">
                <dxe:ASPxDateEdit ID="deCertificationDate" runat="server" TabIndex="5" Width="98%" />
            </td>
            <th style="width: 80px">
                Τύπος:
            </th>
            <td style="width: 300px">
                <asp:DropDownList ID="ddlInstitutionType" runat="server" TabIndex="8" Width="100%" OnInit="ddlInstitutionType_Init" />
            </td>
        </tr>
        <tr>
            <th style="width: 100px">
                Username:
            </th>
            <td style="width: 200px">
                <asp:TextBox ID="txtUserName" runat="server" TabIndex="3" Width="95%" />
            </td>
            <th style="width: 100px">
                E-mail:
            </th>
            <td style="width: 200px">
                <asp:TextBox ID="txtEmail" runat="server" TabIndex="6" Width="95%" />
            </td>
            <th />
            <th />
        </tr>
    </table>
    <div style="padding: 5px 0px 10px;">
        <asp:LinkButton ID="btnSearch" runat="server" Text="Αναζήτηση" OnClick="btnSearch_Click"
            CssClass="icon-btn bg-search" />
        <%--<asp:LinkButton ID="btnExport" runat="server" Text="Εξαγωγή σε Excel" OnClick="btnExport_Click"
            CssClass="icon-btn bg-excel" />--%>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <dxwgv:ASPxGridView ID="gvPublicationsOffices" runat="server" AutoGenerateColumns="False"
                DataSourceID="odsPublicationsOffices" KeyFieldName="ID" EnableRowsCache="false" EnableCallBacks="true"
                Width="100%" DataSourceForceStandardPaging="true" OnHtmlRowPrepared="gvPublicationsOffices_HtmlRowPrepared">
                <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε..." />
                <SettingsPager PageSize="10" Summary-Text="Σελίδα {0} από {1} ({2} Γραφεία Δημοσιευμάτων)" Summary-Position="Left" />
                <Styles>
                    <Cell Font-Size="11px" />
                </Styles>
                <Templates>
                    <EmptyDataRow>
                        Δεν βρέθηκαν αποτελέσματα
                    </EmptyDataRow>
                </Templates>
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="CreatedAt" Name="CreatedAt" Caption="Ημ/νία Δημιουργίας"
                        CellStyle-Wrap="False" CellStyle-HorizontalAlign="Left" SortIndex="0" SortOrder="Descending" Width="100px">
                        <DataItemTemplate>
                            <%# ((DateTime)Eval("CreatedAt")).ToString("dd/MM/yyyy HH:mm")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ID Γραφείου" HeaderStyle-HorizontalAlign="Center"
                        CellStyle-HorizontalAlign="Center">
                        <DataItemTemplate>
                            <%# Eval("ID") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Στοιχεία Γραφείου" CellStyle-HorizontalAlign="Left">
                        <DataItemTemplate>
                            <%# GetPublicationsOfficeDetails((PublicationsOffice)Container.DataItem)%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Στοιχεία Λογαριασμού" CellStyle-HorizontalAlign="Left">
                        <DataItemTemplate>
                            <%# Eval("UserName")%><br />
                            <%# Eval("Email")%><br />
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Επεξεργασία" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Center"
                        CellStyle-HorizontalAlign="Center" Width="50">
                        <DataItemTemplate>
                            <a id="lnkViewAccountDetails" runat="server" style="text-decoration: none" href="javascript:void(0)"
                                onclick=<%# string.Format("popUp.show('ViewAccountDetails.aspx?rid={0}&t={1}', 'Στοιχεία Λογαριασμού');", Eval("ID"), (enReporterType.PublicationsOffice).ToString("D"))%>>
                                <img src="/_img/iconUserEdit.png" alt="Στοιχεία Λογαριασμού" />
                            </a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Στοιχεία Βεβαίωσης" CellStyle-HorizontalAlign="Left">
                        <DataItemTemplate>
                            <%# Eval("CertificationNumber")%>
                            /
                            <%# string.Format("{0:dd-MM-yyyy}", Eval("CertificationDate"))%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Πλήρη Στοιχεία" Width="100px">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                        <CellStyle HorizontalAlign="Center" />
                        <DataItemTemplate>
                            <a id="lnkViewPublicationsOfficeDetails" runat="server" style="text-decoration: none" href="javascript:void(0)"
                                onclick=<%# string.Format("popUp.show('VerifyPublicationsOffice.aspx?pID={0}','Προβολή Στοιχείων Γραφείου Δημοσιευμάτων',cmdRefresh);", Eval("ID"))%>>
                                <img src="/_img/iconViewDetails.png" alt="Προβολή Στοιχείων" />
                            </a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Αλλαγή Στοιχείων" Width="100px">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                        <CellStyle HorizontalAlign="Center" />
                        <DataItemTemplate>
                            <a id="lnkEditPublicationsOfficeDetails" runat="server" style="text-decoration: none" href="javascript:void(0)"
                                visible='<%# (enVerificationStatus)Eval("VerificationStatus") == enVerificationStatus.Verified %>'
                                onclick=<%# string.Format("popUp.show('EditPublicationsOffice.aspx?pID={0}','Αλλαγή Στοιχείων Γραφείου Δημοσιευμάτων',cmdRefresh);", Eval("ID"))%>>
                                <img src="/_img/iconReportEdit.png" alt="Αλλαγή Στοιχείων Εκδότη" />
                            </a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Προβολή Συμβάντων" Width="50px">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                        <CellStyle HorizontalAlign="Center" />
                        <DataItemTemplate>
                            <a id="lnkShowIncidentReport" runat="server" style="text-decoration: none" href='<%# string.Format("~/Helpdesk/ViewReporterReports.aspx?rID={0}", Eval("ID"))%>'>
                                <img src="/_img/iconView.png" alt="Προβολή Συμβάντων" />
                            </a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Αναφορά Συμβάντος" Width="50px">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                        <CellStyle HorizontalAlign="Center" />
                        <DataItemTemplate>
                            <a id="lnkAddIncidentReport" runat="server" style="text-decoration: none" href="javascript:void(0)"
                                onclick=<%# string.Format("popUp.show('ReportIncident.aspx?rID={0}','Αναφορά Συμβάντος', cmdRefresh);", Eval("ID"))%>>
                                <img src="/_img/iconAddNewItem.png" alt="Προσθήκη Συμβάντος" />
                            </a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsPublicationsOffices" runat="server" TypeName="Eudoxus.Portal.DataSources.PublicationsOffices"
        SelectMethod="FindPublicationsOfficesWithCriteria" SelectCountMethod="CountPublicationsOfficesWithCriteria"
        EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsPublicationsOffices_Selecting"
        OnSelected="odsPublicationsOffices_Selected">
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
    <br />
    <br />
    <my:EmailForm ID="EmailForm1" runat="server" OnEmailSending="OnEmailSending" />
</asp:Content>
