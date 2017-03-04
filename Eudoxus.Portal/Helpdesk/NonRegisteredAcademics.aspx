<%@ Page Language="C#" MasterPageFile="~/Helpdesk/Helpdesk.Master" AutoEventWireup="true"
    Inherits="Eudoxus.Portal.Helpdesk.NonRegisteredAcademics" Title="Μη Εγγεγραμμένες Γραμματείες"
    CodeBehind="NonRegisteredAcademics.aspx.cs" %>

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

    <table width="700px" class="dv">
        <tr>
            <th colspan="4" class="popupHeader">
                Φίλτρα Αναζήτησης
            </th>
        </tr>
        <tr>            
            <th style="width: 90px">
                Ίδρυμα:
            </th>
            <td style="width: 330px">
                <asp:TextBox ID="txtInstitutionName" runat="server" TabIndex="7" Width="280px" />
                <a href="javascript:void(0);" id="lnkSelectSchool" onclick="popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');">
                    <img id="Img1" runat="server" align="absmiddle" src="~/_img/iconSelectSchool.png"
                        alt="Επιλογή Σχολής" /></a> <a href="javascript:void(0);" id="lnkRemoveSchoolSelection"
                            onclick="return hd.removeSchoolSelection();" style="display: none;">
                            <img id="Img2" runat="server" align="absmiddle" src="~/_img/iconRemoveSchool.png"
                                alt="Αφαίρεση Σχολής" /></a>
                <asp:HiddenField ID="hfSchoolCode" runat="server" />
            </td>
            <th style="width: 150px">
                Έχει ενημερωθεί:
            </th>
            <td style="width: 150px">
                <asp:DropDownList ID="ddlNotificationStatus" runat="server" Width="100%">
                    <asp:ListItem Text="-- αδιάφορο --" Value="-1" Selected="True" />
                    <asp:ListItem Text="Όχι" Value="0" />
                    <asp:ListItem Text="Ναι" Value="1" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th style="width: 90px">
                Σχολή:
            </th>
            <td style="width: 330px">
                <asp:TextBox ID="txtSchoolName" runat="server" Width="280px" />
            </td>
            <th />
            <th />
        </tr>
        <tr>
            <th style="width: 90px">
                Τμήμα:
            </th>
            <td style="width: 330px">
                <asp:TextBox ID="txtDepartmentName" runat="server" Width="280px" />
            </td>
            <th />
            <th />
        </tr>
    </table>
    <div style="padding: 5px 0px 10px;">
        <asp:LinkButton ID="btnSearch" runat="server" Text="Αναζήτηση" OnClick="btnSearch_Click"
            CssClass="icon-btn bg-search" />
        <asp:LinkButton ID="btnExport" runat="server" Text="Εξαγωγή σε Excel" OnClick="btnExport_Click"
            CssClass="icon-btn bg-excel" />
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <dxwgv:ASPxGridView ID="gvAcademics" runat="server" AutoGenerateColumns="False" DataSourceID="odsAcademics"
                KeyFieldName="ID" EnableRowsCache="false" EnableCallBacks="true" Width="100%"
                DataSourceForceStandardPaging="true">
                <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε..." />
                <SettingsPager PageSize="10" Summary-Text="Σελίδα {0} από {1} ({2} Γραμματείες)"
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
                    <dxwgv:GridViewDataTextColumn FieldName="Institution" Caption="Ίδρυμα">
                        <DataItemTemplate>
                            <%# Eval("Institution")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="School" Caption="Σχολή">
                        <DataItemTemplate>
                            <%# Eval("School")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Department" Caption="Τμήμα">
                        <DataItemTemplate>
                            <%# Eval("Department")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn FieldName="Address" Caption="Διεύθυνση">
                        <DataItemTemplate>
                            <%# Eval("Address")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Phone" Caption="Τηλέφωνο">
                        <DataItemTemplate>
                            <%# Eval("Phone")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Fax" Caption="Fax">
                        <DataItemTemplate>
                            <%# Eval("Fax")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Email" Caption="E-mail">
                        <DataItemTemplate>
                            <%# Eval("Email")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Semesters" Caption="Εξάμηνα">
                        <DataItemTemplate>
                            <%# Eval("Semesters")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Prefix" Caption="Prefix">
                        <DataItemTemplate>
                            <%# Eval("Prefix")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="IsNotified" Caption="Έχει ενημερωθεί" Width="50px">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                        <CellStyle HorizontalAlign="Center" />
                        <DataItemTemplate>
                            <%# (bool)Eval("IsNotified") ? "ΝΑΙ" : "ΟΧΙ"%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Επεξεργασία Στοιχείων" Width="50px">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                        <CellStyle HorizontalAlign="Center" />
                        <DataItemTemplate>
                            <a runat="server" style="text-decoration: none" href="javascript:void(0)" onclick=<%# string.Format("popUp.show('EditAcademicDetails.aspx?aID={0}', 'Επεξεργασία Στοιχείων', cmdRefresh);", Eval("ID"))%>>
                                <img src="/_img/iconEdit.png" alt="Επεξεργασία Στοιχείων" />
                            </a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Αναφορά Συμβάντος" Width="50px">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                        <CellStyle HorizontalAlign="Center" />
                        <DataItemTemplate>
                            <a runat="server" style="text-decoration: none" href="javascript:void(0)" onclick=<%# string.Format("popUp.show('ReportIncident.aspx','Αναφορά Συμβάντος', cmdRefresh);")%>>
                                <img src="/_img/iconAddNewItem.png" alt="Προσθήκη Συμβάντος" />
                            </a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <dxwgv:ASPxGridViewExporter ID="gveAcademics" runat="server" GridViewID="gvAcademics">
    </dxwgv:ASPxGridViewExporter>
    <asp:ObjectDataSource ID="odsAcademics" runat="server" TypeName="Eudoxus.Portal.DataSources.AcademicDetails"
        SelectMethod="FindAcademicDetailsWithCriteria" SelectCountMethod="CountAcademicDetailsWithCriteria"
        EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsAcademics_Selecting" OnSelected="odsAcademics_Selected">
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
