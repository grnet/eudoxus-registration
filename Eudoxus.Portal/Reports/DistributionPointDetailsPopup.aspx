<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DistributionPointDetailsPopup.aspx.cs"
    Inherits="Eudoxus.Portal.Reports.DistributionPointDetailsPopup" Title="Στοιχεία Εγγεγραμμένων Σημείων Διανομής" %>

<%@ Register Src="~/Reports/UserControls/DistributionPointDetailsGridView.ascx" TagName="DistributionPointDetailsGridView"
    TagPrefix="my" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link id="lnkMain" runat="server" href="~/_css/main.css" type="text/css" rel="stylesheet"
        rev="stylesheet" media="all" />
    <title>Στοιχεία Εγγεγραμμένων Σημείων Διανομής</title>
</head>
<body>
    <form id="form1" runat="server">
    <my:DistributionPointDetailsGridView ID="gvDistributionPointDetails" runat="server" DataSourceID="odsDistributionPoints"
        EnableExport="true" />
    <asp:ObjectDataSource ID="odsDistributionPoints" runat="server" TypeName="Eudoxus.Portal.DataSources.Views"
        SelectMethod="FindDistributionPointsWithCriteria" SelectCountMethod="CountDistributionPointsWithCriteria"
        EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsDistributionPoints_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
