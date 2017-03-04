<%@ Control Language="C#" AutoEventWireup="true" Inherits="Eudoxus.Portal.Helpdesk.UserControls.ReporterInput"
    CodeBehind="ReporterInput.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v9.2" Namespace="DevExpress.Web.ASPxCallbackPanel"
    TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v9.2" Namespace="DevExpress.Web.ASPxPanel"
    TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Γενικά Στοιχεία
        </th>
    </tr>
    <th style="width: 30%">
        Κατηγορία αναφέροντος:
    </th>
    <td>
        <asp:Label ID="lblReporterType" runat="server" ForeColor="Blue" />
    </td>
</table>
<table id="tbUnknownDetails" runat="server" visible="false" class="dv" width="100%">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Χρήστη χωρίς λογαριασμό
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Είδος Χρήστη:
        </th>
        <td>
            <asp:DropDownList ID="ddlUnknownReporterType" runat="server" OnInit="ddlUnknownReporterType_Init"
                Width="460px" />
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
<table id="tbStudentDetails" runat="server" visible="false" class="dv" width="100%">
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
<table id="tbProfessorDetails" runat="server" visible="false" class="dv" width="100%">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Καθηγητή
        </th>
    </tr>
</table>
<table id="tbAcademicDetails" runat="server" visible="false" class="dv" style="border-top: hidden" width="100%">
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
            <asp:TextBox ID="txtContactName" runat="server" Width="460px" />
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Τηλέφωνο:
        </td>
        <td>
            <asp:TextBox ID="txtContactPhone" runat="server" MaxLength="10" Width="20%" />
            <imis:PhoneValidator ID="valContactPhone" runat="server" ControlToValidate="txtContactPhone"
                PhoneType="FixedOrMobile" ErrorMessage="Το πεδίο 'Τηλέφωνο' πρέπει να αποτελείται από ακριβώς 10 ψηφία και να ξεκινάει από 2 αν πρόκειται για σταθερό ή από 69 αν πρόκειται για κινητό"
                Display="Dynamic"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></imis:PhoneValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            E-mail:
        </td>
        <td>
            <asp:TextBox ID="txtContactEmail" runat="server" Width="90%" />
            <asp:RegularExpressionValidator ID="revContactEmail" runat="server" Display="Dynamic"
                ControlToValidate="txtContactEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
</table>
<dxpc:ASPxPopupControl ID="dxpcPopup" runat="server" ClientInstanceName="devExPopup"
    Width="750" Height="560" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    AllowDragging="true" CloseAction="CloseButton">
</dxpc:ASPxPopupControl>
