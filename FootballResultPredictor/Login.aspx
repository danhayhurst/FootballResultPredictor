<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FootballResultPredictor.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <h2><%: Title %></h2>
            Username:
    &nbsp;&nbsp;
    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        <br />
<br />
        Password:
    &nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
<br />
<br />
    <asp:Label ID="lblLoginResult" runat="server" Visible="False" style="font-weight: bold"></asp:Label>
<br />
<br />
<asp:Button ID="btnLogin" runat="server" Text="Log In" Width="166px" OnClick="btnLogin_Click"></asp:Button>
<br />
<br />
    <br />
<br />
    <br />
<br />
    <h4>New to Football Result Predictor?</h4>
    

    You'll need to register before you can access this website.
    <br />
<br />
    <asp:Button ID="btnRegister" runat="server" Text="Register" Width="166px" OnClick="btnRegister_Click"></asp:Button>

</asp:Content>
