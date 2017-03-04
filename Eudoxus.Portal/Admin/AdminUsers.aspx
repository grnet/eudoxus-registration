﻿<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    Inherits="Eudoxus.Portal.Admin.AdminUsers" Title="Admin Χρήστες" CodeBehind="AdminUsers.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Import Namespace="Eudoxus.BusinessModel" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .dxgvHeader td
        {
            font-size: 11px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:LinkButton ID="cmdRefresh" runat="server" Style="display: none;" OnClick="cmdRefresh_Click" />
    <div style="padding: 5px 0px 10px;">
        <a runat="server" class="icon-btn bg-addNewItem" href="javascript:void(0)" onclick="popUp.show('AddAdminUser.aspx','Δημιουργία Χρήστη',cmdRefresh)">
            Δημιουργία Χρήστη</a>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <dxwgv:ASPxGridView ID="gvAdminUsers" runat="server" AutoGenerateColumns="False"
                DataSourceID="odsAdminUsers" KeyFieldName="ID" EnableRowsCache="false" EnableCallBacks="true"
                Width="100%" DataSourceForceStandardPaging="true" OnRowCommand="gvAdminUsers_RowCommand">
                <SettingsLoadingPanel Text="Παρακαλώ Περιμένετε..." />
                <SettingsPager PageSize="10" Summary-Text="Σελίδα {0} από {1} ({2} Χρήστες)" Summary-Position="Left" />
                <Styles>
                    <Cell Font-Size="11px" />
                </Styles>
                <Templates>
                    <EmptyDataRow>
                        Δεν βρέθηκαν αποτελέσματα
                    </EmptyDataRow>
                </Templates>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Α/Α" HeaderStyle-HorizontalAlign="Center"
                        CellStyle-HorizontalAlign="Center" Width="50px">
                        <DataItemTemplate>
                            <%# Container.ItemIndex + 1 %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="UserName" Name="UserName" Caption="Στοιχεία Λογαριασμού"
                        CellStyle-HorizontalAlign="Left">
                        <DataItemTemplate>
                            <%# GetAccountDetails((AdminUser)Container.DataItem)%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ContactName" Name="Name" Caption="Στοιχεία Χρήστη"
                        CellStyle-HorizontalAlign="Left">
                        <DataItemTemplate>
                            <%# GetContactDetails((AdminUser)Container.DataItem)%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Master Account Για" CellStyle-HorizontalAlign="Left">
                        <DataItemTemplate>
                            <%# GetAccessDetails((AdminUser)Container.DataItem)%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Επεξεργασία Χρήστη" Width="100px">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                        <CellStyle HorizontalAlign="Center" />
                        <DataItemTemplate>
                            <a id="A2" runat="server" style="text-decoration: none" href="javascript:void(0)"
                                onclick=<%# string.Format("popUp.show('EditAdminUser.aspx?sID={0}', 'Επεξεργασία Χρήστη', cmdRefresh);", Eval("ID"))%>>
                                <img src="/_img/iconEdit.png" alt="Επεξεργασία Χρήστη" />
                            </a>
                            <asp:LinkButton runat="server" CommandName="LockAdminUser" CommandArgument='<%# Eval("ID") %>'
                                OnClientClick="return confirm('Είστε σίγουροι ότι θέλετε να απενεργοποιήσετε το συγκρεκριμένο χρήστη;');"
                                Style="text-decoration: none;" ToolTip="Απενεργοποίηση Χρήστη" Visible='<%# (bool)Eval("IsApproved") %>'>
                                    <img src="/_img/iconLock.png" alt="Απενεργοποίηση Χρήστη" /></asp:LinkButton>
                            <asp:LinkButton runat="server" CommandName="UnLockAdminUser" CommandArgument='<%# Eval("ID") %>'
                                OnClientClick="return confirm('Είστε σίγουροι ότι θέλετε να επανενεργοποιήσετε το συγκρεκριμένο χρήστη;');"
                                Style="text-decoration: none;" ToolTip="Επανενεργοποίηση Χρήστη" Visible='<%# !((bool)Eval("IsApproved")) %>'>
                                    <img src="/_img/iconUnLock.png" alt="Επανενεργοποίηση Χρήστη" /></asp:LinkButton>
                            <asp:LinkButton runat="server" CommandName="DeleteAdminUser" CommandArgument='<%# Eval("ID") %>'
                                OnClientClick="return confirm('Είστε σίγουροι ότι θέλετε να διαγράψετε το συγκρεκριμένο χρήστη;');"
                                Style="text-decoration: none;" ToolTip="Διαγραφή Χρήστη">
                                    <img src="/_img/iconDelete.png" alt="Διαγραφή Χρήστη" /></asp:LinkButton>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsAdminUsers" runat="server" TypeName="Eudoxus.Portal.DataSources.Users"
        SelectMethod="FindAdminUsersWithCriteria" SelectCountMethod="CountAdminUsersWithCriteria"
        EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsAdminUsers_Selecting">
        <SelectParameters>
            <asp:Parameter Name="criteria" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dxpc:ASPxPopupControl ID="dxpcPopup" runat="server" ClientInstanceName="devExPopup"
        Width="800" Height="610" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        AllowDragging="true" CloseAction="CloseButton">
    </dxpc:ASPxPopupControl>
    <script type="text/javascript">
        function cmdRefresh() {
            <%= this.Page.ClientScript.GetPostBackEventReference(this.cmdRefresh, string.Empty) %>;
        }
    </script>
</asp:Content>
