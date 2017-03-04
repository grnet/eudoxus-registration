<%@ Page Language="C#" MasterPageFile="~/Reports/Reports.Master" AutoEventWireup="true"
    CodeBehind="ViewRegisteredLibraries.aspx.cs" Inherits="Eudoxus.Portal.Reports.ViewRegisteredLibraries"
    Title="Στοιχεία Εγγεγραμμένων Βιβλιοθηκών" %>

<%@ Register Src="~/Reports/UserControls/LibraryDetailsGridView.ascx" TagName="LibraryDetailsGridView" TagPrefix="my" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <my:LibraryDetailsGridView ID="gvLibraryDetails" runat="server" DataSourceID="odsLibraries" EnableExport="true" />
    <asp:ObjectDataSource ID="odsLibraries" runat="server" TypeName="Eudoxus.Portal.DataSources.Views" SelectMethod="FindLibrariesWithCriteria"
        SelectCountMethod="CountLibrariesWithCriteria" EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsLibraries_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
