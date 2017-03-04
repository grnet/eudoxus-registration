<%@ Page Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true"
    CodeBehind="AccessDenied.aspx.cs" Inherits="Eudoxus.Portal.Common.AccessDenied"
    Title="Απαγορεύεται η πρόσβαση" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <h1>
        Απαγορεύεται η πρόσβαση</h1>
    <p>
        Απαγορεύεται η πρόσβαση σε αυτή τη σελίδα</p>
    <asp:LoginView runat="server">
        <LoggedInTemplate>
            <p>
                Έχετε συνδεθεί ως
                <asp:LoginName ID="LoginName1" runat="server" />
                <asp:LoginStatus ID="LoginStatus1" runat="server" LogoutAction="Redirect" LogoutPageUrl="~/Default.aspx" />
            </p>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
