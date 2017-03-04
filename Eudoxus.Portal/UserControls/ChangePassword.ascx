<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs"
    Inherits="Eudoxus.Portal.UserControls.Generic.ChangePassword" %>
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            Αλλαγή Κωδικού Πρόσβασης
        </th>
    </tr>
    <tr id="trOldPassword" runat="server" visible="true">
        <th style="width: 30%">
            Παλιός κωδικός πρόσβασης
        </th>
        <td align="left">
            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" Width="30%" onkeyup="Imis.Lib.NoGreekCharacters(this,false)" />
            <imis:CapsWarning ID="CapsWarning2" runat="server" TextBoxControlId="txtOldPassword"
                CssClass="capsLockWarning" Text="Προσοχή: το πλήκτρο Caps Lock είναι πατημένο"></imis:CapsWarning>
            <asp:RequiredFieldValidator ID="rfvOldPassword" Display="Dynamic" runat="server"
                ControlToValidate="txtOldPassword" ErrorMessage="Το πεδίο 'Παλιός κωδικός πρόσβασης' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Νέος κωδικός πρόσβασης
        </th>
        <td align="left">
            <asp:TextBox ID="txtPassword1" runat="server" TextMode="Password" Width="30%" onkeyup="Imis.Lib.NoGreekCharacters(this,false)" />
            <imis:CapsWarning ID="CapsWarning1" runat="server" TextBoxControlId="txtPassword1"
                CssClass="capsLockWarning" Text="Προσοχή: το πλήκτρο Caps Lock είναι πατημένο"></imis:CapsWarning>
            <asp:RequiredFieldValidator ID="rfvPassword1" Display="Dynamic" runat="server" ControlToValidate="txtPassword1"
                ErrorMessage="Το πεδίο 'Νέος κωδικός πρόσβασης' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword1"
                Display="Dynamic" ValidationExpression="^(.){7,}$" ErrorMessage="Ο κωδικός πρόσβασης πρέπει να αποτελείται από τουλάχιστον 7 χαρακτήρες"><img src="/_img/error.gif" title="Ο κωδικός πρόσβασης πρέπει να αποτελείται από τουλάχιστον 7 χαρακτήρες" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Επιβεβαίωση
        </th>
        <td align="left">
            <asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" Width="30%" onkeyup="Imis.Lib.NoGreekCharacters(this,false)" />
            <imis:CapsWarning ID="CapsWarning3" runat="server" TextBoxControlId="txtPassword2"
                CssClass="capsLockWarning" Text="Προσοχή: το πλήκτρο Caps Lock είναι πατημένο"></imis:CapsWarning>
            <asp:RequiredFieldValidator ID="rfvPassword2" Display="Dynamic" runat="server" ControlToValidate="txtPassword2"
                ErrorMessage="Το πεδίο 'Επιβεβαίωση' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvPassword2" ControlToCompare="txtPassword1" ControlToValidate="txtPassword2"
                runat="server" Display="Dynamic" Operator="Equal" Type="String" ValueToCompare="Text"
                ErrorMessage="Οι κωδικοί δεν ταιριάζουν"><img src="/_img/error.gif" title="Οι κωδικοί δεν ταιριάζουν" /></asp:CompareValidator>
        </td>
    </tr>
</table>
