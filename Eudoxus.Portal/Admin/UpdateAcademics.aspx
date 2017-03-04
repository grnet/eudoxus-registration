<%@ Page Title="Κλωνοποίηση Τμημάτων" MasterPageFile="~/Admin/Admin.master" Language="C#"
    AutoEventWireup="true" CodeBehind="UpdateAcademics.aspx.cs" Inherits="Eudoxus.Portal.Admin.UpdateAcademics" %>

<asp:Content runat="server" ContentPlaceHolderID="cphMain">
    <asp:LinkButton ID="btnUpdateInstitutions" runat="server" Text="Update Ιδρυμάτων"
        CssClass="icon-btn bg-accept" OnClick="btnUpdateInstitutions_Click" />
    <asp:LinkButton ID="btnCloneAcademics" runat="server" Text="Κλωνοποίηση Τμημάτων"
        CssClass="icon-btn bg-accept" OnClick="btnCloneAcademics_Click" />
    <asp:LinkButton ID="btnAddNewAcademics" runat="server" Text="Προσθήκη Νέων Τμημάτων"
        CssClass="icon-btn bg-accept" OnClick="btnAddNewAcademics_Click" />
    <br />
    <br />
    <asp:Label ID="lblResult" runat="server" Font-Bold="true" ForeColor="Red" />
</asp:Content>
