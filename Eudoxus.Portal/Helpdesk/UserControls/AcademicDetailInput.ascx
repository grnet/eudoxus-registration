<%@ Control Language="C#" AutoEventWireup="true" Inherits="Eudoxus.Portal.Helpdesk.UserControls.AcademicDetailInput"
    CodeBehind="AcademicDetailInput.ascx.cs" %>
<table width="100%" class="dv">
    <tr>
        <th colspan="2" class="header">
            &raquo; Γενικά Στοιχεία
        </th>
    </tr>
    <tr>
        <th style="width: 30%">
            Ίδρυμα:
        </th>
        <td>
            <asp:Label ID="lblInstitution" runat="server" ForeColor="Blue" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Σχολή:
        </th>
        <td>
            <asp:Label ID="lblSchool" runat="server" ForeColor="Blue" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τμήμα:
        </th>
        <td>
            <asp:Label ID="lblDepartment" runat="server" ForeColor="Blue" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Διεύθυνση:
        </th>
        <td>
            <asp:TextBox ID="txtAddress" runat="server" Width="460px" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Τηλέφωνο:
        </th>
        <td>
            <asp:TextBox ID="txtPhone" runat="server" Width="460px" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Fax:
        </th>
        <td>
            <asp:TextBox ID="txtFax" runat="server" Width="460px" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            E-mail:
        </th>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" Width="460px" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Εξάμηνα:
        </th>
        <td>
            <asp:TextBox ID="txtSemesters" runat="server" Width="460px" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Prefix:
        </th>
        <td>
            <asp:TextBox ID="txtPrefix" runat="server" Width="460px" />
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            Έχει ενημερωθεί:
        </th>
        <td>
            <asp:CheckBox ID="chbxIsNotified" runat="server" />
        </td>
    </tr>
</table>
