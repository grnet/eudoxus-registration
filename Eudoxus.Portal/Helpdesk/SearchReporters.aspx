<%@ Page Language="C#" MasterPageFile="~/Helpdesk/Helpdesk.Master" AutoEventWireup="true"
    Inherits="Eudoxus.Portal.Helpdesk.SearchReporters" Title="Αναζήτηση Αναφερόντων"
    CodeBehind="SearchReporters.aspx.cs" %>

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
        .dxeRadioButtonList td.dxe
        {
            padding: 2px 0;
        }
    </style>

    <script type="text/javascript">
        function showDetails(s, e) {
            var reporterType = dxReporterType.GetValue();

            var unknownDetails = $('#tbUnknownDetails');            
            var publisherDetails = $('#tbPublisherDetails');
            var secretaryDetails = $('#tbSecretaryDetails');
            var distributionPointDetails = $('#tbDistributionPointDetails');
            var institutionDetails = $('#tbInstitutionDetails');
            var libraryDetails = $('#tbLibraryDetails');
            var studentDetails = $('#tbStudentDetails');
            var professorDetails = $('#tbProfessorDetails');
            var academicDetails = $('#tbAcademicDetails');
            var certificationNumberDetails = $('#tbCertificationNumberDetails');

            unknownDetails.hide();            
            publisherDetails.hide();
            secretaryDetails.hide();
            distributionPointDetails.hide();
            institutionDetails.hide();
            libraryDetails.hide();
            studentDetails.hide();
            professorDetails.hide();
            academicDetails.hide();
            certificationNumberDetails.hide();

            if (reporterType == <%= (int)enReporterType.Unknown %>) {
                unknownDetails.show();

                $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
                $get('<%= txtPublisherAFM.ClientID %>').value = '';
                $get('<%= txtPublisherName.ClientID %>').value = '';
                $get('<%= txtPublisherTradeName.ClientID %>').value = '';                
                $get('<%= hfSchoolCode.ClientID %>').value = '';
                $get('<%= txtInstitutionName.ClientID %>').value = '';
                $get('<%= txtSchoolName.ClientID %>').value = '';
                $get('<%= txtDepartmentName.ClientID %>').value = '';
                $get('<%= ddlInstitution.ClientID %>').value = '';
                $get('<%= ddlLibraryInstitution.ClientID %>').value = '';
                $get('<%= txtLibraryName.ClientID %>').value = '';
                $get('<%= txtCertificationNumber.ClientID %>').value = '';
                $get('<%= txtDistributionPointName.ClientID %>').value = '';
            }
            else if (reporterType == <%= (int)enReporterType.Publisher %>) {
                publisherDetails.show();
                certificationNumberDetails.show();

                $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
                $get('<%= txtIdentificationNumber.ClientID %>').value = '';
                $get('<%= txtDescription.ClientID %>').value = '';
                $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
                $get('<%= hfSchoolCode.ClientID %>').value = '';
                $get('<%= txtInstitutionName.ClientID %>').value = '';
                $get('<%= txtSchoolName.ClientID %>').value = '';
                $get('<%= txtDepartmentName.ClientID %>').value = '';
                $get('<%= ddlInstitution.ClientID %>').value = '';
                $get('<%= ddlLibraryInstitution.ClientID %>').value = '';
                $get('<%= txtLibraryName.ClientID %>').value = '';
                $get('<%= txtCertificationNumber.ClientID %>').value = '';
                $get('<%= txtDistributionPointName.ClientID %>').value = '';
            }
            else if (reporterType == <%= (int)enReporterType.Secretary %>) {
                secretaryDetails.show();
                academicDetails.show();
                certificationNumberDetails.show();

                $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
                $get('<%= txtIdentificationNumber.ClientID %>').value = '';
                $get('<%= txtDescription.ClientID %>').value = '';
                $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
                $get('<%= txtPublisherAFM.ClientID %>').value = '';
                $get('<%= txtPublisherName.ClientID %>').value = '';
                $get('<%= txtPublisherTradeName.ClientID %>').value = '';
                $get('<%= ddlInstitution.ClientID %>').value = '';
                $get('<%= ddlLibraryInstitution.ClientID %>').value = '';
                $get('<%= txtLibraryName.ClientID %>').value = '';
                $get('<%= txtCertificationNumber.ClientID %>').value = '';
                $get('<%= txtDistributionPointName.ClientID %>').value = '';
            }
            else if (reporterType == <%= (int)enReporterType.DistributionPoint %>) {
                distributionPointDetails.show();
                certificationNumberDetails.show();

               $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
                $get('<%= txtIdentificationNumber.ClientID %>').value = '';
                $get('<%= txtDescription.ClientID %>').value = '';
                $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
                $get('<%= txtPublisherAFM.ClientID %>').value = '';
                $get('<%= txtPublisherName.ClientID %>').value = '';
                $get('<%= txtPublisherTradeName.ClientID %>').value = '';
                $get('<%= hfSchoolCode.ClientID %>').value = '';
                $get('<%= txtInstitutionName.ClientID %>').value = '';
                $get('<%= txtSchoolName.ClientID %>').value = '';
                $get('<%= txtDepartmentName.ClientID %>').value = '';
                $get('<%= ddlInstitution.ClientID %>').value = '';
                $get('<%= ddlLibraryInstitution.ClientID %>').value = '';
                $get('<%= txtLibraryName.ClientID %>').value = '';
                $get('<%= txtCertificationNumber.ClientID %>').value = '';
            }
            else if (reporterType == <%= (int)enReporterType.PublicationsOffice %>||
                     reporterType == <%= (int)enReporterType.DataCenter %> ||
                     reporterType == <%= (int)enReporterType.BookSupplier %>) {
                institutionDetails.show();
                certificationNumberDetails.show();

                $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
                $get('<%= txtIdentificationNumber.ClientID %>').value = '';
                $get('<%= txtDescription.ClientID %>').value = '';
                $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
                $get('<%= txtPublisherAFM.ClientID %>').value = '';
                $get('<%= txtPublisherName.ClientID %>').value = '';
                $get('<%= txtPublisherTradeName.ClientID %>').value = '';
                $get('<%= hfSchoolCode.ClientID %>').value = '';
                $get('<%= txtInstitutionName.ClientID %>').value = '';
                $get('<%= txtSchoolName.ClientID %>').value = '';
                $get('<%= txtDepartmentName.ClientID %>').value = '';
                $get('<%= txtCertificationNumber.ClientID %>').value = '';
                $get('<%= ddlLibraryInstitution.ClientID %>').value = '';
                $get('<%= txtLibraryName.ClientID %>').value = '';
                $get('<%= txtDistributionPointName.ClientID %>').value = '';
            }
            else if (reporterType == <%= (int)enReporterType.Library %>) {
                libraryDetails.show();
                certificationNumberDetails.show();

                $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
                $get('<%= txtIdentificationNumber.ClientID %>').value = '';
                $get('<%= txtDescription.ClientID %>').value = '';
                $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
                $get('<%= txtPublisherAFM.ClientID %>').value = '';
                $get('<%= txtPublisherName.ClientID %>').value = '';
                $get('<%= txtPublisherTradeName.ClientID %>').value = '';
                $get('<%= ddlInstitution.ClientID %>').value = '';
                $get('<%= hfSchoolCode.ClientID %>').value = '';
                $get('<%= txtInstitutionName.ClientID %>').value = '';
                $get('<%= txtSchoolName.ClientID %>').value = '';
                $get('<%= txtDepartmentName.ClientID %>').value = '';
                $get('<%= txtCertificationNumber.ClientID %>').value = '';
                $get('<%= txtDistributionPointName.ClientID %>').value = '';
            }
            else if (reporterType == <%= (int)enReporterType.Student %>) {
                studentDetails.show();
                academicDetails.show();

                $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
                $get('<%= txtIdentificationNumber.ClientID %>').value = '';
                $get('<%= txtDescription.ClientID %>').value = '';
                $get('<%= txtPublisherAFM.ClientID %>').value = '';
                $get('<%= txtPublisherName.ClientID %>').value = '';
                $get('<%= txtPublisherTradeName.ClientID %>').value = '';
                $get('<%= ddlInstitution.ClientID %>').value = '';
                $get('<%= ddlLibraryInstitution.ClientID %>').value = '';
                $get('<%= txtLibraryName.ClientID %>').value = '';
                $get('<%= txtCertificationNumber.ClientID %>').value = '';                
                $get('<%= txtDistributionPointName.ClientID %>').value = '';
            }
            else if (reporterType == <%= (int)enReporterType.Professor %>) {
                professorDetails.show();
                academicDetails.show();

                $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
                $get('<%= txtIdentificationNumber.ClientID %>').value = '';
                $get('<%= txtDescription.ClientID %>').value = '';
                $get('<%= txtPublisherAFM.ClientID %>').value = '';
                $get('<%= txtPublisherName.ClientID %>').value = '';
                $get('<%= txtPublisherTradeName.ClientID %>').value = '';
                $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
                $get('<%= ddlInstitution.ClientID %>').value = '';
                $get('<%= ddlLibraryInstitution.ClientID %>').value = '';
                $get('<%= txtLibraryName.ClientID %>').value = '';
                $get('<%= txtCertificationNumber.ClientID %>').value = '';
                $get('<%= txtDistributionPointName.ClientID %>').value = '';
            }
            else {
                $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
                $get('<%= txtIdentificationNumber.ClientID %>').value = '';
                $get('<%= txtDescription.ClientID %>').value = '';
                $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
                $get('<%= txtPublisherAFM.ClientID %>').value = '';
                $get('<%= txtPublisherName.ClientID %>').value = '';
                $get('<%= txtPublisherTradeName.ClientID %>').value = '';
                $get('<%= hfSchoolCode.ClientID %>').value = '';
                $get('<%= txtInstitutionName.ClientID %>').value = '';
                $get('<%= txtSchoolName.ClientID %>').value = '';
                $get('<%= txtDepartmentName.ClientID %>').value = '';
                $get('<%= ddlInstitution.ClientID %>').value = '';
                $get('<%= ddlLibraryInstitution.ClientID %>').value = '';
                $get('<%= txtLibraryName.ClientID %>').value = '';
                $get('<%= txtCertificationNumber.ClientID %>').value = '';
                $get('<%= txtDistributionPointName.ClientID %>').value = '';
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:LinkButton ID="cmdRefresh" runat="server" Style="display: none;" OnClick="cmdRefresh_Click" />
    <table cellspacing="0">
        <tr>
            <td style="vertical-align: top">
                <table width="430px" class="dv">
                    <tr>
                        <th colspan="4" class="popupHeader">
                            Γενικά Στοιχεία Αναφέροντος
                        </th>
                    </tr>
                    <tr>
                        <th style="width: 190px">
                            Ον/μο ατόμου επικοινωνίας:
                        </th>
                        <td style="width: 230px">
                            <asp:TextBox ID="txtContactName" runat="server" Width="98%" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 190px">
                            Τηλέφωνο ατόμου επικοινωνίας:
                        </th>
                        <td style="width: 230px">
                            <asp:TextBox ID="txtContactPhone" runat="server" Width="98%" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 190px">
                            E-mail ατόμου επικοινωνίας:
                        </th>
                        <td style="width: 230px">
                            <asp:TextBox ID="txtContactEmail" runat="server" Width="98%" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table width="260px" class="dv">
                    <tr>
                        <th class="popupHeader">
                            Κατηγορία Αναφέροντος
                        </th>
                    </tr>
                    <tr>
                        <td style="width: 170px; padding: 0px;">
                            <dxe:ASPxRadioButtonList ID="rblReporterType" runat="server" ClientInstanceName="dxReporterType"
                                OnInit="rblReporterType_Init" Width="90%" Style="border: none;">
                                <ClientSideEvents SelectedIndexChanged="showDetails" />
                            </dxe:ASPxRadioButtonList>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table id="tbUnknownDetails" class="dv" width="450px">
                    <tr>
                        <th colspan="4" class="popupHeader">
                            Στοιχεία Χρήστη
                        </th>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Είδος Χρήστη:
                        </th>
                        <td style="width: 330px">
                            <asp:DropDownList ID="ddlUnknownReporterType" runat="server" OnInit="ddlUnknownReporterType_Init"
                                Width="98%" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Α.Δ.Τ.:
                        </th>
                        <td style="width: 330px">
                            <asp:TextBox ID="txtIdentificationNumber" runat="server" Width="98%" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Λοιπά Στοιχεία:
                        </th>
                        <td style="width: 330px">
                            <asp:TextBox ID="txtDescription" runat="server" Width="98%" />
                        </td>
                    </tr>
                </table>
                <table id="tbPublisherDetails" class="dv" width="450px">
                    <tr>
                        <th colspan="4" class="popupHeader">
                            Στοιχεία Εκδοτικού Οίκου
                        </th>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Α.Φ.Μ.:
                        </th>
                        <td style="width: 330px">
                            <asp:TextBox ID="txtPublisherAFM" runat="server" Width="98%" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Επωνυμία:
                        </th>
                        <td style="width: 330px">
                            <asp:TextBox ID="txtPublisherName" runat="server" Width="98%" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Διακρ. Τίτλος:
                        </th>
                        <td style="width: 330px">
                            <asp:TextBox ID="txtPublisherTradeName" runat="server" Width="98%" />
                        </td>
                    </tr>
                </table>
                <table id="tbSecretaryDetails" class="dv" width="450px">
                    <tr>
                        <th colspan="4" class="popupHeader">
                            Στοιχεία Γραμματείας
                        </th>
                    </tr>
                </table>
                <table id="tbInstitutionDetails" class="dv" width="450px">
                    <tr>
                        <th colspan="4" class="popupHeader">
                            Στοιχεία Γραφείου
                        </th>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Ίδρυμα:
                        </th>
                        <td style="width: 330px">
                            <asp:DropDownList ID="ddlInstitution" runat="server" Width="280px" OnInit="ddlInstitution_Init" />
                        </td>
                    </tr>
                </table>
                <table id="tbLibraryDetails" class="dv" width="450px">
                    <tr>
                        <th colspan="4" class="popupHeader">
                            Στοιχεία Βιβλιοθήκης
                        </th>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Ίδρυμα:
                        </th>
                        <td style="width: 330px">
                            <asp:DropDownList ID="ddlLibraryInstitution" runat="server" Width="280px" OnInit="ddlInstitution_Init" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Τίτλος:
                        </th>
                        <td style="width: 330px">
                            <asp:TextBox ID="txtLibraryName" runat="server" Width="98%" />
                        </td>
                    </tr>
                </table>
                <table id="tbDistributionPointDetails" class="dv" width="450px">
                    <tr>
                        <th colspan="4" class="popupHeader">
                            Στοιχεία Σημείου Διανομής
                        </th>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Τίτλος:
                        </th>
                        <td style="width: 330px">
                            <asp:TextBox ID="txtDistributionPointName" runat="server" Width="98%" />
                        </td>
                    </tr>
                </table>
                <table id="tbStudentDetails" class="dv" width="450px">
                    <tr>
                        <th colspan="4" class="popupHeader">
                            Στοιχεία Φοιτητή
                        </th>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Αρ. Μητρώου:
                        </th>
                        <td style="width: 330px">
                            <asp:TextBox ID="txtAcademicIdentifier" runat="server" Width="280px" />
                        </td>
                    </tr>
                </table>
                <table id="tbProfessorDetails" class="dv" width="450px">
                    <tr>
                        <th colspan="4" class="popupHeader">
                            Στοιχεία Καθηγητή
                        </th>
                    </tr>
                </table>
                <table id="tbAcademicDetails" class="dv" style="border-top: hidden" width="450px">
                    <tr>
                        <th style="width: 90px">
                            Ίδρυμα:
                        </th>
                        <td colspan="3" style="width: 330px">
                            <asp:TextBox ID="txtInstitutionName" runat="server" TabIndex="7" Width="90%" />
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
                        <th style="width: 90px">
                            Σχολή:
                        </th>
                        <td style="width: 330px">
                            <asp:TextBox ID="txtSchoolName" runat="server" Width="98%" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 90px">
                            Τμήμα:
                        </th>
                        <td style="width: 330px">
                            <asp:TextBox ID="txtDepartmentName" runat="server" Width="98%" />
                        </td>
                    </tr>
                </table>
                <table id="tbCertificationNumberDetails" class="dv" style="border-top: hidden" width="450px">
                    <tr>
                        <th style="width: 90px">
                            Αρ. Βεβαίωσης:
                        </th>
                        <td style="width: 330px">
                            <asp:TextBox ID="txtCertificationNumber" runat="server" Width="98%" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="padding: 5px 0px 10px;">
        <asp:LinkButton ID="btnSearch" runat="server" Text="Αναζήτηση" OnClick="btnSearch_Click"
            CssClass="icon-btn bg-search" />
        <a id="lnkReportIncident" runat="server" class="icon-btn bg-addNewItem" href="javascript:void(0)"
            onclick="popUp.show('ReportIncident.aspx','Αναφορά Συμβάντος', cmdRefresh)">Αναφορά
            Συμβάντος </a>
        <%--<asp:LinkButton ID="btnExport" runat="server" Text="Εξαγωγή σε Excel" OnClick="btnExport_Click"
            CssClass="icon-btn bg-excel" />--%>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <dxwgv:ASPxGridView ID="gvReporters" runat="server" AutoGenerateColumns="False" DataSourceID="odsReporters"
                KeyFieldName="ID" EnableRowsCache="false" EnableCallBacks="true" Width="100%"
                DataSourceForceStandardPaging="true" OnHtmlRowPrepared="gvReporters_HtmlRowPrepared">
                <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε..." />
                <SettingsPager PageSize="10" Summary-Text="Σελίδα {0} από {1} ({2} Αναφέροντες)"
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
                    <dxwgv:GridViewDataTextColumn FieldName="ReporterType" Name="ReporterType" Caption="Κατηγορία Αναφέροντος"
                        CellStyle-HorizontalAlign="Left">
                        <DataItemTemplate>
                            <%# ((enReporterType)Eval("ReporterType")).GetLabel()%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ContactName" Name="ContactName" Caption="Στοιχεία Ατόμου Επικοινωνίας"
                        CellStyle-HorizontalAlign="Left">
                        <DataItemTemplate>
                            <%# Eval("ContactName")%><br />
                            <%# Eval("ContactPhone")%><br />
                            <%# Eval("ContactEmail")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Ειδικά Στοιχεία Αναφέροντος" CellStyle-HorizontalAlign="Left">
                        <DataItemTemplate>
                            <%# GetReporterDetails(Container.DataItem) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Στοιχεία Λογαριασμού" Width="50px">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                        <CellStyle HorizontalAlign="Center" />
                        <DataItemTemplate>
                            <a id="lnkViewAccountDetails" runat="server" style="text-decoration: none" href="javascript:void(0)"
                                visible='<%# ((Reporter)Container.DataItem is Publisher || (Reporter)Container.DataItem is Secretary || (Reporter)Container.DataItem is PublicationsOffice || (Reporter)Container.DataItem is DataCenter || (Reporter)Container.DataItem is Library || (Reporter)Container.DataItem is BookSupplier || (Reporter)Container.DataItem is DistributionPoint)%>'
                                onclick=<%# string.Format("popUp.show('ViewAccountDetails.aspx?rid={0}&t={1}', 'Στοιχεία Λογαριασμού');", Eval("ID"), ((enReporterType)Eval("ReporterType")).ToString("D"))%>>
                                <img src="/_img/iconUserEdit.png" alt="Στοιχεία Λογαριασμού" />
                            </a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Επεξεργασία Αναφέροντα" Width="50px">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                        <CellStyle HorizontalAlign="Center" />
                        <DataItemTemplate>
                            <a id="lnkAddIncidentReport" runat="server" style="text-decoration: none" href="javascript:void(0)"
                                visible='<%# ((Reporter)Container.DataItem is Unknown || (Reporter)Container.DataItem is Student || (Reporter)Container.DataItem is Professor) %>'
                                onclick=<%# string.Format("popUp.show('EditReporter.aspx?rID={0}', 'Επεξεργασία Αναφέροντα', cmdRefresh);", Eval("ID"))%>>
                                <img src="/_img/iconEdit.png" alt="Επεξεργασία Αναφέροντα" />
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
    <asp:ObjectDataSource ID="odsReporters" runat="server" TypeName="Eudoxus.Portal.DataSources.Reporters"
        SelectMethod="FindReportersWithCriteria" SelectCountMethod="CountReportersWithCriteria"
        EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsReporters_Selecting">
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
    
        showDetails();
    </script>

</asp:Content>
