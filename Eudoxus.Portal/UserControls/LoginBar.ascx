<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginBar.ascx.cs" Inherits="Eudoxus.Portal.UserControls.Generic.LoginBar" %>
<%@ Register Src="~/UserControls/ChangePassword.ascx" TagName="ChangePassword"
    TagPrefix="my" %>
<asp:LoginView ID="loginView" runat="server">
    <AnonymousTemplate>
        Δεν έχετε συνδεθεί.
        <asp:LoginStatus ID="loginStatus" runat="server" />
    </AnonymousTemplate>
    <LoggedInTemplate>
        <asp:LoginName ID="loginName" runat="server" FormatString="Έχετε συνδεθεί ως: [<strong>{0}</strong>]" />
        <asp:LoginStatus ID="loginStatus" runat="server" LogoutText="Αποσύνδεση" LogoutAction="RedirectToLoginPage" />
        <asp:LinkButton ID="lnkChangePassword" runat="server" Text="Αλλαγή κωδικού πρόσβασης" />
        <asp:Panel ID="panChangePassword" runat="server" Style="display: none; border: 1px solid #444;
            padding: 5px; z-index: 1000; position: absolute;" CssClass="modalPopup-cp">
            <asp:UpdatePanel ID="updChangePassword" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mv" runat="server" ActiveViewIndex="0">
                        <asp:View ID="vNormal" runat="server">
                            <my:ChangePassword runat="server" ID="cp" ValidationGroup="cp" />
                            <asp:Button ID="btnChangePassword" runat="server" Text="Αλλαγή Κωδικού" OnClick="btnChangePassword_Click"
                                ValidationGroup="cp" />
                            <asp:Button ID="btnCancel" runat="server" Text="Ακύρωση" CausesValidation="false"
                                OnClick="btnCancel_Click" />
                            <p>
                                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" /></p>
                        </asp:View>
                        <asp:View ID="vSuccess" runat="server">
                            <div style="text-align: left">
                                <b>Η αλλαγή πραγματοποιήθηκε επιτυχώς</b><br /><br />
                                <asp:Button ID="btnSuccess" runat="server" Text="Κλείσιμο" OnClick="btnSuccess_Click" />
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="mpeChangePassword" runat="server" TargetControlID="lnkChangePassword"
            PopupControlID="panChangePassword" BackgroundCssClass="modalBackground" />
    </LoggedInTemplate>
</asp:LoginView>
