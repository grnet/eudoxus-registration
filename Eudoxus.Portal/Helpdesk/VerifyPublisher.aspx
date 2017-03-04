<%@ Page Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" Inherits="Eudoxus.Portal.Helpdesk.VerifyPublisher"
    CodeBehind="VerifyPublisher.aspx.cs" Title="Πιστοποίηση Εκδότη" %>

<%@ Register Src="~/UserControls/PublisherInput.ascx" TagName="PublisherInput" TagPrefix="my" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div style="margin: 10px;">
        <my:PublisherInput ID="ucPublisherInput" runat="server" />
    </div>
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="text-align: left; padding-left: 7px;">
                <asp:LinkButton ID="btnVerify" runat="server" Text="Πιστοποίηση" CssClass="icon-btn bg-accept"
                    OnClick="btnVerify_Click" OnClientClick="return confirm('Θέλετε σίγουρα να πιστοποιήσετε τον εκδότη;')" />
                <asp:LinkButton ID="btnUnVerify" runat="server" Text="Από-Πιστοποίηση" CssClass="icon-btn bg-reject"
                    OnClick="btnUnVerify_Click" OnClientClick="return confirm('Θέλετε σίγουρα να από-πιστοποιήσετε τον εκδότη;')" />
                <asp:LinkButton ID="btnDeactivate" runat="server" Text="Απενεργοποίηση" CssClass="icon-btn bg-lock"
                    OnClick="btnDeactivate_Click" OnClientClick="return confirm('Θέλετε σίγουρα να απενεργοποιήσετε τον εκδότη;')" />
                <asp:LinkButton ID="btnActivate" runat="server" Text="Επανενεργοποίηση" CssClass="icon-btn bg-unlock"
                    OnClick="btnActivate_Click" OnClientClick="return confirm('Θέλετε σίγουρα να επανενεργοποιήσετε τον εκδότη;')" />
                <asp:LinkButton ID="btnReject" runat="server" Text="Απόρριψη" CssClass="icon-btn bg-reject"
                    OnClick="btnReject_Click" OnClientClick="return confirm('Θέλετε σίγουρα να απορρίψετε τον εκδότη;')" />
                <asp:LinkButton ID="btnRestore" runat="server" Text="Επαναφορά" CssClass="icon-btn bg-undo"
                    OnClick="btnRestore_Click" OnClientClick="return confirm('Θέλετε σίγουρα να επαναφέρετε τον εκδότη;')" />
                <asp:LinkButton ID="btnCancel" runat="server" Text="Ακύρωση" CssClass="icon-btn bg-cancel"
                    CausesValidation="false" OnClientClick="window.parent.popUp.hide();" />
            </td>
        </tr>
    </table>
</asp:Content>
