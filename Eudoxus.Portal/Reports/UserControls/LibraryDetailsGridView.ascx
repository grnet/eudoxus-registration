<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LibraryDetailsGridView.ascx.cs"
    Inherits="Eudoxus.Portal.Reports.UserControls.LibraryDetailsGridView" %>
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
    <dxwgv:ASPxGridViewExporter ID="gveLibraries" runat="server" GridViewID="gvLibrariesExport"
        OnRenderBrick="gveLibraries_RenderBrick">
    </dxwgv:ASPxGridViewExporter>
    <dxwgv:ASPxGridView ID="gvLibrariesExport" runat="server" AutoGenerateColumns="False"
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
            <dxwgv:GridViewDataTextColumn FieldName="LibraryName" Caption="Τίτλος" />
            <dxwgv:GridViewDataTextColumn FieldName="LibraryOpeningHours" Caption="Ωράριο Λειτουργίας" />
            <dxwgv:GridViewDataTextColumn FieldName="LibraryPhone" Caption="Τηλέφωνο Βιβλιοθήκης" />
            <dxwgv:GridViewDataTextColumn FieldName="LibraryEmail" Caption="E-mail Βιβλιοθήκης" />
            <dxwgv:GridViewDataTextColumn FieldName="LibraryURL" Caption="URL Βιβλιοθήκης" />
            <dxwgv:GridViewDataTextColumn FieldName="DirectorName" Caption="Προϊστάμενος Βιβλιοθήκης" />
            <dxwgv:GridViewDataTextColumn FieldName="LibraryAddress" Caption="Διεύθυνση" />
            <dxwgv:GridViewDataTextColumn FieldName="LibraryZipCode" Caption="Τ.Κ." />
            <dxwgv:GridViewDataTextColumn Name="LibraryCity" Caption="Πόλη" />
            <dxwgv:GridViewDataTextColumn Name="LibraryPrefecture" Caption="Νομός" />
            <dxwgv:GridViewDataTextColumn FieldName="LibraryLocationURL" Caption="URL σε Χαρτογραφικό Σύστημα" />
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
<dxwgv:ASPxGridView ID="gvLibraries" runat="server" AutoGenerateColumns="False"
    KeyFieldName="ID" EnableRowsCache="false" EnableCallBacks="true" Width="100%"
    DataSourceForceStandardPaging="true" OnHtmlRowPrepared="gvLibraries_HtmlRowPrepared">
    <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε..." />
    <SettingsPager PageSize="10" Summary-Text="Σελίδα {0} από {1} ({2} Βιβλιοθήκες)"
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
        <dxwgv:GridViewDataTextColumn FieldName="LibraryAcademicID" Caption="Στοιχεία Βιβλιοθήκης">
            <DataItemTemplate>
                <%# GetInstitutionDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="LibraryPhone" Caption="Στοιχεία Επικοινωνίας">
            <DataItemTemplate>
                <%# GetLibraryInfoDetails(Container.DataItem) %>
            </DataItemTemplate>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="LibraryAddress" Caption="Στοιχεία Διεύθυνσης Βιβλιοθήκης">
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