<%@ Page Language="C#" MasterPageFile="~/Secure/Libraries/Libraries.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Eudoxus.Portal.Secure.Libraries.Default"
    Title="Κεντρική Σελίδα" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:PlaceHolder ID="phNotVerified" runat="server">
        <div class="reminder">
            Ο λογαριασμός σας δεν έχει ακόμα πιστοποιηθεί.
        </div>
        <p>
            Για να πιστοποιηθεί ο λογαριασμός σας πρέπει να εκτυπώσετε τη Βεβαίωση Συμμετοχής,
            να τη συμπληρώσετε και να τη στείλετε με φαξ στο Γραφείο Αρωγής Χρηστών (αριθμός
            φαξ: 215 215 7858).
        </p>
        <p>
            Μέχρι να ολοκληρωθεί η πιστοποίηση του λογαριασμού σας έχετε τη δυνατότητα να επεξεργαστείτε
            τα στοιχεία που δηλώσατε κατά την εγγραφή σας στο σύστημα, μέσα από την καρτέλα
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Secure/Libraries/LibraryDetails.aspx"
                Style="font-weight: bold; color: Blue;">Στοιχεία Βιβλιοθήκης</asp:HyperLink>
        </p>
        <p>
            Σε περίπτωση που τροποποιήσετε τα στοιχεία σας πρέπει να εκτυπώσετε εκ νέου τη Βεβαίωση
            Συμμετοχής και να ακολουθήσετε την ως άνω διαδικασία.
        </p>
        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="icon-btn bg-print" Text="Εκτύπωση Βεβαίωσης Συμμετοχής"
            Target="_blank" NavigateUrl="~/Secure/Libraries/PrintLibraryCertification.aspx" />
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
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Secure/Libraries/LibraryDetails.aspx"
                    Style="font-weight: bold; color: Blue;">Στοιχεία Βιβλιοθήκης</asp:HyperLink></li>
        </ul>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phCannotBeVerified" runat="server">
        <div class="reminder">
            Ο λογαριασμός σας δεν μπορεί να πιστοποιηθεί. Για περισσότερες πληροφορίες, επικοινωνήστε
            με το Γραφείο Αρωγής Χρηστών της δράσης.
        </div>
    </asp:PlaceHolder>
</asp:Content>
