<%@ Control Language="C#" AutoEventWireup="true" Inherits="Eudoxus.Portal.UserControls.DistributionPointInput"
    CodeBehind="DistributionPointInput.ascx.cs" %>
<%@ Register Src="~/UserControls/StoreDistributionPointInput.ascx" TagName="StoreDistributionPointInput"
    TagPrefix="my" %>
<%@ Register Src="~/UserControls/InstitutionDistributionPointInput.ascx" TagName="InstitutionDistributionPointInput"
    TagPrefix="my" %>
<asp:MultiView runat="server" ID="mv">
    <asp:View ID="vStore" runat="server">
        <my:StoreDistributionPointInput ID="sdInput" runat="server" />
    </asp:View>
    <asp:View ID="vSecretary" runat="server">
        <my:InstitutionDistributionPointInput ID="siInput" runat="server" />
    </asp:View>
</asp:MultiView>
