<%@ Page Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs"
    Inherits="Eudoxus.Portal.Common.ForgotPassword" Title="Υπενθύμιση Κωδικού" %>

<%@ Register Assembly="Eudoxus.Portal" Namespace="Eudoxus.Portal.Controls" TagPrefix="lc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <h1>
        Υπενθύμιση Κωδικού Πρόσβασης
    </h1>
    <div class="sub-description" style="font-weight: bold">
        Σε περίπτωση που ξεχάσατε τον κωδικό πρόσβασης, πληκτρολογήστε το e-mail που είχατε δηλώσει κατά τη δημιουργία του λογαριασμού για να
        σταλεί ένας καινούργιος κωδικός. Τον κωδικό αυτό μπορείτε να τον αλλάξετε αφότου συνδεθείτε στο σύστημα.
    </div><br />
    <b>E-mail:</b>
    <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvEmail" Display="Dynamic" runat="server" ControlToValidate="txtEmail"
        ErrorMessage="Το πεδίο 'E-mail' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>    
    <br />
    <lc:BotShield ID="bsCaptcha" runat="server" />
    <br />
    <asp:LinkButton ID="btnSend" runat="server" CssClass="icon-btn bg-accept" Text="Αποστολή Κωδικού Πρόσβασης" OnClick="btnSend_Click" />
    <br />
    <br />
    <asp:Label runat="server" ID="lblInfo" Font-Bold="true" ForeColor="Red"></asp:Label>
</asp:Content>
