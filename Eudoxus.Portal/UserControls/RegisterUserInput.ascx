<%@ Control Language="C#" AutoEventWireup="true" Inherits="Eudoxus.Portal.UserControls.RegisterUserInput"
    CodeBehind="RegisterUserInput.ascx.cs" %>
<%@ Register Src="~/UserControls/TipIcon.ascx" TagPrefix="my" TagName="TipIcon" %>
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Λογαριασμού Χρήστη
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Όνομα Χρήστη:
            <my:TipIcon ID="tipUsername" runat="server" Text="<%$ Resources:RegistrationInput, Username %>" />
        </th>
        <td>
            <asp:TextBox ID="txtUsername" runat="server" Width="30%" onkeyup="Imis.Lib.NoGreekCharacters(this,false)" />
            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername"
                Display="Dynamic" ErrorMessage="Το πεδίο 'Όνομα Χρήστη' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revUsername" runat="server" ControlToValidate="txtUsername"
                Display="Dynamic" ValidationExpression="^([A-Za-z0-9_\-\.~!@#$%^&*()+=]){5,}$" ErrorMessage="Το Όνομα Χρήστη πρέπει να αποτελείται από τουλάχιστον 5 χαρακτήρες"><img src="/_img/error.gif" title="Κάποιος από τους χαρακτήρες που εισάγατε δεν είναι έγκυρος" /></asp:RegularExpressionValidator>
            <asp:CustomValidator ID="cvUsername" runat="server" ErrorMessage="Το Όνομα Χρήστη χρησιμοποιείται"
                Display="Dynamic" ControlToValidate="txtUsername" OnServerValidate="cvUsername_ServerValidate"><img src="/_img/error.gif" title="Το Όνομα Χρήστη χρησιμοποιείται" /></asp:CustomValidator>
        </td>
    </tr>
     <tr id="trPasswordInfo" runat="server">
        <td colspan="2">
            <div class="sub-description">
                Για τη δική σας ασφάλεια συνιστούμε να επιλέξετε έναν συνδυασμό από γράμματα,
                αριθμούς ή σύμβολα για να δημιουργήσετε έναν μοναδικό κωδικό πρόσβασης που δεν σχετίζεται
                με τα προσωπικά σας στοιχεία. Ή, επιλέξτε μια τυχαία λέξη ή φράση και εισαγάγετε
                λέξεις και αριθμούς στην αρχή, στη μέση και στο τέλος, για να είναι ακόμα πιο δύσκολο
                να τη μαντέψει κανείς (για παράδειγμα "m1awra1ap3tal0uda"). Η χρήση απλών λέξεων
                ή φράσεων όπως "password" ή "letmein", οι ακολουθίες πλήκτρων όπως "qwerty" ή "qazwsx"
                ή οι ακολουθίες διαδοχικών χαρακτήρων, όπως "abcd1234" κάνουν πιο εύκολη την αποκρυπτογράφηση
                του κωδικού σας.
            </div>
        </td>
    </tr>
    <tr id="trPassword1" runat="server">
        <th style="width: 30%">
            Κωδικός Πρόσβασης:
            <my:TipIcon ID="tipPassword1" runat="server" Text="<%$ Resources:RegistrationInput, Password %>" />
        </th>
        <td>
            <asp:TextBox ID="txtPassword1" runat="server" TextMode="Password" Width="30%" onkeyup="Imis.Lib.NoGreekCharacters(this,false)" />
            <imis:CapsWarning ID="CapsWarning2" runat="server" TextBoxControlId="txtPassword1"
                CssClass="capsLockWarning" Text="Προσοχή: το πλήκτρο Caps Lock είναι πατημένο"></imis:CapsWarning>
            <asp:RequiredFieldValidator ID="rfvPassword1" runat="server" ControlToValidate="txtPassword1"
                Display="Dynamic" ErrorMessage="Το πεδίο 'Κωδικός Πρόσβασης' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPassword1" runat="server" ControlToValidate="txtPassword1"
                Display="Dynamic" ValidationExpression="^(.){7,}$" ErrorMessage="Ο Κωδικός Πρόσβασης πρέπει να αποτελείται από τουλάχιστον 7 χαρακτήρες"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="trPassword2" runat="server">
        <th style="width: 30%">
            Επιβεβαίωση Κωδικού Πρόσβασης:
            <my:TipIcon ID="tipPassword2" runat="server" Text="<%$ Resources:RegistrationInput, Password %>" />
        </th>
        <td>
            <asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" Width="30%" onkeyup="Imis.Lib.NoGreekCharacters(this,false)" />
            <imis:CapsWarning ID="CapsWarning1" runat="server" TextBoxControlId="txtPassword2"
                CssClass="capsLockWarning" Text="Προσοχή: το πλήκτρο Caps Lock είναι πατημένο"></imis:CapsWarning>
            <asp:RequiredFieldValidator ID="rfvPassword2" runat="server" ControlToValidate="txtPassword2"
                Display="Dynamic" ErrorMessage="Το πεδίο 'Επιβεβαίωση Κωδικού Πρόσβασης' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvPassword2" ControlToCompare="txtPassword1" ControlToValidate="txtPassword2"
                runat="server" Display="Dynamic" ErrorMessage="Ο Κωδικός Πρόσβασης και η Επιβεβαίωση Κωδικού Πρόσβασης πρέπει να ταιριάζουν"
                Operator="Equal" Type="String" ValueToCompare="Text"><img src="/_img/error.gif" title="Ο Κωδικός Πρόσβασης και η Επιβεβαίωση Κωδικού Πρόσβασης πρέπει να ταιριάζουν" /></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trEmailDescription" runat="server">
        <td colspan="2">
            <div class="sub-description">
                Στο e-mail αυτό, θα σας σταλεί το μήνυμα για την ενεργοποίηση του λογαριασμού σας. Βεβαιωθείτε ότι το πληκτρολογήσατε σωστά.
            </div>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            E-mail:
            <my:TipIcon ID="tipEmail" runat="server" Text="<%$ Resources:RegistrationInput, Email %>" />
        </th>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                Display="Dynamic" ErrorMessage="Το πεδίο 'E-mail' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" Display="Dynamic" ControlToValidate="txtEmail"
                runat="server" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
            <asp:CustomValidator ID="cvEmail" ControlToValidate="txtEmail" runat="server" Display="Dynamic"
                ErrorMessage="Το E-mail χρησιμοποιείται ήδη από κάποιο άλλο χρήστη του Πληροφοριακού Συστήματος. Παρακαλούμε επιλέξτε κάποιο άλλο." OnServerValidate="cvEmail_ServerValidate"><img src="/_img/error.gif" title="Το E-mail χρησιμοποιείται" /></asp:CustomValidator>
        </td>
    </tr>
</table>
