using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;

namespace FootballResultPredictor
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Football_Score_Predictor_Connection_String"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegisterSubmit_Click(object sender, EventArgs e)
        {
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (txtRegUsername.Text == "")
                {
                    lblRegisterResult.Visible = true;
                    lblRegisterResult.Text = "Please enter your Username";
                    lblRegisterResult.ForeColor = System.Drawing.Color.Red;
                }
                else if (txtRegPassword.Text == "")
                {
                    lblRegisterResult.Visible = true;
                    lblRegisterResult.Text = "Please enter your Password";
                    lblRegisterResult.ForeColor = System.Drawing.Color.Red;
                }
                else if (txtRegPasswordConfirm.Text == "")
                {
                    lblRegisterResult.Visible = true;
                    lblRegisterResult.Text = "Please remember to confirm your Password";
                    lblRegisterResult.ForeColor = System.Drawing.Color.Red;
                }
                else if (txtRegPassword.Text != txtRegPasswordConfirm.Text)
                {
                    lblRegisterResult.Visible = true;
                    lblRegisterResult.Text = "Passwords do not match. Please try again.";
                    lblRegisterResult.ForeColor = System.Drawing.Color.Red;
                }
                else if(!hasLowerChar.IsMatch(txtRegPassword.Text))
                {
                    lblRegisterResult.Visible = true;
                    lblRegisterResult.Text = "Password should contain At least one lower case letter";
                    lblRegisterResult.ForeColor = System.Drawing.Color.Red;
                }
                else if (!hasUpperChar.IsMatch(txtRegPassword.Text))
                {
                    lblRegisterResult.Visible = true;
                    lblRegisterResult.Text = "Password should contain At least one upper case letter";
                    lblRegisterResult.ForeColor = System.Drawing.Color.Red;
                }
                else if (!hasSymbols.IsMatch(txtRegPassword.Text))
                {
                    lblRegisterResult.Visible = true;
                    lblRegisterResult.Text = "Password should contain At least one special case characters";
                    lblRegisterResult.ForeColor = System.Drawing.Color.Red;
                }

            else                
                {
                try
                {
                    //COLLATE SQL_Latin1_General_CP1_CS_AS makes the password case sensitive
                    SqlCommand cmd = new SqlCommand("SELECT * FROM LOGIN_CREDENTIALS where username = @username", con);
                    cmd.Parameters.AddWithValue("@username", txtRegUsername.Text);
                    SqlDataAdapter SQLAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    SQLAdapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        lblRegisterResult.Visible = true;
                        lblRegisterResult.Text = "Sorry, that username is already taken, please try again.";
                        lblRegisterResult.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        using (con)
                        {
                            cmd = new SqlCommand("INSERT INTO LOGIN_CREDENTIALS values (@username, @password)", con);
                            cmd.Parameters.AddWithValue("@username", txtRegUsername.Text);
                            cmd.Parameters.AddWithValue("@password", txtRegPassword.Text);
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            lblRegisterResult.Visible = true;
                            lblRegisterResult.Text = "Registration Successful! Welcome to Football Result Predictor " + txtRegUsername.Text + "!";
                            lblRegisterResult.ForeColor = System.Drawing.Color.Green;
                            Session["loggedIn"] = true;
                            Session["username"] = txtRegUsername.Text;
                            Response.AppendHeader("Refresh", "2;url=GetPredictions.aspx");
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblRegisterResult.Visible = true;
                    lblRegisterResult.Text = "Sorry there has been an error! Technical data as follows: " + ex;
                    lblRegisterResult.ForeColor = System.Drawing.Color.Red;
                }
                }
          
        }
    }
}