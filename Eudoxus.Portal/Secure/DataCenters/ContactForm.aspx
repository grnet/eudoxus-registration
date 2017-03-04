<%@ Page Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" Inherits="Eudoxus.Portal.Secure.DataCenters.ContactForm"
    CodeBehind="ContactForm.aspx.cs" Title="Ερώτημα προς Γραφείο Αρωγής" %>

<%@ Register Src="~/Secure/UserControls/ContactFormInput.ascx" TagName="ContactFormInput"
    TagPrefix="my" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ValidationSummary ID="vdSummary" runat="server" ValidationGroup="vgContact" />
    <div style="margin: 10px;">
        <my:ContactFormInput ID="ucContactFormInput" runat="server" ValidationGroup="vgContact" />
    </div>
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="text-align: left; padding-left: 7px;">
                <asp:LinkButton ID="btnSubmit" runat="server" ValidationGroup="vgContact" Text="Αποστολή" CssClass="icon-btn bg-accept"
                    OnClick="btnSubmit_Click" />
                <asp:LinkButton ID="btnCancel" runat="server" Text="Ακύρωση" CssClass="icon-btn bg-cancel"
                    CausesValidation="false" OnClientClick="window.parent.popUp.hide();" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblValidationErrors" runat="server" CssClass="error" EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Content>
