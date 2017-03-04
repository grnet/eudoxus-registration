<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TipIcon.ascx.cs" Inherits="Eudoxus.Portal.UserControls.TipIcon" %>
<asp:Image runat="server" ImageUrl="~/_img/iconHelp.png" ID="imgIcon" />
<asp:Panel ID="panTip" runat="server" style="display:none" CssClass="tip">
    <asp:Literal ID="litTip" runat="server" />
</asp:Panel>
<ajaxToolkit:HoverMenuExtender TargetControlID="imgIcon" PopupControlID="panTip" PopupPosition="Bottom"
    ID="hmeTip" runat="server" />
