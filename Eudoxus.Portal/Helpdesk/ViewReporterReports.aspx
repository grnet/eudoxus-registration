<%@ Page Language="C#" MasterPageFile="~/Helpdesk/Helpdesk.Master" AutoEventWireup="true"
    Inherits="Eudoxus.Portal.Helpdesk.ViewReporterReports" Title="Προβολή Συμβάντων"
    CodeBehind="ViewReporterReports.aspx.cs" %>

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
    <asp:LinkButton ID="cmdRefresh" runat="server" Style="display: none;" />
    <div style="padding: 5px 0px 10px;">
        <a id="lnkAddIncidentReportForReporter" runat="server" class="icon-btn bg-addNewItem"
            href="javascript:void(0)" visible="false">Αναφορά συμβάντος για το συγκεκριμένο
            αναφέροντα </a>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <my:IncidentReportsGridview runat="server" ID="gvIncidentReports" DataSourceID="odsIncidentReports">
                <CustomTemplate>
                    <a id="lnkEditIncidentReport" runat="server" style="text-decoration: none" href="javascript:void(0)"
                        visible='<%# !((Reporter)Eval("Reporter") is Online) %>' onclick=<%# string.Format("popUp.show('EditIncidentReport.aspx?irID={0}','Επεξεργασία Συμβάντος', cmdRefresh);", Eval("ID"))%>>
                        <img src="/_img/iconReportEdit.png" alt="Επεξεργασία Συμβάντος" /></a> <a id="lnkViewIncidentReport"
                            runat="server" style="text-decoration: none" href="javascript:void(0)" onclick=<%# string.Format("popUp.show('ViewIncident.aspx?ID={0}','Προβολή Συμβάντος');", Eval("ID"))%>>
                            <img src="/_img/iconView.png" alt="Προβολή Συμβάντος" />
                        </a><a id="lnkAddIncidentReportPost" runat="server" style="text-decoration: none"
                            href="javascript:void(0)" onclick=<%# string.Format("popUp.show('AddIncidentReportPost.aspx?ID={0}','Προσθήκη Απάντησης', cmdRefresh);", Eval("ID"))%>>
                            <img src="/_img/iconAddNewItem.png" alt="Προσθήκη Απάντησης" />
                        </a>
                </CustomTemplate>
            </my:IncidentReportsGridview>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsIncidentReports" runat="server" TypeName="Eudoxus.Portal.DataSources.IncidentReports"
        SelectMethod="FindIncidentReportsWithCriteria" SelectCountMethod="CountIncidentReportsWithCriteria"
        EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsIncidentReports_Selecting">
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
