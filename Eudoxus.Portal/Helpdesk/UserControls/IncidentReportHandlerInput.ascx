<%@ Control Language="C#" AutoEventWireup="true" Inherits="Eudoxus.Portal.Helpdesk.UserControls.IncidentReportHandlerInput"
    CodeBehind="IncidentReportHandlerInput.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v9.2" Namespace="DevExpress.Web.ASPxCallbackPanel"
    TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v9.2" Namespace="DevExpress.Web.ASPxPanel"
    TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Import Namespace="Eudoxus.BusinessModel" %>

<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Χειρισμού Συμβάντος
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Κατηγορία Χειριστή Συμβάντος:
        </th>
        <td>
            <asp:DropDownList ID="ddlHandlerType" runat="server" OnInit="ddlHandlerType_Init" Width="460px" />
            <asp:RequiredFieldValidator ID="rfvHandlerType" Display="Dynamic" runat="server"
                ControlToValidate="ddlHandlerType" ErrorMessage="Το πεδίο 'Κατηγορία Χειριστή Συμβάντος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Επικοινωνία με Χειριστή Συμβάντος:
        </th>
        <td>
            <asp:DropDownList ID="ddlHandlerStatus" runat="server" OnInit="ddlHandlerStatus_Init"
                Width="460px" />
            <asp:RequiredFieldValidator ID="rfvHandlerStatus" Display="Dynamic" runat="server"
                ControlToValidate="ddlHandlerStatus" ErrorMessage="Το πεδίο 'Επικοινωνία με Χειριστή Συμβάντος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
</table>