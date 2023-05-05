<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="FootballResultPredictor.Register" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <h2><%: Title %></h2>

    Please complete the fields below and then click on 'Register' 
    <br />
<br /><br />
<br />
            Username:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtRegUsername" runat="server"></asp:TextBox>
        <br />
<br />
        Password:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtRegPassword" TextMode="Password" runat="server"></asp:TextBox>
&nbsp;Note: The Password must contain one lower case, one upper case and one special character.<br />
<br />
            Confirm Password:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtRegPasswordConfirm" TextMode="Password" runat="server"></asp:TextBox>
    <br />
<br />
    <asp:Label ID="lblRegisterResult" runat="server" Visible="False" style="font-weight: bold"></asp:Label>
<br />
<br />
<asp:Button ID="btnRegisterSubmit" runat="server" Text="Register" Width="166px" OnClick="btnRegisterSubmit_Click"></asp:Button>

</asp:Content>
