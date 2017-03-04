<%@ Control Language="C#" AutoEventWireup="true" Inherits="Eudoxus.Portal.UserControls.EbookPublisherInput" CodeBehind="EbookPublisherInput.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Src="~/UserControls/TipIcon.ascx" TagPrefix="my" TagName="TipIcon" %>
<%@ Register Src="~/UserControls/IdentityControl.ascx" TagName="IdentityControl" TagPrefix="my" %>
<%@ Import Namespace="Eudoxus.BusinessModel" %>

<table width="100%" class="dv">
    <tr id="trSelfPublisher">
        <th colspan="2" class="header">
            &raquo; Στοιχεία Διαθέτη Δωρεάν Ηλεκτρονικών Βοηθημάτων και Σημειώσεων
        </th>
    </tr>
    <tr>        
        <th style="width: 30%">
            Κατηγορία:
        </th>
        <td style="font-weight: bold; color: Blue">
            <%= enPublisherType.EbookPublisher.GetLabel() %>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Ονοματεπώνυμο:
            <my:TipIcon ID="tipPublisherName" runat="server" Text="<%$ Resources:EbookPublisherInput, PublisherName %>" />
        </th>
        <td>
            <asp:TextBox ID="txtPublisherName" runat="server" MaxLength="500" Width="90%" CssClass="source t1" />
            <asp:RequiredFieldValidator ID="rfvPublisherName" Display="Dynamic" runat="server" ControlToValidate="txtPublisherName" ErrorMessage="Το πεδίο 'Ονοματεπώνυμο' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (σταθερό):
            <my:TipIcon ID="tipPublisherPhone" runat="server" Text="<%$ Resources:EbookPublisherInput, PublisherPhone %>" />
        </th>
        <td>
            <asp:TextBox CssClass="source t2" ID="txtPublisherPhone" runat="server" MaxLength="10" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvPublisherPhone" Display="Dynamic" runat="server" ControlToValidate="txtPublisherPhone" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό)' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPublisherPhone" runat="server" Display="Dynamic" ControlToValidate="txtPublisherPhone" ValidationExpression="^2[0-9]{9}$"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό)' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="trPublisherMobilePhone">
        <td class="notRequired" style="width: 30%">
            Τηλέφωνο (κινητό):
            <my:TipIcon ID="tipPublisherMobilePhone" runat="server" Text="<%$ Resources:EbookPublisherInput, PublisherMobilePhone %>" />
        </td>
        <td>
            <asp:TextBox CssClass="source t3" ID="txtPublisherMobilePhone" runat="server" MaxLength="10" Width="20%" />
            <asp:RegularExpressionValidator ID="revPublisherMobilePhone" runat="server" Display="Dynamic" ControlToValidate="txtPublisherMobilePhone"
                ValidationExpression="^69[0-9]{8}$" ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό)' πρέπει να ξεκινάει από 69 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            E-mail:
            <my:TipIcon ID="tipPublisherEmail" runat="server" Text="<%$ Resources:EbookPublisherInput, PublisherEmail %>" />
        </th>
        <td>
            <asp:TextBox CssClass="source t4" ID="txtPublisherEmail" runat="server" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvPublisherEmail" runat="server" ControlToValidate="txtPublisherEmail" Display="Dynamic" ErrorMessage="Το πεδίο 'E-mail' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPublisherEmail" runat="server" Display="Dynamic" ControlToValidate="txtPublisherEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail του Διαθέτη Δωρεάν Ηλεκτρονικών Βοηθημάτων και Σημειώσεων δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">Ιστοσελίδα: </td>
        <td>
            <asp:TextBox ID="txtPublisherURL" runat="server" MaxLength="200" Width="90%" />
        </td>
    </tr>
</table>
<my:IdentityControl runat="server" ID="idEbookPublisher" />
<br />
<table width="100%" class="dv">
    <tr id="trSelfPublisherAddress">
        <th colspan="2" class="header">
            &raquo; Στοιχεία Ταχυδρομικής Διεύθυνσης
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Οδός - Αριθμός:
        </th>
        <td>
            <asp:TextBox ID="txtPublisherAddress" runat="server" MaxLength="100" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvPublisherAddress" Display="Dynamic" runat="server" ControlToValidate="txtPublisherAddress" ErrorMessage="Το πεδίο 'Οδός - Αριθμός' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τ.Κ.:
        </th>
        <td>
            <asp:TextBox ID="txtPublisherZipCode" runat="server" Columns="30" MaxLength="5" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvPublisherZipCode" Display="Dynamic" runat="server" ControlToValidate="txtPublisherZipCode" ErrorMessage="Το πεδίο 'Τ.Κ.' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPublisherZipCode" runat="server" ControlToValidate="txtPublisherZipCode" Display="Dynamic" ValidationExpression="^\d{5}$"
                ErrorMessage="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία"><img src="/_img/error.gif" title="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Νομός:
        </th>
        <td>
            <asp:DropDownList ID="ddlPublisherPrefecture" runat="server" Width="90%" OnInit="ddlPublisherPrefecture_Init" DataTextField="Name"
                DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvPublisherPrefecture" runat="server" Display="Dynamic" ControlToValidate="ddlPublisherPrefecture"
                ErrorMessage="Το πεδίο 'Νομός' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Πόλη:
        </th>
        <td>
            <asp:DropDownList ID="ddlPublisherCity" runat="server" Width="90%" DataTextField="Name" DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvPublisherCity" Display="Dynamic" runat="server" ControlToValidate="ddlPublisherCity" ErrorMessage="Το πεδίο 'Πόλη' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <ajaxToolkit:CascadingDropDown ID="cddPublisherCity" runat="server" TargetControlID="ddlPublisherCity" ParentControlID="ddlPublisherPrefecture"
                Category="Cities" PromptText="-- επιλέξτε πόλη --" ServicePath="~/PortalServices/Services.asmx" ServiceMethod="GetCities" LoadingText="Παρακαλω περιμένετε">
            </ajaxToolkit:CascadingDropDown>
        </td>
    </tr>
</table>