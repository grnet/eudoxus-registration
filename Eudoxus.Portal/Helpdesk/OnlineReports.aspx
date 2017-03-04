<%@ Page Language="C#" MasterPageFile="~/Helpdesk/Helpdesk.Master" AutoEventWireup="true"
    Inherits="Eudoxus.Portal.Helpdesk.OnlineReports" Title="Αναζήτηση Συμβάντων"
    CodeBehind="OnlineReports.aspx.cs" %>

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
<%@ Register TagPrefix="my" TagName="IncidentReportsGridview" Src="~/Helpdesk/UserControls/IncidentReportsGridview.ascx" %>
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
            ddlReportStatus = $('#<%= ddlReportStatus.ClientID %>');
            isValueChanged = ddlReportStatus.val() != '';
        });
    </script>
    <asp:LinkButton ID="cmdRefresh" runat="server" Style="display: none;" OnClick="cmdRefresh_Click" />
    <div style="width: 1500px;">
        <div class="left">
            <table width="820px" class="dv">
                <tr>
                    <th colspan="6" class="popupHeader">
                        Στοιχεία Αναφοράς
                    </th>
                </tr>
                <tr>
                    <th style="width: 115px">
                        Χειρισμός Από:
                    </th>
                    <td style="width: 120px">
                        <asp:DropDownList ID="ddlHandlerType" runat="server" Width="115px" TabIndex="1" OnInit="ddlHandlerType_Init"
                            ClientIDMode="Static" />
                    </td>
                    <th style="width: 90px">
                        Επικοινωνία:
                    </th>
                    <td style="width: 120px">
                        <asp:DropDownList ID="ddlHandlerStatus" runat="server" Width="115px" TabIndex="5"
                            OnInit="ddlHandlerStatus_Init" />
                    </td>
                    <th style="width: 90px">
                        Ίδρυμα:
                    </th>
                    <td colspan="3" style="width: 230px">
                        <asp:TextBox ID="txtInstitutionName" runat="server" TabIndex="9" Width="80%" />
                        <a href="javascript:void(0);" id="lnkSelectSchool" onclick="popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');">
                            <img id="Img1" runat="server" align="absmiddle" src="~/_img/iconSelectSchool.png"
                                alt="Επιλογή Σχολής" /></a> <a href="javascript:void(0);" id="lnkRemoveSchoolSelection"
                                    onclick="return hd.removeSchoolSelection();" style="display: none;">
                                    <img id="Img2" runat="server" align="absmiddle" src="~/_img/iconRemoveSchool.png"
                                        alt="Αφαίρεση Σχολής" /></a>
                        <asp:HiddenField ID="hfSchoolCode" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th style="width: 115px">
                        Κατάσταση:
                    </th>
                    <td style="width: 120px">
                        <asp:DropDownList ID="ddlReportStatus" runat="server" Width="115px" TabIndex="2"
                            OnInit="ddlReportStatus_Init" />
                    </td>
                    <th style="width: 75px">
                        Ημ/νία (από):
                    </th>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="deIncidentReportDateFrom" runat="server" TabIndex="6" Width="115px" />
                    </td>
                     <th style="width: 90px">
                        Σχολή:
                    </th>
                    <td style="width: 230px">
                        <asp:TextBox ID="txtSchoolName" runat="server" TabIndex="10" Width="80%" />
                    </td>
                </tr>
                <tr>
                    <th style="width: 115px">
                        Πηγή αναφοράς:
                    </th>
                    <td style="width: 120px">
                        <asp:DropDownList ID="ddlReporterType" runat="server" TabIndex="3" OnInit="ddlReporterType_Init"
                            Width="115px" />
                    </td>
                    <th style="width: 75px">
                        Ημ/νία (έως):
                    </th>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="deIncidentReportDateTo" runat="server" TabIndex="7" Width="115px" />
                    </td>
                    <th style="width: 90px">
                        Τμήμα:
                    </th>
                    <td style="width: 230px">
                        <asp:TextBox ID="txtDepartmentName" runat="server" TabIndex="11" Width="80%" />
                    </td>
                </tr>
                <tr>
                    <th style="width: 115px">
                        Είδος αναφοράς:
                    </th>
                    <td style="width: 120px">
                        <asp:DropDownList ID="ddlIncidentType" runat="server" TabIndex="4" Width="115px"
                            DataTextField="Name" DataValueField="ID" />
                        <ajaxToolkit:CascadingDropDown ID="cddIncidentType" runat="server" TargetControlID="ddlIncidentType"
                            ParentControlID="ddlReporterType" Category="IncidentTypes" PromptText="-- αδιάφορο --"
                            ServicePath="~/PortalServices/Services.asmx" ServiceMethod="GetIncidentTypes"
                            LoadingText="Παρακαλω περιμένετε">
                        </ajaxToolkit:CascadingDropDown>
                    </td>
                    <th style="width: 75px">
                        ID αναφοράς:
                    </th>
                    <td style="width: 120px">
                        <asp:TextBox ID="txtReportID" runat="server" TabIndex="5" Width="110px" />
                    </td>
                    <th />
                    <th />
                </tr>
                <tr>
            <th style="width: 115px">
                Τελ. απάντηση από:
            </th>
            <td colspan="5" style="width: 350px">
                <asp:DropDownList ID="ddlUpdatedBy" runat="server" TabIndex="8" Width="345px" DataTextField="Name"
                    DataValueField="ID" OnInit="ddlUpdatedBy_Init" />
            </td>
        </tr>
            </table>
            <div style="padding: 5px 0px 10px;">
                <asp:LinkButton ID="btnSearch" runat="server" Text="Αναζήτηση" OnClick="btnSearch_Click"
                    CssClass="icon-btn bg-search" />
            </div>
        </div>
        <div class="left" style="padding-left: 20px">
            <table width="320px" class="dv">
                <tr>
                    <th style="width: 200px">
                        Ημ/νία Εξαγωγής (από):
                    </th>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="deDateExportFrom" runat="server" Width="115px">
                            <validationsettings validationgroup="vgExport" errordisplaymode="ImageWithTooltip">
                                <RequiredField IsRequired="true" ErrorText="To πεδίο 'Ημ/νία Εξαγωγής (από)' είναι υποχρεωτικό" />
                            </validationsettings>
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <th style="width: 200px">
                        Ημ/νία Εξαγωγής (έως):
                    </th>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="deDateExportTo" runat="server" Width="115px">
                            <validationsettings validationgroup="vgExport" errordisplaymode="ImageWithTooltip">
                                <RequiredField IsRequired="true" ErrorText="To πεδίο 'Ημ/νία Εξαγωγής (έως)' είναι υποχρεωτικό" />
                            </validationsettings>
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
            </table>
            <div style="padding: 5px 0px 10px;">
                <asp:LinkButton ID="btnExport" runat="server" Text="Εξαγωγή σε Excel" OnClick="btnExport_Click"
                    CssClass="icon-btn bg-excel" ValidationGroup="vgExport" />
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="clear">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <my:IncidentReportsGridview runat="server" ID="gvIncidentReports">
                <CustomTemplate>
                    <a id="lnkViewIncidentReport" runat="server" style="text-decoration: none" href="javascript:void(0)"
                        onclick=<%# string.Format("popUp.show('ViewIncident.aspx?ID={0}','Προβολή Συμβάντος', cmdRefresh);", Eval("ID"))%>>
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
    <dxwgv:ASPxGridViewExporter ID="gveIncidentReports" runat="server" GridViewID="gvIncidentReportsExport">
    </dxwgv:ASPxGridViewExporter>
    <dxwgv:ASPxGridView ID="gvIncidentReportsExport" runat="server" AutoGenerateColumns="False"
        DataSourceID="sdsIncidentReportsExport" Visible="false">
        <Columns>
            <dxwgv:GridViewDataTextColumn FieldName="ID" Caption="ID" />
            <dxwgv:GridViewDataDateColumn FieldName="CreatedAt" Caption="Ημ/νία Αναφοράς Συμβάντος"
                PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy HH:mm" />
            <dxwgv:GridViewDataTextColumn FieldName="CreatedBy" Caption="Αναφορά Συμβάντος Από" />
            <dxwgv:GridViewDataTextColumn FieldName="ReportStatus" Caption="Κατάσταση" />
            <dxwgv:GridViewDataTextColumn FieldName="IncidentCallType" Caption="Τύπος Κλήσης Συμβάντος" />
            <dxwgv:GridViewDataTextColumn FieldName="ReporterType" Caption="Κατηγορία Αναφέροντα" />
            <dxwgv:GridViewDataTextColumn FieldName="StudentInstitution" Caption="Ίδρυμα Φοιτητή/Γραμματείας" />
            <dxwgv:GridViewDataTextColumn FieldName="StudentSchool" Caption="Σχολή Φοιτητή/Γραμματείας" />
            <dxwgv:GridViewDataTextColumn FieldName="StudentDepartment" Caption="Τμήμα Φοιτητή/Γραμματείας" />
            <dxwgv:GridViewDataTextColumn FieldName="ReporterName" Caption="Ον/μο Αναφέροντα" />
            <dxwgv:GridViewDataTextColumn FieldName="ReporterPhone" Caption="Τηλέφωνο Αναφέροντα" />
            <dxwgv:GridViewDataTextColumn FieldName="ReporterEmail" Caption="E-mail Αναφέροντα" />
            <dxwgv:GridViewDataTextColumn FieldName="IncidentType" Caption="Τύπος Συμβάντος" />
            <dxwgv:GridViewDataTextColumn FieldName="ReportText" Caption="Κείμενο Αναφοράς" />
            <dxwgv:GridViewDataDateColumn FieldName="AnsweredAt" Caption="Ημ/νία Απάντησης" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy HH:mm" />
            <dxwgv:GridViewDataTextColumn FieldName="AnsweredBy" Caption="Απάντηση Από" />
            <dxwgv:GridViewDataTextColumn FieldName="AnswerCallType" Caption="Τύπος Κλήσης Απάντησης" />
            <dxwgv:GridViewDataTextColumn FieldName="AnswerText" Caption="Κείμενο Απάντησης" />
        </Columns>
    </dxwgv:ASPxGridView>
    <asp:SqlDataSource ID="sdsIncidentReportsExport" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
        ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [vOnlineReportDetails] WHERE [CreatedAt] >= @FromDate AND [CreatedAt] <= @ToDate ORDER BY ID"
        OnSelecting="sdsIncidentReportsExport_Selecting">
        <SelectParameters>
            <asp:Parameter Name="FromDate" Type="DateTime" />
            <asp:Parameter Name="ToDate" Type="DateTime" />
        </SelectParameters>
    </asp:SqlDataSource>
    <script type="text/javascript">
        function cmdRefresh() {
            <%= this.Page.ClientScript.GetPostBackEventReference(this.cmdRefresh, string.Empty) %>;
        }
    </script>
</asp:Content>
