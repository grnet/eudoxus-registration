<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisteredPublishersDetails.aspx.cs"
    Inherits="Eudoxus.Portal.Reports.RegisteredPublishersDetails" Title="Στοιχεία Εγγεγραμμένων Εκδοτών" %>

<%@ Register Src="~/Reports/UserControls/RegisteredPublishersDetails.ascx" TagName="RegisteredPublisherDetails"
    TagPrefix="my" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Στοιχεία Εγγεγραμμένων Εκδοτών</title>
</head>
<body>
    <form id="form1" runat="server">
    <my:RegisteredPublisherDetails runat="server" />
    </form>
</body>
</html>
