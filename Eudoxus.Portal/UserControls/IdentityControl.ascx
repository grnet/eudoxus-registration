<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IdentityControl.ascx.cs"
    Inherits="Eudoxus.Portal.UserControls.IdentityControl" %>
<table id="tbIdentificationInfo" width="100%" class="dv" style="border-top: hidden"
    runat="server">
    <tr>
        <th style="width: 30%">
            Τύπος Εγγράφου Πιστοποίησης:
        </th>
        <td>
            <asp:RadioButtonList ID="rblIdType" runat="server" CssClass="SelfPublisher" RepeatLayout="Flow"
                RepeatDirection="Horizontal" OnInit="rblIdType_Init">
            </asp:RadioButtonList>
            <%--<asp:CustomValidator runat="server" ID="cv" />
            <asp:RequiredFieldValidator ID="rfvType" runat="server" Display="Dynamic" ControlToValidate="rblIdType" ErrorMessage="Το πεδίο 'Τύπος Εγγράφου Πιστοποίησης' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>--%>
        </td>
    </tr>
    <tr>
        <th style="width: 30%">
            <asp:Label ID="lblIdNumber" runat="server" Style="font-size: 11px">Αριθμός Εγγράφου Πιστοποίησης:</asp:Label>
        </th>
        <td>
            <asp:TextBox ID="txtIdNumber" runat="server" MaxLength="100" Width="90%" />
            <asp:CustomValidator Display="Dynamic" runat="server" ID="cvNumber" ErrorMessage="Το πεδίο 'Αριθμός Εγγράφου Πιστοποίησης' είναι υποχρεωτικό"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό">
                <img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" />
            </asp:CustomValidator>
        </td>
    </tr>
    <tr class="idIssuer">
        <th style="width: 30%">
            Αρχή Έκδοσης:
        </th>
        <td>
            <asp:TextBox ID="txtIdIssuer" runat="server" MaxLength="100" Width="90%" />
            <asp:CustomValidator runat="server" Display="Dynamic" ID="cvIssuer" ErrorMessage="Το πεδίο 'Αρχή Έκδοσης Εγγράφου Πιστοποίησης' είναι υποχρεωτικό">
                <img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" />
            </asp:CustomValidator>
        </td>
    </tr>
    <tr class="idIssueDate">
        <th style="width: 30%">
            Ημ/νία Έκδοσης:
        </th>
        <td>
            <asp:TextBox ID="txtIdIssueDate" runat="server" MaxLength="100" Width="20%" />
            <asp:Hyperlink ID="lnkSelectDate" runat="server" NavigateUrl="#"><img runat="server" style="border:none;vertical-align:middle" src="~/_img/iconCalendar.png" /></asp:Hyperlink>
            <ajaxToolkit:CalendarExtender ID="ceSelectDate" runat="server" PopupButtonID="lnkSelectDate" TargetControlID="txtIdIssueDate" Format="dd/MM/yyyy" />
            <asp:CustomValidator Display="Dynamic" runat="server" ID="cuvIssueDate" ErrorMessage="Το πεδίο 'Ημ/νία Έκδοσης Εγγράφου Πιστοποίησης' είναι υποχρεωτικό">
                <img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" />
            </asp:CustomValidator>
            <asp:CompareValidator ID="cvIssueDate" runat="server" Display="Dynamic" Type="Date"
                Operator="DataTypeCheck" ControlToValidate="txtIdIssueDate" ErrorMessage="Το πεδίο 'Ημ/νία Έκδοσης Εγγράφου Πιστοποίησης' δεν είναι έγκυρο">
                    <img src="/_img/error.gif" title="Η ημ/νία έκδοσης δεν είναι έγκυρη" />
            </asp:CompareValidator>
            <asp:CustomValidator runat="server" ID="cvMaxDate" OnServerValidate="cvMaxDate_ServerValidate"
                Display="Dynamic" ErrorMessage="Η Ημ/νία Έκδοσης Εγγράφου Πιστοποίησης πρέπει να είναι προγενέστερη της σημερινής">
                    <img src="/_img/error.gif" title="Η ημ/νία έκδοσης πρέπει να είναι προγενέστερη της σημερινής" />
            </asp:CustomValidator>
        </td>
    </tr>
</table>
<br />
