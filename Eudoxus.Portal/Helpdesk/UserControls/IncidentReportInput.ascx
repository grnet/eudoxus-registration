<%@ Control Language="C#" AutoEventWireup="true" Inherits="Eudoxus.Portal.Helpdesk.UserControls.IncidentReportInput"
    CodeBehind="IncidentReportInput.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v9.2" Namespace="DevExpress.Web.ASPxCallbackPanel"
    TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v9.2" Namespace="DevExpress.Web.ASPxPanel"
    TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Import Namespace="Eudoxus.BusinessModel" %>
<script type="text/javascript">
    $(function() {
        showDetails('#<%= ddlReporterType.ClientID %>');
    });

    var checkUnknownDetails = false;

    function showDetails(elem) {
        var reporterType = $(elem).val();

        var unknownDetails = $('#tbUnknownDetails');
        var studentDetails = $('#tbStudentDetails');
        var professorDetails = $('#tbProfessorDetails');
        var publisherDetails = $('#tbPublisherDetails');
        var secretaryDetails = $('#tbSecretaryDetails');
        var distributionPointDetails = $('#tbDistributionPointDetails');
        var institutionDetails = $('#tbInstitutionDetails');
        var libraryDetails = $('#tbLibraryDetails');
        var academicDetails = $('#tbAcademicDetails');        

        unknownDetails.hide();
        publisherDetails.hide();
        secretaryDetails.hide();
        distributionPointDetails.hide();
        institutionDetails.hide();
        libraryDetails.hide();
        studentDetails.hide();
        professorDetails.hide();
        academicDetails.hide();

        if (reporterType == <%= (int)enReporterType.Unknown %>) {
            unknownDetails.show();
            checkUnknownDetails = true;

            $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
            $get('<%= txtInstitutionName.ClientID %>').value = '';
            $get('<%= txtSchoolName.ClientID %>').value = '';
            $get('<%= txtDepartmentName.ClientID %>').value = '';
            $get('<%= hfSchoolCode.ClientID %>').value = '';
        }
        else if (reporterType == <%= (int)enReporterType.Publisher %>) {
            publisherDetails.show();
            checkUnknownDetails = false;            

            $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
            $get('<%= txtIdentificationNumber.ClientID %>').value = '';
            $get('<%= txtDescription.ClientID %>').value = '';
            $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
            $get('<%= txtInstitutionName.ClientID %>').value = '';
            $get('<%= txtSchoolName.ClientID %>').value = '';
            $get('<%= txtDepartmentName.ClientID %>').value = '';
            $get('<%= hfSchoolCode.ClientID %>').value = '';
        }
        else if (reporterType == <%= (int)enReporterType.Secretary %>) {
            secretaryDetails.show();
            checkUnknownDetails = false;            

            $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
            $get('<%= txtIdentificationNumber.ClientID %>').value = '';
            $get('<%= txtDescription.ClientID %>').value = '';
            $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
            $get('<%= txtInstitutionName.ClientID %>').value = '';
            $get('<%= txtSchoolName.ClientID %>').value = '';
            $get('<%= txtDepartmentName.ClientID %>').value = '';
            $get('<%= hfSchoolCode.ClientID %>').value = '';
        }
        else if (reporterType == <%= (int)enReporterType.DistributionPoint %>) {
            distributionPointDetails.show();
            checkUnknownDetails = false;            

            $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
            $get('<%= txtIdentificationNumber.ClientID %>').value = '';
            $get('<%= txtDescription.ClientID %>').value = '';
            $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
            $get('<%= txtInstitutionName.ClientID %>').value = '';
            $get('<%= txtSchoolName.ClientID %>').value = '';
            $get('<%= txtDepartmentName.ClientID %>').value = '';
            $get('<%= hfSchoolCode.ClientID %>').value = '';
        }
        else if (reporterType == <%= (int)enReporterType.PublicationsOffice %> || 
                 reporterType == <%= (int)enReporterType.DataCenter %> ||
                 reporterType == <%= (int)enReporterType.DataCenter %>) {
            institutionDetails.show();
            checkUnknownDetails = false;            

            $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
            $get('<%= txtIdentificationNumber.ClientID %>').value = '';
            $get('<%= txtDescription.ClientID %>').value = '';
            $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
            $get('<%= txtInstitutionName.ClientID %>').value = '';
            $get('<%= txtSchoolName.ClientID %>').value = '';
            $get('<%= txtDepartmentName.ClientID %>').value = '';
            $get('<%= hfSchoolCode.ClientID %>').value = '';
        }
        else if (reporterType == <%= (int)enReporterType.Library %>) {
            libraryDetails.show();
            checkUnknownDetails = false;            

            $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
            $get('<%= txtIdentificationNumber.ClientID %>').value = '';
            $get('<%= txtDescription.ClientID %>').value = '';
            $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
            $get('<%= txtInstitutionName.ClientID %>').value = '';
            $get('<%= txtSchoolName.ClientID %>').value = '';
            $get('<%= txtDepartmentName.ClientID %>').value = '';
            $get('<%= hfSchoolCode.ClientID %>').value = '';
        }
        else if (reporterType == <%= (int)enReporterType.Student %>) {
            studentDetails.show();
            academicDetails.show();
            checkUnknownDetails = false;

            $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
            $get('<%= txtIdentificationNumber.ClientID %>').value = '';
            $get('<%= txtDescription.ClientID %>').value = '';
        }
        else if (reporterType == <%= (int)enReporterType.Professor %>) {
            professorDetails.show();
            academicDetails.show();
            checkUnknownDetails = false;

            $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
            $get('<%= txtIdentificationNumber.ClientID %>').value = '';
            $get('<%= txtDescription.ClientID %>').value = '';
        }
        else {
            $get('<%= ddlUnknownReporterType.ClientID %>').value = '';
            $get('<%= txtIdentificationNumber.ClientID %>').value = '';
            $get('<%= txtDescription.ClientID %>').value = '';
            $get('<%= txtAcademicIdentifier.ClientID %>').value = '';
            $get('<%= txtInstitutionName.ClientID %>').value = '';
            $get('<%= txtSchoolName.ClientID %>').value = '';
            $get('<%= txtDepartmentName.ClientID %>').value = '';
            $get('<%= hfSchoolCode.ClientID %>').value = '';
        }
    }

    function validate() {
        if (checkUnknownDetails) {
            return Page_ClientValidate('vgUnknownDetails');
        }
    }
</script>
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Γενικά Στοιχεία
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Κατηγορία αναφέροντος:
        </th>
        <td>
            <asp:DropDownList ID="ddlReporterType" runat="server" OnInit="ddlReporterType_Init"
                onchange="showDetails(this)" Width="460px" />
            <asp:RequiredFieldValidator ID="rfvReporterType" Display="Dynamic" runat="server"
                ControlToValidate="ddlReporterType" ErrorMessage="Το πεδίο 'Κατηγορία αναφέροντος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Είδος συμβάντος:
        </th>
        <td>
            <asp:DropDownList ID="ddlIncidentType" runat="server" Width="460px" DataTextField="Name"
                DataValueField="ID" />
            <ajaxToolkit:CascadingDropDown ID="cddIncidentType" runat="server" TargetControlID="ddlIncidentType"
                ParentControlID="ddlReporterType" Category="IncidentTypes" PromptText="-- επιλέξτε πηγή αναφοράς --"
                ServicePath="~/PortalServices/Services.asmx" ServiceMethod="GetIncidentTypes"
                LoadingText="Παρακαλω περιμένετε">
            </ajaxToolkit:CascadingDropDown>
            <asp:RequiredFieldValidator ID="rfvIncidentType" Display="Dynamic" runat="server"
                ControlToValidate="ddlIncidentType" ErrorMessage="Το πεδίο 'Είδος συμβάντος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τύπος Κλήσης:
        </th>
        <td>
            <asp:DropDownList ID="ddlCallType" runat="server" OnInit="ddlCallType_Init" Width="460px" />
            <asp:RequiredFieldValidator ID="rfvCallType" Display="Dynamic" runat="server"
                ControlToValidate="ddlCallType" ErrorMessage="Το πεδίο 'Τύπος Κλήσης' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<table id="tbUnknownDetails" class="dv" width="100%">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Χρήστη
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Είδος Χρήστη:
        </th>
        <td>
            <asp:DropDownList ID="ddlUnknownReporterType" runat="server" OnInit="ddlUnknownReporterType_Init"
                Width="460px" />
            <asp:RequiredFieldValidator ID="rfvUnknownReporterType" runat="server" ValidationGroup="vgUnknownDetails"
                Display="Dynamic" ControlToValidate="ddlUnknownReporterType" ErrorMessage="Το πεδίο 'Είδος Χρήστη' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Α.Δ.Τ.:
        </td>
        <td>
            <asp:TextBox ID="txtIdentificationNumber" runat="server" Width="460px" />
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Λοιπά Στοιχεία:
        </td>
        <td>
            <asp:TextBox ID="txtDescription" runat="server" Width="460px" />
        </td>
    </tr>
</table>
<table id="tbStudentDetails" class="dv" width="100%">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Φοιτητή
        </th>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Αρ. Μητρώου:
        </td>
        <td>
            <asp:TextBox ID="txtAcademicIdentifier" runat="server" Width="460px" />
        </td>
    </tr>
</table>
<table id="tbProfessorDetails" class="dv" width="100%">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Καθηγητή
        </th>
    </tr>
</table>
<table id="tbAcademicDetails" class="dv" style="border-top: hidden" width="100%">
    <tr>
        <td class="notRequired" style="width: 30%">
            Ίδρυμα:
        </td>
        <td>
            <asp:TextBox ID="txtInstitutionName" runat="server" Width="460px" />
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
        <td class="notRequired" style="width: 30%">
            Σχολή:
        </td>
        <td>
            <asp:TextBox ID="txtSchoolName" runat="server" Width="460px" />
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Τμήμα:
        </td>
        <td>
            <asp:TextBox ID="txtDepartmentName" runat="server" Width="460px" />
        </td>
    </tr>
</table>
<table id="tbPublisherDetails" class="dv" width="100%">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Εκδότη
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Α.Φ.Μ.:
        </th>
        <td>
            <asp:Label ID="lblPublisherAFM" runat="server" ForeColor="Blue" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Επωνυμία:
        </th>
        <td>
            <asp:Label ID="lblPublisherName" runat="server" ForeColor="Blue" />
        </td>
    </tr>
</table>
<table id="tbSecretaryDetails" class="dv" width="100%">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Γραμματείας
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Ίδρυμα:
        </th>
        <td>
            <asp:Label ID="lblInstitution" runat="server" ForeColor="Blue" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Σχολή:
        </th>
        <td>
            <asp:Label ID="lblSchool" runat="server" ForeColor="Blue" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τμήμα:
        </th>
        <td>
            <asp:Label ID="lblDepartment" runat="server" ForeColor="Blue" />
        </td>
    </tr>
</table>
<table id="tbInstitutionDetails" class="dv" width="100%">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Γραφείου
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Ίδρυμα:
        </th>
        <td>
            <asp:Label ID="lblPubInstitution" runat="server" ForeColor="Blue" />
        </td>
    </tr>
</table>
<table id="tbLibraryDetails" class="dv" width="100%">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Βιβλιοθήκης
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Ίδρυμα:
        </th>
        <td>
            <asp:Label ID="lblLibraryInstitution" runat="server" ForeColor="Blue" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τίτλος:
        </th>
        <td>
            <asp:Label ID="lblLibraryName" runat="server" ForeColor="Blue" />
        </td>
    </tr>
</table>
<table id="tbDistributionPointDetails" class="dv" width="100%">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Σημείου Διανομής
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Τίτλος:
        </th>
        <td>
            <asp:Label ID="lblDistributionPointName" runat="server" ForeColor="Blue" />
        </td>
    </tr>
</table>
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Ατόμου Επικοινωνίας
        </th>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Ον/μο:
        </td>
        <td>
            <asp:TextBox ID="txtReporterName" runat="server" Width="460px" />
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Τηλέφωνο:
        </td>
        <td>
            <asp:TextBox ID="txtReporterPhone" runat="server" MaxLength="10" Width="20%" />
            <imis:PhoneValidator ID="valReporterPhone" runat="server" ControlToValidate="txtReporterPhone"
                PhoneType="FixedOrMobile" ErrorMessage="Το πεδίο 'Τηλέφωνο' πρέπει να αποτελείται από ακριβώς 10 ψηφία και να ξεκινάει από 2 αν πρόκειται για σταθερό ή από 69 αν πρόκειται για κινητό"
                Display="Dynamic"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></imis:PhoneValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            E-mail:
        </td>
        <td>
            <asp:TextBox ID="txtReporterEmail" runat="server" Width="90%" />
            <asp:RegularExpressionValidator ID="revReporterEmail" runat="server" Display="Dynamic"
                ControlToValidate="txtReporterEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th colspan="2" class="header">
            &raquo; Λεπτομέρειες Συμβάντος
        </th>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Κατάσταση συμβάντος:
        </td>
        <td>
            <asp:DropDownList ID="ddlReportStatus" runat="server" OnInit="ddlReportStatus_Init"
                Width="460px" />
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Πλήρες κείμενο αναφοράς:
        </td>
        <td>
            <asp:TextBox ID="txtReportText" runat="server" TextMode="MultiLine" Rows="4" Width="460px" />
            <asp:RequiredFieldValidator ID="rfvReportText" Display="Dynamic" runat="server" ControlToValidate="txtReportText"
                ErrorMessage="Το πεδίο 'Πλήρες κείμενο αναφοράς' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<dxpc:ASPxPopupControl ID="dxpcPopup" runat="server" ClientInstanceName="devExPopup"
    Width="750" Height="560" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    AllowDragging="true" CloseAction="CloseButton">
</dxpc:ASPxPopupControl>
