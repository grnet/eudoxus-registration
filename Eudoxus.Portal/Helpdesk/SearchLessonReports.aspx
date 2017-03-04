<%@ Page Language="C#" MasterPageFile="~/Helpdesk/Helpdesk.Master" AutoEventWireup="true"
    Inherits="Eudoxus.Portal.Helpdesk.SearchLessonReports" Title="Αναζήτηση Συμβάντων"
    CodeBehind="SearchLessonReports.aspx.cs" %>

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
    <table cellspacing="0">
        <tr>
            <td style="vertical-align: top">
                <table id="tbFilters" runat="server" width="770px" class="dv">
                    <tr>
                        <th colspan="4" class="popupHeader">
                            Φίλτρα Αναζήτησης
                        </th>
                    </tr>
                    <tr>
                        <th style="width: 100px">
                            Κατάσταση:
                        </th>
                        <td style="width: 120px">
                            <asp:DropDownList ID="ddlReportStatus" runat="server" Width="115px" TabIndex="1"
                                OnInit="ddlReportStatus_Init" ClientIDMode="Static" />
                        </td>
                        <th style="width: 90px">
                            Ακαδημαϊκό Έτος:
                        </th>
                        <td style="width: 320px">
                            <asp:DropDownList ID="ddlAcademicYear" runat="server" Width="315px" TabIndex="4"
                                OnInit="ddlAcademicYear_Init" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 100px">
                            Χειρισμός Από:
                        </th>
                        <td style="width: 120px">
                            <asp:DropDownList ID="ddlHandlerType" runat="server" Width="115px" TabIndex="2" OnInit="ddlHandlerType_Init"
                                ClientIDMode="Static" />
                        </td>
                        <th style="width: 90px">
                            Ίδρυμα
                        </th>
                        <td style="width: 320px">
                            <asp:DropDownList ID="ddlInstitution" runat="server" TabIndex="5" Width="315px" OnInit="ddlInstitution_Init" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 100px">
                            Επικοινωνία:
                        </th>
                        <td style="width: 120px">
                            <asp:DropDownList ID="ddlHandlerStatus" runat="server" Width="115px" TabIndex="7"
                                OnInit="ddlHandlerStatus_Init" />
                        </td>
                        <th />
                        <th />
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table width="450px" class="dv">
                    <tr>
                        <th colspan="2" class="popupHeader">
                            Επιλογή Σχολής/Τμήματος
                        </th>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Ίδρυμα:
                        </th>
                        <td colspan="3" style="width: 330px">
                            <asp:TextBox ID="txtInstitutionName" runat="server" TabIndex="7" Width="280px" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Σχολή:
                        </th>
                        <td style="width: 330px">
                            <asp:TextBox ID="txtSchoolName" runat="server" TabIndex="8" Width="280px" />
                            <a href="javascript:void(0);" id="lnkSelectSchool" onclick="popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');">
                                <img id="Img1" runat="server" align="absmiddle" src="~/_img/iconSelectSchool.png"
                                    alt="Επιλογή Σχολής" title="Επιλογή Σχολής" /></a> <a href="javascript:void(0);"
                                        id="lnkRemoveSchoolSelection" onclick="return hd.removeSchoolSelection();" style="display: none;">
                                        <img id="Img2" runat="server" align="absmiddle" src="~/_img/iconRemoveSchool.png"
                                            alt="Αφαίρεση Σχολής" title="Αφαίρεση Σχολής" /></a>
                            <asp:HiddenField ID="hfSchoolCode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Τμήμα:
                        </th>
                        <td style="width: 330px">
                            <asp:TextBox ID="txtDepartmentName" runat="server" TabIndex="9" Width="280px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="padding: 5px 0px 10px;">
        <asp:LinkButton ID="btnSearch" runat="server" Text="Αναζήτηση" OnClick="btnSearch_Click"
            CssClass="icon-btn bg-search" />
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <my:IncidentReportsGridview runat="server" ID="gvIncidentReports" DataSourceID="odsIncidentReports">
                <CustomTemplate>
                    <a id="lnkViewIncidentReport" runat="server" style="text-decoration: none" href="javascript:void(0)"
                        onclick=<%# string.Format("popUp.show('ViewIncident.aspx?ID={0}','Προβολή Συμβάντος');", Eval("ID"))%>>
                        <img src="/_img/iconView.png" alt="Προβολή Συμβάντος" />
                    </a><a id="lnkAddIncidentReportPost" runat="server" style="text-decoration: none"
                        href="javascript:void(0)" onclick=<%# string.Format("popUp.show('AddIncidentReportPost.aspx?ID={0}','Προσθήκη Απάντησης', cmdRefresh);", Eval("ID"))%>>
                        <img src="/_img/iconAddNewItem.png" alt="Προσθήκη Απάντησης" />
                    </a>
                </CustomTemplate>
            </my:IncidentReportsGridview>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsIncidentReports" runat="server" TypeName="Eudoxus.Portal.DataSources.LessonReports"
        SelectMethod="FindLessonReportsWithCriteria" SelectCountMethod="CountLessonReportsWithCriteria"
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
