<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataCenterDetailsGridView.ascx.cs"
    Inherits="Eudoxus.Portal.Reports.UserControls.DataCenterDetailsGridView" %>
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
    <dxwgv:ASPxGridViewExporter ID="gveDataCenters" runat="server" GridViewID="gvDataCentersExport"
        OnRenderBrick="gveDataCenters_RenderBrick">
    </dxwgv:ASPxGridViewExporter>
    <dxwgv:ASPxGridView ID="gvDataCentersExport" runat="server" AutoGenerateColumns="False"
        EnableRowsCache="false" EnableCallBacks="true" Width="100%" DataSourceForceStandardPaging="true"
        Visible="false">
        <Columns>
            <dxwgv:GridViewDataDateColumn FieldName="RegistrationDate" Caption="Ημ/νία Εγγραφής"
                PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" />
            <dxwgv:GridViewDataTextColumn Name="VerificationStatus" Caption="Κατάσταση Πιστοποίησης" />
            <dxwgv:GridViewDataDateColumn FieldName="VerificationDate" Caption="Ημ/νία Πιστοποίησης"
                PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" />
            <dxwgv:GridViewDataTextColumn FieldName="ID" Caption="ID" />
            <dxwgv:GridViewDataTextColumn Name="Institution" Caption="Ίδρυμα" />
            <dxwgv:GridViewDataTextColumn FieldName="DataCenterPhone" Caption="Τηλέφωνο Γραφείου" />
            <dxwgv:GridViewDataTextColumn FieldName="DataCenterEmail" Caption="E-mail Γραφείου" />
            <dxwgv:GridViewDataTextColumn FieldName="DirectorName" Caption="Προϊστάμενος Γραφείου" />
            <dxwgv:GridViewDataTextColumn FieldName="DataCenterAddress" Caption="Διεύθυνση" />
            <dxwgv:GridViewDataTextColumn FieldName="DataCenterZipCode" Caption="Τ.Κ." />
            <dxwgv:GridViewDataTextColumn Name="DataCenterCity" Caption="Πόλη" />
            <dxwgv:GridViewDataTextColumn Name="DataCenterPrefecture" Caption="Νομός" />
            <dxwgv:GridViewDataTextColumn FieldName="ContactName" Caption="Ον/μο Υπευθύνου" />
            <dxwgv:GridViewDataTextColumn FieldName="ContactPhone" Caption="Τηλ. Υπευθύνου" />
            <dxwgv:GridViewDataTextColumn FieldName="ContactMobilePhone" Caption="Κινητό Υπευθύνου" />
            <dxwgv:GridViewDataTextColumn FieldName="ContactEmail" Caption="Email Υπευθύνου" />
            <dxwgv:GridViewDataTextColumn FieldName="AlternateContactName" Caption="Ον/μο Αναπληρωτή Υπευθύνου" />
            <dxwgv:GridViewDataTextColumn FieldName="AlternateContactPhone" Caption="Τηλ. Αναπληρωτή Υπευθύνου" />
            <dxwgv:GridViewDataTextColumn FieldName="AlternateContactMobilePhone" Caption="Κινητό Αναπληρωτή Υπευθύνου" />
            <dxwgv:GridViewDataTextColumn FieldName="AlternateContactEmail" Caption="Email Αναπληρωτή Υπευθύνου" />
        </Columns>
    </dxwgv:ASPxGridView>
</asp:PlaceHolder>
<dxwgv:ASPxGridView ID="gvDataCenters" runat="server" AutoGenerateColumns="False"
    KeyFieldName="ID" EnableRowsCache="false" EnableCallBacks="true" Width="100%"
    DataSourceForceStandardPaging="true" OnHtmlRowPrepared="gvDataCenters_HtmlRowPrepared">
    <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε..." />
    <SettingsPager PageSize="10" Summary-Text="Σελίδα {0} από {1} ({2} Γραφεία Μηχανογράφησης)"
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
        <dxwgv:GridViewDataTextColumn FieldName="DataCenterAcademicID" Caption="Ίδρυμα">
            <DataItemTemplate>
                <%# GetInstitutionDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="DataCenterPhone" Caption="Στοιχεία Επικοινωνίας">
            <DataItemTemplate>
                <%# GetDataCenterInfoDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="DataCenterAddress" Caption="Στοιχεία Διεύθυνσης Γραφείου">
            <DataItemTemplate>
                <%# GetAddressDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="ContactName" Caption="Στοιχεία Υπευθύνου">
            <DataItemTemplate>
                <%# GetContactDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="AlternateContactName" Caption="Στοιχεία Αναπληρωτή Υπευθύνου"
            HeaderStyle-Wrap="true">
            <DataItemTemplate>
                <%# GetAlternateContactDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
    </Columns>
</dxwgv:ASPxGridView>