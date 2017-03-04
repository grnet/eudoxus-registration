<%@ Page Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" Inherits="Eudoxus.Portal.Helpdesk.VerifyPricingCommittee"
    CodeBehind="VerifyPricingCommittee.aspx.cs" Title="Πιστοποίηση Μέλους Επιτροπής" %>

<%@ Register Src="~/UserControls/PricingCommitteeInput.ascx" TagName="PricingCommitteeInput"
    TagPrefix="my" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div style="margin: 10px;">
        <my:PricingCommitteeInput ID="ucPricingCommitteeInput" runat="server" ValidationGroup="vg" />
        <asp:PlaceHolder ID="phMinistryAuthorization" runat="server">
            <br />
            <br />
            <table width="100%" class="dv">
                <colgroup>
                    <col style="width: 30%" />
                </colgroup>
                <tr>
                    <th colspan="2" class="header">
                        &raquo; Εξουσιοδότηση Μέλους Επιτροπής
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
                            runat="server" ControlToValidate="ddlMinistryAuthorization" ErrorMessage="Το πεδίο 'Εξουσιοδότηση' είναι υποχρεωτικό"
                            ValidationGroup="vg"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vg" />
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="text-align: left; padding-left: 7px;">
                <asp:LinkButton ID="btnVerify" runat="server" Text="Πιστοποίηση" CssClass="icon-btn bg-accept"
                    OnClick="btnVerify_Click" OnClientClick="return accept();" ValidationGroup="vg" />
                <asp:LinkButton ID="btnUnVerify" runat="server" Text="Από-Πιστοποίηση" CssClass="icon-btn bg-reject"
                    OnClick="btnUnVerify_Click" OnClientClick="return confirm('Θέλετε σίγουρα να από-πιστοποιήσετε το μέλος επιτροπής;')" />
                <asp:LinkButton ID="btnReject" runat="server" Text="Απόρριψη" CssClass="icon-btn bg-reject"
                    OnClick="btnReject_Click" OnClientClick="return confirm('Θέλετε σίγουρα να απορρίψετε το μέλος επιτροπής;')" />
                <asp:LinkButton ID="btnRestore" runat="server" Text="Επαναφορά" CssClass="icon-btn bg-undo"
                    OnClick="btnRestore_Click" OnClientClick="return confirm('Θέλετε σίγουρα να επαναφέρετε το μέλος επιτροπής;')" />
                <asp:LinkButton ID="btnCancel" runat="server" Text="Ακύρωση" CssClass="icon-btn bg-cancel"
                    CausesValidation="false" OnClientClick="window.parent.popUp.hide();" />
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function accept() {
            if (Page_ClientValidate())
                return confirm('Θέλετε σίγουρα να πιστοποιήσετε το μέλος επιτροπής;');
            else return false;

        }
    </script>
</asp:Content>
