<%@ Page Language="C#" MasterPageFile="~/Reports/Reports.Master" AutoEventWireup="true"
    CodeBehind="ViewRegisteredDataCenters.aspx.cs" Inherits="Eudoxus.Portal.Reports.ViewRegisteredDataCenters"
    Title="Στοιχεία Εγγεγραμμένων Γραφείων Μηχανογράφησης" %>

<%@ Register Src="~/Reports/UserControls/DataCenterDetailsGridView.ascx" TagName="DataCenterDetailsGridView" TagPrefix="my" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <my:DataCenterDetailsGridView ID="gvDataCenterDetails" runat="server" DataSourceID="odsDataCenters" EnableExport="true" />
    <asp:ObjectDataSource ID="odsDataCenters" runat="server" TypeName="Eudoxus.Portal.DataSources.Views" SelectMethod="FindDataCentersWithCriteria"
        SelectCountMethod="CountDataCentersWithCriteria" EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsDataCenters_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
