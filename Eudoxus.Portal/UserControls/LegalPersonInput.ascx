<%@ Control Language="C#" AutoEventWireup="true" Inherits="Eudoxus.Portal.UserControls.LegalPersonInput" CodeBehind="LegalPersonInput.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Src="~/UserControls/TipIcon.ascx" TagPrefix="my" TagName="TipIcon" %>
<%@ Register Assembly="Eudoxus.Portal" Namespace="Eudoxus.Portal.Controls" TagPrefix="lc" %>
<%@ Register Src="~/UserControls/IdentityControl.ascx" TagName="IdentityControl" TagPrefix="my" %>
<%@ Import Namespace="Eudoxus.BusinessModel" %>

<script type="text/javascript">
    var altName;
    var altEmail;
    var altMobile;
    var altPhone;
    $(function() {
        //Cache the objects for extra speed 
        altName = $('#<%= txtAlternateContactName.ClientID %>');
        altEmail = $('#<%= txtAlternateContactEmail.ClientID %>');
        altMobile = $('#<%= txtAlternateContactMobilePhone.ClientID %>');
        altPhone = $('#<%= txtAlternateContactPhone.ClientID %>');
    });
    function validateAlternativeGroup(s, e) {
        if (altEmail.val() == '' && altMobile.val() == ''
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

<table width="100%" class="dv">
    <tr id="trLegalPerson">
        <th colspan="2" class="header">
            &raquo; Στοιχεία Εκδοτικού Οίκου
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Κατηγορία:
        </th>
        <td style="font-weight: bold; color: Blue">
            <%= enPublisherType.LegalPerson.GetLabel() %>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            <span id="sPublisherName" style="font-size: 11px">Επωνυμία:</span>
            <my:TipIcon ID="tipPublisherName" runat="server" Text="<%$ Resources:LegalPersonInput, PublisherName %>" />
        </th>
        <td>
            <asp:TextBox ID="txtPublisherName" runat="server" MaxLength="500" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvPublisherName" Display="Dynamic" runat="server" ControlToValidate="txtPublisherName" ErrorMessage="Το πεδίο 'Επωνυμία' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trPublisherTradeName">
        <td class="notRequired" style="width: 30%">Διακριτικός Τίτλος:
            <my:TipIcon ID="tipPublisherTradeName" runat="server" Text="<%$ Resources:LegalPersonInput, PublisherTradename %>" />
        </td>
        <td>
            <asp:TextBox ID="txtPublisherTradeName" runat="server" MaxLength="500" Width="90%" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Α.Φ.Μ.:
            <my:TipIcon ID="tipPublisherAFM" runat="server" Text="<%$ Resources:LegalPersonInput, PublisherAFM %>" />
        </th>
        <td> 
            <lc:AFMInput ID="txtPublisherAFM" runat="server" ReadOnly="false" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Δ.Ο.Υ.:
        </th>
        <td>
            <asp:DropDownList ID="ddlPublisherDOY" runat="server" OnInit="ddlDOY_Init" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvPublisherDOY" Display="Dynamic" runat="server" ControlToValidate="ddlPublisherDOY" ErrorMessage="Το πεδίο 'Δ.Ο.Υ.' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (σταθερό):
            <my:TipIcon ID="tipPublisherPhone" runat="server" Text="<%$ Resources:LegalPersonInput, PublisherPhone %>" />
        </th>
        <td>
            <asp:TextBox ID="txtPublisherPhone" runat="server" MaxLength="10" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvPublisherPhone" Display="Dynamic" runat="server" ControlToValidate="txtPublisherPhone" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό)' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPublisherPhone" runat="server" Display="Dynamic" ControlToValidate="txtPublisherPhone" ValidationExpression="^2[0-9]{9}$"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό)' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="trPublisherMobilePhone">
        <td class="notRequired" style="width: 30%">Τηλέφωνο (κινητό):
            <my:TipIcon ID="tipPublisherMobilePhone" runat="server" Text="<%$ Resources:LegalPersonInput, PublisherMobilePhone %>" />
        </td>
        <td>
            <asp:TextBox ID="txtPublisherMobilePhone" runat="server" MaxLength="10" Width="20%" />
            <asp:RegularExpressionValidator ID="revPublisherMobilePhone" runat="server" Display="Dynamic" ControlToValidate="txtPublisherMobilePhone"
                ValidationExpression="^69[0-9]{8}$" ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό)' πρέπει να ξεκινάει από 69 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">Fax: </td>
        <td>
            <asp:TextBox ID="txtPublisherFax" runat="server" MaxLength="10" Width="20%" />
            <asp:RegularExpressionValidator ID="revPublisherFax" runat="server" Display="Dynamic" ControlToValidate="txtPublisherFax" ValidationExpression="^2[0-9]{9}$"
                ErrorMessage="Το πεδίο 'Fax' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός Fax" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            E-mail:
            <my:TipIcon ID="tipPublisherEmail" runat="server" Text="<%$ Resources:LegalPersonInput, PublisherEmail %>" />
        </th>
        <td>
            <asp:TextBox ID="txtPublisherEmail" runat="server" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvPublisherEmail" runat="server" ControlToValidate="txtPublisherEmail" Display="Dynamic" ErrorMessage="Το πεδίο 'E-mail' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPublisherEmail" runat="server" Display="Dynamic" ControlToValidate="txtPublisherEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail της επιχείρησης δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">Ιστοσελίδα: </td>
        <td>
            <asp:TextBox ID="txtPublisherURL" runat="server" MaxLength="200" Width="90%" />
        </td>
    </tr>
</table>
<br />
<table width="100%" class="dv">
    <tr id="trLegalPersonAddress">
        <th colspan="2" class="header">
            &raquo; Στοιχεία Διεύθυνσης Έδρας
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Οδός - Αριθμός:
        </th>
        <td>
            <asp:TextBox ID="txtPublisherAddress" runat="server" MaxLength="256" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvPublisherAddress" Display="Dynamic" runat="server" ControlToValidate="txtPublisherAddress" ErrorMessage="Το πεδίο 'Οδός - Αριθμός' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τ.Κ.:
        </th>
        <td>
            <asp:TextBox ID="txtPublisherZipCode" runat="server" Columns="30" MaxLength="5" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvPublisherZipCode" Display="Dynamic" runat="server" ControlToValidate="txtPublisherZipCode" ErrorMessage="Το πεδίο 'Τ.Κ.' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPublisherZipCode" runat="server" ControlToValidate="txtPublisherZipCode" Display="Dynamic" ValidationExpression="^\d{5}$"
                ErrorMessage="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία"><img src="/_img/error.gif" title="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Νομός:
        </th>
        <td>
            <asp:DropDownList ID="ddlPublisherPrefecture" runat="server" Width="90%" OnInit="ddlPublisherPrefecture_Init" DataTextField="Name"
                DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvPublisherPrefecture" runat="server" Display="Dynamic" ControlToValidate="ddlPublisherPrefecture"
                ErrorMessage="Το πεδίο 'Νομός' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Πόλη:
        </th>
        <td>
            <asp:DropDownList ID="ddlPublisherCity" runat="server" Width="90%" DataTextField="Name" DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvPublisherCity" Display="Dynamic" runat="server" ControlToValidate="ddlPublisherCity" ErrorMessage="Το πεδίο 'Πόλη' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <ajaxToolkit:CascadingDropDown ID="cddPublisherCity" runat="server" TargetControlID="ddlPublisherCity" ParentControlID="ddlPublisherPrefecture"
                Category="Cities" PromptText="-- επιλέξτε πόλη --" ServicePath="~/PortalServices/Services.asmx" ServiceMethod="GetCities" LoadingText="Παρακαλω περιμένετε">
            </ajaxToolkit:CascadingDropDown>
        </td>
    </tr>
</table>
<div id="divLegalPersonDetails">
    <br />
    <table width="100%" class="dv">
        <tr>
            <th colspan="2" class="header">
                &raquo; Στοιχεία Νομίμου Εκπροσώπου της Εταιρείας
            </th>
        </tr>
        <tr>
            <th style="width: 30%">
                Ονοματεπώνυμο:
                <my:TipIcon ID="tipLegalPersonName" runat="server" Text="<%$ Resources:LegalPersonInput, LegalPersonName %>" />
            </th>
            <td>
                <asp:TextBox ID="txtLegalPersonName" runat="server" MaxLength="100" Width="90%" />
                <asp:RequiredFieldValidator ID="rfvLegalPersonName" runat="server" ValidationGroup="vgLegalPerson" Display="Dynamic" ControlToValidate="txtLegalPersonName"
                    ErrorMessage="Το πεδίο 'Ονοματεπώνυμο Νομίμου Εκπροσώπου' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width: 30%">
                Τηλέφωνο:
                <my:TipIcon ID="tipLegalPersonPhone" runat="server" Text="<%$ Resources:LegalPersonInput, LegalPersonPhone %>" />
            </th>
            <td>
                <asp:TextBox ID="txtLegalPersonPhone" runat="server" MaxLength="10" Width="20%" />
                <asp:RequiredFieldValidator ID="rfvLegalPersonPhone" runat="server" ValidationGroup="vgLegalPerson" Display="Dynamic" ControlToValidate="txtLegalPersonPhone"
                    ErrorMessage="Το πεδίο 'Τηλέφωνο Νομίμου Εκπροσώπου' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revLegalPersonPhone" runat="server" Display="Dynamic" ControlToValidate="txtLegalPersonPhone" ValidationExpression="^(2[0-9]{9})|(69[0-9]{8})$"
                    ErrorMessage="Το πεδίο 'Τηλέφωνο' πρέπει να αποτελείται από ακριβώς 10 ψηφία και να ξεκινάει από 2 αν πρόκειται για σταθερό ή από 69 αν πρόκειται για κινητό."><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th style="width: 30%">
                E-mail:
                <my:TipIcon ID="tipLegalPersonEmail" runat="server" Text="<%$ Resources:LegalPersonInput, LegalPersonEmail %>" />
            </th>
            <td>
                <asp:TextBox ID="txtLegalPersonEmail" runat="server" Width="90%" />
                <asp:RequiredFieldValidator ID="rfvLegalPersonEmail" runat="server" ValidationGroup="vgLegalPerson" ControlToValidate="txtLegalPersonEmail"
                    Display="Dynamic" ErrorMessage="Το πεδίο 'E-mail Νομίμου Εκπροσώπου' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revLegalPersonEmail" runat="server" ValidationGroup="vgLegalPerson" Display="Dynamic" ControlToValidate="txtLegalPersonEmail"
                    ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                    ErrorMessage="Το E-mail του Νομίμου Εκπροσώπου δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
            </td>
        </tr>
        <my:IdentityControl runat="server" ID="idLegalPerson" />
    </table>
</div>
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
            <my:TipIcon ID="tipContactName" runat="server" Text="<%$ Resources:LegalPersonInput, ContactName %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactName" runat="server" MaxLength="100" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvContactName" Display="Dynamic" runat="server" ValidationGroup="vgContact" ControlToValidate="txtContactName"
                ErrorMessage="Το πεδίο 'Ονοματεπώνυμο Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (σταθερό):
            <my:TipIcon ID="tipContactPhone" runat="server" Text="<%$ Resources:LegalPersonInput, ContactPhone %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactPhone" runat="server" MaxLength="10" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvContactPhone" Display="Dynamic" runat="server" ValidationGroup="vgContact" ControlToValidate="txtContactPhone"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactPhone" runat="server" Display="Dynamic" ControlToValidate="txtContactPhone" ValidationExpression="^2[0-9]{9}$"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Υπευθύνου για το Εύδοξος' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (κινητό):
            <my:TipIcon ID="tipContactMobilePhone" runat="server" Text="<%$ Resources:LegalPersonInput, ContactMobilePhone %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactMobilePhone" runat="server" MaxLength="10" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvContactMobilePhone" runat="server" Display="Dynamic" ValidationGroup="vgContact" ControlToValidate="txtContactMobilePhone"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactMobilePhone" runat="server" Display="Dynamic" ControlToValidate="txtContactMobilePhone"
                ValidationExpression="^69[0-9]{8}$" ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Υπευθύνου για το Εύδοξος' πρέπει να ξεκινάει από 69 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            E-mail:
            <my:TipIcon ID="tipContactEmail" runat="server" Text="<%$ Resources:LegalPersonInput, ContactEmail %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactEmail" runat="server" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server" ControlToValidate="txtContactEmail" ValidationGroup="vgContact" Display="Dynamic"
                ErrorMessage="Το πεδίο 'E-mail Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactEmail" runat="server" Display="Dynamic" ControlToValidate="txtContactEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail του Υπευθύνου για το Εύδοξος δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <my:IdentityControl runat="server" ID="idContact" ValidationGroup="vgContact" />
</table>
<div id="divAlternateContact">
    <br />
    <table width="100%" class="dv">
        <tr>
            <th colspan="2" class="header">
                &raquo; Στοιχεία Αναπληρωτή Υπευθύνου για το Εύδοξος
            </th>
        </tr>
        <tr>
            <td class="notRequired" style="width: 30%">Ονοματεπώνυμο:
                <my:TipIcon ID="tipAlternateContactName" runat="server" Text="<%$ Resources:LegalPersonInput, AlternateContactName %>" />
            </td>
            <td>
                <asp:TextBox ID="txtAlternateContactName" runat="server" MaxLength="100" Width="90%" />
                <asp:CustomValidator runat="server" ClientValidationFunction="validateAlternativeGroup" ControlToValidate="txtAlternateContactName"
                    Display="Dynamic" OnServerValidate="cvAlternativeGroup_ServerValidate" ValidateEmptyText="true" ErrorMessage="Το πεδίο 'Ονοματεπώνυμο Αναπληρωτή Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό">
                    <img src="/_img/error.gif" title="Πρέπει να συμπληρώσετε όλα τα στοιχεία του Αναπληρωτή Υπευθύνου" />
                </asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td class="notRequired" style="width: 30%">Τηλέφωνο (σταθερό):
                <my:TipIcon ID="tipAlternateContactPhone" runat="server" Text="<%$ Resources:LegalPersonInput, AlternateContactPhone %>" />
            </td>
            <td>
                <asp:TextBox ID="txtAlternateContactPhone" runat="server" MaxLength="10" Width="20%" />
                <asp:CustomValidator runat="server" ClientValidationFunction="validateAlternativeGroup" ControlToValidate="txtAlternateContactPhone"
                    Display="Dynamic" OnServerValidate="cvAlternativeGroup_ServerValidate" ValidateEmptyText="true" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Αναπληρωτή Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό">
                    <img src="/_img/error.gif" title="Πρέπει να συμπληρώσετε όλα τα στοιχεία του Αναπληρωτή Υπευθύνου" />
                </asp:CustomValidator>
                <asp:RegularExpressionValidator ID="revAlternateContactPhone" runat="server" Display="Dynamic" ControlToValidate="txtAlternateContactPhone"
                    ValidationExpression="^2[0-9]{9}$" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Αναπληρωτή Υπευθύνου για το Εύδοξος' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="notRequired" style="width: 30%">Τηλέφωνο (κινητό):
                <my:TipIcon ID="tipAlternateContactMobilePhone" runat="server" Text="<%$ Resources:LegalPersonInput, AlternateContactMobilePhone %>" />
            </td>
            <td>
                <asp:TextBox ID="txtAlternateContactMobilePhone" runat="server" MaxLength="10" Width="20%" />
                <asp:CustomValidator runat="server" ClientValidationFunction="validateAlternativeGroup" ControlToValidate="txtAlternateContactMobilePhone"
                    Display="Dynamic" OnServerValidate="cvAlternativeGroup_ServerValidate" ValidateEmptyText="true" ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Αναπληρωτή Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό">
                    <img src="/_img/error.gif" title="Πρέπει να συμπληρώσετε όλα τα στοιχεία του Αναπληρωτή Υπευθύνου" />
                </asp:CustomValidator>
                <asp:RegularExpressionValidator ID="revAlternateContactMobilePhone" runat="server" Display="Dynamic" ControlToValidate="txtAlternateContactMobilePhone"
                    ValidationExpression="^69[0-9]{8}$" ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Αναπληρωτή Υπευθύνου για το Εύδοξος' πρέπει να ξεκινάει από 69 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="notRequired" style="width: 30%">E-mail:
                <my:TipIcon ID="tipAlternateContactEmail" runat="server" Text="<%$ Resources:LegalPersonInput, AlternateContactEmail %>" />
            </td>
            <td>
                <asp:TextBox ID="txtAlternateContactEmail" runat="server" Width="90%" />
                <asp:CustomValidator runat="server" ClientValidationFunction="validateAlternativeGroup" ControlToValidate="txtAlternateContactEmail"
                    Display="Dynamic" OnServerValidate="cvAlternativeGroup_ServerValidate" ValidateEmptyText="true" ErrorMessage="Το πεδίο 'E-mail Αναπληρωτή Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό">
                    <img src="/_img/error.gif" title="Πρέπει να συμπληρώσετε όλα τα στοιχεία του Αναπληρωτή Υπευθύνου" />
                </asp:CustomValidator>
                <asp:RegularExpressionValidator ID="revAlternateContactEmail" runat="server" Display="Dynamic" ControlToValidate="txtAlternateContactEmail"
                    ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                    ErrorMessage="Το E-mail του Αναπληρωτή Υπευθύνου για το Εύδοξος δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
            </td>
        </tr>
    </table>
</div>
