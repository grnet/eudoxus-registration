<%@ Page Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" Inherits="Eudoxus.Portal.Helpdesk.EditPublisher" CodeBehind="EditPublisher.aspx.cs"
    Title="Αλλαγή Στοιχείων Εκδότη" %>

<%@ Register Src="~/UserControls/PublisherInput.ascx" TagName="PublisherInput" TagPrefix="my" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div style="margin: 10px;">
        <my:PublisherInput ID="ucPublisherInput" runat="server" HelpDeskEditMode="true" />
    </div>

    <script type="text/javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('Θέλετε σίγουρα να αλλάξετε τα στοιχεία του εκδότη;');
            else return false;
            
        }
    </script>

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="text-align: left; padding-left: 7px;">
                <asp:LinkButton ID="btnSubmit" runat="server" Text="Αποθήκευση" CssClass="icon-btn bg-accept" OnClick="btnSubmit_Click" OnClientClick="return validate();" />
                <asp:LinkButton ID="btnCancel" runat="server" Text="Ακύρωση" CssClass="icon-btn bg-cancel" CausesValidation="false" OnClientClick="window.parent.popUp.hide();" />
            </td>
        </tr>
    </table>
</asp:Content>
