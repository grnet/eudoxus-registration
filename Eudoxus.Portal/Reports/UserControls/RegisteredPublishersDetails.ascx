<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisteredPublishersDetails.ascx.cs"
    Inherits="Eudoxus.Portal.Reports.UserControls.RegisteredPublishersDetails" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2.Export, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>
<%@ Import Namespace="Eudoxus.BusinessModel" %>
<style type="text/css">
    .dxgvHeader td
    {
        font-size: 11px;
    }
    
    .dxgv
    {
        font-size: 11px;
    }
</style>
<div style="padding: 5px 0 5px 5px;">
    <dxe:ASPxButton ID="btnExport" runat="server" Image-Url="~/_img/iconExcel.png" Text="Εξαγωγή σε Excel"
        OnClick="btnExport_Click" UseSubmitBehavior="true" Style="float: left" />
    <div style="clear: both">
    </div>
</div>
<dxwgv:ASPxGridView ID="gvPublishers" runat="server" AutoGenerateColumns="False"
    DataSourceID="sdsPublishers" EnableRowsCache="false" EnableCallBacks="true" OnHtmlRowPrepared="gvPublishers_HtmlRowPrepared"
    Width="100%">
    <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε..." />
    <SettingsPager PageSize="10" Summary-Text="Σελίδα {0} από {1} ({2} Εκδότες)" Summary-Position="Left" />
    <Settings ShowFilterRow="true" />
    <SettingsBehavior AutoFilterRowInputDelay="900" />
    <Templates>
        <EmptyDataRow>
            Δεν βρέθηκαν αποτελέσματα
        </EmptyDataRow>
    </Templates>
    <Columns>
        <dxwgv:GridViewDataTextColumn FieldName="PublisherType" Caption="Τύπος Εκδότη" HeaderStyle-Wrap="true"
            CellStyle-HorizontalAlign="Left">
            <DataItemTemplate>
                <%# GetPublisherType(Container.DataItem) %>
            </DataItemTemplate>
            <Settings AutoFilterCondition="Contains" />
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="PublisherTradeName" Caption="Στοιχεία Εκδότη"
            HeaderStyle-Wrap="true">
            <DataItemTemplate>
                <%# GetPublisherDetails(Container.DataItem) %>
            </DataItemTemplate>
            <Settings AutoFilterCondition="Contains" />
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="PublisherPrefecture" Caption="Στοιχεία Διεύθυνσης"
            HeaderStyle-Wrap="true">
            <DataItemTemplate>
                <%# Eval("PublisherAddress") %><br />
                <%# Eval("PublisherZipCode") %><br />
                <%# Eval("PublisherCity") %><br />
                <%# Eval("PublisherPrefecture") %>
            </DataItemTemplate>
            <Settings AutoFilterCondition="Contains" />
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="LegalPersonName" Caption="Στοιχεία Νομίμου Εκπροσώπου"
            HeaderStyle-Wrap="true">
            <DataItemTemplate>
                <%# GetLegalPersonDetails(Container.DataItem) %>
            </DataItemTemplate>
            <Settings AutoFilterCondition="Contains" />
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="ContactName" Caption="Στοιχεία Υπευθύνου"
            HeaderStyle-Wrap="true">
            <DataItemTemplate>
                <%# Eval("ContactName") %><br />
                <%# Eval("ContactPhone") %><br />
                <%# Eval("ContactMobilePhone") %><br />
                <%# Eval("ContactEmail") %>
            </DataItemTemplate>
            <Settings AutoFilterCondition="Contains" />
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="AlternateContactName" Caption="Στοιχεία Αναπληρωτή Υπευθύνου"
            HeaderStyle-Wrap="true">
            <DataItemTemplate>
                <%# GetAlternateContactDetails(Container.DataItem) %>
            </DataItemTemplate>
            <Settings AutoFilterCondition="Contains" />
        </dxwgv:GridViewDataTextColumn>
    </Columns>
</dxwgv:ASPxGridView>
<dxwgv:ASPxGridView ID="gvPublishersExport" runat="server" AutoGenerateColumns="False"
    DataSourceID="sdsPublishers" EnableRowsCache="false" EnableCallBacks="true" Width="100%"
    Visible="false">
    <Columns>
        <dxwgv:GridViewDataTextColumn FieldName="VerificationStatus" Caption="Πιστοποιημένος" />
        <dxwgv:GridViewDataTextColumn FieldName="IsActive" Caption="Ενεργός" />
        <dxwgv:GridViewDataTextColumn FieldName="PublisherType" Caption="Τύπος Εκδότη" />
        <dxwgv:GridViewDataTextColumn FieldName="PublisherID" Caption="ID" />
        <dxwgv:GridViewDataTextColumn FieldName="PublisherName" Caption="Επωνυμία - Ον/μο Αυτοεκδότη" />
        <dxwgv:GridViewDataTextColumn FieldName="PublisherTradeName" Caption="Διακριτικός Τίτλος" />
        <dxwgv:GridViewDataTextColumn FieldName="PublisherAFM" Caption="Α.Φ.Μ." />
        <dxwgv:GridViewDataTextColumn FieldName="PublisherAddress" Caption="Διεύθυνση" />
        <dxwgv:GridViewDataTextColumn FieldName="PublisherZipCode" Caption="Τ.Κ." />
        <dxwgv:GridViewDataTextColumn FieldName="PublisherCity" Caption="Πόλη" />
        <dxwgv:GridViewDataTextColumn FieldName="PublisherPrefecture" Caption="Νομός" />
        <dxwgv:GridViewDataTextColumn FieldName="LegalPersonName" Caption="Ον/μο Νομίμου Εκπροσώπου" />
        <dxwgv:GridViewDataTextColumn FieldName="LegalPersonPhone" Caption="Τηλ. Νομίμου Εκπροσώπου" />
        <dxwgv:GridViewDataTextColumn FieldName="LegalPersonEmail" Caption="Email Νομίμου Εκπροσώπου" />
        <dxwgv:GridViewDataTextColumn FieldName="ContactName" Caption="Ον/μο Υπευθύνου" />
        <dxwgv:GridViewDataTextColumn FieldName="ContactPhone" Caption="Τηλ. Υπευθύνου" />
        <dxwgv:GridViewDataTextColumn FieldName="ContactMobilePhone" Caption="Κινητό Υπευθύνου" />
        <dxwgv:GridViewDataTextColumn FieldName="ContactEmail" Caption="Email Υπευθύνου" />
        <dxwgv:GridViewDataTextColumn FieldName="AlternateContactName" Caption="Ον/μο Αναπληρωτή Υπευθύνου" />
        <dxwgv:GridViewDataTextColumn FieldName="AlternateContactPhone" Caption="Τηλ. Αναπληρωτή Υπευθύνου" />
        <dxwgv:GridViewDataTextColumn FieldName="AlternateContactMobilePhone" Caption="Κινητό Αναπληρωτή Υπευθύνου" />
        <dxwgv:GridViewDataTextColumn FieldName="AlternateContactEmail" Caption="Email Αναπληρωτή Υπευθύνου" />
        <dxwgv:GridViewDataTextColumn FieldName="HasLogisticBooks" Caption="Υπόχρεος Τήρησης Λογιστικών Βιβλίων" />
    </Columns>
</dxwgv:ASPxGridView>
<dxwgv:ASPxGridViewExporter ID="gvePublishers" runat="server" GridViewID="gvPublishersExport"
    OnRenderBrick="gvePublishers_RenderBrick">
</dxwgv:ASPxGridViewExporter>
<asp:SqlDataSource ID="sdsPublishers" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
    ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [vRegisteredPublishers]"
    OnSelecting="sdsPublishers_Selecting"></asp:SqlDataSource>
