<%@ Page Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" Inherits="Eudoxus.Portal.Helpdesk.EditMinistryPaymentsUser" CodeBehind="EditMinistryPaymentsUser.aspx.cs"
    Title="Αλλαγή Στοιχείων Σημείου Διανομής" %>

<%@ Register Src="~/UserControls/MinistryPaymentsUserInput.ascx" TagName="MinistryPaymentsUserInput" TagPrefix="my" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div style="margin: 10px;">
        <my:MinistryPaymentsUserInput ID="ucMinistryPaymentsUserInput" runat="server" ValidationGroup="vg" />
        <br />
        <br />
        <table width="100%" class="dv">
            <colgroup>
                <col style="width: 30%" />
            </colgroup>
            <tr>
                <th colspan="2" class="header">
                    &raquo; Εξουσιοδότηση Χρήστη Υπουργείου - Πληρωμών
                </th>
            </tr>
            <tr>
                <th>
                    Εξουσιοδότηση:
                </th>
                <td>
                    <asp:DropDownList ID="ddlMinistryAuthorization" runat="server" OnInit="ddlMinistryAuthorization_Init"
                        Width="90%" />
                    <asp:RequiredFieldValidator ID="rfvMinistryAuthorization" Display="Dynamic"
                        runat="server" ControlToValidate="ddlMinistryAuthorization" ErrorMessage="Το πεδίο 'Εξουσιοδότηση' είναι υποχρεωτικό" ValidationGroup="vg"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('Θέλετε σίγουρα να αλλάξετε τα στοιχεία του χρήστη;');
            else return false;

        }
    </script>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vg" />
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="text-align: left; padding-left: 7px;">
                <asp:LinkButton ID="btnSubmit" runat="server" Text="Αποθήκευση" CssClass="icon-btn bg-accept" OnClick="btnSubmit_Click" OnClientClick="return validate();"
                    ValidationGroup="vg" />
                <asp:LinkButton ID="btnCancel" runat="server" Text="Ακύρωση" CssClass="icon-btn bg-cancel" CausesValidation="false" OnClientClick="window.parent.popUp.hide();" />
            </td>
        </tr>
    </table>
</asp:Content>
