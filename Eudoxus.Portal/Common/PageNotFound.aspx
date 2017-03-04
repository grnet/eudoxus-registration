<%@ Page Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="PageNotFound.aspx.cs"
    Inherits="Eudoxus.Portal.Common.PageNotFound" Title="Σφάλμα" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <p>
        Η σελίδα που ζητήσατε δεν βρέθηκε.
    </p>
    <p>
        Μετάβαση στην <a id="A1" href="~/Default.aspx" runat="server">αρχική σελίδα</a>
    </p>
</asp:Content>
