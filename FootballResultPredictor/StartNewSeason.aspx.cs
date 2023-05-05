using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using FootballResultPredictor.App_Code;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace FootballResultPredictor
{
    public partial class StartNewSeason : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Football_Score_Predictor_Connection_String"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnStartNewSeason_Click(object sender, EventArgs e)
        {
            try
            {
                using (con)
                {
                    //First delete all existing rows in the table
                    SqlCommand cmd = new SqlCommand("DELETE FROM LEAGUE_TABLE", con);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                    //Now add the new teams
                    cmd = new SqlCommand("INSERT INTO LEAGUE_TABLE VALUES (@teamName,0,0,0,0,0,0,0,0,'-----',0,@squadVal)", con);

                    var teams = new FootballTeams().footballTeams;

                    cmd.Parameters.Add("@teamName", SqlDbType.Text);
                    cmd.Parameters.Add("@squadVal", SqlDbType.Int);
                    // iterate over all football teams and execute the INSERT statement for each of them adding the team name and its value
                    foreach (KeyValuePair<string, int> entry in teams)
                    {
                        cmd.Parameters["@teamName"].Value = entry.Key;
                        cmd.Parameters["@squadVal"].Value = entry.Value;
                        cmd.ExecuteNonQuery();
                    }
                    
                    lblStartNewSeasonResult.Visible = true;
                    lblStartNewSeasonResult.Text = "New Season Started - You are all set to go!";
                    lblStartNewSeasonResult.ForeColor = System.Drawing.Color.Green;

                }
            }
            catch (Exception ex)
            {
                lblStartNewSeasonResult.Visible = true;
                lblStartNewSeasonResult.Text = "Sorry there has been an error! Technical data as follows: " + ex;
                lblStartNewSeasonResult.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}