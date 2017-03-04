<%@ Page Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" Inherits="Eudoxus.Portal.Helpdesk.AddIncidentReportPost"
    CodeBehind="AddIncidentReportPost.aspx.cs" Title="Προβολή Συμβάντος" %>

<%@ Register Src="~/Helpdesk/UserControls/IncidentReportView.ascx" TagName="IncidentReportView"
    TagPrefix="my" %>
<%@ Register Src="~/Helpdesk/UserControls/IncidentReportPostsView.ascx" TagName="IncidentReportPostsView"
    TagPrefix="my" %>
<%@ Register Src="~/Helpdesk/UserControls/IncidentReportPostInput.ascx" TagName="IncidentReportPostInput"
    TagPrefix="my" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div style="padding-left: 10px">
        <asp:LinkButton ID="lnkUnlockReport" runat="server" Text="Ξεκλείδωμα Συμβάντος" CssClass="icon-btn bg-unlock"
            OnClick="btnUnlockReport_Click" OnClientClick="return confirm('Μετά το ξεκλείδωμα, το συμβάν θα τεθεί σε κατάσταση «Εκκρεμεί» και θα επιτρέπεται η επεξεργασία του και η προσθήκη απαντήσεων. Είστε σίγουροι ότι θέλετε να συνεχίσετε;')" />
    </div>
    <div style="margin: 10px;">
        <my:IncidentReportView ID="ucIncidentReportView" runat="server" />
        <br />
        <my:IncidentReportPostsView ID="ucIncidentReportPostsView" runat="server" />
        <br />
        <my:IncidentReportPostInput ID="ucIncidentReportPostInput" runat="server" />
    </div>
    <table id="tbActions" runat="server" style="width: 100%">
        <tr>
            <td colspan="2" style="text-align: left; padding-left: 7px;">
                <asp:LinkButton ID="btnSubmit" runat="server" Text="Προσθήκη" CssClass="icon-btn bg-accept"
                    OnClick="btnSubmit_Click" OnClientClick="return ASPxClientEdit.ValidateGroup();" />
                <asp:LinkButton ID="btnCancel" runat="server" Text="Ακύρωση" CssClass="icon-btn bg-cancel"
                    CausesValidation="false" OnClientClick="window.parent.popUp.hide();" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblValidationErrors" runat="server" CssClass="error" EnableViewState="false" />
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="odsIncidentReportPosts" runat="server" TypeName="Eudoxus.Portal.DataSources.IncidentReportPosts"
        SelectMethod="FindByIncidentReportID">
        <SelectParameters>
            <asp:QueryStringParameter Name="incidentReportID" Type="Int32" QueryStringField="id"
                DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
