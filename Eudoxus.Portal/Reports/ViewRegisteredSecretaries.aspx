<%@ Page Language="C#" MasterPageFile="~/Reports/Reports.Master" AutoEventWireup="true"
    CodeBehind="ViewRegisteredSecretaries.aspx.cs" Inherits="Eudoxus.Portal.Reports.ViewRegisteredSecretaries"
    Title="Στοιχεία Εγγεγραμμένων Γραμματειών" %>

<%@ Register Src="~/Reports/UserControls/SecretaryDetailsGridView.ascx" TagName="SecretaryDetailsGridView" TagPrefix="my" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <my:SecretaryDetailsGridView ID="gvSecretaryDetails" runat="server" DataSourceID="odsSecretaries" EnableExport="true" />
    <asp:ObjectDataSource ID="odsSecretaries" runat="server" TypeName="Eudoxus.Portal.DataSources.Views" SelectMethod="FindSecretariesWithCriteria"
        SelectCountMethod="CountSecretariesWithCriteria" EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsSecretaries_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
