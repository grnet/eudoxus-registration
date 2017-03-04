<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SecretaryInput.ascx.cs"
    Inherits="Eudoxus.Portal.UserControls.SecretaryInput" %>
<%@ Register Src="~/UserControls/TipIcon.ascx" TagPrefix="my" TagName="TipIcon" %>
<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Import Namespace="Eudoxus.BusinessModel" %>
<script type="text/javascript">
    var altName;
    var altEmail;
    var altMobile;
    var altPhone;
    var rbtlRepresentativeType;
    $(function () {
        //Cache the objects for extra speed 
        altName = $('#txtAlternateContactName');
        altEmail = $('#txtAlternateContactEmail');
        altPhone = $('#txtAlternateContactPhone');
        rbtlRepresentativeType = $('#rbtlRepresentativeType input');
        rbtlRepresentativeType.click(updateRepresentativeDisplay);
        rbtlRepresentativeType.next().click(updateRepresentativeDisplay);
        updateRepresentativeDisplay();
    });
    function validateAlternativeGroup(s, e) {
        if (altEmail.val() == '' //&& altMobile.val() == ''
         && altPhone.val() == '' && altName.val() == '') {
            e.IsValid = true;
        }
        else {
            if ($('#' + s.controltovalidate).val() != '') {
                e.IsValid = true;
            }
            else {
                e.IsValid = false;
            }
        }
    }

    function updateRepresentativeDisplay() {
        $('#trRepresentativeName').hide();
        rbtlRepresentativeType.each(function (index) {
            var btn = $(rbtlRepresentativeType[index]);

            if (btn.attr('checked') == true) {
                $('#trRepresentativeName').show();

                if (index == 0) {
                    $('#sRepresentativeName').html('Ονοματεπώνυμο Προέδρου:');
                }
                else {
                    $('#sRepresentativeName').html('Ονοματεπώνυμο Προϊσταμένου:');
                }
            }
        })
    }    
</script>
<asp:ScriptManagerProxy runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/_js/popUp1.js" />
        <asp:ScriptReference Path="~/_js/SchoolSearch.js" />
    </Scripts>
</asp:ScriptManagerProxy>
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Γραμματείας
        </th>
    </tr>
    <tr>
        <th style="width: 90px">
            Ίδρυμα:
        </th>
        <td colspan="3">
            <asp:TextBox ID="txtInstitutionName" runat="server" Width="90%" />
            <asp:PlaceHolder runat="server" ID="phSelectAcademic"><a href="javascript:void(0);"
                id="lnkSelectSchool" onclick="popUp.show('/Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');"
                title="Επιλογή Σχολής">
                <img id="Img1" runat="server" align="absmiddle" src="~/_img/iconSelectSchool.png"
                    alt="Επιλογή Σχολής" /></a> <a href="javascript:void(0);" title="Αφαίρεση Σχολής"
                        id="lnkRemoveSchoolSelection" onclick="return hd.removeSchoolSelection();" style="display: none;">
                        <img id="Img2" runat="server" align="absmiddle" src="~/_img/iconRemoveSchool.png"
                            alt="Αφαίρεση Σχολής" /></a> </asp:PlaceHolder>
            <asp:HiddenField ID="hfSchoolCode" runat="server" />
            <asp:RequiredFieldValidator runat="server" ID="rfvInstitutionName" ControlToValidate="txtInstitutionName"
                Display="Dynamic" ErrorMessage="Το πεδίο 'Ίδρυμα' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 90px">
            Σχολή:
        </th>
        <td>
            <asp:TextBox ID="txtSchoolName" runat="server" Width="90%" CssClass="tb-disabled" />
            <asp:CustomValidator runat="server" ID="cvCheckAcademic" SetFocusOnError="true" ErrorMessage="Υπάρχει ήδη πιστοποιημένος χρήστης για τη συγκεκριμένη Σχολή/Τμήμα. Για περισσότερες πληροφορίες, επικοινωνήστε με το Γραφείο Αρωγής Χρηστών."
                OnServerValidate="cvCheckAcademic_ServerValidate" Display="Dynamic"><img src="/_img/error.gif" title="Υπάρχει ήδη πιστοποιημένος χρήστης για τη συγκεκριμένη Σχολή/Τμήμα." /></asp:CustomValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 90px">
            Τμήμα:
        </th>
        <td>
            <asp:TextBox ID="txtDepartmentName" runat="server" Width="90%" CssClass="tb-disabled" />
        </td>
    </tr>
    <tr>
        <th style="width: 36%">
            Εξάμηνα Σπουδών:
        </th>
        <td>
            <asp:DropDownList ID="ddlSemesters" runat="server" OnInit="ddlSemesters_Init"
                Width="90%" DataTextField="Name" DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvSemesters" Display="Dynamic" runat="server"
                ControlToValidate="ddlSemesters" ErrorMessage="Το πεδίο 'Εξάμηνα Σπουδών' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο Γραμματείας (σταθερό):
            <my:TipIcon ID="tipSecretaryPhone" runat="server" Text="<%$ Resources:SecretaryInput, SecretaryPhone %>" />
        </th>
        <td>
            <asp:TextBox CssClass="source t2" ID="txtSecretaryPhone" runat="server" MaxLength="10"
                Width="20%" />
            <asp:RequiredFieldValidator ID="rfvSecretaryPhone" Display="Dynamic" runat="server"
                ControlToValidate="txtSecretaryPhone" ErrorMessage="Το πεδίο 'Τηλέφωνο Γραμματείας (σταθερό)' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revSecretaryPhone" runat="server" Display="Dynamic"
                ControlToValidate="txtSecretaryPhone" ValidationExpression="^2[0-9]{9}$" ErrorMessage="Το πεδίο 'Τηλέφωνο Γραμματείας (σταθερό)' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            E-mail Γραμματείας:
            <my:TipIcon ID="tipSecretaryEmail" runat="server" Text="<%$ Resources:SecretaryInput, SecretaryEmail %>" />
        </th>
        <td>
            <asp:TextBox CssClass="source t4" ID="txtSecretaryEmail" runat="server" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvSecretaryEmail" runat="server" ControlToValidate="txtSecretaryEmail"
                Display="Dynamic" ErrorMessage="Το πεδίο 'E-mail Γραμματείας' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revSecretaryEmail" runat="server" Display="Dynamic"
                ControlToValidate="txtSecretaryEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail της Γραμματείας δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="trRepresentative" runat="server">
        <td colspan="2">
            <div class="sub-description">
                Επιλέξτε Πρόεδρο Τμήματος ή Προϊστάμενο Γραμματείας, ανάλογα με το ποιος θα υπογράψει
                τη Βεβαίωση Συμμετοχής και θα ορίσει Υπεύθυνο για το πρόγραμμα «Εύδοξος»
            </div>
        </td>
    </tr>
    <tr>
        <th style="width: 90px">
            Πρόεδρος ή Προϊστάμενος:
        </th>
        <td>
            <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" OnInit="rbtlRepresentativeType_Init"
                ID="rbtlRepresentativeType" ClientIDMode="Static">
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfvlRepresentativeType" Display="Dynamic" runat="server"
                ControlToValidate="rbtlRepresentativeType" ErrorMessage="Το πεδίο 'Πρόεδρος ή Προϊστάμενος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trRepresentativeName">
        <th style="width: 30%">
            <span id="sRepresentativeName" style="font-size: 11px">Ονοματεπώνυμο:</span>
        </th>
        <td>
            <asp:TextBox ID="txtPresidentName" runat="server" MaxLength="500" Width="90%" CssClass="source t1" />
            <asp:RequiredFieldValidator ID="rfvPresidentName" Display="Dynamic" runat="server"
                ControlToValidate="txtPresidentName" ErrorMessage="Το πεδίο 'Ονοματεπώνυμο' είναι υποχρεωτικό">
                <img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" />
            </asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<br />
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Διεύθυνσης Γραμματείας Σχολής/Τμήματος
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Διεύθυνση:
        </th>
        <td>
            <asp:TextBox ID="txtSecretaryAddress" runat="server" MaxLength="100" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvSecretaryAddress" Display="Dynamic" runat="server"
                ControlToValidate="txtSecretaryAddress" ErrorMessage="Το πεδίο 'Διεύθυνση' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τ.Κ.:
        </th>
        <td>
            <asp:TextBox ID="txtSecretaryZipCode" runat="server" Columns="30" MaxLength="5" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvSecretaryZipCode" Display="Dynamic" runat="server"
                ControlToValidate="txtSecretaryZipCode" ErrorMessage="Το πεδίο 'Τ.Κ.' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revSecretaryZipCode" runat="server" ControlToValidate="txtSecretaryZipCode"
                Display="Dynamic" ValidationExpression="^\d{5}$" ErrorMessage="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία"><img src="/_img/error.gif" title="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Νομός:
            <my:TipIcon ID="tipSecretaryPrefecture" runat="server" Text="<%$ Resources:SecretaryInput, SecretaryPrefecture %>" />
        </th>
        <td>
            <asp:DropDownList ID="ddlSecretaryPrefecture" runat="server" Width="90%" OnInit="ddlSecretaryPrefecture_Init"
                DataTextField="Name" DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvSecretaryPrefecture" runat="server" Display="Dynamic"
                ControlToValidate="ddlSecretaryPrefecture" ErrorMessage="Το πεδίο 'Νομός' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Πόλη:
            <my:TipIcon ID="tipSecretaryCity" runat="server" Text="<%$ Resources:SecretaryInput, SecretaryCity %>" />
        </th>
        <td>
            <asp:DropDownList ID="ddlSecretaryCity" runat="server" Width="90%" DataTextField="Name"
                DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvSecretaryCity" Display="Dynamic" runat="server"
                ControlToValidate="ddlSecretaryCity" ErrorMessage="Το πεδίο 'Πόλη' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <ajaxToolkit:CascadingDropDown ID="cddSecretaryCity" runat="server" TargetControlID="ddlSecretaryCity"
                ParentControlID="ddlSecretaryPrefecture" Category="Cities" PromptText="-- επιλέξτε πόλη --"
                ServicePath="~/PortalServices/Services.asmx" ServiceMethod="GetCities" LoadingText="Παρακαλω περιμένετε">
            </ajaxToolkit:CascadingDropDown>
        </td>
    </tr>
</table>
<br />
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Υπευθύνου για το Εύδοξος
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Ονοματεπώνυμο:
            <my:TipIcon ID="tipContactName" runat="server" Text="<%$ Resources:SecretaryInput, ContactName %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactName" runat="server" MaxLength="100" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvContactName" Display="Dynamic" runat="server"
                ValidationGroup="vgContact" ControlToValidate="txtContactName" ErrorMessage="Το πεδίο 'Ονοματεπώνυμο Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (σταθερό):
            <my:TipIcon ID="tipContactPhone" runat="server" Text="<%$ Resources:SecretaryInput, ContactPhone %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactPhone" runat="server" MaxLength="10" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvContactPhone" Display="Dynamic" runat="server"
                ValidationGroup="vgContact" ControlToValidate="txtContactPhone" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactPhone" runat="server" Display="Dynamic"
                ControlToValidate="txtContactPhone" ValidationExpression="^2[0-9]{9}$" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Υπευθύνου για το Εύδοξος' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (κινητό):
            <my:TipIcon ID="tipContactMobilePhone" runat="server" Text="<%$ Resources:SecretaryInput, ContactMobilePhone %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactMobilePhone" runat="server" MaxLength="10" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvContactMobilePhone" runat="server" Display="Dynamic"
                ValidationGroup="vgContact" ControlToValidate="txtContactMobilePhone" ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactMobilePhone" runat="server" Display="Dynamic"
                ControlToValidate="txtContactMobilePhone" ValidationExpression="^69[0-9]{8}$"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Υπευθύνου για το Εύδοξος' πρέπει να ξεκινάει από 69 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            E-mail:
            <my:TipIcon ID="tipContactEmail" runat="server" Text="<%$ Resources:SecretaryInput, ContactEmail %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactEmail" runat="server" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server" ControlToValidate="txtContactEmail"
                ValidationGroup="vgContact" Display="Dynamic" ErrorMessage="Το πεδίο 'E-mail Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactEmail" runat="server" Display="Dynamic"
                ControlToValidate="txtContactEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail του Υπευθύνου για το Εύδοξος δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
</table>
<br />
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Αναπληρωτή Υπευθύνου για το Εύδοξος
        </th>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Ονοματεπώνυμο:
            <my:TipIcon ID="tipAlternateContactName" runat="server" Text="<%$ Resources:SecretaryInput, AlternateContactName %>" />
        </td>
        <td>
            <asp:TextBox ID="txtAlternateContactName" runat="server" MaxLength="100" Width="90%"
                ClientIDMode="Static" />
            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validateAlternativeGroup"
                ControlToValidate="txtAlternateContactName" Display="Dynamic" OnServerValidate="cvAlternativeGroup_ServerValidate"
                ValidateEmptyText="true" ErrorMessage="Το πεδίο 'Ονοματεπώνυμο Αναπληρωτή Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό">
                    <img src="/_img/error.gif" title="Πρέπει να συμπληρώσετε όλα τα στοιχεία του Αναπληρωτή Υπευθύνου" />
            </asp:CustomValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Τηλέφωνο (σταθερό):
            <my:TipIcon ID="tipAlternateContactPhone" runat="server" Text="<%$ Resources:SecretaryInput, AlternateContactPhone %>" />
        </td>
        <td>
            <asp:TextBox ID="txtAlternateContactPhone" runat="server" MaxLength="10" Width="20%"
                ClientIDMode="Static" />
            <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="validateAlternativeGroup"
                ControlToValidate="txtAlternateContactPhone" Display="Dynamic" OnServerValidate="cvAlternativeGroup_ServerValidate"
                ValidateEmptyText="true" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Αναπληρωτή Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό">
                    <img src="/_img/error.gif" title="Πρέπει να συμπληρώσετε όλα τα στοιχεία του Αναπληρωτή Υπευθύνου" />
            </asp:CustomValidator>
            <asp:RegularExpressionValidator ID="revAlternateContactPhone" runat="server" Display="Dynamic"
                ControlToValidate="txtAlternateContactPhone" ValidationExpression="^2[0-9]{9}$"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Αναπληρωτή Υπευθύνου για το Εύδοξος' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Τηλέφωνο (κινητό):
            <my:TipIcon ID="tipAlternateContactMobilePhone" runat="server" Text="<%$ Resources:SecretaryInput, AlternateContactMobilePhone %>" />
        </td>
        <td>
            <asp:TextBox ID="txtAlternateContactMobilePhone" runat="server" MaxLength="10" Width="20%" />
            <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="validateAlternativeGroup"
                ControlToValidate="txtAlternateContactMobilePhone" Display="Dynamic" OnServerValidate="cvAlternativeGroup_ServerValidate"
                ValidateEmptyText="true" ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Αναπληρωτή Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό">
                    <img src="/_img/error.gif" title="Πρέπει να συμπληρώσετε όλα τα στοιχεία του Αναπληρωτή Υπευθύνου" />
            </asp:CustomValidator>
            <asp:RegularExpressionValidator ID="revAlternateContactMobilePhone" runat="server"
                Display="Dynamic" ControlToValidate="txtAlternateContactMobilePhone" ValidationExpression="^69[0-9]{8}$"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Αναπληρωτή Υπευθύνου για το Εύδοξος' πρέπει να ξεκινάει από 69 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            E-mail:
            <my:TipIcon ID="tipAlternateContactEmail" runat="server" Text="<%$ Resources:SecretaryInput, AlternateContactEmail %>" />
        </td>
        <td>
            <asp:TextBox ID="txtAlternateContactEmail" runat="server" Width="90%" ClientIDMode="Static" />
            <asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="validateAlternativeGroup"
                ControlToValidate="txtAlternateContactEmail" Display="Dynamic" OnServerValidate="cvAlternativeGroup_ServerValidate"
                ValidateEmptyText="true" ErrorMessage="Το πεδίο 'E-mail Αναπληρωτή Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό">
                    <img src="/_img/error.gif" title="Πρέπει να συμπληρώσετε όλα τα στοιχεία του Αναπληρωτή Υπευθύνου" />
            </asp:CustomValidator>
            <asp:RegularExpressionValidator ID="revAlternateContactEmail" runat="server" Display="Dynamic"
                ControlToValidate="txtAlternateContactEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail του Αναπληρωτή Υπευθύνου για το Εύδοξος δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
</table>
<dxpc:ASPxPopupControl ID="dxpcPopup" runat="server" ClientInstanceName="devExPopup"
    Width="800" Height="610" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    AllowDragging="true" CloseAction="CloseButton">
</dxpc:ASPxPopupControl>
