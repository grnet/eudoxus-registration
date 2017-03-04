<%@ Page Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="PublisherRegistration.aspx.cs"
    Inherits="Eudoxus.Portal.Common.PublisherRegistration" Title="Δημιουργία Λογαριασμού" %>

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
                    <li>Κάθε συµµετέχων-εκδότης οφείλει να διαβάσει προσεκτικά τους όρους αυτούς πριν από την συµµετοχή του στο πρόγραµµα «Εύδοξος – Ηλεκτρονική Υπηρεσία Ολοκληρωµένης ∆ιαχείρισης Συγγραµµάτων». Η συµµετοχή στο πρόγραµµα συνεπάγεται την αποδοχή των παρόντων όρων συµµετοχής.</li>
                    <li>Ο συµµετέχων-εκδότης στο πρόγραµµα «Εύδοξος – Ηλεκτρονική Υπηρεσία Ολοκληρωµένης ∆ιαχείρισης Συγγραµµάτων» δηλώνει ότι τα συγγράµµατα που καταχωρίζει στο Κεντρικό Πληροφοριακό Σύστηµα (ΚΠΣ) του Έργου υποβάλλονται νοµίµως και σύµφωνα µε τους όρους και τις προϋποθέσεις του έργου.</li>
                    <li>Ο συµµετέχων-εκδότης δηλώνει και εγγυάται ότι διατηρεί τα δικαιώµατα διανοµής στα Πανεπιστήμια, στα Τεχνολογικά Εκπαιδευτικά Ιδρύματα (Τ.Ε.Ι.), στις Ανώτατες Εκκλησιαστικές Ακαδημίες (Α.Ε.Α.) και στο Ελληνικό Ανοιχτό Πανεπιστήμιο (Ε.Α.Π.) για τα συγγράµµατα τα οποία καταχωρίζει στο ΚΠΣ της ∆ράσης και συνεπώς δεν παραβιάζει πνευµατικά δικαιώµατα, δικαιώµατα ευρεσιτεχνίας, εµπορικά σήµατα ή µυστικά ή άλλα δικαιώµατα οποιουδήποτε φυσικού ή νοµικού προσώπου στην Ελλάδα ή το εξωτερικό, άλλως ότι είναι δικαιούχος ή/και ότι έχει αποκτήσει νοµίµως τα αντίστοιχα δικαιώµατα.</li>
                    <li>Ο συμμετέχων-εκδότης δεσμεύεται να παραδώσει τα Συγγράμματα που έχει καταχωρίσει στο ΚΠΣ σε όλους τους φοιτητές και τις Βιβλιοθήκες όλων των Τμημάτων των Πανεπιστημίων, Τ.Ε.Ι. και Α.Ε.Α. της χώρας καθώς και των Προγραμμάτων Σπουδών του Ε.Α.Π. που θα επιλέξουν κάποιο Σύγγραμμα του συγκεκριμένου εκδότη κατά το επόμενο ακαδημαϊκό έτος.</li>
                    <li>Ο συµµετέχων-εκδότης αναλαµβάνει να παραδώσει τα συγγράµµατα στους φοιτητές µε δικό του κόστος.</li>
                    <li>Η Βεβαίωση Συµµετοχής που υπογράφει ο νόµιµος εκπρόσωπος της εταιρίας (σε περίπτωση που πρόκειται για νοµικό πρόσωπο) ή ο ίδιος ο εκδότης (σε περίπτωση που πρόκειται για φυσικό πρόσωπο) επέχει θέση Υπεύθυνης ∆ήλωσης.</li>
                    <li>Με τη Βεβαίωση Συµµετοχής ο συµµετέχων-εκδότης εξουσιοδοτεί νοµίµως ένα υπάλληλο ή συνεργάτη του, ο οποίος καθίσταται υπεύθυνος για το πρόγραµµα «Εύδοξος» και ο οποίος αποδέχεται αυτοδικαίως τους Κανόνες του Προγράµµατος και υποχρεώνεται να συµµορφώνεται µε αυτούς. Κάθε πράξη ή παράλειψη του εξουσιοδοτηµένου υπαλλήλου ή συνεργάτη θεωρείται πράξη ή παράλειψη του εκδότη, ο οποίος και ευθύνεται εξ ολοκλήρου.</li>
                    <li>Ο συµµετέχων-εκδότης υποχρεούται να αναδέχεται οποιαδήποτε αξίωση προβληθεί κατά της Ε∆ΕΤ Α.Ε. και να απαλλάξει την Ε∆ΕΤ Α.Ε. και τους διευθυντές, υπαλλήλους, εργαζοµένους και αντιπροσώπους της από κάθε ευθύνη για αποζηµίωση, έξοδα (συµπεριλαµβανοµένων και των ευλόγων δικαστικών εξόδων), δικαστικές αποφάσεις και άλλες δαπάνες ή απαιτήσεις τρίτων που τυχόν προέλθουν από παραβιάσεις: δικαιωµάτων τρίτων, όπως ενδεικτικά των δικαιωµάτων πνευµατικής ή βιοµηχανικής ιδιοκτησίας, σύµφωνα µε τα όσα αναφέρονται στην προηγούµενη παράγραφο, δηµοσιότητας, εµπιστευτικότητας.</li>
                    <li>Ο συµµετέχων-εκδότης δηλώνει ρητά και υπεύθυνα ότι κάθε στοιχείo που δηλώνει είναι ακριβές.</li>
                    <li>Ο συµµετέχων-εκδότης δηλώνει ρητά και υπεύθυνα ότι αποδέχεται όλους τους ως άνω όρους.</li>
                    <li>Η διαχείριση και προστασία των προσωπικών δεδοµένων του συµµετέχοντα εκδότη υπόκειται στους παρόντες όρους καθώς και στις σχετικές διατάξεις του ελληνικού και ευρωπαϊκού δικαίου για την εν γένει προστασία του ατόµου από την επεξεργασία δεδοµένων προσωπικού χαρακτήρα και το απόρρητο των επικοινωνιών, όπως ερµηνεύονται µε τις αποφάσεις των αρµόδιων Ανεξάρτητων ∆ιοικητικών Αρχών. Σε κάθε περίπτωση η Ε∆ΕΤ Α.Ε. διατηρεί το δικαίωµα αλλαγής των όρων προστασίας των προσωπικών δεδοµένων κατόπιν ενηµέρωσης των συµµετεχόντων εκδοτών µέσω της παρούσας ιστοσελίδας.</li>
                </ol>
                <div style="margin-left: 20px">
                    Η συµµετοχή στο πρόγραµµα συνεπάγεται τη ρητή και ανεπιφύλακτη συναίνεση του συµµετέχοντα - εκδότη για καταχώριση των προσωπικών του στοιχείων σε αρχείο που θα τηρείται µε αυτά και την επεξεργασία τους από την Ε∆ΕΤ Α.Ε. για την υλοποίηση του παρόντος έργου σύµφωνα µε τις διατάξεις του Ν.2472/1997 όπως ισχύει.
                    <br />
                    <br />
                    Η Ε∆ΕΤ Α.Ε. διαφυλάσσει τον προσωπικό χαρακτήρα των στοιχείων που δηλώνουν οι συµµετέχοντες εκδότες και δε δύναται να τα µεταβιβάσει χωρίς τη συναίνεση τους σε οποιονδήποτε τρίτο (φυσικό ή νοµικό πρόσωπο) για κανένα λόγο µε την εξαίρεση σχετικών διατάξεων του νόµου και προς τις αρµόδιες και µόνο Aρχές.
                    <br />
                    <br />
                    Η Ε∆ΕΤ Α.Ε. µπορεί να χρησιµοποιεί τα προσωπικά στοιχεία για την επικοινωνία µε τους συµµετέχοντες-εκδότες και την ενηµέρωση τους για ζητήµατα που σχετίζονται µε το έργο.
                </div>
            </div>
            <br />
            <asp:LinkButton ID="btnAcceptTerms" runat="server" Text="Αποδοχή Όρων & Συνέχεια Εγγραφής"
                CssClass="icon-btn bg-accept" OnClick="btnAcceptTerms_Click" />
        </asp:View>
        <asp:View ID="vRegister" runat="server">
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
