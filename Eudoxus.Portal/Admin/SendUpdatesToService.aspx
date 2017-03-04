<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SendUpdatesToService.aspx.cs" Inherits="Eudoxus.Portal.Admin.SendUpdatesToService" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    Σύνολο Εγγραφών για αποστολή: 
    <asp:TextBox runat="server" ID="txtTake" />    
    <asp:Button Text="Αποστολή Εκδοτών" OnClick="SendPublisherUpdates" runat="server" />

    <asp:Button Text="Αποστολή Γραμματειών" OnClick="SendSecretaryUpdates" runat="server" />
    
    <asp:Button Text="Αποστολή Γραφείων Δημοσιευμάτων" OnClick="SendPublicationsOfficeUpdates" runat="server" />

    <asp:Button Text="Αποστολή Γραφείων Μηχανογράφησης" OnClick="SendDataCenterUpdates" runat="server" />

    <asp:Button Text="Αποστολή Σημείων Διανομής" OnClick="SendDistributionPointUpdates" runat="server" />

    <asp:Button ID="Button1" Text="Αποστολή Σημείων Διανομής" OnClick="SendLibraryUpdates" runat="server" />
</asp:Content>
