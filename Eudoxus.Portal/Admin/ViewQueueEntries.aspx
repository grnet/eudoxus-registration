<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" Inherits="Eudoxus.Portal.Admin.ViewQueueEntries"
    Title="Προβολή Service Queue" CodeBehind="ViewQueueEntries.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView"
    TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <dxwgv:ASPxGridView ID="gvQueueEntries" runat="server" AutoGenerateColumns="False" DataSourceID="edsQueueEntries" KeyFieldName="ID"
        EnableRowsCache="False" Width="100%" OnHtmlDataCellPrepared="gvQueueEntries_HtmlDataCellPrepared">
        <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε..." />
        <SettingsPager PageSize="10" Summary-Text="Σελίδα {0} από {1} ({2} Entries)" Summary-Position="Left" />
        <Columns>
            <dxwgv:GridViewDataTextColumn FieldName="ID">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="QueueDataID">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataColumn Name="QueueData">
            </dxwgv:GridViewDataColumn>
        </Columns>
        <SettingsPager>
            <Summary Text="Σελίδα {0} από {1} ({2} Entries)"></Summary>
        </SettingsPager>
        <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε....."></SettingsLoadingPanel>
        <Styles>
            <Cell Font-Size="11px" />
            <Cell Font-Size="11px">
            </Cell>
        </Styles>
        <Templates>
            <EmptyDataRow>
                Δεν βρέθηκαν αποτελέσματα
            </EmptyDataRow>
        </Templates>
    </dxwgv:ASPxGridView>
    <asp:EntityDataSource ID="edsQueueEntries" runat="server" ConnectionString="name=HelpDeskEntities" DefaultContainerName="HelpDeskEntities"
        EnableFlattening="False" EntitySetName="QueueEntrySet" EntityTypeFilter="QueueEntry" />
</asp:Content>
