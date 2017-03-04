<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="SendMessageToSecretaries.aspx.cs" Inherits="Eudoxus.Portal.Admin.SendMessageToSecretaries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Button ID="btnSendMessage" runat="server" Text="Αποστολή Μηνύματος" OnClick="btnSendMessage_Click" />
</asp:Content>
