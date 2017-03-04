<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublisherDetailsGridView.ascx.cs"
    Inherits="Eudoxus.Portal.Reports.UserControls.PublisherDetailsGridView" %>
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
    <dxwgv:ASPxGridViewExporter ID="gveSecretaries" runat="server" GridViewID="gvSecretariesExport"
        OnRenderBrick="gveSecretaries_RenderBrick">
    </dxwgv:ASPxGridViewExporter>
    <dxwgv:ASPxGridView ID="gvSecretariesExport" runat="server" AutoGenerateColumns="False"
        EnableRowsCache="false" EnableCallBacks="true" Width="100%" DataSourceForceStandardPaging="true"
        Visible="false">
        <Columns>
            <dxwgv:GridViewDataDateColumn FieldName="RegistrationDate" Caption="Ημ/νία Εγγραφής"
                PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" />
            <dxwgv:GridViewDataTextColumn Name="VerificationStatus" Caption="Κατάσταση Πιστοποίησης" />
            <dxwgv:GridViewDataDateColumn FieldName="VerificationDate" Caption="Ημ/νία Πιστοποίησης"
                PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" />
            <dxwgv:GridViewDataTextColumn Name="Institution" Caption="Ίδρυμα" />
            <dxwgv:GridViewDataTextColumn Name="School" Caption="Σχολή" />
            <dxwgv:GridViewDataTextColumn Name="Department" Caption="Τμήμα" />
            <dxwgv:GridViewDataTextColumn FieldName="SecretaryPhone" Caption="Τηλέφωνο Γραμματείας" />
            <dxwgv:GridViewDataTextColumn FieldName="SecretaryEmail" Caption="E-mail Γραμματείας" />
            <dxwgv:GridViewDataTextColumn Name="RepresentativeType" Caption="Πρόεδρος ή Προϊστάμενος" />
            <dxwgv:GridViewDataTextColumn FieldName="RepresentativeName" Caption="Ον/μο Προέδρου ή Προϊσταμένου" />
            <dxwgv:GridViewDataTextColumn FieldName="SecretaryAddress" Caption="Διεύθυνση" />
            <dxwgv:GridViewDataTextColumn FieldName="SecretaryZipCode" Caption="Τ.Κ." />
            <dxwgv:GridViewDataTextColumn Name="SecretaryCity" Caption="Πόλη" />
            <dxwgv:GridViewDataTextColumn Name="SecretaryPrefecture" Caption="Νομός" />
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
<dxwgv:ASPxGridView ID="gvSecretaries" runat="server" AutoGenerateColumns="False"
    KeyFieldName="ID" EnableRowsCache="false" EnableCallBacks="true" Width="100%"
    DataSourceForceStandardPaging="true" OnHtmlRowPrepared="gvSecretaries_HtmlRowPrepared">
    <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε..." />
    <SettingsPager PageSize="10" Summary-Text="Σελίδα {0} από {1} ({2} Γραμματείες)"
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
        <dxwgv:GridViewDataTextColumn FieldName="SecretaryAcademicID" Caption="Στοιχεία Σχολής/Τμήματος">
            <DataItemTemplate>
                <%# GetAcademicDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="SecretaryPhone" Caption="Στοιχεία Επικοινωνίας">
            <DataItemTemplate>
                <%# GetSecretaryInfoDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="SecretaryAddress" Caption="Στοιχεία Διεύθυνσης Γραμματείας">
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