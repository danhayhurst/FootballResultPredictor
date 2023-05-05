<%@ Page Title="League Table" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeagueTable.aspx.cs" Inherits="FootballResultPredictor.LeagueTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

            <h2><%: Title %>
                        <br />
                        <br />              
                        <asp:GridView ID="LeagueTableGrid" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                            <Columns>
                                <asp:BoundField DataField="league_position" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" HeaderText="" ReadOnly="True" SortExpression="league_position" />   
                                <asp:BoundField DataField="teamname" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderText="TEAM" SortExpression="teamname" />
                                <asp:BoundField DataField="played" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" HeaderText="&nbsp;&nbsp;P" SortExpression="played" />
                                <asp:BoundField DataField="wins" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" HeaderText="&nbsp;W" SortExpression="wins" />
                                <asp:BoundField DataField="draws" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" HeaderText="&nbsp;&nbsp;D" SortExpression="draws" />
                                <asp:BoundField DataField="losses" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3.5%" HeaderText="&nbsp;&nbsp;L" SortExpression="losses" />
                                <asp:BoundField DataField="goals_for" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" HeaderText="&nbsp;GF" SortExpression="goals_for" />
                                <asp:BoundField DataField="goals_against" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" HeaderText="&nbsp;GA" SortExpression="goals_against" />
                                <asp:BoundField DataField="goal_difference" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" HeaderText="&nbsp;GD" SortExpression="goal_difference" />
                                <asp:BoundField DataField="points" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" HeaderText="&nbsp;&nbsp;PTS" SortExpression="points" />
                                <asp:BoundField DataField="form" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderText="&nbsp;&nbsp;FORM" SortExpression="form" />
                                <asp:BoundField DataField="squad_val" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderText="&nbsp;&nbsp;SQUAD &nbsp;&nbsp;VALUE &nbsp;&nbsp;(million)" SortExpression="squad_val" />
                            </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Football_Score_Predictor_Connection_String %>" SelectCommand="SELECT *,
   RANK() OVER(ORDER BY [points] DESC, [goal_difference] DESC, [goals_for] DESC, [teamname] ASC) league_position 
 FROM [Football Score Predictor].[dbo].[LEAGUE_TABLE]"></asp:SqlDataSource>
                        <br />

            </h2>

</asp:Content>
