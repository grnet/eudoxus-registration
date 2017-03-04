<%@ Page Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="ChangeDefaultPassword.aspx.cs"
    Inherits="Eudoxus.Portal.Common.ChangeDefaultPassword" Title="Αλλαγή Κωδικού Πρόσβασης" %>

<%@ Register Assembly="Eudoxus.Portal" Namespace="Eudoxus.Portal.Controls"
    TagPrefix="lc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <h1>
        Αλλαγή Κωδικού Πρόσβασης
    </h1>
    <div class="sub-description" style="font-weight: bold">
        Επειδή είναι η πρώτη φορά που συνδέεστε στην εφαρμογή μετά την Υπενθύμιση Κωδικού
        Πρόσβασης παρακαλούμε να αλλάξετε τον κωδικό που σας ήρθε με e-mail.
        <br />
        <br />
        Για τη δική σας ασφάλεια σας συνιστούμε να επιλέξετε έναν συνδυασμό από γράμματα,
        αριθμούς ή σύμβολα για να δημιουργήσετε έναν μοναδικό κωδικό πρόσβασης που δεν σχετίζεται
        με τα προσωπικά σας στοιχεία. Ή, επιλέξτε μια τυχαία λέξη ή φράση και εισαγάγετε
        λέξεις και αριθμούς στην αρχή, στη μέση και στο τέλος, για να είναι ακόμα πιο δύσκολο
        να τη μαντέψει κανείς (για παράδειγμα "m1awra1ap3tal0uda"). Η χρήση απλών λέξεων
        ή φράσεων όπως "password" ή "letmein", οι ακολουθίες πλήκτρων όπως "qwerty" ή "qazwsx"
        ή οι ακολουθίες διαδοχικών χαρακτήρων, όπως "abcd1234" κάνουν πιο εύκολη την αποκρυπτογράφηση
        του κωδικού σας. Επίσης, σε περίπτωση που συνδέεστε στο σύστημα από δημόσιο υπολογιστή,
        βεβαιωθείτε ότι πάντα πατάτε το κουμπί "Αποσύνδεση" πάνω δεξιά στην οθόνη κατά την
        έξοδό σας.
    </div>
    <br />
    <asp:MultiView ID="mvChangePassword" runat="server" ActiveViewIndex="0">
        <asp:View ID="vChangePassword" runat="server">
            <asp:ValidationSummary ID="vsStudentValidation" runat="server" HeaderText="Υπάρχει σφάλμα ή έλλειψη συμπλήρωσης ενός από τα πεδία της φόρμας. Παρακαλώ κάντε τις απαραίτητες διορθώσεις." />
            <table width="100%" class="dv">
                <tr>
                    <th colspan="2" class="header">
                        Αλλαγή Κωδικού Πρόσβασης
                    </th>
                </tr>
                <tr>
                    <th style="width: 30%">
                        Παλιός κωδικός πρόσβασης
                    </th>
                    <td align="left">
                        <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" Width="30%" onkeyup="StudentCard.Lib.NoGreekCharacters(this,false)" />
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
                        <asp:TextBox ID="txtPassword1" runat="server" TextMode="Password" Width="30%" onkeyup="StudentCard.Lib.NoGreekCharacters(this,false)" />
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
                        <asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" Width="30%" onkeyup="StudentCard.Lib.NoGreekCharacters(this,false)" />
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
            <br />
            <lc:BotShield ID="bsCaptcha" runat="server" />
            <br />
            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="icon-btn bg-accept" Text="Αλλαγή Κωδικού Πρόσβασης"
                OnClick="btnSubmit_Click" />
            <br />
            <br />
            <asp:Label runat="server" ID="lblInfo" Font-Bold="true" ForeColor="Red"></asp:Label>
        </asp:View>
        <asp:View ID="vPasswordChanged" runat="server">
            <span style="font-weight: bold; color: Red">Η αλλαγή του κωδικού πρόσβασης πραγματοποιήθηκε
                επιτυχώς. Για να συνεχίσετε με τη χρήση της εφαρμογής πατήστε</span> <a runat="server"
                    style="font-weight:bold; color: Blue" href="../Default.aspx">εδώ</a>
        </asp:View>
    </asp:MultiView>
</asp:Content>
