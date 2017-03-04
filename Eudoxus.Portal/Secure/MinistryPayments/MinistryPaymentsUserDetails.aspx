<%@ Page Language="C#" MasterPageFile="~/Secure/MinistryPayments/MinistryPayments.Master" AutoEventWireup="true" CodeBehind="MinistryPaymentsUserDetails.aspx.cs"
    Inherits="Eudoxus.Portal.Secure.MinistryPayments.MinistryPaymentsUserDetails" Title="Διαχείριση Λογαριασμού" %>

<%@ Register Src="~/UserControls/FlashMessage.ascx" TagName="FlashMessage" TagPrefix="my" %>
<%@ Register Src="~/UserControls/MinistryPaymentsUserInput.ascx" TagName="MinistryPaymentsUserInput" TagPrefix="my" %>
<%@ Register Src="~/UserControls/RegisterUserInput.ascx" TagName="RegisterUserInput" TagPrefix="my" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

    <script type="text/javascript">
        function validate(group) {
            return Page_ClientValidate(group);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <my:FlashMessage ID="fm" runat="server" CssClass="fade" />
    <asp:PlaceHolder ID="phCanBeVerified" runat="server">
        <p style="font-weight: bold">
            Σε κάθε κατηγορία στοιχείων πρέπει να αποθηκεύετε τις αλλαγές που υλοποιείτε, με το κουμπί «Ενημέρωση Στοιχείων» που εμφανίζεται στο
            κάτω τμήμα της αντίστοιχης κατηγορίας.
        </p>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phCannotBeVerified" runat="server">
        <div class="reminder">
            Δεν μπορείτε να επεξεργαστείτε τα στοιχεία σας, γιατί ο λογαριασμός σας είναι απενεργοποιημένος.<br />
            Για περισσότερες πληροφορίες, επικοινωνήστε με το Γραφείο Αρωγής Χρηστών της δράσης.
        </div>
    </asp:PlaceHolder>
    <ajaxToolkit:TabContainer ID="tabs" runat="server">
        <ajaxToolkit:TabPanel ID="tabMinistryPaymentsUser" runat="server" HeaderText="Στοιχεία Μέλους Επιτροπής">
            <ContentTemplate>
                <asp:ValidationSummary ID="vdMinistryPaymentsUser" runat="server" ValidationGroup="vgMinistryPaymentsUser" />
                <br />
                <my:MinistryPaymentsUserInput ID="ucMinistryPaymentsUserInput" runat="server" ValidationGroup="vgMinistryPaymentsUser" />
                <br />
                <asp:LinkButton ID="btnUpdateMinistryPaymentsUser" runat="server" Text="Ενημέρωση Στοιχείων Χρήστη Υπουργείου - Πληρωμών" CssClass="icon-btn bg-accept" ValidationGroup="vgMinistryPaymentsUser"
                    OnClick="btnUpdateMinistryPaymentsUser_Click" OnClientClick="if (validate('vgMinistryPaymentsUser')) { return confirm('Ο λογαριασμός σας δεν έχει ακόμα πιστοποιηθεί και για αυτό το λόγο μπορείτε να επεξεργαστείτε τα στοιχεία του.\n\nΘέλετε όντως να παραγματοποιήσετε τις αλλαγές;'); } else { return false; }" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="tabUser" runat="server" HeaderText="Στοιχεία Χρήστη">
            <ContentTemplate>
                <my:RegisterUserInput ID="registerUserInput" runat="server" ValidationGroup="vgUser" />
                <br />
                <asp:LinkButton ID="btnUpdateProfile" runat="server" Text="Ενημέρωση Στοιχείων Χρήστη" CssClass="icon-btn bg-accept" ValidationGroup="vgUser"
                    OnClick="btnUpdateProfile_Click" OnClientClick="if (validate('vgUser')) { return confirm('Ο λογαριασμός σας δεν έχει ακόμα πιστοποιηθεί και για αυτό το λόγο μπορείτε να επεξεργαστείτε τα στοιχεία του.\n\nΘέλετε όντως να παραγματοποιήσετε τις αλλαγές;'); } else { return false; }" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
