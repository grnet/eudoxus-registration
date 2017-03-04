<%@ Control Language="C#" AutoEventWireup="true" Inherits="Eudoxus.Portal.UserControls.SelfPublisherInput"
    CodeBehind="SelfPublisherInput.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dxe" %>
<%@ Register Src="~/UserControls/TipIcon.ascx" TagPrefix="my" TagName="TipIcon" %>
<%@ Register Assembly="Eudoxus.Portal" Namespace="Eudoxus.Portal.Controls" TagPrefix="lc" %>
<%@ Register Src="~/UserControls/IdentityControl.ascx" TagName="IdentityControl"
    TagPrefix="my" %>
<%@ Import Namespace="Eudoxus.BusinessModel" %>
<% if (DesignMode)
   { %>
<script src="/_js/jquery.js" type="text/javascript"></script>
<%} %>
<script type="text/javascript">
    var cbxSelfRepresented;

    $(function () {
        cbxSelfRepresented = $('#<%= chbxSelfRepresented.ClientID %>');
        showContactDetails(true);
        if ($find('<%= idSelfPublisher.ClientInstanceName %>') != null) {
            $find('<%= idSelfPublisher.ClientInstanceName %>').addRecordsChanged(function () {
                if (cbxSelfRepresented.attr('checked'))
                    $find('<%= idContact.ClientInstanceName %>').setValue($find('<%= idSelfPublisher.ClientInstanceName %>').getValue());
            });
        }
        $('.source').blur(function () {
            if (cbxSelfRepresented.attr('checked')) {
                for (var i = 1; i < 5; i++) {
                    //debugger;
                    if ($(this).hasClass('t' + i)) {
                        $('.target').filter('.t' + i).val($(this).val());
                    }
                }
            }
        });
    });   
</script>
<% if (HelpDeskEditMode)
   {    %>
<script type="text/javascript">
    function showContactDetails() {
        var copyChecked = cbxSelfRepresented.attr('checked');

        if (copyChecked) {
            $('#tbContactDetails').hide();
            $('#<%= idContact.ClientInstanceName %>').hide();
            var idContact = $find('<%= idContact.ClientInstanceName %>');
            var idSelfPublisher = $find('<%= idSelfPublisher.ClientInstanceName %>');
            if (idContact != null && idSelfPublisher != null)
                idContact.setValue(idSelfPublisher.getValue());
            $get('<%= txtContactEmail.ClientID %>').value = $get("<%= txtPublisherEmail.ClientID %>").value;
            $get('<%= txtContactPhone.ClientID %>').value = $get("<%= txtPublisherPhone.ClientID %>").value;
            $get('<%= txtContactMobilePhone.ClientID %>').value = $get("<%= txtPublisherMobilePhone.ClientID %>").value;
            $get('<%= txtContactName.ClientID %>').value = $get("<%= txtPublisherName.ClientID %>").value;
        }
        else {
            $('#tbContactDetails').show();
            $('#<%= idContact.ClientInstanceName %>').show();
        }
    }
</script>
<%}
   else
   { %>
<script type="text/javascript">
    function showContactDetails(isFirstLoad) {
        var copyChecked = cbxSelfRepresented.attr('checked');

        if (copyChecked) {
            $('#tbContactDetails').hide();
            $('#<%= idContact.ClientInstanceName %>').hide();
            var idContact = $find('<%= idContact.ClientInstanceName %>');
            var idSelfPublisher = $find('<%= idSelfPublisher.ClientInstanceName %>');
            if (idContact != null && idSelfPublisher != null)
                idContact.setValue(idSelfPublisher.getValue());
            $get('<%= txtContactEmail.ClientID %>').value = $get("<%= txtPublisherEmail.ClientID %>").value;
            $get('<%= txtContactPhone.ClientID %>').value = $get("<%= txtPublisherPhone.ClientID %>").value;
            $get('<%= txtContactMobilePhone.ClientID %>').value = $get("<%= txtPublisherMobilePhone.ClientID %>").value;
            $get('<%= txtContactName.ClientID %>').value = $get("<%= txtPublisherName.ClientID %>").value;
        }
        else {
            $('#tbContactDetails').show();
            $('#<%= idContact.ClientInstanceName %>').show();

            if (cbxSelfRepresented.attr('disabled'))
                return;
            if (isFirstLoad)
                return;
            var idContact = $find('<%= idContact.ClientInstanceName %>');
            $get('<%= txtContactEmail.ClientID %>').value = '';
            $get('<%= txtContactPhone.ClientID %>').value = '';
            $get('<%= txtContactMobilePhone.ClientID %>').value = '';
            $get('<%= txtContactName.ClientID %>').value = '';
            if (idContact != null) {
                idContact.clear();
            }
        }
    }
</script>
<%} %>
<table width="100%" class="dv">
    <tr id="trSelfPublisher">
        <th colspan="2" class="header">
            &raquo; Στοιχεία Αυτοεκδότη
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Κατηγορία:
        </th>
        <td style="font-weight: bold; color: Blue">
            <%= enPublisherType.SelfPublisher.GetLabel() %>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            <span id="sPublisherName" style="font-size: 11px">Ονοματεπώνυμο:</span>
            <my:TipIcon ID="tipPublisherName" runat="server" Text="<%$ Resources:SelfPublisherInput, PublisherName %>" />
        </th>
        <td>
            <asp:TextBox ID="txtPublisherName" runat="server" MaxLength="500" Width="90%" CssClass="source t1" />
            <asp:RequiredFieldValidator ID="rfvPublisherName" Display="Dynamic" runat="server"
                ControlToValidate="txtPublisherName" ErrorMessage="Το πεδίο 'Ονοματεπώνυμο' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Α.Φ.Μ.:
            <my:TipIcon ID="tipPublisherAFM" runat="server" Text="<%$ Resources:SelfPublisherInput, PublisherAFM %>" />
        </th>
        <td>
            <lc:AFMInput ID="txtPublisherAFM" runat="server" ReadOnly="false" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Δ.Ο.Υ.:
        </th>
        <td>
            <asp:DropDownList ID="ddlPublisherDOY" runat="server" OnInit="ddlDOY_Init" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvPublisherDOY" Display="Dynamic" runat="server"
                ControlToValidate="ddlPublisherDOY" ErrorMessage="Το πεδίο 'Δ.Ο.Υ.' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Υπόχρεος Τήρησης Λογιστικών Βιβλίων:
        </th>
        <td>
            <asp:DropDownList ID="ddlHasLogisticBooks" runat="server"
                Width="30%">
                <asp:ListItem Text="-- παρακαλώ επιλέξτε --" Value="" />
                <asp:ListItem Text="Ναι" Value="1" />
                <asp:ListItem Text="Όχι" Value="0" />                
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvHasLogisticBooks" Display="Dynamic" runat="server"
                ControlToValidate="ddlHasLogisticBooks" ErrorMessage="Το πεδίο 'Υπόχρεος Τήρησης Λογιστικών Βιβλίων' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (σταθερό):
            <my:TipIcon ID="tipPublisherPhone" runat="server" Text="<%$ Resources:SelfPublisherInput, PublisherPhone %>" />
        </th>
        <td>
            <asp:TextBox CssClass="source t2" ID="txtPublisherPhone" runat="server" MaxLength="10"
                Width="20%" />
            <asp:RequiredFieldValidator ID="rfvPublisherPhone" Display="Dynamic" runat="server"
                ControlToValidate="txtPublisherPhone" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό)' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPublisherPhone" runat="server" Display="Dynamic"
                ControlToValidate="txtPublisherPhone" ValidationExpression="^2[0-9]{9}$" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό)' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="trPublisherMobilePhone">
        <th style="width: 30%">
            Τηλέφωνο (κινητό):
            <my:TipIcon ID="tipPublisherMobilePhone" runat="server" Text="<%$ Resources:SelfPublisherInput, PublisherMobilePhone %>" />
        </th>
        <td>
            <asp:TextBox CssClass="source t3" ID="txtPublisherMobilePhone" runat="server" MaxLength="10"
                Width="20%" />
            <asp:RequiredFieldValidator ID="rfvPublisherMobilePhone" Display="Dynamic" runat="server"
                ValidationGroup="vgSelfPublisher" ControlToValidate="txtPublisherMobilePhone"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό)' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPublisherMobilePhone" runat="server" Display="Dynamic"
                ControlToValidate="txtPublisherMobilePhone" ValidationExpression="^69[0-9]{8}$"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό)' πρέπει να ξεκινάει από 69 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Fax:
        </td>
        <td>
            <asp:TextBox ID="txtPublisherFax" runat="server" MaxLength="10" Width="20%" />
            <asp:RegularExpressionValidator ID="revPublisherFax" runat="server" Display="Dynamic"
                ControlToValidate="txtPublisherFax" ValidationExpression="^2[0-9]{9}$" ErrorMessage="Το πεδίο 'Fax' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός Fax" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            E-mail:
            <my:TipIcon ID="tipPublisherEmail" runat="server" Text="<%$ Resources:SelfPublisherInput, PublisherEmail %>" />
        </th>
        <td>
            <asp:TextBox CssClass="source t4" ID="txtPublisherEmail" runat="server" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvPublisherEmail" runat="server" ControlToValidate="txtPublisherEmail"
                Display="Dynamic" ErrorMessage="Το πεδίο 'E-mail' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPublisherEmail" runat="server" Display="Dynamic"
                ControlToValidate="txtPublisherEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail της επιχείρησης δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="notRequired" style="width: 30%">
            Ιστοσελίδα:
        </td>
        <td>
            <asp:TextBox ID="txtPublisherURL" runat="server" MaxLength="200" Width="90%" />
        </td>
    </tr>
</table>
<my:IdentityControl runat="server" ID="idSelfPublisher" />
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
            <asp:TextBox ID="txtPublisherAddress" runat="server" MaxLength="256" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvPublisherAddress" Display="Dynamic" runat="server"
                ControlToValidate="txtPublisherAddress" ErrorMessage="Το πεδίο 'Οδός - Αριθμός' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τ.Κ.:
        </th>
        <td>
            <asp:TextBox ID="txtPublisherZipCode" runat="server" Columns="30" MaxLength="5" Width="20%" />
            <asp:RequiredFieldValidator ID="rfvPublisherZipCode" Display="Dynamic" runat="server"
                ControlToValidate="txtPublisherZipCode" ErrorMessage="Το πεδίο 'Τ.Κ.' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPublisherZipCode" runat="server" ControlToValidate="txtPublisherZipCode"
                Display="Dynamic" ValidationExpression="^\d{5}$" ErrorMessage="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία"><img src="/_img/error.gif" title="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Νομός:
        </th>
        <td>
            <asp:DropDownList ID="ddlPublisherPrefecture" runat="server" Width="90%" OnInit="ddlPublisherPrefecture_Init"
                DataTextField="Name" DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvPublisherPrefecture" runat="server" Display="Dynamic"
                ControlToValidate="ddlPublisherPrefecture" ErrorMessage="Το πεδίο 'Νομός' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Πόλη:
        </th>
        <td>
            <asp:DropDownList ID="ddlPublisherCity" runat="server" Width="90%" DataTextField="Name"
                DataValueField="ID" />
            <asp:RequiredFieldValidator ID="rfvPublisherCity" Display="Dynamic" runat="server"
                ControlToValidate="ddlPublisherCity" ErrorMessage="Το πεδίο 'Πόλη' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <ajaxToolkit:CascadingDropDown ID="cddPublisherCity" runat="server" TargetControlID="ddlPublisherCity"
                ParentControlID="ddlPublisherPrefecture" Category="Cities" PromptText="-- επιλέξτε πόλη --"
                ServicePath="~/PortalServices/Services.asmx" ServiceMethod="GetCities" LoadingText="Παρακαλω περιμένετε">
            </ajaxToolkit:CascadingDropDown>
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
    <tr id="trSelfRepresented">
        <th style="width: 30%">
            Εκπροσωπείται από τον ίδιο:
        </th>
        <td>
            <asp:CheckBox ID="chbxSelfRepresented" runat="server" onclick="showContactDetails()" />
        </td>
    </tr>
</table>
<table id="tbContactDetails" width="100%" class="dv" style="border-top: hidden">
    <tr>
        <th style="width: 30%">
            Ονοματεπώνυμο:
            <my:TipIcon ID="tipContactName" runat="server" Text="<%$ Resources:SelfPublisherInput, ContactName %>" />
        </th>
        <td>
            <asp:TextBox CssClass="target t1" ID="txtContactName" runat="server" MaxLength="100"
                Width="90%" />
            <asp:RequiredFieldValidator ID="rfvContactName" Display="Dynamic" runat="server"
                ControlToValidate="txtContactName" ErrorMessage="Το πεδίο 'Ονοματεπώνυμο Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (σταθερό):
            <my:TipIcon ID="tipContactPhone" runat="server" Text="<%$ Resources:SelfPublisherInput, ContactPhone %>" />
        </th>
        <td>
            <asp:TextBox CssClass="target t2" ID="txtContactPhone" runat="server" MaxLength="10"
                Width="20%" />
            <asp:RequiredFieldValidator ID="rfvContactPhone" Display="Dynamic" runat="server"
                ControlToValidate="txtContactPhone" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactPhone" runat="server" Display="Dynamic"
                ControlToValidate="txtContactPhone" ValidationExpression="^2[0-9]{9}$" ErrorMessage="Το πεδίο 'Τηλέφωνο (σταθερό) του Υπευθύνου για το Εύδοξος' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο (κινητό):
            <my:TipIcon ID="tipContactMobilePhone" runat="server" Text="<%$ Resources:SelfPublisherInput, ContactMobilePhone %>" />
        </th>
        <td>
            <asp:TextBox CssClass="target t3" ID="txtContactMobilePhone" runat="server" MaxLength="10"
                Width="20%" />
            <asp:RequiredFieldValidator ID="rfvContactMobilePhone" runat="server" Display="Dynamic"
                ControlToValidate="txtContactMobilePhone" ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactMobilePhone" runat="server" Display="Dynamic"
                ControlToValidate="txtContactMobilePhone" ValidationExpression="^69[0-9]{8}$"
                ErrorMessage="Το πεδίο 'Τηλέφωνο (κινητό) του Υπευθύνου για το Εύδοξος' πρέπει να ξεκινάει από 69 και να αποτελείται από ακριβώς 10 ψηφία"><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            E-mail:
            <my:TipIcon ID="tipContactEmail" runat="server" Text="<%$ Resources:SelfPublisherInput, ContactEmail %>" />
        </th>
        <td>
            <asp:TextBox CssClass="target t4" ID="txtContactEmail" runat="server" Width="90%" />
            <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server" ControlToValidate="txtContactEmail"
                Display="Dynamic" ErrorMessage="Το πεδίο 'E-mail Υπευθύνου για το Εύδοξος' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactEmail" runat="server" Display="Dynamic"
                ControlToValidate="txtContactEmail" ValidationExpression="^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$"
                ErrorMessage="Το E-mail του Υπευθύνου για το Εύδοξος δεν είναι έγκυρο"><img src="/_img/error.gif" title="Το πεδίο δεν είναι έγκυρο" /></asp:RegularExpressionValidator>
        </td>
    </tr>
    <my:IdentityControl runat="server" ID="idContact" ValidationGroup="vgContact" />
</table>
