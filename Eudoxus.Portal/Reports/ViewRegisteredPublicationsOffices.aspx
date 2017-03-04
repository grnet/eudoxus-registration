<%@ Page Language="C#" MasterPageFile="~/Reports/Reports.Master" AutoEventWireup="true"
    CodeBehind="ViewRegisteredPublicationsOffices.aspx.cs" Inherits="Eudoxus.Portal.Reports.ViewRegisteredPublicationsOffices"
    Title="Στοιχεία Εγγεγραμμένων Γραφείων Διδακτικών Συγγραμμάτων" %>

<%@ Register Src="~/Reports/UserControls/PublicationsOfficeDetailsGridView.ascx" TagName="PublicationsOfficeDetailsGridView" TagPrefix="my" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <my:PublicationsOfficeDetailsGridView ID="gvPublicationsOfficeDetails" runat="server" DataSourceID="odsPublicationsOffices" EnableExport="true" />
    <asp:ObjectDataSource ID="odsPublicationsOffices" runat="server" TypeName="Eudoxus.Portal.DataSources.Views" SelectMethod="FindPublicationsOfficesWithCriteria"
        SelectCountMethod="CountPublicationsOfficesWithCriteria" EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsPublicationsOffices_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
