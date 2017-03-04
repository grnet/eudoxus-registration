<%@ Page Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="GeneralError.aspx.cs" Inherits="Eudoxus.Portal.Common.GeneralError"
    Title="Σφάλμα" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <p>
        Παρουσιάστηκε κάποιο σφάλμα στην εφαρμογή. Ζητούμε συγνώμη για το πρόβλημα.</p>
    <p>
        Μπορείτε να μεταβείτε στην <a id="A1" href="~/Default.aspx" runat="server">αρχική σελίδα</a> για να ξαναπροσπαθήσετε.
    </p>
    <p>
        Μην προσπαθήσετε να χρησιμοποιήσετε τα πλήκτρα "back" ή "reload" του browser σας καθώς θα επαναληφθεί το ίδιο σφάλμα.</p>
</asp:Content>
