using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FootballResultPredictor
{
    public partial class GetPredictions : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Football_Score_Predictor_Connection_String"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmitTeams_Click(object sender, EventArgs e)
        {
            if (drpdwnHomeTeam.SelectedValue == "-1")
            {
                lblScorePrediction.Visible = true;
                lblScorePrediction.Text = "Please select the Home Team.";
                lblScorePrediction.ForeColor = System.Drawing.Color.Red;
            }

            else if (drpdwnAwayTeam.SelectedValue == "-1")
            {
                lblScorePrediction.Visible = true;
                lblScorePrediction.Text = "Please select the Away Team.";
                lblScorePrediction.ForeColor = System.Drawing.Color.Red;
            }

            else if (drpdwnHomeTeam.SelectedValue == drpdwnAwayTeam.SelectedValue)
            {
                lblScorePrediction.Visible = true;
                lblScorePrediction.Text = "Home Team and Away Team cannot be the same.";
                lblScorePrediction.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                //Declare the variables that will be assiged values by the results of the sql queries below
                string homeTeamName = drpdwnHomeTeam.SelectedValue;
                String awayTeamName = drpdwnAwayTeam.SelectedValue;
                int homeTeamSquadValue = 0;
                int homeTeamLeaguePosition = 0;
                String homeTeamForm = null;
                int homeTeamFormNumGames = 0;
                int awayTeamSquadValue = 0;
                int awayTeamLeaguePosition = 0;
                String awayTeamForm = null;
                int awayTeamFormNumGames = 0;

                //Send the sql queries and capture the results
                try
                {
                    String sqlQuery =
                        @"select * from (
                        SELECT *, RANK() OVER(ORDER BY[points] DESC, [goal_difference] DESC, [goals_for] DESC, [teamname] ASC) league_position FROM[Football Score Predictor].[dbo].[LEAGUE_TABLE])
                        AS SUBQUERY
                        WHERE [teamname] = @teamName";

                    //Get the Home Team Stats
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    cmd.Parameters.AddWithValue("@teamName", homeTeamName);
                    SqlDataAdapter SQLAdapter = new SqlDataAdapter(cmd);
                    DataTable dtHomeTeam = new DataTable();
                    SQLAdapter.Fill(dtHomeTeam);
                    //Extract the values from the result. Note for the numbers we first have to change them to strings and then to integers.
                    homeTeamSquadValue = Int32.Parse(dtHomeTeam.Rows[0]["squad_val"].ToString());
                    homeTeamLeaguePosition = Int32.Parse(dtHomeTeam.Rows[0]["league_position"].ToString());
                    homeTeamForm = dtHomeTeam.Rows[0]["form"].ToString();
                    homeTeamFormNumGames = Int32.Parse(dtHomeTeam.Rows[0]["form_num_games"].ToString());

                    //Get the Away Team Stats
                    cmd = new SqlCommand(sqlQuery, con);
                    cmd.Parameters.AddWithValue("@teamName", awayTeamName);
                    SQLAdapter = new SqlDataAdapter(cmd);
                    DataTable dtAwayTeam = new DataTable();
                    SQLAdapter.Fill(dtAwayTeam);
                    //Extract the values from the result
                    awayTeamSquadValue = Int32.Parse(dtAwayTeam.Rows[0]["squad_val"].ToString());
                    awayTeamLeaguePosition = Int32.Parse(dtAwayTeam.Rows[0]["league_position"].ToString());
                    awayTeamForm = dtAwayTeam.Rows[0]["form"].ToString();
                    awayTeamFormNumGames = Int32.Parse(dtAwayTeam.Rows[0]["form_num_games"].ToString());

                }
                catch (Exception ex)
                {
                    lblScorePrediction.Visible = true;
                    lblScorePrediction.Text = "Sorry there has been an error! Technical data as follows: " + ex;
                    lblScorePrediction.ForeColor = System.Drawing.Color.Red;
                }

                //Start calculating the weightings

                //(1)Calculate the Home Team Weighting

                //The home team automatically gets a weighting of 10 points or 5 if less than 5 games have been played
                //(You don't get points for league position if less than 5 games have been played - so 10 points for the home team would be to much of an advantage!)
                int noOfGamesToUseForForm = Math.Min(homeTeamFormNumGames, awayTeamFormNumGames);
                int awayTeamTotalWeighting = 0;
                int homeTeamTotalWeighting = 0;

                if (noOfGamesToUseForForm > 4)
                {
                    homeTeamTotalWeighting = 10;
                }
                else
                {
                    homeTeamTotalWeighting = 5;
                }

                //(2) Calculate FORM Weighting for each team
                //Find which team has the lowest number of games from which their form is based on and we will only look at that number of games.

                homeTeamForm = homeTeamForm.Substring(0, noOfGamesToUseForForm);
                awayTeamForm = awayTeamForm.Substring(0, noOfGamesToUseForForm);

                int homeTeamFormWeighting = getFormWeighting(homeTeamForm);
                int awayTeamFormWeighting = getFormWeighting(awayTeamForm);

                homeTeamTotalWeighting = homeTeamTotalWeighting + homeTeamFormWeighting;
                awayTeamTotalWeighting = awayTeamTotalWeighting + awayTeamFormWeighting;

                //(3) Calculate LEAGUE Position Weighting for each team
                int homeTeamLeagueWeighting = 0;
                int awayTeamLeagueWeighting = 0;

                //ignore league position if less than 5 games played!
                if (noOfGamesToUseForForm > 4)                    
                {
                    homeTeamLeagueWeighting = 21 - homeTeamLeaguePosition;
                    awayTeamLeagueWeighting = 21 - awayTeamLeaguePosition;
                }

                homeTeamTotalWeighting = homeTeamTotalWeighting + homeTeamLeagueWeighting;
                awayTeamTotalWeighting = awayTeamTotalWeighting + awayTeamLeagueWeighting;

                //(4) Calculate Squad Value Weighting for each team - Squad value/100 then rounded to the nearest int e.g 368million = 4
                int homeTeamSquadValueWeighting = getSquadValueWeighting(homeTeamSquadValue);
                int awayTeamSquadValueWeighting = getSquadValueWeighting(awayTeamSquadValue);

                homeTeamTotalWeighting = homeTeamTotalWeighting + homeTeamSquadValueWeighting;
                awayTeamTotalWeighting = awayTeamTotalWeighting + awayTeamSquadValueWeighting;

                // Now we can predict the Result based on the difference in the weightings
                string result = null;
                var random = new Random();
                var list = new List<string>();

                
                if (homeTeamTotalWeighting > awayTeamTotalWeighting + 3)
                {
                    //Code for a home team win
                    result = homeTeamName + " win: " + predictWinningScore(homeTeamTotalWeighting, awayTeamTotalWeighting);
                }

                else if (awayTeamTotalWeighting > homeTeamTotalWeighting + 3)
                {
                    //Code for an away team win
                    String predictedScore = predictWinningScore(awayTeamTotalWeighting, homeTeamTotalWeighting);
                    //The following code REVERSES the string so the winning score is the AWAY score (you have to convert the string to charArray, reverse and then change back again)
                    char[] charArray = predictedScore.ToCharArray();
                    Array.Reverse(charArray);
                    String predictedScoreCorrectOrder = new string(charArray);
                    result = awayTeamName + " win: " + predictedScoreCorrectOrder;
                }
                else
                {
                    //Code for a draw
                    //Pick a random draw score:
                    list = new List<string> { "0 - 0", "1 - 1", "2 - 2", "3 - 3" };
                    int index = random.Next(list.Count);
                    result = "Draw! : " + (list[index]);
                }

                //Display the result
                lblScorePrediction.Visible = true;
                lblScorePrediction.Text = $"Predicted Result: {result}";
                lblScorePrediction.ForeColor = System.Drawing.Color.Green;
            }
        }
        protected int getFormWeighting(String teamFormString)
        {
            int teamFormWeighting = 0;
            foreach (char c in teamFormString)
            {
                switch (c)
                {
                    case 'W':
                        teamFormWeighting = teamFormWeighting + 3;
                        break;
                    case 'D':
                        teamFormWeighting = teamFormWeighting + 1;
                        break;
                    case 'L':
                        break;
                }
            }
            return teamFormWeighting;
        }

        protected int getSquadValueWeighting(int teamSquadValue)
        {
            //In order to round properly we have to convert the squad value to a decimal, do the rounding and then turn it back to an integer
            
            decimal squadValueDecimal = teamSquadValue;

            //I want to round UP so am using MidpointRounding.AwayFromZero:
            decimal squadValueWeightingDecimal = Math.Round(squadValueDecimal / 100, 0, MidpointRounding.AwayFromZero);
            int squadValueWeighting = Convert.ToInt32(squadValueWeightingDecimal);

            return squadValueWeighting;
        }


        protected string predictWinningScore(int winningTeamWeighting, int losingTeamWeighting)
        {
            String winningScore = null;
            var random = new Random();
            var list = new List<string>();
            int weightingDifference = winningTeamWeighting - losingTeamWeighting;

            if (weightingDifference < 10)
            {
                //Pick a winning score by 1 goal with a random selection from the list:
                list = new List<string> { "1 - 0", "2 - 1", "3 - 2"};
                int index = random.Next(list.Count);
                winningScore = (list[index]);
            }
            if (weightingDifference >= 10 && weightingDifference < 25)
            {
                //Pick a winning score by 2 goals with a random selection from the list:
                list = new List<string> { "2 - 0", "3 - 1", "4 - 2" };
                int index = random.Next(list.Count);
                winningScore = (list[index]);
            }
            if (weightingDifference >= 25 && weightingDifference < 40)
            {
                //Pick a winning score by 3 goals with a random selection from the list:
                list = new List<string> { "3 - 0", "4 - 1", "5 - 2" };
                int index = random.Next(list.Count);
                winningScore = (list[index]);
            }
            if (weightingDifference >= 40)
            {
                //Pick a winning score by 4 goals with a random selection from the list: (There is such a big weighting difference there could be ANY score!
                list = new List<string> { "4 - 0", "5 - 0", "6 - 0", "5 - 1", "6 - 0" };
                int index = random.Next(list.Count);
                winningScore = (list[index]);
            }
            return winningScore;
        }        

}
}
