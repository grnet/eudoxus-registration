﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="DataCenterRegistration.aspx.cs"
    Inherits="Eudoxus.Portal.Common.DataCenterRegistration" %>
<%@ Register Src="~/UserControls/DataCenterInput.ascx" TagName="DataCenterInput" TagPrefix="my" %>
<%@ Register Src="~/UserControls/RegisterUserInput.ascx" TagName="RegisterUserInput"
    TagPrefix="my" %>
<%@ Register Assembly="Eudoxus.Portal" Namespace="Eudoxus.Portal.Controls" TagPrefix="lc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">    

    <script type="text/javascript">
        function validate() {
            return Page_ClientValidate();
        }
    </script>
    
    <h1>
        <asp:Label ID="lblTitle" runat="server" Text="Δημιουργία Χρήστη Γραφείου Μηχανογράφησης Ιδρύματος" Font-Size="14pt" />
    </h1>
    <asp:MultiView ID="mvRegistration" runat="server" ActiveViewIndex="0">
        <asp:View ID="vRegister" runat="server">
            <asp:ValidationSummary ID="vdSummary" runat="server" ValidationGroup="vdRegistration"
                HeaderText="Υπάρχει σφάλμα ή έλλειψη συμπλήρωσης ενός από τα πεδία της φόρμας. Παρακαλώ κάντε τις απαραίτητες διορθώσεις." />
            <asp:Label ID="lblErrors" runat="server" Font-Bold="true" ForeColor="Red" />
            <my:RegisterUserInput ID="registerUserInput" runat="server" ValidationGroup="vdRegistration" />
            <br />
            <my:DataCenterInput ID="ucDataCenterInput" runat="server" ValidationGroup="vdRegistration" />
            <br />
            <lc:BotShield ID="bsPublisher" runat="server" ValidationGroup="vdRegistration" />
            <br />
            <br />
            <div style="clear: both; text-align: left">
                <asp:LinkButton ID="btnCreate" runat="server" Text="Δημιουργία Λογαριασμού" CssClass="icon-btn bg-accept"
                    ValidationGroup="vdRegistration" OnClick="btnCreate_Click" />
            </div>
        </asp:View>
        <asp:View ID="vComplete" runat="server">
            <asp:Label ID="lblCompletionMessage" runat="server" />
        </asp:View>
        <asp:View ID="vNotAllowed" runat="server">
            <div class="reminder">
                Η δημιουργία λογαριασμού χρήστη θα είναι σύντομα διαθέσιμη.
            </div>
        </asp:View>
    </asp:MultiView>
    
</asp:Content>
