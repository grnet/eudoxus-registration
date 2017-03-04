<%@ Control Language="C#" AutoEventWireup="true" Inherits="Eudoxus.Portal.UserControls.PublisherInput" CodeBehind="PublisherInput.ascx.cs" %>
<%@ Register Src="~/UserControls/LegalPersonInput.ascx" TagName="LegalPersonInput" TagPrefix="my" %>
<%@ Register Src="~/UserControls/ForeignLegalPersonInput.ascx" TagName="ForeignLegalPersonInput" TagPrefix="my" %>
<%@ Register Src="~/UserControls/SelfPublisherInput.ascx" TagName="SelfPublisherInput" TagPrefix="my" %>
<%@ Register Src="~/UserControls/EbookPublisherInput.ascx" TagName="EbookPublisherInput" TagPrefix="my" %>

<asp:MultiView runat="server" ID="mv">
    <asp:View ID="vLegalPerson" runat="server">
        <my:LegalPersonInput ID="lrInput" runat="server" />
    </asp:View>
    <asp:View ID="vForeignLegalPerson" runat="server">
        <my:ForeignLegalPersonInput ID="flrInput" runat="server" />
    </asp:View>
    <asp:View ID="vSelfPublisher" runat="server">
        <my:SelfPublisherInput ID="spInput" runat="server" />
    </asp:View>    
    <asp:View ID="vEbookPublisher" runat="server">
        <my:EbookPublisherInput ID="epInput" runat="server" />
    </asp:View>
</asp:MultiView>
