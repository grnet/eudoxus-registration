<%@ Page Language="C#" MasterPageFile="~/Reports/Reports.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Eudoxus.Portal.Reports.Default" Title="Αναφορές" %>

<asp:Content ID="Content4" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Repeater runat="server" ID="rptStatistics" DataSourceID="sdsStatistics">
        <ItemTemplate>
            <h1>
                Γενικά Στατιστικά
            </h1>
            <p class="importantStatistic">
                Εγγεγραμμένοι Εκδότες :
                <asp:Label ID="lblTotalRegisteredPublishers" runat="server" Font-Bold="true"><%# Eval("TotalRegisteredPublishers", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
                <p style="margin-left: 30px">
                    Εκδοτικοί Οίκοι :
                    <asp:Label ID="lblTotalRegisteredLegalPersons" runat="server" Font-Bold="true"><%# Eval("TotalRegisteredLegalPersons", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
                    <br />
                    Αυτοεκδότες&nbsp;&nbsp;&nbsp; :
                    <asp:Label ID="lblTotalRegisteredSelfPublishers" runat="server" Font-Bold="true"><%# Eval("TotalRegisteredSelfPublishers", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
                    <br />
                    Διαθέτες Δωρεάν Ηλεκτρονικών Σημειώσεων :
                    <asp:Label ID="lblTotalRegisteredEbookPublishers" runat="server" Font-Bold="true"><%# Eval("TotalRegisteredEbookPublishers", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
                </p>
            </p>
            <p class="importantStatistic">
                Πιστοποιημένοι Εκδότες :
                <asp:Label ID="lblTotalVerifiedPublishers" runat="server" Font-Bold="true"><%# Eval("TotalVerifiedPublishers", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
                <p style="margin-left: 30px">
                    Εκδοτικοί Οίκοι :
                    <asp:Label ID="lblTotalVerifiedLegalPersons" runat="server" Font-Bold="true"><%# Eval("TotalVerifiedLegalPersons", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
                    <br />
                    Αυτοεκδότες&nbsp;&nbsp;&nbsp; :
                    <asp:Label ID="lblTotalVerifiedSelfPublishers" runat="server" Font-Bold="true"><%# Eval("TotalVerifiedSelfPublishers", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
                    <br />
                    Διαθέτες Δωρεάν Ηλεκτρονικών Σημειώσεων :
                    <asp:Label ID="lblTotalVerifiedEbookPublishers" runat="server" Font-Bold="true"><%# Eval("TotalVerifiedEbookPublishers", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
                </p>
            </p>
            <br />
            <p class="importantStatistic">
                Εγγεγραμμένες Γραμματείες :
                <asp:Label ID="lblTotalRegisteredSecretaries" runat="server" Font-Bold="true"><%# Eval("TotalRegisteredSecretaries", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
            </p>
            <p class="importantStatistic">
                Πιστοποιημένες Γραμματείες :
                <asp:Label ID="lblTotalVerifiedSecretaries" runat="server" Font-Bold="true"><%# Eval("TotalVerifiedSecretaries", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
            </p>
            <br />
            <p class="importantStatistic">
                Εγγεγραμμένα Γραφεία Διδακτικών Συγγραμμάτων :
                <asp:Label ID="lblTotalRegisteredPublicationsOffices" runat="server" Font-Bold="true"><%# Eval("TotalRegisteredPublicationsOffices", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
            </p>
            <p class="importantStatistic">
                Πιστοποιημένα Γραφεία Διδακτικών Συγγραμμάτων :
                <asp:Label ID="lblTotalVerifiedPublicationsOffices" runat="server" Font-Bold="true"><%# Eval("TotalVerifiedPublicationsOffices", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
            </p>
            <br />
            <p class="importantStatistic">
                Εγγεγραμμένα Γραφεία Μηχανογράφησης :
                <asp:Label ID="lblTotalRegisteredDataCenters" runat="server" Font-Bold="true"><%# Eval("TotalRegisteredDataCenters", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
            </p>
            <p class="importantStatistic">
                Πιστοποιημένα Γραφεία Μηχανογράφησης :
                <asp:Label ID="lblTotalVerifiedDataCenters" runat="server" Font-Bold="true"><%# Eval("TotalVerifiedDataCenters", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
            </p>
            <br />
            <p class="importantStatistic">
                Εγγεγραμμένες Βιβλιοθήκες :
                <asp:Label ID="lblTotalRegisteredLibraries" runat="server" Font-Bold="true"><%# Eval("TotalRegisteredLibraries", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
            </p>
            <p class="importantStatistic">
                Πιστοποιημένες Βιβλιοθήκες :
                <asp:Label ID="lblTotalVerifiedLibraries" runat="server" Font-Bold="true"><%# Eval("TotalVerifiedLibraries", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
            </p>
            <br />
            <p class="importantStatistic">
                Εγγεγραμμένα Σημεία Διανομής :
                <asp:Label ID="lblTotalRegisteredDistributionPoints" runat="server" Font-Bold="true"><%# Eval("TotalRegisteredDistributionPoints", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
            </p>
            <p class="importantStatistic">
                Πιστοποιημένα Σημεία Διανομής :
                <asp:Label ID="lblTotalVerifiedDistributionPoints" runat="server" Font-Bold="true"><%# Eval("TotalVerifiedDistributionPoints", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
            </p>
            <br />
            <p class="importantStatistic">
                Εγγεγραμμένοι Υπεύθυνοι Παραγγελίας Βιβλίων :
                <asp:Label ID="lblTotalRegisteredBookSuppliers" runat="server" Font-Bold="true"><%# Eval("TotalRegisteredBookSuppliers", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
            </p>
            <p class="importantStatistic">
                Πιστοποιημένοι Υπεύθυνοι Παραγγελίας Βιβλίων :
                <asp:Label ID="lblTotalVerifiedBookSuppliers" runat="server" Font-Bold="true"><%# Eval("TotalVerifiedBookSuppliers", "{0:n0}").ToString().Replace(",", ".")%></asp:Label>
            </p>
        </ItemTemplate>
    </asp:Repeater>
    <asp:SqlDataSource ID="sdsStatistics" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
        ProviderName="System.Data.SqlClient" SelectCommand="SELECT TOP 1 * FROM [vReportsDefault]">
    </asp:SqlDataSource>
</asp:Content>
