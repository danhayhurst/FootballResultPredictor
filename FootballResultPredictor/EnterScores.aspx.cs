using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace FootballResultPredictor
{
    public partial class EnterScores : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Football_Score_Predictor_Connection_String"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmitScore_Click(object sender, EventArgs e)
        {
            if (drpdwnHomeTeam.SelectedValue == "-1")
            {
                lblAddScoreResult.Visible = true;
                lblAddScoreResult.Text = "Please select the Home Team.";
                lblAddScoreResult.ForeColor = System.Drawing.Color.Red;
            }
            else if (drpdwnHomeScore.SelectedValue == "-1")
            {
                lblAddScoreResult.Visible = true;
                lblAddScoreResult.Text = "Please select the Home Score.";
                lblAddScoreResult.ForeColor = System.Drawing.Color.Red;
            }
            else if (drpdwnAwayTeam.SelectedValue == "-1")
            {
                lblAddScoreResult.Visible = true;
                lblAddScoreResult.Text = "Please select the Away Team.";
                lblAddScoreResult.ForeColor = System.Drawing.Color.Red;
            }
            else if (drpdwnAwayScore.SelectedValue == "-1")
            {
                lblAddScoreResult.Visible = true;
                lblAddScoreResult.Text = "Please select the Away Score.";
                lblAddScoreResult.ForeColor = System.Drawing.Color.Red;
            }
            else if (drpdwnHomeTeam.SelectedValue == drpdwnAwayTeam.SelectedValue)
            {
                lblAddScoreResult.Visible = true;
                lblAddScoreResult.Text = "Home Team and Away Team cannot be the same.";
                lblAddScoreResult.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                //Convert the selected scores from strings to Integers so they can be compared
                int homeScore = Int32.Parse(drpdwnHomeScore.SelectedValue);
                int awayScore = Int32.Parse(drpdwnAwayScore.SelectedValue);
                string homeTeamName = drpdwnHomeTeam.SelectedValue;
                String awayTeamName = drpdwnAwayTeam.SelectedValue;

                String homeTeamSqlRequest = "UPDATE LEAGUE_TABLE set played = (played+1)," +
                                            "goals_for = goals_for + @homeScore," +
                                            "goals_against = goals_against + @awayScore," +
                                            "goal_difference = goal_difference + @homeScore - @awayScore," +
                                            "form_num_games = CASE WHEN form_num_games< 5 THEN form_num_games + 1 ELSE 5 END,";


                String awayTeamSqlRequest = "UPDATE LEAGUE_TABLE set played = (played+1)," +
                                            "goals_for = goals_for + @awayScore," +
                                            "goals_against = goals_against + @homeScore," +
                                            "goal_difference = goal_difference + @awayScore - @homeScore,"+
                                            "form_num_games = CASE WHEN form_num_games< 5 THEN form_num_games + 1 ELSE 5 END,";

                if (homeScore > awayScore) {
                    homeTeamSqlRequest = homeTeamSqlRequest + "wins = (wins+1), points = (points+3), form =  'W' + LEFT(form, 4)";
                    awayTeamSqlRequest = awayTeamSqlRequest + "losses = (losses+1), form =  'L' + LEFT(form, 4)";

                }
                else if (homeScore < awayScore)
                {
                    homeTeamSqlRequest = homeTeamSqlRequest + "losses = (losses+1),form = 'L' + LEFT(form, 4)";
                    awayTeamSqlRequest = awayTeamSqlRequest + "wins = (wins+1), points = (points+3),form = 'W' + LEFT(form, 4)";
                }
                else if (homeScore == awayScore)
                {
                    homeTeamSqlRequest = homeTeamSqlRequest + "draws = (draws+1),points = (points+1),form = 'D' + LEFT(form, 4)";
                    awayTeamSqlRequest = awayTeamSqlRequest + "draws = (draws+1),points = (points+1),form = 'D' + LEFT(form, 4)";
                }

                homeTeamSqlRequest = homeTeamSqlRequest + " WHERE teamname = @homeTeamName ";
                awayTeamSqlRequest = awayTeamSqlRequest + " WHERE teamname = @awayTeamName";
                String fullSqlRequest = homeTeamSqlRequest + awayTeamSqlRequest;


                try
                {
                    
                    using (con)
                    {
                        //Update the HOME Teams Stats
                        SqlCommand cmd = new SqlCommand(fullSqlRequest, con);
                        cmd.Parameters.AddWithValue("@homeScore", homeScore);
                        cmd.Parameters.AddWithValue("@awayScore", awayScore);
                        cmd.Parameters.AddWithValue("@homeTeamName", homeTeamName);
                        cmd.Parameters.AddWithValue("@awayTeamName", awayTeamName);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        lblAddScoreResult.Visible = true;
                        lblAddScoreResult.Text = $"Score successfully added! {homeTeamName} {homeScore} - {awayTeamName} {awayScore}";
                        lblAddScoreResult.ForeColor = System.Drawing.Color.Green;
                    }
                }
                catch (Exception ex)
                {
                    lblAddScoreResult.Visible = true;
                    lblAddScoreResult.Text = "Sorry there has been an error! Technical data as follows: " + ex;
                    lblAddScoreResult.ForeColor = System.Drawing.Color.Red;
                }
            }                      
        }
    }
}