<%@ Page Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="EbookPublisherRegistration.aspx.cs"
    Inherits="Eudoxus.Portal.Common.EbookPublisherRegistration" Title="Δημιουργία Λογαριασμού" %>

<%@ Register Src="~/UserControls/RegisterUserInput.ascx" TagName="RegisterUserInput"
    TagPrefix="my" %>
<%@ Register Src="~/UserControls/PublisherInput.ascx" TagName="PublisherInput" TagPrefix="my" %>
<%@ Register Assembly="Eudoxus.Portal" Namespace="Eudoxus.Portal.Controls" TagPrefix="lc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/_css/jquery-ui.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <style type="text/css">
        .btn-mask
        {
            color: Gray;
            filter: alpha(opacity=50);
            -moz-opacity: 0.5;
            opacity: 0.5;
        }
    </style>
    <script type="text/javascript">
        function validate() {
            return Page_ClientValidate();
        }
    </script>
    <h1>
        <asp:Label ID="lblTitle" runat="server" Text="Δημιουργία Χρήστη" Font-Size="14pt" />
    </h1>
    <asp:MultiView ID="mvRegistration" runat="server" ActiveViewIndex="0">
        <asp:View ID="vAcceptTerms" runat="server">
            <script type="text/javascript">
                var terms, termsRead, btnAcceptTerms, acceptTermsRef;
                $(function () {
                    btnAcceptTerms = $("#<%= btnAcceptTerms.ClientID %>");
                    acceptTermsRef = btnAcceptTerms.attr('href');
                    terms = $("#divTerms");

                    if (terms != null) {
                        terms.scroll(function () {
                            acceptTerms();
                        });

                        acceptTerms();
                    }
                });

                function acceptTerms() {
                    if (!termsRead) {
                        termsRead = terms[0].scrollHeight <= (terms[0].offsetHeight + terms[0].scrollTop);
                    }

                    btnAcceptTerms.unbind('click');
                    if (termsRead) {
                        btnAcceptTerms.removeClass('btn-mask');
                        btnAcceptTerms.attr('href', acceptTermsRef);
                    }
                    else {
                        btnAcceptTerms.addClass('btn-mask');
                        btnAcceptTerms.attr('href', '#');
                        btnAcceptTerms.click(function () { return false; });
                    }
                }
            </script>
            <table width="100%" class="dv">
                <tr>
                    <th colspan="2" class="popupHeader">
                        Δηλώνω υπεύθυνα ότι:
                    </th>
                </tr>
                <tr>
                    <td align="left">
                        <span style="font-weight: bold; font-size: 12px">Έχω διαβάσει και αποδέχομαι τους <u>
                            Όρους και Προϋποθέσεις</u> του προγράμματος «Εύδοξος» (κάντε scroll για να τους
                            διαβάσετε)</span>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <div id="divTerms" style="width: 925px; height: 400px; overflow: auto; border: 1px solid #AAAAAA;">
                <div style="font-weight: bold; text-align: center">
                    Όροι και Προϋποθέσεις Συµµετοχής στο Πρόγραµµα Εύδοξος</div>
                <ol>
                    <li>Κάθε συμμετέχων-εκδότης οφείλει να διαβάσει προσεκτικά τους όρους αυτούς πριν από
                        την συμμετοχή του στο πρόγραμμα «Εύδοξος – Ηλεκτρονική Υπηρεσία Ολοκληρωμένης Διαχείρισης
                        Συγγραμμάτων». Η συμμετοχή στο πρόγραμμα συνεπάγεται την αποδοχή των παρόντων όρων
                        συμμετοχής.</li>
                    <li>Ο συμμετέχων-εκδότης στο πρόγραμμα «Εύδοξος – Ηλεκτρονική Υπηρεσία Ολοκληρωμένης
                        Διαχείρισης Συγγραμμάτων» δηλώνει ότι τα συγγράμματα που καταχωρίζει στο Κεντρικό
                        Πληροφοριακό Σύστημα (ΚΠΣ) του Έργου υποβάλλονται νομίμως και σύμφωνα με τους όρους
                        και τις προϋποθέσεις του έργου.</li>
                    <li>Ο συμμετέχων-εκδότης δηλώνει και εγγυάται ότι διατηρεί τα δικαιώματα διανομής για
                        τα συγγράμματα τα οποία καταχωρίζει στο ΚΠΣ της Δράσης και συνεπώς δεν παραβιάζει
                        πνευματικά δικαιώματα, δικαιώματα ευρεσιτεχνίας, εμπορικά σήματα ή μυστικά ή άλλα
                        δικαιώματα οποιουδήποτε φυσικού ή νομικού προσώπου στην Ελλάδα ή το εξωτερικό, ότι
                        είναι δικαιούχος ή έχει αποκτήσει νομίμως τα δικαιώματα διανοητικής ιδιοκτησίας
                        που είναι απαραίτητα προκειμένου να συμμετέχει στο πρόγραμμα «Εύδοξος – Ηλεκτρονική
                        Υπηρεσία Ολοκληρωμένης Διαχείρισης Συγγραμμάτων» σύμφωνα με τους όρους και τις προϋποθέσεις
                        του έργου.</li>
                    <li>Η Βεβαίωση Συμμετοχής που υπογράφει ο νόμιμος εκπρόσωπος της εταιρίας (σε περίπτωση
                        που πρόκειται για νομικό πρόσωπο) ή ο ίδιος ο εκδότης (σε περίπτωση που πρόκειται
                        για φυσικό πρόσωπο) επέχει θέση Υπεύθυνης Δήλωσης.</li>
                    <li>Ο συμμετέχων-εκδότης υποχρεούται να αναδέχεται οποιαδήποτε αξίωση προβληθεί κατά
                        της ΕΔΕΤ Α.Ε. και να απαλλάξει την ΕΔΕΤ Α.Ε. και τους διευθυντές, υπαλλήλους, εργαζομένους
                        και αντιπροσώπους της από κάθε ευθύνη για αποζημίωση, έξοδα (συμπεριλαμβανομένων
                        και των ευλόγων δικαστικών εξόδων), δικαστικές αποφάσεις και άλλες δαπάνες ή απαιτήσεις
                        τρίτων που τυχόν προέλθουν από παραβιάσεις: δικαιωμάτων τρίτων, όπως ενδεικτικά
                        των δικαιωμάτων πνευματικής ή βιομηχανικής ιδιοκτησίας, σύμφωνα με τα όσα αναφέρονται
                        στην προηγούμενη παράγραφο, δημοσιότητας, εμπιστευτικότητας.</li>
                    <li>Ο συμμετέχων-εκδότης δηλώνει ρητά και υπεύθυνα ότι κάθε στοιχείο που δηλώνει είναι
                        ακριβές.</li>
                    <li>Ο συμμετέχων-εκδότης δηλώνει ρητά, υπεύθυνα και ανεπιφύλακτα ότι αποδέχεται όλους
                        τους ως άνω όρους.</li>
                    <li>Η διαχείριση και προστασία των προσωπικών δεδομένων του συμμετέχοντα-εκδότη υπόκεινται
                        στους παρόντες όρους καθώς και στις σχετικές διατάξεις του ελληνικού και ευρωπαϊκού
                        δικαίου για την εν γένει προστασία του ατόμου από την επεξεργασία δεδομένων προσωπικού
                        χαρακτήρα και το απόρρητο των επικοινωνιών, όπως ερμηνεύονται με τις αποφάσεις των
                        αρμόδιων Ανεξάρτητων Διοικητικών Αρχών. Σε κάθε περίπτωση η ΕΔΕΤ Α.Ε. διατηρεί το
                        δικαίωμα αλλαγής των όρων προστασίας των προσωπικών δεδομένων κατόπιν ενημέρωσης
                        των συμμετεχόντων εκδοτών μέσω της παρούσας ιστοσελίδας.
                        <br /><br />
                        Η συμμετοχή στο πρόγραμμα συνεπάγεται τη ρητή και ανεπιφύλακτη συναίνεση του συμμετέχοντα-εκδότη
                        για καταχώριση των προσωπικών του στοιχείων σε αρχείο που θα τηρείται με αυτά και
                        την επεξεργασία τους από την ΕΔΕΤ Α.Ε. για την υλοποίηση του παρόντος έργου σύμφωνα
                        με τις διατάξεις του Ν.2472/1997 όπως ισχύει.
                        <br /><br />
                        Η ΕΔΕΤ Α.Ε. διαφυλάσσει τον προσωπικό χαρακτήρα των στοιχείων που δηλώνουν οι συμμετέχοντες-εκδότες
                        και δε δύναται να τα μεταβιβάσει χωρίς τη συναίνεση τους σε οποιονδήποτε τρίτο (φυσικό
                        ή νομικό πρόσωπο) για κανένα λόγο με την εξαίρεση σχετικών διατάξεων του νόμου και
                        προς τις αρμόδιες και μόνο Αρχές.
                        <br /><br />
                        Η ΕΔΕΤ Α.Ε. μπορεί να χρησιμοποιεί τα προσωπικά στοιχεία για την επικοινωνία με
                        τους συμμετέχοντες-εκδότες και την ενημέρωση τους για ζητήματα που σχετίζονται με
                        το έργο.
                    </li>
                    <li>Σε κάθε περίπτωση η ΕΔΕΤ Α.Ε. διατηρεί το δικαίωμα αλλαγής των όρων του παρόντος
                        συμφωνητικού, ύστερα από ενημέρωση των συμμετεχόντων Εκδοτών μέσω της ιστοσελίδας
                        του προγράμματος «Εύδοξος».</li>
                </ol>
            </div>
            <br />
            <asp:LinkButton ID="btnAcceptTerms" runat="server" Text="Αποδοχή Όρων & Συνέχεια Εγγραφής"
                CssClass="icon-btn bg-accept" OnClick="btnAcceptTerms_Click" />
        </asp:View>
        <asp:View ID="vRegister" runat="server">
            <div class="reminder">
                Προσοχή: Αν είστε φοιτητής και θέλετε να πραγματοποιήσετε δήλωση συγγραμμάτων, θα
                πρέπει να μεταβείτε <a href="http://service.eudoxus.gr/student/">εδώ</a>
            </div>
            <asp:ValidationSummary ID="vdSummary" runat="server" ValidationGroup="vdRegistration"
                HeaderText="Υπάρχει σφάλμα ή έλλειψη συμπλήρωσης ενός από τα πεδία της φόρμας. Παρακαλώ κάντε τις απαραίτητες διορθώσεις." />
            <asp:Label ID="lblErrors" runat="server" Font-Bold="true" ForeColor="Red" />
            <my:RegisterUserInput ID="registerUserInput" runat="server" ValidationGroup="vdRegistration" />
            <br />
            <my:PublisherInput ID="publisherInput" runat="server" ValidationGroup="vdRegistration" />
            <br />
            <lc:BotShield ID="bsPublisher" runat="server" ValidationGroup="vdRegistration" />
            <br />
            <div style="clear: both; text-align: left">
                <asp:LinkButton ID="btnCreate" runat="server" Text="Δημιουργία Λογαριασμού" CssClass="icon-btn bg-accept"
                    ValidationGroup="vdRegistration" OnClick="btnCreate_Click" />
            </div>
        </asp:View>
        <asp:View ID="vComplete" runat="server">
            <asp:Label ID="lblCompletionMessage" runat="server" />
        </asp:View>
        <asp:View ID="vNotAllowed" runat="server">
            <div class="reminder">
                Η δημιουργία λογαριασμού χρήστη θα είναι σύντομα διαθέσιμη.
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
