<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactFormInput.ascx.cs"
    Inherits="Eudoxus.Portal.Secure.UserControls.ContactFormInput" %>
<table width="100%" class="dv">
    <tr>
        <th style="width: 100px">
            Ονοματεπώνυμο:
        </th>
        <td>
            <asp:TextBox ID="txtReporterName" runat="server" Width="88%" onkeyup="Imis.Lib.ToUpper"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvReporterName" runat="server" ControlToValidate="txtReporterName"
                ErrorMessage="Το πεδίο 'Ονοματεπώνυμο' είναι υποχρεωτικό" Display="Dynamic"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 100px">
            Τηλέφωνο:
        </th>
        <td>
            <asp:TextBox ID="txtReporterPhone" runat="server" MaxLength="10" Width="88%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvReporterPhone" runat="server" ControlToValidate="txtReporterPhone"
                Display="Dynamic" ErrorMessage="Το πεδίο 'Τηλέφωνο' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revReporterPhone" runat="server" ControlToValidate="txtReporterPhone"
                Display="Dynamic" ValidationExpression="^(2[0-9]{9})|(69[0-9]{8})$" ErrorMessage="Το πεδίο 'Τηλέφωνο' πρέπει να αποτελείται από ακριβώς 10 ψηφία και να ξεκινάει από 2 αν πρόκειται για σταθερό ή από 69 αν πρόκειται για κινητό."><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 100px">
            E-mail:
        </th>
        <td>
            <asp:TextBox ID="txtReporterEmail" runat="server" Width="88%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvReporterEmail" runat="server" ControlToValidate="txtReporterEmail"
                Display="Dynamic" ErrorMessage="Το πεδίο 'E-mail' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revReporterEmail" runat="server" ControlToValidate="txtReporterEmail"
                Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                ErrorMessage="Το E-mail δεν είναι έγκυρο"><img src="/_img/error.gif" title="Μη έγκυρος e-mail" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 100px">
            Είδος Χρήστη:
        </th>
        <td align="left">
            <asp:DropDownList ID="ddlReporterType" runat="server" OnInit="ddlReporterType_Init"
                Width="88%" />
            <asp:RequiredFieldValidator ID="rfvReporterType" runat="server" ControlToValidate="ddlReporterType"
                Display="Dynamic" ErrorMessage="Το πεδίο 'Είδος Χρήστη' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 100px">
            Είδος Αναφοράς:
        </th>
        <td>
            <asp:DropDownList ID="ddlIncidentType" runat="server" Width="88%" />
            <ajaxToolkit:CascadingDropDown ID="cddIncidentType" runat="server" TargetControlID="ddlIncidentType"
                ParentControlID="ddlReporterType" Category="IncidentTypes" PromptText="-- επιλέξτε πηγή αναφοράς --"
                ServicePath="~/PortalServices/Services.asmx" ServiceMethod="GetIncidentTypes"
                LoadingText="Παρακαλω περιμένετε">
            </ajaxToolkit:CascadingDropDown>
            <asp:RequiredFieldValidator ID="rfvIncidentType" runat="server" ControlToValidate="ddlIncidentType"
                Display="Dynamic" ErrorMessage="Το πεδίο 'Είδος Αναφοράς' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 100px">
            Κείμενο:
        </th>
        <td>
            <asp:TextBox ID="txtReportText" runat="server" TextMode="MultiLine" Height="100px"
                Width="88%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rvfReportText" runat="server" ControlToValidate="txtReportText"
                Display="Dynamic" ErrorMessage="Το πεδίο 'Κείμενο' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
