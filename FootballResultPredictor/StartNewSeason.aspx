<%@ Page Title="Start New Season" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StartNewSeason.aspx.cs" Inherits="FootballResultPredictor.StartNewSeason" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            <h2><%: Title %></h2>

    WARNING! If you click on the button below, it will clear all of the results from the database and start a brand new season.<br />
            <br />
    <br />
<br />
    <asp:Label ID="lblStartNewSeasonResult" runat="server" Visible="False" style="font-weight: bold"></asp:Label>
<br />
<br />

&nbsp;<asp:Button ID="btnStartNewSeason" runat="server" Text="Start New Season" Width="166px" OnClick="btnStartNewSeason_Click" OnClientClick="return confirm('Are you sure? Click OK to Confirm.')"></asp:Button>

</asp:Content>
