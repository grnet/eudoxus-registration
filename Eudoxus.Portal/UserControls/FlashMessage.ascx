<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlashMessage.ascx.cs"
    Inherits="Eudoxus.Portal.UserControls.Generic.FlashMessage" %>
<asp:Label ID="lblMessage" runat="server" EnableViewState="false" />
<ajaxToolkit:AnimationExtender ID="AnimationExtender2" TargetControlID="lblMessage"
    runat="server">
    <Animations>
        <OnLoad>
            <Parallel Duration="1">
                <Color 
                    StartValue="#dddd00"
                    EndValue="#ffffff"
                    Property="style"
                    PropertyKey="backgroundColor" />
                <Color 
                    StartValue="#999900"
                    EndValue="#FFFFFF"
                    Property="style"
                    PropertyKey="borderColor" />
            </Parallel>
        </OnLoad>
    </Animations>
</ajaxToolkit:AnimationExtender>
