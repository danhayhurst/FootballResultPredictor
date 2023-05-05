using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FootballResultPredictor
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = (string)Session["username"];
            lblLogout.ForeColor = System.Drawing.Color.Green;
            lblLogout.Text = "Click the button below to Logout " + username + ".";
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            lblLogoutResult.Visible = true;
            lblLogoutResult.Text = "Logout Successful! You will now be taken back to the Login Page.";
            lblLogoutResult.ForeColor = System.Drawing.Color.Green;
            Session.Clear();
            Response.AppendHeader("Refresh", "3;url=Login.aspx");
        }
    }
}