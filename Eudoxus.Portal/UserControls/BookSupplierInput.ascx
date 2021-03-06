﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookSupplierInput.ascx.cs"
    Inherits="Eudoxus.Portal.UserControls.BookSupplierInput" %>
<%@ Register Src="~/UserControls/TipIcon.ascx" TagPrefix="my" TagName="TipIcon" %>
<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Import Namespace="Eudoxus.BusinessModel" %>
<script type="text/javascript">
    var altName;
    var altEmail;
    var altMobile;
    var altPhone;
    var rbtlCertifierType;
    $(function () {
        //Cache the objects for extra speed 
        altName = $('#txtAlternateContactName');
        altEmail = $('#txtAlternateContactEmail');
        altPhone = $('#txtAlternateContactPhone');
        rbtlCertifierType = $('#rbtlCertifierType input');
        rbtlCertifierType.click(updateCertifierDisplay);
        rbtlCertifierType.next().click(updateCertifierDisplay);
        updateCertifierDisplay();
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

    function updateCertifierDisplay() {
        $('#trCertifierName').hide();
        rbtlCertifierType.each(function (index) {
            var btn = $(rbtlCertifierType[index]);

            if (btn.attr('checked') == true) {
                $('#trCertifierName').show();

                if (index == 0) {
                    $('#sCertifierName').html('Ονοματεπώνυμο Πρύτανη (Πανεπιστημίου):');
                }
                else {
                    $('#sCertifierName').html('Ονοματεπώνυμο Προέδρου (ΤΕΙ):');
                }
            }
        })
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
            &raquo; Στοιχεία Υπεύθυνου Παραγγελίας Βιβλίων
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
            <asp:CustomValidator runat="server" ID="cvCheckAcademic" SetFocusOnError="true" ErrorMessage="Υπάρχει ήδη πιστοποιημένος χρήστης για το συγκεκριμένο Ίδρυμα. Για περισσότερες πληροφορίες, επικοινωνήστε με το Γραφείο Αρωγής Χρηστών."
                OnServerValidate="cvCheckAcademic_ServerValidate" Display="Dynamic"><img src="/_img/error.gif" title="Υπάρχει ήδη πιστοποιημένος χρήστης για το συγκεκριμένο Ίδρυμα." /></asp:CustomValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Ονοματεπώνυμο υπεύθυνου:
            <my:TipIcon ID="tipContactName" runat="server" Text="<%$ Resources:BookSupplierInput, ContactName %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactName" runat="server" MaxLength="100" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvContactName" Display="Dynamic" runat="server"
                ValidationGroup="vgContact" ControlToValidate="txtContactName" ErrorMessage="Το πεδίο 'Ονοματεπώνυμο Υπεύθυνου Παραγγελίας Βιβλίων' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (σταθερό):
            <my:TipIcon ID="tipContactPhone" runat="server" Text="<%$ Resources:BookSupplierInput, ContactPhone %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactPhone" runat="server" MaxLength="10" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvContactPhone" Display="Dynamic" runat="server"
                ValidationGroup="vgContact" ControlToValidate="txtContactPhone" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Υπεύθυνου Παραγγελίας Βιβλίων' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactPhone" runat="server" Display="Dynamic"
                ControlToValidate="txtContactPhone" ValidationExpression="^2[0-9]{9}$" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Υπεύθυνου Παραγγελίας Βιβλίων' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (κινητό):
            <my:TipIcon ID="tipContactMobilePhone" runat="server" Text="<%$ Resources:BookSupplierInput, ContactMobilePhone %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactMobilePhone" runat="server" MaxLength="10" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvContactMobilePhone" runat="server" Display="Dynamic"
                ValidationGroup="vgContact" ControlToValidate="txtContactMobilePhone" ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Υπεύθυνου Παραγγελίας Βιβλίων' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactMobilePhone" runat="server" Display="Dynamic"
                ControlToValidate="txtContactMobilePhone" ValidationExpression="^69[0-9]{8}$"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Υπευθύνου Υπεύθυνου Παραγγελίας Βιβλίων' πρέπει να ξεκινάει από 69 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            E-mail:
            <my:TipIcon ID="tipContactEmail" runat="server" Text="<%$ Resources:BookSupplierInput, ContactEmail %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactEmail" runat="server" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server" ControlToValidate="txtContactEmail"
                ValidationGroup="vgContact" Display="Dynamic" ErrorMessage="Το πεδίο 'E-mail Υπευθύνου Υπεύθυνου Παραγγελίας Βιβλίων' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactEmail" runat="server" Display="Dynamic"
                ControlToValidate="txtContactEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail του Υπεύθυνου Παραγγελίας Βιβλίων δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
</table>
<br />
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Πιστοποιούσα Αρχή
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Πρύτανης ή Πρόεδρος:
        </th>
        <td>
            <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" OnInit="rbtlCertifierType_Init"
                ID="rbtlCertifierType" ClientIDMode="Static">
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfvlCertifierType" Display="Dynamic" runat="server"
                ControlToValidate="rbtlCertifierType" ErrorMessage="Το πεδίο 'Πρύτανης ή Πρόεδρος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trCertifierName">
        <th style="width: 30%">
            <span id="sCertifierName" style="font-size: 11px">Ονοματεπώνυμο:</span>
        </th>
        <td>
            <asp:TextBox ID="txtCertifierName" runat="server" MaxLength="500" Width="90%" CssClass="source t1" />
            <asp:RequiredFieldValidator ID="rfvCertifierName" Display="Dynamic" runat="server"
                ControlToValidate="txtCertifierName" ErrorMessage="Το πεδίο 'Ονοματεπώνυμο' είναι υποχρεωτικό">
                <img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" />
            </asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<br />
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Διεύθυνσης Υπεύθυνου Παραγγελίας Βιβλίων
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Διεύθυνση:
        </th>
        <td>
            <asp:TextBox ID="txtBookSupplierAddress" runat="server" MaxLength="100" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvBookSupplierAddress" Display="Dynamic" runat="server"
                ControlToValidate="txtBookSupplierAddress" ErrorMessage="Το πεδίο 'Διεύθυνση' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τ.Κ.:
        </th>
        <td>
            <asp:TextBox ID="txtBookSupplierZipCode" runat="server" Columns="30" MaxLength="5"
                Width="20%" />
            <asp:RequiredFieldValidator ID="rfvBookSupplierZipCode" Display="Dynamic" runat="server"
                ControlToValidate="txtBookSupplierZipCode" ErrorMessage="Το πεδίο 'Τ.Κ.' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revBookSupplierZipCode" runat="server" ControlToValidate="txtBookSupplierZipCode"
                Display="Dynamic" ValidationExpression="^\d{5}$" ErrorMessage="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία"><img src="/_img/error.gif" title="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Νομός:
        </th>
        <td>
            <asp:DropDownList ID="ddlBookSupplierPrefecture" runat="server" Width="90%" OnInit="ddlBookSupplierPrefecture_Init"
                DataTextField="Name" DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvBookSupplierPrefecture" runat="server" Display="Dynamic"
                ControlToValidate="ddlBookSupplierPrefecture" ErrorMessage="Το πεδίο 'Νομός' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Πόλη:
        </th>
        <td>
            <asp:DropDownList ID="ddlBookSupplierCity" runat="server" Width="90%" DataTextField="Name"
                DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvBookSupplierCity" Display="Dynamic" runat="server"
                ControlToValidate="ddlBookSupplierCity" ErrorMessage="Το πεδίο 'Πόλη' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <ajaxToolkit:CascadingDropDown ID="cddBookSupplierCity" runat="server" TargetControlID="ddlBookSupplierCity"
                ParentControlID="ddlBookSupplierPrefecture" Category="Cities" PromptText="-- επιλέξτε πόλη --"
                ServicePath="~/PortalServices/Services.asmx" ServiceMethod="GetCities" LoadingText="Παρακαλω περιμένετε">
            </ajaxToolkit:CascadingDropDown>
        </td>
    </tr>
</table>
<br />
<br />
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Αναπληρωτή Υπευθύνου Παραγγελίας Βιβλίων
        </th>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Ονοματεπώνυμο:
            <my:TipIcon ID="tipAlternateContactName" runat="server" Text="<%$ Resources:BookSupplierInput, AlternateContactName %>" />
        </td>
        <td>
            <asp:TextBox ID="txtAlternateContactName" runat="server" MaxLength="100" Width="90%"
                ClientIDMode="Static" />
            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validateAlternativeGroup"
                ControlToValidate="txtAlternateContactName" Display="Dynamic" OnServerValidate="cvAlternativeGroup_ServerValidate"
                ValidateEmptyText="true" ErrorMessage="Το πεδίο 'Ονοματεπώνυμο Αναπληρωτή Υπευθύνου Παραγγελίας Βιβλίωνς' είναι υποχρεωτικό">
                    <img src="/_img/error.gif" title="Πρέπει να συμπληρώσετε όλα τα στοιχεία του Αναπληρωτή Υπευθύνου" />
            </asp:CustomValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Τηλέφωνο (σταθερό):
            <my:TipIcon ID="tipAlternateContactPhone" runat="server" Text="<%$ Resources:BookSupplierInput, AlternateContactPhone %>" />
        </td>
        <td>
            <asp:TextBox ID="txtAlternateContactPhone" runat="server" MaxLength="10" Width="20%"
                ClientIDMode="Static" />
            <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="validateAlternativeGroup"
                ControlToValidate="txtAlternateContactPhone" Display="Dynamic" OnServerValidate="cvAlternativeGroup_ServerValidate"
                ValidateEmptyText="true" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Αναπληρωτή Υπευθύνου Παραγγελίας Βιβλίων' είναι υποχρεωτικό">
                    <img src="/_img/error.gif" title="Πρέπει να συμπληρώσετε όλα τα στοιχεία του Αναπληρωτή Υπευθύνου" />
            </asp:CustomValidator>
            <asp:RegularExpressionValidator ID="revAlternateContactPhone" runat="server" Display="Dynamic"
                ControlToValidate="txtAlternateContactPhone" ValidationExpression="^2[0-9]{9}$"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Αναπληρωτή Υπευθύνου Παραγγελίας Βιβλίων' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Τηλέφωνο (κινητό):
            <my:TipIcon ID="tipAlternateContactMobilePhone" runat="server" Text="<%$ Resources:BookSupplierInput, AlternateContactMobilePhone %>" />
        </td>
        <td>
            <asp:TextBox ID="txtAlternateContactMobilePhone" runat="server" MaxLength="10" Width="20%" />
            <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="validateAlternativeGroup"
                ControlToValidate="txtAlternateContactMobilePhone" Display="Dynamic" OnServerValidate="cvAlternativeGroup_ServerValidate"
                ValidateEmptyText="true" ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Αναπληρωτή Υπευθύνου Παραγγελίας Βιβλίων' είναι υποχρεωτικό">
                    <img src="/_img/error.gif" title="Πρέπει να συμπληρώσετε όλα τα στοιχεία του Αναπληρωτή Υπευθύνου" />
            </asp:CustomValidator>
            <asp:RegularExpressionValidator ID="revAlternateContactMobilePhone" runat="server"
                Display="Dynamic" ControlToValidate="txtAlternateContactMobilePhone" ValidationExpression="^69[0-9]{8}$"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Αναπληρωτή Υπευθύνου Παραγγελίας Βιβλίων' πρέπει να ξεκινάει από 69 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            E-mail:
            <my:TipIcon ID="tipAlternateContactEmail" runat="server" Text="<%$ Resources:BookSupplierInput, AlternateContactEmail %>" />
        </td>
        <td>
            <asp:TextBox ID="txtAlternateContactEmail" runat="server" Width="90%" ClientIDMode="Static" />
            <asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="validateAlternativeGroup"
                ControlToValidate="txtAlternateContactEmail" Display="Dynamic" OnServerValidate="cvAlternativeGroup_ServerValidate"
                ValidateEmptyText="true" ErrorMessage="Το πεδίο 'E-mail Αναπληρωτή Υπευθύνου Παραγγελίας Βιβλίων' είναι υποχρεωτικό">
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
