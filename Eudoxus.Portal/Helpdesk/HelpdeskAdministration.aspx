<%@ Page Language="C#" AutoEventWireup="true" Inherits="Eudoxus.Portal.Helpdesk.HelpdeskAdministration"
    MasterPageFile="~/Helpdesk/Helpdesk.Master" Title="Διαχείριση Γραφείου Αρωγής"
    Codebehind="HelpdeskAdministration.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
<table id="tbFilters" runat="server" width="500px" class="dv">
        <tr>
            <th colspan="2" class="popupHeader">
                Δημιουργία Ετήσιας Λίστας Συμβάντων για καταχώριση Μαθημάτων/Συγγραμμάτων
            </th>
        </tr>
        <tr>
            <th style="width: 200px">
                Ακαδημαϊκό Έτος:
            </th>
            <td style="width: 300px">
                <asp:DropDownList ID="ddlAcademicYear" runat="server" Width="250px" TabIndex="1"
                    OnInit="ddlAcademicYear_Init" />
            </td>
        </tr>
    </table>
    <div style="padding: 5px 0px 10px;">
        <asp:LinkButton ID="btnCreateLessonReports" runat="server" Text="Δημιουργία Συμβάντων" OnClick="btnCreateLessonReports_Click"
            CssClass="icon-btn bg-addNewItem" />
    </div>
    <br />
    <asp:Label ID="lblResults" runat="server" Font-Bold="true" ForeColor="Red" />
</asp:Content>
