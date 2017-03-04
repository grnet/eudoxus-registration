<%@ Control Language="C#" AutoEventWireup="true" Inherits="Eudoxus.Portal.Admin.UserControls.SubSystemInput"
    CodeBehind="SubSystemInput.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dxe" %>

<script type="text/javascript">
    function validateRole(s, e) {
        if (s.GetSelectedItem() == null) {
            e.isValid = false;
            e.errorText = 'Το πεδίο είναι υποχρεωτικό';
        }
    }

    function validateReporterType(s, e) {
        if (s.GetSelectedItem() == null) {
            e.isValid = false;
            e.errorText = 'Πρέπει να επιλέξετε τουλάχιστον ένα τύπο αναφέροντα';
        }
    }
</script>

<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Στοιχεία Υποσυστήματος
        </th>
    </tr>
    <tr>
        <th style="width: 70px">
            Όνομα:
        </th>
        <td>
            <dxe:ASPxTextBox ID="txtName" runat="server" Width="460px">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο είναι υποχρεωτικό'"
                    ErrorDisplayMode="ImageWithTooltip" Display="Static" />
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <th style="width: 70px">
            Ρόλος:
        </th>
        <td>
            <dxe:ASPxComboBox ID="cbxRole" runat="server" OnInit="cbxRole_Init" Width="460px">
                <ClientSideEvents Validation="validateRole" />
                <ValidationSettings Display="Static" ErrorDisplayMode="ImageWithTooltip" />
            </dxe:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <th style="width: 70px">
            Κατηγορίες Αναφερόντων:
        </th>
        <td>
            <dxe:ASPxListBox ID="cbxReporterType" runat="server" SelectionMode="CheckColumn"
                OnInit="cbxReporterType_Init" Width="460px" Style="border: none;">
                <ClientSideEvents Validation="validateReporterType" />
                <ValidationSettings Display="Static" ErrorDisplayMode="ImageWithTooltip" />
            </dxe:ASPxListBox>
        </td>
    </tr>
</table>
