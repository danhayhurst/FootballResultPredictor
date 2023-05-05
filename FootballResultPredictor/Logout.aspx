<%@ Page Title="Logout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="FootballResultPredictor.Logout" %>
    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <h2><%: Title %></h2>
  <asp:Label ID="lblLogout" runat="server" Text="Label"></asp:Label>
<br />
        <br />
    <asp:Label ID="lblLogoutResult" runat="server" Visible="False" style="font-weight: bold"></asp:Label>
<br />
<br />
<asp:Button ID="btnLogout" runat="server" Text="Log Out" Width="166px" OnClick="btnLogout_Click"></asp:Button>
</asp:Content>
