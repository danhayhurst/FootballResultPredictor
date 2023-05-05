<%@ Page Title="Update Squad Value" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateSquadValue.aspx.cs" Inherits="FootballResultPredictor.UpdateSquadValue" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <h2><%: Title %></h2>
    <br />
<h4><b>Please select a team and enter the new Squad Value</b></h4>
    <br />
    <br />
    Note: The Squad Value is in <b>MILLIONS</b> so:
    <br />
    <br />
    <ul>
  <li>For 1 million just type 1</li>
  <li>For 10 million, type 10</li>
  <li>For 100 million type 100</li>
<li>For 1 billion type 1000</li>
</ul>

                         <br />
    <h2>
                    <asp:DropDownList ID="drpdwnTeam" runat="server" DataSourceID="SqlDataSource1" AppendDataBoundItems="true" DataTextField="teamname" DataValueField="teamname">
                        <asp:ListItem Enabled="true" Text="Select Team" Value="-1" />
                    </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="lblNewSquadValue" runat="server" text="Enter New Squad Value Here:" style="font-weight: bold"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="txtBoxNewValue" runat="server" MaxLength="4"  Style="text-align: right">
        </asp:TextBox>
        Million
             

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Football_Score_Predictor_Connection_String %>" SelectCommand="SELECT [teamname] FROM [LEAGUE_TABLE]"></asp:SqlDataSource>
        <br />
        <br />  
        <br />    

    <asp:Label ID="lblUpdateSquadValueResult" runat="server" Visible="False" style="font-weight: bold"></asp:Label>
                                            <br />
                         <br />
                                                   <br />
                    <asp:Button ID="btnSubmitUpdate" runat="server" Text="Submit" OnClick="btnSubmitUpdate_Click" />
        </h2>
                    
</asp:Content>
