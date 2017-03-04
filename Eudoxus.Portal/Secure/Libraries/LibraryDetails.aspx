<%@ Page Language="C#" MasterPageFile="~/Secure/Libraries/Libraries.Master" AutoEventWireup="true" CodeBehind="LibraryDetails.aspx.cs"
    Inherits="Eudoxus.Portal.Secure.Libraries.LibraryDetails" Title="Διαχείριση Λογαριασμού" %>

<%@ Register Src="~/UserControls/FlashMessage.ascx" TagName="FlashMessage" TagPrefix="my" %>
<%@ Register Src="~/UserControls/LibraryInput.ascx" TagName="LibraryInput" TagPrefix="my" %>
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
        <ajaxToolkit:TabPanel ID="tabLibrary" runat="server" HeaderText="Στοιχεία Βιβλιοθήκης">
            <ContentTemplate>
                <asp:ValidationSummary ID="vdLibrary" runat="server" ValidationGroup="vgLibrary" />
                <br />
                <my:LibraryInput ID="libraryInput" runat="server" ValidationGroup="vgLibrary" />
                <br />
                <asp:LinkButton ID="btnUpdateLibrary" runat="server" Text="Ενημέρωση Στοιχείων Βιβλιοθήκης" CssClass="icon-btn bg-accept" ValidationGroup="vgLibrary"
                    OnClick="btnUpdateLibrary_Click" OnClientClick="if (validate('vgLibrary')) { return confirm('Ο λογαριασμός σας δεν έχει ακόμα πιστοποιηθεί και για αυτό το λόγο μπορείτε να επεξεργαστείτε τα στοιχεία του.\n\nΕάν έχετε ήδη αποστείλει Fax στο Γραφείο Αρωγής, τότε οποιαδήποτε αλλαγή κάνετε θα έχει ως αποτέλεσμα να μην πιστοποιηθείτε, αφού τα στοιχεία του Fax θα διαφέρουν από αυτά του λογαριασμού σας.\n\nΓια το λόγο αυτό, θα πρέπει να στείλετε εκ νέου το Fax με τα νέα στοιχεία στο Γραφείο Αρωγής.\n\nΘέλετε όντως να παραγματοποιήσετε τις αλλαγές;'); } else { return false; }" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="tabUser" runat="server" HeaderText="Στοιχεία Χρήστη">
            <ContentTemplate>
                <my:RegisterUserInput ID="registerUserInput" runat="server" ValidationGroup="vgUser" />
                <br />
                <asp:LinkButton ID="btnUpdateProfile" runat="server" Text="Ενημέρωση Στοιχείων Χρήστη" CssClass="icon-btn bg-accept" ValidationGroup="vgUser"
                    OnClick="btnUpdateProfile_Click" OnClientClick="if (validate('vgUser')) { return confirm('Ο λογαριασμός σας δεν έχει ακόμα πιστοποιηθεί και για αυτό το λόγο μπορείτε να επεξεργαστείτε τα στοιχεία του.\n\nΕάν έχετε ήδη αποστείλει Fax στο Γραφείο Αρωγής, τότε οποιαδήποτε αλλαγή κάνετε θα έχει ως αποτέλεσμα να μην πιστοποιηθείτε, αφού τα στοιχεία του Fax θα διαφέρουν από αυτά του λογαριασμού σας.\n\nΓια το λόγο αυτό, θα πρέπει να στείλετε εκ νέου το Fax με τα νέα στοιχεία στο Γραφείο Αρωγής.\n\nΘέλετε όντως να παραγματοποιήσετε τις αλλαγές;'); } else { return false; }" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
