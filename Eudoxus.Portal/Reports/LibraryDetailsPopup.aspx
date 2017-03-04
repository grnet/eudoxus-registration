﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LibraryDetailsPopup.aspx.cs"
    Inherits="Eudoxus.Portal.Reports.LibraryDetailsPopup" Title="Στοιχεία Εγγεγραμμένων Βιβλιοθηκών" %>

<%@ Register Src="~/Reports/UserControls/LibraryDetailsGridView.ascx" TagName="LibraryDetailsGridView"
    TagPrefix="my" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link id="lnkMain" runat="server" href="~/_css/main.css" type="text/css" rel="stylesheet"
        rev="stylesheet" media="all" />
    <title>Στοιχεία Εγγεγραμμένων Βιβλιοθηκών</title>
</head>
<body>
    <form id="form1" runat="server">
    <my:LibraryDetailsGridView ID="gvLibraryDetails" runat="server" DataSourceID="odsLibraries"
        EnableExport="true" />
    <asp:ObjectDataSource ID="odsLibraries" runat="server" TypeName="Eudoxus.Portal.DataSources.Views"
        SelectMethod="FindLibrariesWithCriteria" SelectCountMethod="CountLibrariesWithCriteria"
        EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsLibraries_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
