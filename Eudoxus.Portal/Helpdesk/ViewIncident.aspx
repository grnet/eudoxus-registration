<%@ Page Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" EnableViewState="false"
    Inherits="Eudoxus.Portal.Helpdesk.ViewIncident" CodeBehind="ViewIncident.aspx.cs"
    Title="Προβολή Συμβάντος" %>

<%@ Register TagPrefix="my" TagName="IncidentReportView" Src="~/Helpdesk/UserControls/IncidentReportView.ascx" %>
<%@ Register TagPrefix="my" TagName="IncidentReportPostsView" Src="~/Helpdesk/UserControls/IncidentReportPostsView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div>
        <asp:PlaceHolder EnableViewState="false" ID="phFromSuccess" runat="server" Visible='<%# !string.IsNullOrEmpty(Request.QueryString["s"]) %>'>
            <p>
                <b>Η αναφορά καταχωρήθηκε με επιτυχία.</b>
            </p>
        </asp:PlaceHolder>
        <my:IncidentReportView ID="incidentReportView" runat="server" />
        <br />
        <my:IncidentReportPostsView ID="incidentReportPostsView" runat="server" />
        <p style="text-align: center">
            <a href="javascript:void(0)" onclick="window.parent.popUp.hide();" class="icon-btn bg-cancel">
                Κλείσιμο</a>
            <asp:LinkButton ID="btnSendEmail" runat="server" Text="Αποστολή Απάντησης" CssClass="icon-btn bg-email"
                OnClick="btnSendEmail_Click" OnClientClick="return confirm('Είστε σίγουροι ότι θέλετε να στείλετε την απάντηση με e-mail;');" />
        </p>
    </div>
    <asp:ObjectDataSource ID="odsIncidentReportPosts" runat="server" TypeName="Eudoxus.Portal.DataSources.IncidentReportPosts"
        SelectMethod="FindByIncidentReportID">
        <SelectParameters>
            <asp:QueryStringParameter Name="incidentReportID" Type="Int32" QueryStringField="id"
                DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
