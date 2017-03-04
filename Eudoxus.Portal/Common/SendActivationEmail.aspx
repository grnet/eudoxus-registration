<%@ Page Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="SendActivationEmail.aspx.cs"
    Inherits="Eudoxus.Portal.Stores.SendActivationEmail" Title="Αποστολή e-mail ενεργοποίησης" %>

<%@ Register Assembly="Eudoxus.Portal" Namespace="Eudoxus.Portal.Controls" TagPrefix="lc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:MultiView ID="mvActivationEmail" runat="server" ActiveViewIndex="0">
        <asp:View ID="vInsertEmail" runat="server">
            <h1>
                Επαναποστολή e-mail ενεργοποίησης</h1>
            <div class="sub-description" style="font-weight: bold">
                Πληκτρολογήστε το e-mail που είχατε δηλώσει κατά τη δημιουργία λογαριασμού, για
                να λάβετε ξανά το e-mail ενεργοποίησης.
            </div>
            <br />
            <b>E-mail:</b>
            <asp:TextBox ID="txtEmail" runat="server">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvEmail" ErrorMessage="Το e-mail είναι υποχρεωτικό."
                ControlToValidate="txtEmail" Display="Dynamic" Font-Bold="true">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail"
                ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail που εισάγατε δεν είναι έγκυρο" Font-Bold="true" />
            <br />
            <lc:BotShield ID="bsCaptcha" runat="server" />
            <br />
            <asp:LinkButton ID="btnSend" runat="server" CssClass="icon-btn bg-accept" Text="Αποστολή E-mail Ενεργοποίησης" OnClick="btnSend_Click" />
            <br />
            <br />
            <asp:Label runat="server" ID="lblInfo" Font-Bold="true" ForeColor="Red"></asp:Label>
        </asp:View>
        <asp:View ID="vActivationEmailSent" runat="server">
            <asp:Label ID="lblCompletionMessage" runat="server" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
