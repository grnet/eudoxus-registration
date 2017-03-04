<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SendPublishersForPayment.aspx.cs" Inherits="Eudoxus.Portal.Admin.SendPublishersForPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <fieldset>
        <legend>Μυνήματα</legend>
        <%= ShowMessage() %>
    </fieldset>
    Σύνολο τελευταίων τροποποιημένων εγγραφών για αποστολή:
    <asp:TextBox ID="txtPublisherCount" runat="server" />
    <asp:Button ID="btnSendPublishers" runat="server" Text="Αποστολή Εκδοτών" OnClick="btnSendPublishers_Click" />
</asp:Content>
