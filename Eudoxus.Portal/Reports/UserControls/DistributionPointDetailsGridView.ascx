<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DistributionPointDetailsGridView.ascx.cs"
    Inherits="Eudoxus.Portal.Reports.UserControls.DistributionPointDetailsGridView" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2.Export, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>
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
<asp:PlaceHolder ID="phExport" runat="server">
    <div style="padding: 5px 0px 10px;">
        <asp:LinkButton ID="btnExport" runat="server" Text="Εξαγωγή σε Excel" OnClick="btnExport_Click"
            CssClass="icon-btn bg-excel" />
    </div>
    <dxwgv:ASPxGridViewExporter ID="gveDistributionPoints" runat="server" GridViewID="gvDistributionPointsExport"
        OnRenderBrick="gveDistributionPoints_RenderBrick">
    </dxwgv:ASPxGridViewExporter>
    <dxwgv:ASPxGridView ID="gvDistributionPointsExport" runat="server" AutoGenerateColumns="False"
        EnableRowsCache="false" EnableCallBacks="true" Width="100%" DataSourceForceStandardPaging="true"
        Visible="false">
        <Columns>
            <dxwgv:GridViewDataDateColumn FieldName="RegistrationDate" Caption="Ημ/νία Εγγραφής"
                PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" />
            <dxwgv:GridViewDataTextColumn Name="VerificationStatus" Caption="Κατάσταση Πιστοποίησης" />
            <dxwgv:GridViewDataDateColumn FieldName="VerificationDate" Caption="Ημ/νία Πιστοποίησης"
                PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" />
            <dxwgv:GridViewDataTextColumn FieldName="ID" Caption="ID" />
            <dxwgv:GridViewDataTextColumn Name="DistributionPointType" Caption="Τύπος" />
            <dxwgv:GridViewDataTextColumn Name="DistributionPointInstitution" Caption="Ίδρυμα" />
            <dxwgv:GridViewDataTextColumn FieldName="DistributionPointName" Caption="Τίτλος" />
            <dxwgv:GridViewDataTextColumn FieldName="DistributionPointOpeningHours" Caption="Ωράριο Λειτουργίας" />
            <dxwgv:GridViewDataTextColumn FieldName="DistributionPointPhone" Caption="Τηλέφωνο (σταθερό)" />
            <dxwgv:GridViewDataTextColumn FieldName="DistributionPointMobilePhone" Caption="Τηλέφωνο (κινητό)" />
            <dxwgv:GridViewDataTextColumn FieldName="DistributionPointFax" Caption="Fax" />
            <dxwgv:GridViewDataTextColumn FieldName="DistributionPointEmail" Caption="Email" />
            <dxwgv:GridViewDataTextColumn FieldName="DistributionPointURL" Caption="URL" />
            <dxwgv:GridViewDataTextColumn FieldName="DistributionPointAddress" Caption="Διεύθυνση" />
            <dxwgv:GridViewDataTextColumn FieldName="DistributionPointZipCode" Caption="Τ.Κ." />
            <dxwgv:GridViewDataTextColumn Name="DistributionPointCity" Caption="Πόλη" />
            <dxwgv:GridViewDataTextColumn Name="DistributionPointPrefecture" Caption="Νομός" />
            <dxwgv:GridViewDataTextColumn FieldName="DistributionPointLocationURL" Caption="URL σε Χαρτογραφικό Σύστημα" />
            <dxwgv:GridViewDataTextColumn FieldName="ContactName" Caption="Ον/μο Υπευθύνου" />
            <dxwgv:GridViewDataTextColumn FieldName="ContactPhone" Caption="Τηλ. Υπευθύνου" />
            <dxwgv:GridViewDataTextColumn FieldName="ContactMobilePhone" Caption="Κινητό Υπευθύνου" />
            <dxwgv:GridViewDataTextColumn FieldName="ContactEmail" Caption="Email Υπευθύνου" />
        </Columns>
    </dxwgv:ASPxGridView>
</asp:PlaceHolder>
<dxwgv:ASPxGridView ID="gvDistributionPoints" runat="server" AutoGenerateColumns="False"
    KeyFieldName="ID" EnableRowsCache="false" EnableCallBacks="true" Width="100%"
    DataSourceForceStandardPaging="true" OnHtmlRowPrepared="gvDistributionPoints_HtmlRowPrepared">
    <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε..." />
    <SettingsPager PageSize="10" Summary-Text="Σελίδα {0} από {1} ({2} Σημεία Διανομής)"
        Summary-Position="Left" />
    <Templates>
        <EmptyDataRow>
            Δεν βρέθηκαν αποτελέσματα
        </EmptyDataRow>
    </Templates>
    <Styles>
        <Header Wrap="True" />
        <Cell HorizontalAlign="Left" />
    </Styles>
    <Columns>
        <dxwgv:GridViewDataTextColumn FieldName="RegistrationDate" Name="RegistrationDate"
            Caption="Ημ/νία Εγγραφής" CellStyle-Wrap="False" SortIndex="0" SortOrder="Descending">
            <DataItemTemplate>
                <%# ((DateTime)Eval("RegistrationDate")).ToString("dd/MM/yyyy HH:mm")%>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="VerificationStatus" Caption="Κατάσταση Πιστοποίησης">
            <DataItemTemplate>
                <%# GetVerificationDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="DistributionPointType" Caption="Τύπος Σημείου Διανομής">
            <DataItemTemplate>
                <%# GetDistributionPointTypeDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="DistributionPointAcademicID" Caption="Στοιχεία Σημείου Διανομής">
            <DataItemTemplate>
                <%# GetDistributionPointDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="DistributionPointAddress" Caption="Στοιχεία Διεύθυνσης">
            <DataItemTemplate>
                <%# GetAddressDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="ContactName" Caption="Στοιχεία Υπευθύνου">
            <DataItemTemplate>
                <%# GetContactDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
    </Columns>
</dxwgv:ASPxGridView>