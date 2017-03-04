<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookSupplierDetailsPopup.aspx.cs"
    Inherits="Eudoxus.Portal.Reports.BookSupplierDetailsPopup" Title="Στοιχεία Εγγεγραμμένων Υπεύθυνων Παραγγελίας Βιβλίων" %>

<%@ Register Src="~/Reports/UserControls/BookSupplierDetailsGridView.ascx" TagName="BookSupplierDetailsGridView"
    TagPrefix="my" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link id="lnkMain" runat="server" href="~/_css/main.css" type="text/css" rel="stylesheet"
        rev="stylesheet" media="all" />
    <title>Στοιχεία Εγγεγραμμένων Υπεύθυνων Παραγγελίας Βιβλίων</title>
</head>
<body>
    <form id="form1" runat="server">
    <my:BookSupplierDetailsGridView ID="gvBookSupplierDetails" runat="server" DataSourceID="odsBookSuppliers"
        EnableExport="true" />
    <asp:ObjectDataSource ID="odsBookSuppliers" runat="server" TypeName="Eudoxus.Portal.DataSources.Views"
        SelectMethod="FindBookSuppliersWithCriteria" SelectCountMethod="CountBookSuppliersWithCriteria"
        EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsBookSuppliers_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
