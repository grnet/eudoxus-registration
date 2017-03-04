<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LibraryInput.ascx.cs"
    Inherits="Eudoxus.Portal.UserControls.LibraryInput" %>
<%@ Register Src="~/UserControls/TipIcon.ascx" TagPrefix="my" TagName="TipIcon" %>
<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Import Namespace="Eudoxus.BusinessModel" %>
<script type="text/javascript">
    var altName;
    var altEmail;
    var altMobile;
    var altPhone;

    $(function () {
        //Cache the objects for extra speed 
        altName = $('#txtAlternateContactName');
        altEmail = $('#txtAlternateContactEmail');
        altPhone = $('#txtAlternateContactPhone');
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
</script>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/_js/popUp1.js" />
        <asp:ScriptReference Path="~/_js/SchoolSearch.js" />
    </Scripts>
</asp:ScriptManagerProxy>
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Βιβλιοθήκης
        </th>
    </tr>
    <tr>
        <th style="width: 90px">
            Ίδρυμα:
        </th>
        <td>
            <asp:DropDownList ID="ddlInstitution" runat="server" Width="90%" OnInit="ddlInstitution_Init" />
            <asp:RequiredFieldValidator ID="rfvInstitution" runat="server" Display="Dynamic"
                ControlToValidate="ddlInstitution" ErrorMessage="Το πεδίο 'Ίδρυμα' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τίτλος:
            <my:TipIcon ID="tipLibraryName" runat="server" Text="<%$ Resources:LibraryInput, LibraryName %>" />
        </th>
        <td>
            <asp:TextBox ID="txtLibraryName" runat="server" MaxLength="500" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvLibraryName" Display="Dynamic" runat="server" ControlToValidate="txtLibraryName" ErrorMessage="Το πεδίο 'Τίτλος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Ωράριο Λειτουργίας:
            <my:TipIcon ID="tipLibraryOpeningHours" runat="server" Text="<%$ Resources:LibraryInput, LibraryOpeningHours %>" />
        </th>
        <td>
            <asp:TextBox ID="txtLibraryOpeningHours" runat="server" MaxLength="500" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvLibraryOpeningHours" Display="Dynamic" runat="server" ControlToValidate="txtLibraryOpeningHours" ErrorMessage="Το πεδίο 'Ωράριο Λειτουργίας' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο Βιβλιοθήκης (σταθερό):
            <my:TipIcon ID="tipLibraryPhone" runat="server" Text="<%$ Resources:LibraryInput, LibraryPhone %>" />
        </th>
        <td>
            <asp:TextBox CssClass="source t2" ID="txtLibraryPhone" runat="server" MaxLength="10"
                Width="20%" />
            <asp:RequiredFieldValidator ID="rfvLibraryPhone" Display="Dynamic" runat="server"
                ControlToValidate="txtLibraryPhone" ErrorMessage="Το πεδίο 'Τηλέφωνο Βιβλιοθήκης (σταθερό)' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revLibraryPhone" runat="server" Display="Dynamic"
                ControlToValidate="txtLibraryPhone" ValidationExpression="^2[0-9]{9}$" ErrorMessage="Το πεδίο 'Τηλέφωνο Βιβλιοθήκης (σταθερό)' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            E-mail Βιβλιοθήκης:
            <my:TipIcon ID="tipLibraryEmail" runat="server" Text="<%$ Resources:LibraryInput, LibraryEmail %>" />
        </th>
        <td>
            <asp:TextBox CssClass="source t4" ID="txtLibraryEmail" runat="server" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvLibraryEmail" runat="server" ControlToValidate="txtLibraryEmail"
                Display="Dynamic" ErrorMessage="Το πεδίο 'E-mail Βιβλιοθήκης' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revLibraryEmail" runat="server" Display="Dynamic"
                ControlToValidate="txtLibraryEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail του Βιβλιοθήκης δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Ιστοσελίδα:
        </td>
        <td>
            <asp:TextBox ID="txtLibraryURL" runat="server" MaxLength="200" Width="90%" />
        </td>
    </tr>
    <tr id="trDirectorName">
        <th style="width: 30%">
            <span id="sDirectorName" style="font-size: 11px">Προϊστάμενος Βιβλιοθήκης:</span>
        </th>
        <td>
            <asp:TextBox ID="txtDirectorName" runat="server" MaxLength="500" Width="90%" CssClass="source t1" />
            <asp:RequiredFieldValidator ID="rfvDirectorName" Display="Dynamic" runat="server"
                ControlToValidate="txtDirectorName" ErrorMessage="Το πεδίο 'Προϊστάμενος Βιβλιοθήκης' είναι υποχρεωτικό">
                <img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" />
            </asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<br />
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Διεύθυνσης Βιβλιοθήκης
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Διεύθυνση:
        </th>
        <td>
            <asp:TextBox ID="txtLibraryAddress" runat="server" MaxLength="100" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvLibraryAddress" Display="Dynamic" runat="server"
                ControlToValidate="txtLibraryAddress" ErrorMessage="Το πεδίο 'Διεύθυνση' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τ.Κ.:
        </th>
        <td>
            <asp:TextBox ID="txtLibraryZipCode" runat="server" Columns="30" MaxLength="5" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvLibraryZipCode" Display="Dynamic" runat="server"
                ControlToValidate="txtLibraryZipCode" ErrorMessage="Το πεδίο 'Τ.Κ.' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revLibraryZipCode" runat="server" ControlToValidate="txtLibraryZipCode"
                Display="Dynamic" ValidationExpression="^\d{5}$" ErrorMessage="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία"><img src="/_img/error.gif" title="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Νομός:
            <my:TipIcon ID="tipLibraryPrefecture" runat="server" Text="<%$ Resources:LibraryInput, LibraryPrefecture %>" />
        </th>
        <td>
            <asp:DropDownList ID="ddlLibraryPrefecture" runat="server" Width="90%" OnInit="ddlLibraryPrefecture_Init"
                DataTextField="Name" DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvLibraryPrefecture" runat="server" Display="Dynamic"
                ControlToValidate="ddlLibraryPrefecture" ErrorMessage="Το πεδίο 'Νομός' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Πόλη:
            <my:TipIcon ID="tipLibraryCity" runat="server" Text="<%$ Resources:LibraryInput, LibraryCity %>" />
        </th>
        <td>
            <asp:DropDownList ID="ddlLibraryCity" runat="server" Width="90%" DataTextField="Name"
                DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvLibraryCity" Display="Dynamic" runat="server"
                ControlToValidate="ddlLibraryCity" ErrorMessage="Το πεδίο 'Πόλη' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <ajaxToolkit:CascadingDropDown ID="cddLibraryCity" runat="server" TargetControlID="ddlLibraryCity"
                ParentControlID="ddlLibraryPrefecture" Category="Cities" PromptText="-- επιλέξτε πόλη --"
                ServicePath="~/PortalServices/Services.asmx" ServiceMethod="GetCities" LoadingText="Παρακαλω περιμένετε">
            </ajaxToolkit:CascadingDropDown>
        </td>
    </tr>
    <tr>        
        <td class="notRequired" style="width: 30%">
            URL σε Χαρτογραφικό Σύστημα:
            <my:TipIcon ID="tipLibraryLocationURL" runat="server" Text="<%$ Resources:LibraryInput, LibraryLocationURL %>" />
            <br /><a href="http://eudoxus.gr/Files/Maps_URL_Manual_.pdf"  target="_blank">Οδηγίες</a>
        </td>
        <td>
            <asp:TextBox ID="txtLibraryLocationURL" runat="server" Width="90%" />
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
            <my:TipIcon ID="tipContactName" runat="server" Text="<%$ Resources:LibraryInput, ContactName %>" />
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
            <my:TipIcon ID="tipContactPhone" runat="server" Text="<%$ Resources:LibraryInput, ContactPhone %>" />
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
            <my:TipIcon ID="tipContactMobilePhone" runat="server" Text="<%$ Resources:LibraryInput, ContactMobilePhone %>" />
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
            <my:TipIcon ID="tipContactEmail" runat="server" Text="<%$ Resources:LibraryInput, ContactEmail %>" />
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
            <my:TipIcon ID="tipAlternateContactName" runat="server" Text="<%$ Resources:LibraryInput, AlternateContactName %>" />
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
            <my:TipIcon ID="tipAlternateContactPhone" runat="server" Text="<%$ Resources:LibraryInput, AlternateContactPhone %>" />
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
            <my:TipIcon ID="tipAlternateContactMobilePhone" runat="server" Text="<%$ Resources:LibraryInput, AlternateContactMobilePhone %>" />
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
            <my:TipIcon ID="tipAlternateContactEmail" runat="server" Text="<%$ Resources:LibraryInput, AlternateContactEmail %>" />
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
