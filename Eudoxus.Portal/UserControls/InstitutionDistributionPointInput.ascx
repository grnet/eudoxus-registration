<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InstitutionDistributionPointInput.ascx.cs"
    Inherits="Eudoxus.Portal.UserControls.InstitutionDistributionPointInput" %>
<%@ Register Src="~/UserControls/TipIcon.ascx" TagPrefix="my" TagName="TipIcon" %>
<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Import Namespace="Eudoxus.BusinessModel" %>

<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Σημείου Διανομής
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Ίδρυμα:
        </th>
        <td>
            <asp:Label ID="lblInstitution" runat="server" Font-Bold="true" ForeColor="Blue" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τίτλος:
            <my:TipIcon ID="tipDistributionPointName" runat="server" Text="<%$ Resources:DistributionPointInput, DistributionPointName %>" />
        </th>
        <td>
            <asp:TextBox ID="txtDistributionPointName" runat="server" MaxLength="500" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvDistributionPointName" Display="Dynamic" runat="server" ControlToValidate="txtDistributionPointName" ErrorMessage="Το πεδίο 'Τίτλος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Ωράριο Λειτουργίας:
            <my:TipIcon ID="tipDistributionPointOpeningHours" runat="server" Text="<%$ Resources:DistributionPointInput, DistributionPointOpeningHours %>" />
        </th>
        <td>
            <asp:TextBox ID="txtDistributionPointOpeningHours" runat="server" MaxLength="500" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvDistributionPointOpeningHours" Display="Dynamic" runat="server" ControlToValidate="txtDistributionPointOpeningHours" ErrorMessage="Το πεδίο 'Ωράριο Λειτουργίας' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (σταθερό):
            <my:TipIcon ID="tipDistributionPointPhone" runat="server" Text="<%$ Resources:DistributionPointInput, DistributionPointPhone %>" />
        </th>
        <td>
            <asp:TextBox CssClass="source t2" ID="txtDistributionPointPhone" runat="server" MaxLength="10"
                Width="20%" />
            <asp:RequiredFieldValidator ID="rfvDistributionPointPhone" Display="Dynamic" runat="server"
                ControlToValidate="txtDistributionPointPhone" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό)' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revDistributionPointPhone" runat="server" Display="Dynamic"
                ControlToValidate="txtDistributionPointPhone" ValidationExpression="^2[0-9]{9}$" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό)' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Τηλέφωνο (κινητό):
            <my:TipIcon ID="tipDistributionPointMobilePhone" runat="server" Text="<%$ Resources:DistributionPointInput, DistributionPointMobilePhone %>" />
        </td>
        <td>
            <asp:TextBox ID="txtDistributionPointMobilePhone" runat="server" MaxLength="10" Width="20%" />
            <asp:RegularExpressionValidator ID="revDistributionPointMobilePhone" runat="server" Display="Dynamic" ControlToValidate="txtDistributionPointMobilePhone"
                ValidationExpression="^69[0-9]{8}$" ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό)' πρέπει να ξεκινάει από 69 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">Fax: </td>
        <td>
            <asp:TextBox ID="txtDistributionPointFax" runat="server" MaxLength="10" Width="20%" />
            <asp:RegularExpressionValidator ID="revDistributionPointFax" runat="server" Display="Dynamic" ControlToValidate="txtDistributionPointFax" ValidationExpression="^2[0-9]{9}$"
                ErrorMessage="Το πεδίο 'Fax' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός Fax" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            E-mail:
            <my:TipIcon ID="tipDistributionPointEmail" runat="server" Text="<%$ Resources:DistributionPointInput, DistributionPointEmail %>" />
        </th>
        <td>
            <asp:TextBox CssClass="source t4" ID="txtDistributionPointEmail" runat="server" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvDistributionPointEmail" runat="server" ControlToValidate="txtDistributionPointEmail"
                Display="Dynamic" ErrorMessage="Το πεδίο 'E-mail' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revDistributionPointEmail" runat="server" Display="Dynamic"
                ControlToValidate="txtDistributionPointEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail του Γραφείου δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Ιστοσελίδα:
        </td>
        <td>
            <asp:TextBox ID="txtDistributionPointURL" runat="server" MaxLength="200" Width="90%" />
        </td>
    </tr>
</table>
<br />
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Ταχυδρομικής Διεύθυνσης
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Οδός - Αριθμός:
        </th>
        <td>
            <asp:TextBox ID="txtDistributionPointAddress" runat="server" MaxLength="100" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvDistributionPointAddress" Display="Dynamic" runat="server"
                ControlToValidate="txtDistributionPointAddress" ErrorMessage="Το πεδίο 'Οδός - Αριθμός' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τ.Κ.:
        </th>
        <td>
            <asp:TextBox ID="txtDistributionPointZipCode" runat="server" Columns="30" MaxLength="5" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvDistributionPointZipCode" Display="Dynamic" runat="server"
                ControlToValidate="txtDistributionPointZipCode" ErrorMessage="Το πεδίο 'Τ.Κ.' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revDistributionPointZipCode" runat="server" ControlToValidate="txtDistributionPointZipCode"
                Display="Dynamic" ValidationExpression="^\d{5}$" ErrorMessage="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία"><img src="/_img/error.gif" title="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Νομός:
            <my:TipIcon ID="tipDistributionPointPrefecture" runat="server" Text="<%$ Resources:DistributionPointInput, DistributionPointPrefecture %>" />
        </th>
        <td>
            <asp:DropDownList ID="ddlDistributionPointPrefecture" runat="server" Width="90%" OnInit="ddlDistributionPointPrefecture_Init"
                DataTextField="Name" DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvDistributionPointPrefecture" runat="server" Display="Dynamic"
                ControlToValidate="ddlDistributionPointPrefecture" ErrorMessage="Το πεδίο 'Νομός' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Πόλη:
            <my:TipIcon ID="tipDistributionPointCity" runat="server" Text="<%$ Resources:DistributionPointInput, DistributionPointCity %>" />
        </th>
        <td>
            <asp:DropDownList ID="ddlDistributionPointCity" runat="server" Width="90%" DataTextField="Name"
                DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvDistributionPointCity" Display="Dynamic" runat="server"
                ControlToValidate="ddlDistributionPointCity" ErrorMessage="Το πεδίο 'Πόλη' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <ajaxToolkit:CascadingDropDown ID="cddDistributionPointCity" runat="server" TargetControlID="ddlDistributionPointCity"
                ParentControlID="ddlDistributionPointPrefecture" Category="Cities" PromptText="-- επιλέξτε πόλη --"
                ServicePath="~/PortalServices/Services.asmx" ServiceMethod="GetCities" LoadingText="Παρακαλω περιμένετε">
            </ajaxToolkit:CascadingDropDown>
        </td>
    </tr>
    <tr>        
        <td class="notRequired" style="width: 30%">
            URL σε Χαρτογραφικό Σύστημα:
            <my:TipIcon ID="tipDistributionPointLocationURL" runat="server" Text="<%$ Resources:DistributionPointInput, DistributionPointLocationURL %>" />
            <br /><a href="http://eudoxus.gr/Files/Maps_URL_Manual_.pdf"  target="_blank">Οδηγίες</a>
        </td>
        <td>
            <asp:TextBox ID="txtDistributionPointLocationURL" runat="server" Width="90%" />
        </td>
    </tr>
</table>
<br />
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Υπευθύνου για το Εύδοξος
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Ονοματεπώνυμο:
            <my:TipIcon ID="tipContactName" runat="server" Text="<%$ Resources:DistributionPointInput, ContactName %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactName" runat="server" MaxLength="100" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvContactName" Display="Dynamic" runat="server"
                ValidationGroup="vgContact" ControlToValidate="txtContactName" ErrorMessage="Το πεδίο 'Ονοματεπώνυμο Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (σταθερό):
            <my:TipIcon ID="tipContactPhone" runat="server" Text="<%$ Resources:DistributionPointInput, ContactPhone %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactPhone" runat="server" MaxLength="10" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvContactPhone" Display="Dynamic" runat="server"
                ValidationGroup="vgContact" ControlToValidate="txtContactPhone" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactPhone" runat="server" Display="Dynamic"
                ControlToValidate="txtContactPhone" ValidationExpression="^2[0-9]{9}$" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Υπευθύνου για το Εύδοξος' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (κινητό):
            <my:TipIcon ID="tipContactMobilePhone" runat="server" Text="<%$ Resources:DistributionPointInput, ContactMobilePhone %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactMobilePhone" runat="server" MaxLength="10" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvContactMobilePhone" runat="server" Display="Dynamic"
                ValidationGroup="vgContact" ControlToValidate="txtContactMobilePhone" ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactMobilePhone" runat="server" Display="Dynamic"
                ControlToValidate="txtContactMobilePhone" ValidationExpression="^69[0-9]{8}$"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Υπευθύνου για το Εύδοξος' πρέπει να ξεκινάει από 69 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            E-mail:
            <my:TipIcon ID="tipContactEmail" runat="server" Text="<%$ Resources:DistributionPointInput, ContactEmail %>" />
        </th>
        <td>
            <asp:TextBox ID="txtContactEmail" runat="server" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server" ControlToValidate="txtContactEmail"
                ValidationGroup="vgContact" Display="Dynamic" ErrorMessage="Το πεδίο 'E-mail Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactEmail" runat="server" Display="Dynamic"
                ControlToValidate="txtContactEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail του Υπευθύνου για το Εύδοξος δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
</table>
<dxpc:ASPxPopupControl ID="dxpcPopup" runat="server" ClientInstanceName="devExPopup"
    Width="800" Height="610" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    AllowDragging="true" CloseAction="CloseButton">
</dxpc:ASPxPopupControl>
