﻿<%@ Page Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" Inherits="Eudoxus.Portal.Helpdesk.VerifyPublicationsOffice"
    CodeBehind="VerifyPublicationsOffice.aspx.cs" Title="Πιστοποίηση Γραφείου Δημοσιευμάτων" %>

<%@ Register Src="~/UserControls/PublicationsOfficeInput.ascx" TagName="PublicationsOfficeInput"
    TagPrefix="my" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div style="margin: 10px;">
        <my:PublicationsOfficeInput ID="ucPublicationsOfficeInput" runat="server" />
    </div>
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="text-align: left; padding-left: 7px;">
                <asp:LinkButton ID="btnVerify" runat="server" Text="Πιστοποίηση" CssClass="icon-btn bg-accept"
                    OnClick="btnVerify_Click" OnClientClick="return confirm('Θέλετε σίγουρα να πιστοποιήσετε το γραφείο;')" />
                <asp:LinkButton ID="btnUnVerify" runat="server" Text="Από-Πιστοποίηση" CssClass="icon-btn bg-reject"
                    OnClick="btnUnVerify_Click" OnClientClick="return confirm('Θέλετε σίγουρα να από-πιστοποιήσετε το γραφείο;')" />
                <asp:LinkButton ID="btnReject" runat="server" Text="Απόρριψη" CssClass="icon-btn bg-reject"
                    OnClick="btnReject_Click" OnClientClick="return confirm('Θέλετε σίγουρα να απορρίψετε το γραφείο;')" />
                <asp:LinkButton ID="btnRestore" runat="server" Text="Επαναφορά" CssClass="icon-btn bg-undo"
                    OnClick="btnRestore_Click" OnClientClick="return confirm('Θέλετε σίγουρα να επαναφέρετε το γραφείο;')" />
                <asp:LinkButton ID="btnCancel" runat="server" Text="Ακύρωση" CssClass="icon-btn bg-cancel"
                    CausesValidation="false" OnClientClick="window.parent.popUp.hide();" />
            </td>
        </tr>
    </table>
</asp:Content>
