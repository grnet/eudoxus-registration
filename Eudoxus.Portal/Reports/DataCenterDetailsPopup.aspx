<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataCenterDetailsPopup.aspx.cs"
    Inherits="Eudoxus.Portal.Reports.DataCenterDetailsPopup" Title="Στοιχεία Εγγεγραμμένων Γραφείων Μηχανογράφησης" %>

<%@ Register Src="~/Reports/UserControls/DataCenterDetailsGridView.ascx" TagName="DataCenterDetailsGridView"
    TagPrefix="my" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link id="lnkMain" runat="server" href="~/_css/main.css" type="text/css" rel="stylesheet"
        rev="stylesheet" media="all" />
    <title>Στοιχεία Εγγεγραμμένων Γραφείων Μηχανογράφησης</title>
</head>
<body>
    <form id="form1" runat="server">
    <my:DataCenterDetailsGridView ID="gvDataCenterDetails" runat="server" DataSourceID="odsDataCenters"
        EnableExport="true" />
    <asp:ObjectDataSource ID="odsDataCenters" runat="server" TypeName="Eudoxus.Portal.DataSources.Views"
        SelectMethod="FindDataCentersWithCriteria" SelectCountMethod="CountDataCentersWithCriteria"
        EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsDataCenters_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
