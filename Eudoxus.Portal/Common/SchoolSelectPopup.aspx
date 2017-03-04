<%@ Page Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="SchoolSelectPopup.aspx.cs"
    Inherits="Eudoxus.Portal.Common.SchoolSelectPopup" Title="Επιλογή Σχολής" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div>
        <dxwgv:ASPxGridView ID="gv" ClientInstanceName="gv" runat="server" Width="98%" AutoGenerateColumns="False"
            DataSourceID="odsAcademics" KeyFieldName="ID">
            <Settings ShowGroupPanel="false" ShowFilterRow="true" />
            <SettingsBehavior AllowFocusedRow="True" AllowDragDrop="False" AllowGroup="False"
                AllowSort="True" AutoFilterRowInputDelay="900" />
            <SettingsPager PageSize="10" />
            <Styles Cell-Font-Size="XX-Small" />
            <Columns>
                <dxwgv:GridViewDataTextColumn FieldName="ID" VisibleIndex="0" Visible="false">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Institution" VisibleIndex="1" Caption="Ίδρυμα"
                    SortIndex="0" SortOrder="Ascending">
                    <Settings AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="School" VisibleIndex="2" Caption="Σχολή">
                    <Settings AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Department" VisibleIndex="3" Caption="Τμήμα">
                    <Settings AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <asp:ObjectDataSource ID="odsAcademics" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetAll" TypeName="Eudoxus.Portal.DataSources.Academics" />
        <div style="text-align: right; padding: 6px 18px 6px 6px;">
            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="icon-btn bg-accept" Text="Επιλογή"
                OnClientClick="return onSchoolSelected();" />
            <asp:LinkButton ID="btnCancel" runat="server" CssClass="icon-btn bg-cancel" Text="Ακύρωση"
                OnClientClick="window.parent.popUp.hide();" />
        </div>
    </div>

    <script type="text/javascript">
        function onSchoolSelected() {
            gv.GetRowValues(gv.GetFocusedRowIndex(), 'ID;Institution;School;Department', window.parent.hd.onSchoolSelected);
            return false;
        }    
    </script>

</asp:Content>
