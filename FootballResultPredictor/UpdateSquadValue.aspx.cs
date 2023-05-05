using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FootballResultPredictor
{
    public partial class UpdateSquadValue : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Football_Score_Predictor_Connection_String"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmitUpdate_Click(object sender, EventArgs e)
        {
            string teamName = drpdwnTeam.SelectedValue;

            if (teamName == "-1")
            {
                lblUpdateSquadValueResult.Visible = true;
                lblUpdateSquadValueResult.Text = "Please select the team you want to update.";
                lblUpdateSquadValueResult.ForeColor = System.Drawing.Color.Red;
            }
            else if (txtBoxNewValue.Text == "")
            {
                lblUpdateSquadValueResult.Visible = true;
                lblUpdateSquadValueResult.Text = "Please enter the new Squad Value.";
                lblUpdateSquadValueResult.ForeColor = System.Drawing.Color.Red;
            }
            else if (Regex.IsMatch(txtBoxNewValue.Text, @"^\d+$") == false)
            {
                lblUpdateSquadValueResult.Visible = true;
                lblUpdateSquadValueResult.Text = "Squad Value should contain numbers only!";
                lblUpdateSquadValueResult.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                //Convert the Squad Value from a String to an Int
                int newSquadValue = Int32.Parse(txtBoxNewValue.Text);
                try
                {
                    using (con)
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE LEAGUE_TABLE SET squad_val = @newSquadValue where teamName = @teamName", con);
                        cmd.Parameters.AddWithValue("@teamName", teamName);
                        cmd.Parameters.AddWithValue("@newSquadValue", newSquadValue);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        lblUpdateSquadValueResult.Visible = true;
                        lblUpdateSquadValueResult.Text = $"Squad Value successfully added! {teamName}'s new Squad Value is: £{newSquadValue}m.";
                        lblUpdateSquadValueResult.ForeColor = System.Drawing.Color.Green;
                    }
                }
                catch (Exception ex)
                {
                    lblUpdateSquadValueResult.Visible = true;
                    lblUpdateSquadValueResult.Text = "Sorry there has been an error! Technical data as follows: " + ex;
                    lblUpdateSquadValueResult.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
    }
}