<%@ Page Title="Enter Scores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnterScores.aspx.cs" Inherits="FootballResultPredictor.EnterScores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
                <h2><%: Title %></h2>
                        <br />
                    <h4>Please enter the score below and then click on the Submit button:</h4>
                         <br />
    <h2>
                    <asp:DropDownList ID="drpdwnHomeTeam" runat="server" DataSourceID="SqlDataSource1" AppendDataBoundItems="true" DataTextField="teamname" DataValueField="teamname">
                        <asp:ListItem Enabled="true" Text="Select Home Team" Value="-1" />
                    </asp:DropDownList>

                    <asp:DropDownList ID="drpdwnHomeScore" runat="server">
                            <asp:ListItem Text="" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="0" Value=0></asp:ListItem>
                            <asp:ListItem Text="1" Value=1></asp:ListItem>
                            <asp:ListItem Text="2" Value=2></asp:ListItem>
                            <asp:ListItem Text="3" Value=3></asp:ListItem>
                            <asp:ListItem Text="4" Value=4></asp:ListItem>
                            <asp:ListItem Text="5" Value=5></asp:ListItem>
                            <asp:ListItem Text="6" Value=6></asp:ListItem>
                            <asp:ListItem Text="7" Value=7></asp:ListItem>
                            <asp:ListItem Text="8" Value=8></asp:ListItem>
                            <asp:ListItem Text="9" Value=9></asp:ListItem>
                            <asp:ListItem Text="10" Value=10></asp:ListItem>
                    </asp:DropDownList>

                    <asp:DropDownList ID="drpdwnAwayTeam" runat="server" DataSourceID="SqlDataSource1" AppendDataBoundItems="true" DataTextField="teamname" DataValueField="teamname">
                      <asp:ListItem Enabled="true" Text="Select Away Team" Value="-1" />
                    </asp:DropDownList>

                    <asp:DropDownList ID="drpdwnAwayScore" runat="server">
                            <asp:ListItem Text="" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="0" Value=0></asp:ListItem>
                            <asp:ListItem Text="1" Value=1></asp:ListItem>
                            <asp:ListItem Text="2" Value=2></asp:ListItem>
                            <asp:ListItem Text="3" Value=3></asp:ListItem>
                            <asp:ListItem Text="4" Value=4></asp:ListItem>
                            <asp:ListItem Text="5" Value=5></asp:ListItem>
                            <asp:ListItem Text="6" Value=6></asp:ListItem>
                            <asp:ListItem Text="7" Value=7></asp:ListItem>
                            <asp:ListItem Text="8" Value=8></asp:ListItem>
                            <asp:ListItem Text="9" Value=9></asp:ListItem>
                            <asp:ListItem Text="10" Value=10></asp:ListItem>
                    </asp:DropDownList>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Football_Score_Predictor_Connection_String %>" SelectCommand="SELECT [teamname] FROM [LEAGUE_TABLE] ORDER BY [teamname]"></asp:SqlDataSource>
        <br />
        <br />
    <asp:Label ID="lblAddScoreResult" runat="server" Visible="False" style="font-weight: bold"></asp:Label>
                                            <br />
                         <br />
                    <asp:Button ID="btnSubmitScore" runat="server" Text="Submit" OnClick="btnSubmitScore_Click" />
        </h2>
                    
</asp:Content>
