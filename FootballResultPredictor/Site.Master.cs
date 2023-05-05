using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FootballResultPredictor
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //If the User is not logged in - redirect to the Login Page
            string currentUrl = HttpContext.Current.Request.Url.LocalPath;

            if (Session["loggedIn"] == null && currentUrl.EndsWith("Login") == false && currentUrl.EndsWith("Register") == false)

            {
                Response.Redirect("Login.aspx");
            }

        }
    }
}