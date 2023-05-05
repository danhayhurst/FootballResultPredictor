using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace FootballResultPredictor
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Football_Score_Predictor_Connection_String"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                lblLoginResult.Visible = true;
                lblLoginResult.Text = "Please enter your Username";
                lblLoginResult.ForeColor = System.Drawing.Color.Red;
            }
            else if (txtPassword.Text == "")
            {
                lblLoginResult.Visible = true;
                lblLoginResult.Text = "Please enter your Password";
                lblLoginResult.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                try
                {
                    //COLLATE SQL_Latin1_General_CP1_CS_AS makes the password case sensitive
                    SqlCommand cmd = new SqlCommand("SELECT * FROM LOGIN_CREDENTIALS where username = @username and password = @password COLLATE SQL_Latin1_General_CP1_CS_AS", con);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    SqlDataAdapter SQLAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    SQLAdapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        lblLoginResult.Visible = true;
                        lblLoginResult.Text = "Login Successful! Welcome back " + txtUsername.Text + ".";
                        lblLoginResult.ForeColor = System.Drawing.Color.Green;
                        Session["loggedIn"] = true;
                        Session["username"] = txtUsername.Text;
                        Response.AppendHeader("Refresh", "2;url=GetPredictions.aspx");
                    }
                    else
                    {
                        lblLoginResult.Visible = true;
                        lblLoginResult.Text = "Sorry, we did not recognise your sign-in details, please try again.";
                        lblLoginResult.ForeColor = System.Drawing.Color.Red;
                    }

                }
                catch (Exception ex)
                {
                    lblLoginResult.Visible = true;
                    lblLoginResult.Text = "Sorry there has been an error! Technical data as follows: " + ex;
                    lblLoginResult.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

     }
 }
