<%@ Page Title="Get Score Predictions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GetPredictions.aspx.cs" Inherits="FootballResultPredictor.GetPredictions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
                <h2><%: Title %></h2>
    <br />
                        <h4>In order to get a Score Prediction, please select the teams below and then click on the Submit button:</h4>
                         <br />
    <h2>
                    <asp:DropDownList ID="drpdwnHomeTeam" runat="server" DataSourceID="SqlDataSource1" AppendDataBoundItems="true" DataTextField="teamname" DataValueField="teamname">
                        <asp:ListItem Enabled="true" Text="Select Home Team" Value="-1" />
                    </asp:DropDownList>

                    <asp:DropDownList ID="drpdwnAwayTeam" runat="server" DataSourceID="SqlDataSource1" AppendDataBoundItems="true" DataTextField="teamname" DataValueField="teamname">
                      <asp:ListItem Enabled="true" Text="Select Away Team" Value="-1" />
                    </asp:DropDownList>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Football_Score_Predictor_Connection_String %>" SelectCommand="SELECT [teamname] FROM [LEAGUE_TABLE] ORDER BY [teamname]"></asp:SqlDataSource>
        <br />
        <br />
    <asp:Label ID="lblScorePrediction" runat="server" Visible="False" style="font-weight: bold"></asp:Label>
                                            <br />
                         <br />
                    <asp:Button ID="btnSubmitTeams" runat="server" Text="Submit" OnClick="btnSubmitTeams_Click" />
        </h2>
                    
</asp:Content>
