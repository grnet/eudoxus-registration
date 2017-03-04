<%@ Page Language="C#" MasterPageFile="~/Reports/Reports.Master" AutoEventWireup="true"
    CodeBehind="ViewRegisteredDistributionPoints.aspx.cs" Inherits="Eudoxus.Portal.Reports.ViewRegisteredDistributionPoints"
    Title="Στοιχεία Εγγεγραμμένων Γραμματειών" %>

<%@ Register Src="~/Reports/UserControls/DistributionPointDetailsGridView.ascx" TagName="DistributionPointDetailsGridView" TagPrefix="my" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <my:DistributionPointDetailsGridView ID="gvDistributionPointDetails" runat="server" DataSourceID="odsDistributionPoints" EnableExport="true" />
    <asp:ObjectDataSource ID="odsDistributionPoints" runat="server" TypeName="Eudoxus.Portal.DataSources.Views" SelectMethod="FindDistributionPointsWithCriteria"
        SelectCountMethod="CountDistributionPointsWithCriteria" EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsDistributionPoints_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
