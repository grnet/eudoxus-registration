<%@ Page Language="C#" MasterPageFile="~/Reports/Reports.Master" AutoEventWireup="true"
    CodeBehind="ViewRegisteredBookSuppliers.aspx.cs" Inherits="Eudoxus.Portal.Reports.ViewRegisteredBookSuppliers"
    Title="Στοιχεία Εγγεγραμμένων Υπεύθυνων Παραγγελίας Βιβλίων" %>

<%@ Register Src="~/Reports/UserControls/BookSupplierDetailsGridView.ascx" TagName="BookSupplierDetailsGridView" TagPrefix="my" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <my:BookSupplierDetailsGridView ID="gvBookSupplierDetails" runat="server" DataSourceID="odsBookSuppliers" EnableExport="true" />
    <asp:ObjectDataSource ID="odsBookSuppliers" runat="server" TypeName="Eudoxus.Portal.DataSources.Views" SelectMethod="FindBookSuppliersWithCriteria"
        SelectCountMethod="CountBookSuppliersWithCriteria" EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsBookSuppliers_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
