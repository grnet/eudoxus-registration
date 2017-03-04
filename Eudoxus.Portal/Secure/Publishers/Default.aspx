<%@ Page Language="C#" MasterPageFile="~/Secure/Publishers/Publishers.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Eudoxus.Portal.Secure.Publishers.Default"
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
            <asp:HyperLink ID="lnkPublisherDetails" runat="server" NavigateUrl="~/Secure/Publishers/PublisherDetails.aspx"
                Style="font-weight: bold; color: Blue;">Στοιχεία Εκδότη</asp:HyperLink>
        </p>
        <p>
            Σε περίπτωση που τροποποιήσετε τα στοιχεία σας πρέπει να εκτυπώσετε εκ νέου τη Βεβαίωση
            Συμμετοχής και να ακολουθήσετε την ως άνω διαδικασία.
        </p>
        <asp:HyperLink ID="lnkPublisherVerification" runat="server" CssClass="icon-btn bg-print"
            Text="Εκτύπωση Βεβαίωσης Συμμετοχής" Target="_blank" NavigateUrl="~/Secure/Publishers/PrintPublisherCertification.aspx" />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phPublisherVerified" runat="server">
        <div class="reminder">
            Ο λογαριασμός σας έχει πιστοποιηθεί.
        </div>
        <p>
            Οι ενέργειες τις οποίες μπορείτε να εκτελέσετε είναι οι εξής:
        </p>
        <ul>
            <li>Να καταχωρίσετε τα Συγγράμματά σας μέσα από το <a href="https://service.eudoxus.gr"
                target="_blank" class="hyperlink">service.eudoxus.gr</a></li>
            <li>Να επεξεργαστείτε όσα στοιχεία δεν είναι απενεργοποιημένα για τροποποίηση, μέσα
                από την καρτέλα
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Secure//Publishers/PublisherDetails.aspx"
                    Style="font-weight: bold; color: Blue;">Στοιχεία Εκδότη</asp:HyperLink></li>
            <li>Για όσα στοιχεία δεν σας επιτρέπεται να τα επεξεργαστείτε (π.χ. Α.Φ.Μ.) θα πρέπει
                να επικοινωνήσετε με το Γραφείο Αρωγής Χρηστών για να σας εξηγήσει τη διαδικασία
                τροποποίησής τους.</li>
        </ul>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phEbookPublisherVerified" runat="server">
        <div class="reminder">
            Ο λογαριασμός σας έχει πιστοποιηθεί.
        </div>
        <p>
            Οι ενέργειες τις οποίες μπορείτε να εκτελέσετε είναι οι εξής:
        </p>
        <ul>
            <li>Να καταχωρίσετε τα Δωρεάν Ηλεκτρονικά Βοηθήματα και Σημειώσεις σας μέσα από το <a href="https://service.eudoxus.gr"
                target="_blank" class="hyperlink">service.eudoxus.gr</a></li>
            <li>Να επεξεργαστείτε όσα στοιχεία δεν είναι απενεργοποιημένα για τροποποίηση, μέσα
                από την καρτέλα
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Secure//Publishers/PublisherDetails.aspx"
                    Style="font-weight: bold; color: Blue;">Στοιχεία Εκδότη</asp:HyperLink></li>
            <li>Για όσα στοιχεία δεν σας επιτρέπεται να τα επεξεργαστείτε (π.χ. Αριθμός Ταυτότητας) θα πρέπει
                να επικοινωνήσετε με το Γραφείο Αρωγής Χρηστών για να σας εξηγήσει τη διαδικασία
                τροποποίησής τους.</li>
        </ul>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phCannotBeVerified" runat="server">
        <div class="reminder">
            Ο λογαριασμός σας δεν μπορεί να πιστοποιηθεί, γιατί υπάρχει ήδη πιστοποιημένος χρήστης
            με το Α.Φ.Μ. που έχετε δηλώσει.<br />
            Για περισσότερες πληροφορίες, επικοινωνήστε με το Γραφείο Αρωγής Χρηστών της δράσης.
        </div>
    </asp:PlaceHolder>
</asp:Content>
