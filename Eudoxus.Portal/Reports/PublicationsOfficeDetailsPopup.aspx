<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublicationsOfficeDetailsPopup.aspx.cs"
    Inherits="Eudoxus.Portal.Reports.PublicationsOfficeDetailsPopup" Title="Στοιχεία Εγγεγραμμένων Γραφείων Διδακτικών Συγγραμμάτων" %>

<%@ Register Src="~/Reports/UserControls/PublicationsOfficeDetailsGridView.ascx" TagName="PublicationsOfficeDetailsGridView"
    TagPrefix="my" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link id="lnkMain" runat="server" href="~/_css/main.css" type="text/css" rel="stylesheet"
        rev="stylesheet" media="all" />
    <title>Στοιχεία Εγγεγραμμένων Γραφείων Διδακτικών Συγγραμμάτων</title>
</head>
<body>
    <form id="form1" runat="server">
    <my:PublicationsOfficeDetailsGridView ID="gvPublicationsOfficeDetails" runat="server" DataSourceID="odsPublicationsOffices"
        EnableExport="true" />
    <asp:ObjectDataSource ID="odsPublicationsOffices" runat="server" TypeName="Eudoxus.Portal.DataSources.Views"
        SelectMethod="FindPublicationsOfficesWithCriteria" SelectCountMethod="CountPublicationsOfficesWithCriteria"
        EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsPublicationsOffices_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
