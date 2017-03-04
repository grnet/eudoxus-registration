<%@ Page Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" Inherits="Eudoxus.Portal.Helpdesk.EditBookSupplier" CodeBehind="EditBookSupplier.aspx.cs"
    Title="Αλλαγή Στοιχείων Υπεύθυνου Παραγγελίας Βιβλίων" %>

<%@ Register Src="~/UserControls/BookSupplierInput.ascx" TagName="BookSupplierInput" TagPrefix="my" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div style="margin: 10px;">
        <my:BookSupplierInput ID="ucBookSupplierInput" runat="server" ValidationGroup="vg" />
    </div>

    <script type="text/javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('Θέλετε σίγουρα να αλλάξετε τα στοιχεία του υπεύθυνου παραγγελίας βιβλίων;');
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
