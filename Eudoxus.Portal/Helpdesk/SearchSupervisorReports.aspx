<%@ Page Language="C#" MasterPageFile="~/Helpdesk/Helpdesk.Master" AutoEventWireup="true" Inherits="Eudoxus.Portal.Helpdesk.SearchSupervisorReports"
    Title="Αναζήτηση Συμβάντων" CodeBehind="SearchSupervisorReports.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView"
    TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl"
    TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.2" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v9.2" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2.Export, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>
<%@ Register TagPrefix="my" TagName="IncidentReportsGridview" Src="~/Helpdesk/UserControls/IncidentReportsGridview.ascx" %>
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
    <script type="text/javascript">
        var isValueChanged = false;
        var ddlReportStatus = null;
        $(function () {
            ddlReportStatus = $('#ddlReportStatus');
            isValueChanged = ddlReportStatus.val() != '';
        });
    </script>

    <asp:LinkButton ID="cmdRefresh" runat="server" Style="display: none;" />
    <table id="tbFilters" runat="server" width="500px" class="dv">
        <th colspan="4" class="popupHeader">
            Στοιχεία Αναφοράς
        </th>
        <tr>
            <th style="width: 115px">
                Κατάσταση:
            </th>
            <td style="width: 120px">
                <asp:DropDownList ID="ddlReportStatus" runat="server" Width="115px" TabIndex="1" OnInit="ddlReportStatus_Init" ClientIDMode="Static" />
            </td>
            <th style="width: 90px">
                Τύπος Κλήσης:
            </th>
            <td style="width: 120px">
                <asp:DropDownList ID="ddlCallType" runat="server" Width="115px" TabIndex="6" OnInit="ddlCallType_Init" />
            </td>
        </tr>
        <tr>
            <th style="width: 115px">
                Αναφορά από:
            </th>
            <td style="width: 120px">
                <asp:DropDownList ID="ddlReportedBy" runat="server" TabIndex="2" OnInit="ddlReportedBy_Init" Width="115px" />
            </td>
            <th style="width: 90px">
                Ημ/νία (από):
            </th>
            <td style="width: 120px">
                <dxe:ASPxDateEdit ID="deIncidentReportDateFrom" runat="server" TabIndex="7" Width="115px" />
            </td>
        </tr>
        <tr>
            <th style="width: 115px">
                Πηγή αναφοράς:
            </th>
            <td style="width: 120px">
                <asp:DropDownList ID="ddlReporterType" runat="server" TabIndex="3" OnInit="ddlReporterType_Init" Width="115px" />
            </td>
            <th style="width: 90px">
                Ημ/νία (έως):
            </th>
            <td style="width: 120px">
                <dxe:ASPxDateEdit ID="deIncidentReportDateTo" runat="server" TabIndex="8" Width="115px" />
            </td>
        </tr>
        <tr>
            <th style="width: 115px">
                Είδος αναφοράς:
            </th>
            <td colspan="3" style="width: 350px">
                <asp:DropDownList ID="ddlIncidentType" runat="server" TabIndex="4" Width="355px" DataTextField="Name" DataValueField="ID" />
                <ajaxToolkit:CascadingDropDown ID="cddIncidentType" runat="server" TargetControlID="ddlIncidentType" ParentControlID="ddlReporterType"
                    Category="IncidentTypes" PromptText="-- επιλέξτε πηγή αναφοράς --" ServicePath="~/PortalServices/Services.asmx" ServiceMethod="GetIncidentTypes"
                    LoadingText="Παρακαλω περιμένετε">
                </ajaxToolkit:CascadingDropDown>
            </td>
        </tr>
         <tr>
            <th style="width: 115px">
                Επικοινωνία:
            </th>
            <td colspan="3" style="width: 350px">
                <asp:DropDownList ID="ddlHandlerStatus" runat="server" Width="355px" TabIndex="5" OnInit="ddlHandlerStatus_Init" ClientIDMode="Static" />
            </td>
        </tr>
    </table>
    <div style="padding: 5px 0px 10px;">
        <asp:LinkButton ID="btnSearch" runat="server" Text="Αναζήτηση" OnClick="btnSearch_Click" CssClass="icon-btn bg-search" />
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <my:IncidentReportsGridview runat="server" ID="gvIncidentReports" DataSourceID="odsIncidentReports">
                <CustomTemplate>
                    <a id="lnkEditIncidentReport" runat="server" style="text-decoration: none" href="javascript:void(0)" visible='<%# !((Reporter)Eval("Reporter") is Online) && !Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.Supervisor) %>'
                        onclick=<%# string.Format("popUp.show('EditIncidentReport.aspx?irID={0}','Επεξεργασία Συμβάντος', cmdRefresh);", Eval("ID"))%>>
                        <img src="/_img/iconReportEdit.png" alt="Επεξεργασία Συμβάντος" /></a> <a id="lnkViewIncidentReport" runat="server" style="text-decoration: none"
                            href="javascript:void(0)" onclick=<%# string.Format("popUp.show('ViewIncident.aspx?ID={0}','Προβολή Συμβάντος');", Eval("ID"))%>>
                            <img src="/_img/iconView.png" alt="Προβολή Συμβάντος" />
                        </a><a id="lnkAddIncidentReportPost" runat="server" style="text-decoration: none" href="javascript:void(0)" onclick=<%# string.Format("popUp.show('AddIncidentReportPost.aspx?ID={0}','Προσθήκη Απάντησης', cmdRefresh);", Eval("ID"))%>>
                            <img src="/_img/iconAddNewItem.png" alt="Προσθήκη Απάντησης" />
                        </a>
                </CustomTemplate>
            </my:IncidentReportsGridview>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsIncidentReports" runat="server" TypeName="Eudoxus.Portal.DataSources.IncidentReports" SelectMethod="FindIncidentReportsWithCriteria"
        SelectCountMethod="CountIncidentReportsWithCriteria" EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsIncidentReports_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dxpc:ASPxPopupControl ID="dxpcPopup" runat="server" ClientInstanceName="devExPopup" Width="800" Height="610" Modal="true" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" AllowDragging="true" CloseAction="CloseButton">
    </dxpc:ASPxPopupControl>

    <script type="text/javascript">
        function cmdRefresh() {
            <%= this.Page.ClientScript.GetPostBackEventReference(this.cmdRefresh, string.Empty) %>;
        }
    </script>

</asp:Content>
