<%@ Page Title="Κεντρική Σελίδα" Language="C#" MasterPageFile="~/Secure/MinistryPayments/MinistryPayments.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Eudoxus.Portal.Secure.MinistryPayments.Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:PlaceHolder ID="phNotVerified" runat="server">
        <div class="reminder">
            Ο λογαριασμός σας δεν έχει ακόμα πιστοποιηθεί.
        </div>
        <p>
            Για να πιστοποιηθεί ο λογαριασμός σας πρέπει να επικοινωνήσετε με το Γραφείο Αρωγής Χρηστών της δράσης στο τηλέφωνο 215 215 7850. Ώρες λειτουργίας Δευτέρα με Παρασκευή 09:00 πμ - 17:00 μμ
        </p>
        <p>
            Μέχρι να ολοκληρωθεί η πιστοποίηση του λογαριασμού σας έχετε τη δυνατότητα να επεξεργαστείτε
            τα στοιχεία που δηλώσατε κατά την εγγραφή σας στο σύστημα, μέσα από την καρτέλα
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Secure/MinistryPayments/MinistryPaymentsUserDetails.aspx"
                Style="font-weight: bold; color: Blue;">Στοιχεία Χρήστη Υπουργείου - Πληρωμών</asp:HyperLink>
        </p>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phVerified" runat="server">
        <div class="reminder">
            Ο λογαριασμός σας έχει πιστοποιηθεί.
        </div>
        <p>
            Αν θέλετε να επεξεργαστείτε τα στοιχεία που δηλώσατε, θα πρέπει:
        </p>
        <ul>
            <li>Να επικοινωνήσετε με το Γραφείο Αρωγής της δράσης για να σας επιτραπεί να κάνετε
                τις τροποποιήσεις.</li>
            <li>Να επεξεργαστείτε τα στοιχεία που δηλώσατε κατά την εγγραφή σας στο σύστημα, μέσα
                από την καρτέλα
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Secure/MinistryPayments/MinistryPaymentsUserDetails.aspx"
                Style="font-weight: bold; color: Blue;">Στοιχεία Χρήστη Υπουργείου - Πληρωμών</asp:HyperLink>
        </ul>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phCannotBeVerified" runat="server">
        <div class="reminder">
            Ο λογαριασμός σας δεν μπορεί να πιστοποιηθεί. Για περισσότερες πληροφορίες, επικοινωνήστε
            με το Γραφείο Αρωγής Χρηστών της δράσης.
        </div>
    </asp:PlaceHolder>
</asp:Content>
