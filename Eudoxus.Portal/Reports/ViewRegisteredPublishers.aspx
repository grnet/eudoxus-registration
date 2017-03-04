<%@ Page Language="C#" MasterPageFile="~/Reports/Reports.Master" AutoEventWireup="true"
    CodeBehind="ViewRegisteredPublishers.aspx.cs" Inherits="Eudoxus.Portal.Reports.ViewRegisteredPublishers"
    Title="Στοιχεία Εγγεγραμμένων Εκδοτών" %>

<%@ Register Src="~/Reports/UserControls/RegisteredPublishersDetails.ascx" TagName="RegisteredPublisherDetails" TagPrefix="my" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <my:RegisteredPublisherDetails runat="server" />
</asp:Content>
