<%@ Page Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="Eudoxus.Portal.Default" Title="Αρχική Σελίδα" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <h1>
        Καλώς ήλθατε</h1>
    <div class="login-cell1 cell1_box">
        <div class="cell1_top">
            <div>
            </div>
        </div>
        <div class="cell-heading">
            ΣΥΝΔΕΣΗ</div>
        <div class="cell1_content outer-content">
            <div class="middle-content">
                <div class="inner-content">
                    <asp:Login runat="server" ID="login1" DestinationPageUrl="~/Default.aspx" LoginButtonText="Σύνδεση"
                        PasswordLabelText="Κωδικός πρόσβασης:" PasswordRecoveryText="Ξέχασα τον κωδικό μου"
                        PasswordRecoveryUrl="~/Common/ForgotPassword.aspx" PasswordRequiredErrorMessage="Ο κωδικός πρόσβασης είναι υποχρεωτικός"
                        RememberMeText="Θυμήσου με" TitleText="" UserNameLabelText="Όνομα χρήστη:" UserNameRequiredErrorMessage="Το όνομα χρήστη είναι υποχρεωτικό"
                        FailureText="Λάθος όνομα χρήστη ή κωδικός πρόσβασης." CssClass="justifiedText"
                        OnLoggingIn="login1_LoggingIn">
                        <LayoutTemplate>
                            <table border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Όνομα 
                                    χρήστη:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server" Width="145px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" Display="Dynamic"
                                            ControlToValidate="UserName" ErrorMessage="Το όνομα χρήστη είναι υποχρεωτικό"
                                            ToolTip="Το όνομα χρήστη είναι υποχρεωτικό" ValidationGroup="login1"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Κωδικός 
                                    πρόσβασης:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="145px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" Display="Dynamic"
                                            ControlToValidate="Password" ErrorMessage="Ο κωδικός πρόσβασης είναι υποχρεωτικός"
                                            ToolTip="Ο κωδικός πρόσβασης είναι υποχρεωτικός" ValidationGroup="login1"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
                                        <%--<lc:CapsWarning ID="CapsWarning1" runat="server" TextBoxControlId="Password" Text="Προσοχή: το πλήκτρο Caps Lock είναι πατημένο"
                                            CssClass="capsLockWarning" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="RememberMe" runat="server" Text="Θυμήσου με" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color: Red;">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:LinkButton ID="LoginButton" runat="server" CommandName="Login" CssClass="btn btn-login"
                                            Text="Σύνδεση" ValidationGroup="login1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:HyperLink ID="PasswordRecoveryLink" runat="server" Font-Bold="true" NavigateUrl="~/Common/ForgotPassword.aspx">Υπενθύμιση κωδικού πρόσβασης</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:HyperLink ID="ActivationEmailResend" runat="server" Font-Bold="true" NavigateUrl="~/Common/SendActivationEmail.aspx">Επαναποστολή e-mail ενεργοποίησης</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <div class="justifiedText">
                                <%--Εάν αντιμετωπίζετε πρόβλημα σύνδεσης με το λογαριασμό σας, μπορείτε να επικοινωνήσετε
                                με το Γραφείο Αρωγής Χρηστών στο τηλέφωνο <b>215 215 7850</b>--%>
                                Εάν αντιμετωπίζετε πρόβλημα σύνδεσης με το λογαριασμό σας, μπορείτε να επικοινωνήσετε με το <a href="http://eudoxus.gr/OnlineReport.aspx">Γραφείο Αρωγής Χρηστών</a>
                            </div>
                            <br />
                            <div class="justifiedText">
                                Για να δείτε τις αναλυτικές οδηγίες για τη συμμετοχή στη δράση μπορείτε να επισκεφτείτε
                                το δικτυακό τόπο του έργου <a href="http://www.eudoxus.gr" target="_blank" style="font-weight: bold;
                                    color: Blue;">http://www.eudoxus.gr</a>
                            </div>
                        </LayoutTemplate>
                    </asp:Login>
                </div>
            </div>
        </div>
        <div class="cell1_bottom">
            <div>
            </div>
        </div>
    </div>
    <div class="login-cell2 cell2_box">
        <div class="cell2_top">
            <div>
            </div>
        </div>
        <div class="cell-heading">
            ΕΓΓΡΑΦΕΣ ΝΕΩΝ ΧΡΗΣΤΩΝ</div>
        <div class="cell2_content outer-content">
            <div class="middle-content">
                <div class="inner-content">
                    Για να δημιουργήσετε λογαριασμό χρήστη, πατήστε το αντίστοιχο από τα παρακάτω κουμπιά
                </div>
                <br />
                <div class="buttonwrapper">
                    <a class="squarebutton" runat="server" href="~/Common/SecretaryRegistration.aspx"><span>
                        Γραμματεία Τμήματος</span></a>
                </div>
                <br />
                <div class="buttonwrapper">
                    <a class="squarebutton" runat="server" href="~/Common/PublisherRegistration.aspx?t=1">
                        <span>Εκδοτικός Οίκος</span></a>
                </div>
                <br />
                <div class="buttonwrapper">
                    <a class="squarebutton" runat="server" href="~/Common/PublisherRegistration.aspx?t=2">
                        <span>Αυτοεκδότης</span></a>
                </div>
                <br />
                <div class="buttonwrapper">
                    <a class="squarebutton" runat="server" href="~/Common/ForeignPublisherRegistration.aspx">
                        <span>Εκδοτικός Οίκος (ΕΞΩΤΕΡΙΚΟΥ)</span></a>
                </div>
                <br />
                <div class="buttonwrapper">
                    <a class="squarebutton" runat="server" href="~/Common/EbookPublisherRegistration.aspx">
                        <span>Διαθέτης Δωρεάν Ηλεκτρονικών Βοηθημάτων και Σημειώσεων</span></a>
                </div>
                <br />                
                <div class="buttonwrapper">
                    <a class="squarebutton" runat="server" href="~/Common/PublicationsOfficeRegistration.aspx"><span>
                        Γραφείο Διδακτικών Συγγραμμάτων Ιδρύματος</span></a>
                </div>
                <br />
                <div class="buttonwrapper">
                    <a class="squarebutton" runat="server" href="~/Common/DataCenterRegistration.aspx"><span>
                        Γραφείο Μηχανογράφησης Ιδρύματος</span></a>
                </div>
                <br />
                <div class="buttonwrapper">
                    <a class="squarebutton" runat="server" href="~/Common/DistributionPointRegistration.aspx"><span>
                        Σημείο Διανομής</span></a>
                </div>
                <br />
                <div class="buttonwrapper">
                    <a class="squarebutton" runat="server" href="~/Common/LibraryRegistration.aspx"><span>
                        Βιβλιοθήκη</span></a>
                </div>
                <br />
                <div class="buttonwrapper">
                    <a class="squarebutton" runat="server" href="~/Common/BookSupplierRegistration.aspx"><span>
                        Υπεύθυνος Παραγγελίας Βιβλίων</span></a>
                </div>
            </div>
        </div>
        <div class="cell2_bottom">
            <div>
            </div>
        </div>
    </div>
</asp:Content>
