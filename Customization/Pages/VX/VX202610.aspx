<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" 
    CodeFile="VX202610.aspx.cs" Inherits="Pages_VX_VX202610" Title="Box User Profile"
    ValidateRequest="false"  %>

<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" PrimaryView="User" TypeName="VX.MailTrack.PushNotificationSetup" PageLoadBehavior="GoFirstRecord" SuspendUnloading="False">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Cancel" Visible="False" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
   <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Caption="User Settings" Width="100%" DataMember="User">
		<Template>
            <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="M" LabelsWidth="M" SuppressLabel="False" />
            <px:PXCheckBox runat="server" DataField="Active" AlignLeft="True" ID="chkActive" />
		</Template>
        <AutoSize Container="Window" Enabled="True" MinHeight="200" />
    </px:PXFormView>
</asp:Content>
