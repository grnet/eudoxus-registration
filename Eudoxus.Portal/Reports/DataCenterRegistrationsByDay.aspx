<%@ Page Language="C#" MasterPageFile="~/Reports/Reports.Master" AutoEventWireup="true"
    CodeBehind="DataCenterRegistrationsByDay.aspx.cs" Inherits="Eudoxus.Portal.Reports.DataCenterRegistrationsByDay"
    Title="Εγγραφές Ανά Ημέρα" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2.Export, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div style="padding: 5px 0 5px 5px;">
        <dxe:ASPxButton ID="btnExport" runat="server" Image-Url="~/_img/iconExcel.png" Text="Εξαγωγή σε Excel"
            OnClick="btnExport_Click" UseSubmitBehavior="true" Style="float: left" />
        <div style="clear: both">
        </div>
    </div>
    <dxwgv:ASPxGridView ID="gvDataCenters" runat="server" AutoGenerateColumns="False" DataSourceID="sdsDataCenters"
        EnableRowsCache="false" EnableCallBacks="true">
        <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε..." />
        <Settings ShowFooter="true" ShowGroupFooter="VisibleAlways" />
        <Styles Footer-BackColor="DarkGray" Footer-Font-Bold="true" />
        <Templates>
            <EmptyDataRow>
                Δεν βρέθηκαν αποτελέσματα
            </EmptyDataRow>
        </Templates>
        <Columns>
            <dxwgv:GridViewDataColumn FieldName="CreatedAt" Caption="Ημερομηνία" Width="70px" />
            <dxwgv:GridViewDataTextColumn FieldName="DataCentersRegistered" Caption="Πλήθος Εγγραφών"
                Width="70px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                <PropertiesTextEdit DisplayFormatString="{0:n0}" />
                <DataItemTemplate>
                    <asp:PlaceHolder ID="phRegistrationsExist" runat="server" Visible='<%# !string.IsNullOrEmpty(Eval("DataCentersRegistered").ToString()) %>'>
                        <a id="lnkRegistrationsCreated" runat="server" class="hyperlink" href="javascript:void(0);"
                            onclick=<%# string.Format("window.open('DataCenterDetailsPopup.aspx?t=10&d={0}','','width=1024, height=768, directories=no, location=no, menubar=no, resizable=yes, scrollbars=1, status=no, toolbar=no');", string.Format("{0:dd/MM/yyyy}",Eval("CreatedAt")))%>>
                            <%# string.Format("{0:n0}", Eval("DataCentersRegistered"))%>
                        </a>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phRegistrationsNotExist" runat="server" Visible='<%# string.IsNullOrEmpty(Eval("DataCentersRegistered").ToString()) %>'>
                        &nbsp; </asp:PlaceHolder>
                </DataItemTemplate>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="DataCentersVerified" Caption="Πλήθος Πιστοποιήσεων"
                Width="80px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                <PropertiesTextEdit DisplayFormatString="{0:n0}" />
                <DataItemTemplate>
                    <asp:PlaceHolder ID="phVerificationsExist" runat="server" Visible='<%# !string.IsNullOrEmpty(Eval("DataCentersVerified").ToString()) %>'>
                        <a id="lnkVerificationsCreated" runat="server" class="hyperlink" style="font-weight:normal" href="javascript:void(0);"
                            onclick=<%# string.Format("window.open('DataCenterDetailsPopup.aspx?t=11&d={0}','','width=1024, height=768, directories=no, location=no, menubar=no, resizable=yes, scrollbars=1, status=no, toolbar=no');", string.Format("{0:dd/MM/yyyy}",Eval("CreatedAt")))%>>
                            <%# string.Format("{0:n0}", Eval("DataCentersVerified"))%>
                        </a>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phVerificationsNotExist" runat="server" Visible='<%# string.IsNullOrEmpty(Eval("DataCentersVerified").ToString()) %>'>
                        &nbsp; </asp:PlaceHolder>
                </DataItemTemplate>
            </dxwgv:GridViewDataTextColumn>
        </Columns>
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="DataCentersRegistered" SummaryType="Sum" DisplayFormat="{0:n0}" />
            <dxwgv:ASPxSummaryItem FieldName="DataCentersVerified" SummaryType="Sum" DisplayFormat="{0:n0}" />
        </TotalSummary>
    </dxwgv:ASPxGridView>
    <dxwgv:ASPxGridViewExporter ID="gveDataCenters" runat="server" GridViewID="gvDataCenters">
    </dxwgv:ASPxGridViewExporter>
    <asp:SqlDataSource ID="sdsDataCenters" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
        SelectCommand="sp_DataCenterRegistrationsByDay" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="StartDate" Type="DateTime" DefaultValue="07/06/2010" />
        </SelectParameters>
    </asp:SqlDataSource>
    <dxpc:ASPxPopupControl runat="server" ID="dxpcPopup" AllowDragging="true" HeaderText="Λεπτομέρειες Εγγραφών"
        Height="600" Width="800" Modal="true" PopupHorizontalAlign="WindowCenter" ClientInstanceName="devExPopup"
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
        <ClientSideEvents CloseUp="function(s,e){popUp.hide();}" />
    </dxpc:ASPxPopupControl>
</asp:Content>
