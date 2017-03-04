<%@ Control Language="C#" AutoEventWireup="true" Inherits="Eudoxus.Portal.Admin.UserControls.IncidentTypeInput"
    CodeBehind="IncidentTypeInput.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dxe" %>

<script type="text/javascript">
    function validateSubSystem(s, e) {
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
            &raquo; Στοιχεία Τύπου Συμβάντος
        </th>
    </tr>
    <tr>
        <th style="width: 70px">
            Υποσύστημα:
        </th>
        <td>
            <asp:Label ID="lblSubSystem" runat="server" ForeColor="Blue" />
        </td>
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
