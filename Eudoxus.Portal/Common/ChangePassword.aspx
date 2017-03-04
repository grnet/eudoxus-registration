<%@ Page Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs"
    Inherits="Eudoxus.Portal.Common.ChangePassword" Title="Αλλαγή Κωδικού Πρόσβασης" %>

<%@ Register Src="~/UserControls/ChangePassword.ascx" TagName="ChangePassword" TagPrefix="my" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

    <script type="text/javascript">
        function clearErrors() {
            $('#divErrors').hide();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ValidationSummary ID="vdChangePassword" runat="server" ValidationGroup="vgChangePassword" />
    <div id="divErrors">
        <asp:Label ID="lblErrors" runat="server" Font-Bold="true" ForeColor="Red" Visible="false"
            Text="Ο παλιός κωδικός πρόσβασης δεν είναι σωστός." />
    </div>
    <div style="margin: 10px;">
        <my:ChangePassword ID="changePassword" runat="server" ValidationGroup="vgChangePassword" />
    </div>
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="text-align: left; padding-left: 7px;">
                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="icon-btn bg-accept" Text="Ενημέρωση"
                    OnClick="btnSubmit_Click" ValidationGroup="vgChangePassword" OnClientClick="clearErrors()" />
                <asp:LinkButton ID="btnCancel" runat="server" CssClass="icon-btn bg-cancel" Text="Ακύρωση"
                    CausesValidation="false" OnClientClick="window.parent.popUp.hide();" />
            </td>
        </tr>
    </table>
</asp:Content>
