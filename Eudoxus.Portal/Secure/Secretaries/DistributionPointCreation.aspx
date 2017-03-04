<%@ Page Title="" Language="C#" MasterPageFile="~/Secure/Secretaries/Secretaries.Master"
    AutoEventWireup="true" CodeBehind="DistributionPointCreation.aspx.cs" Inherits="Eudoxus.Portal.Secure.Secretaries.DistributionPointCreation" %>

<%@ Register Src="~/UserControls/FlashMessage.ascx" TagName="FlashMessage" TagPrefix="my" %>
<%@ Register Src="~/UserControls/DistributionPointInput.ascx" TagName="DistributionPointInput"
    TagPrefix="my" %>
<%@ Register Src="~/UserControls/RegisterUserInput.ascx" TagName="RegisterUserInput"
    TagPrefix="my" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <script type="text/javascript">
        function validate() {
            return Page_ClientValidate();
        }
    </script>
    <asp:MultiView ID="mvAccount" runat="server" ActiveViewIndex="0">
        <asp:View ID="vVerified" runat="server">
            <asp:PlaceHolder ID="phMessage" runat="server" Visible="false">
                <div style="font-weight: bold; color: Red">
                    Το Σημείο Διανομής έχει δημιουργηθεί με επιτυχία.<br />
                    <br />
                    Για να επεξεργαστείτε τα στοιχεία του παρακαλώ συνδεθείτε στην εφαρμογή χρησιμοποιώντας
                    το Όνομα Χρήστη και τον Κωδικό Πρόσβασης που ορίσατε κατά τη δημιουργία του.
                    <br />
                    <br />
                </div>
                <table width="100%" class="dv">
                    <tr>
                        <th colspan="2" class="header">
                            &raquo; Στοιχεία Λογαριασμού Χρήστη
                        </th>
                    </tr>
                    <tr>
                        <th style="width: 30%">
                            Όνομα Χρήστη:
                        </th>
                        <td>
                            <asp:TextBox ID="txtUsername" runat="server" Width="30%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 30%">
                            E-mail:
                        </th>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" Width="90%" Enabled="false" />
                        </td>
                    </tr>
                </table>
            </asp:PlaceHolder>
            <asp:ValidationSummary ID="vdSummary" runat="server" ValidationGroup="vdRegistration"
                HeaderText="Υπάρχει σφάλμα ή έλλειψη συμπλήρωσης ενός από τα πεδία της φόρμας. Παρακαλώ κάντε τις απαραίτητες διορθώσεις." />
            <asp:Label ID="lblErrors" runat="server" Font-Bold="true" ForeColor="Red" />
            <my:RegisterUserInput ID="registerUserInput" runat="server" ValidationGroup="vdRegistration"
                EmailInfoHidden="true" />
            <br />
            <my:DistributionPointInput ID="ucDistributionPointInput" runat="server" ValidationGroup="vdRegistration" />
            <br />
            <div style="clear: both; text-align: left">
                <asp:LinkButton ID="btnCreate" runat="server" Text="Δημιουργία Σημείου Διανομής"
                    CssClass="icon-btn bg-accept" ValidationGroup="vdRegistration" OnClick="btnCreate_Click" />
            </div>
        </asp:View>
        <asp:View ID="vNotVerified" runat="server">
            <div class="reminder">
                Δεν μπορείτε να δημιουργήσετε Σημείο Διανομής, γιατί δεν έχετε ακόμα πιστοποιήσει
                το λογαριασμό σας.
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
