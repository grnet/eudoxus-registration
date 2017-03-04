﻿<%@ Page Language="C#" MasterPageFile="~/Reports/Reports.Master" AutoEventWireup="true"
    CodeBehind="PublisherRegistrationsByDay.aspx.cs" Inherits="Eudoxus.Portal.Reports.PublisherRegistrationsByDay"
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
    <dxwgv:ASPxGridView ID="gvPublishers" runat="server" AutoGenerateColumns="False" DataSourceID="sdsPublishers"
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
            <dxwgv:GridViewDataTextColumn FieldName="PublishersRegistered" Caption="Πλήθος Εγγραφών"
                Width="70px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                <PropertiesTextEdit DisplayFormatString="{0:n0}" />
                <DataItemTemplate>
                    <asp:PlaceHolder ID="phRegistrationsExist" runat="server" Visible='<%# !string.IsNullOrEmpty(Eval("PublishersRegistered").ToString()) %>'>
                        <a id="lnkRegistrationsCreated" runat="server" class="hyperlink" href="javascript:void(0);"
                            onclick=<%# string.Format("window.open('RegisteredPublishersDetails.aspx?t=10&registrationDate={0}','','width=1024, height=768, directories=no, location=no, menubar=no, resizable=yes, scrollbars=1, status=no, toolbar=no');", string.Format("{0:dd/MM/yyyy}",Eval("CreatedAt")))%>>
                            <%# string.Format("{0:n0}", Eval("PublishersRegistered"))%>
                        </a>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phRegistrationsNotExist" runat="server" Visible='<%# string.IsNullOrEmpty(Eval("PublishersRegistered").ToString()) %>'>
                        &nbsp; </asp:PlaceHolder>
                </DataItemTemplate>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="PublishersVerified" Caption="Πλήθος Πιστοποιήσεων"
                Width="80px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                <PropertiesTextEdit DisplayFormatString="{0:n0}" />
                <DataItemTemplate>
                    <asp:PlaceHolder ID="phVerificationsExist" runat="server" Visible='<%# !string.IsNullOrEmpty(Eval("PublishersVerified").ToString()) %>'>
                        <a id="lnkVerificationsCreated" runat="server" class="hyperlink" style="font-weight:normal" href="javascript:void(0);"
                            onclick=<%# string.Format("window.open('RegisteredPublishersDetails.aspx?t=11&verificationDate={0}','','width=1024, height=768, directories=no, location=no, menubar=no, resizable=yes, scrollbars=1, status=no, toolbar=no');", string.Format("{0:dd/MM/yyyy}",Eval("CreatedAt")))%>>
                            <%# string.Format("{0:n0}", Eval("PublishersVerified"))%>
                        </a>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phVerificationsNotExist" runat="server" Visible='<%# string.IsNullOrEmpty(Eval("PublishersVerified").ToString()) %>'>
                        &nbsp; </asp:PlaceHolder>
                </DataItemTemplate>
            </dxwgv:GridViewDataTextColumn>
        </Columns>
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="PublishersRegistered" SummaryType="Sum" DisplayFormat="{0:n0}" />
            <dxwgv:ASPxSummaryItem FieldName="PublishersVerified" SummaryType="Sum" DisplayFormat="{0:n0}" />
        </TotalSummary>
    </dxwgv:ASPxGridView>
    <dxwgv:ASPxGridViewExporter ID="gvePublshers" runat="server" GridViewID="gvPublishers">
    </dxwgv:ASPxGridViewExporter>
    <asp:SqlDataSource ID="sdsPublishers" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
        SelectCommand="sp_PublisherRegistrationsByDay" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="StartDate" Type="DateTime" DefaultValue="21/04/2010" />
        </SelectParameters>
    </asp:SqlDataSource>
    <dxpc:ASPxPopupControl runat="server" ID="dxpcPopup" AllowDragging="true" HeaderText="Λεπτομέρειες Εγγραφών"
        Height="600" Width="800" Modal="true" PopupHorizontalAlign="WindowCenter" ClientInstanceName="devExPopup"
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
        <ClientSideEvents CloseUp="function(s,e){popUp.hide();}" />
    </dxpc:ASPxPopupControl>
</asp:Content>
