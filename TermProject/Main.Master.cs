using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TermProject.UserControls;

namespace TermProject
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If the user isn't logged in, hide the nav buttons for logged in users
            if(Session["UserID"] == null)
            {
                navLoggedIn.Visible = false;
                navLoggedOut.Visible = true;
            }

            //If the user is logged in, spin up the notifier and set the logged in text.
            else
            {
                NotifierPlaceholder.Visible = true;
                Notifier NotifierControl = (Notifier)Page.LoadControl("~/UserControls/Notifier.ascx");
                NotifierPlaceholder.Controls.Add(NotifierControl);
                navLoggedIn.Visible = true;
                navLoggedOut.Visible = false;
                lblLoggedIn.Text = "Signed in as " + Session["FirstName"] + " " + Session["LastName"];

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //When the login button is clicked, send the user to the login page with the current page as a GET variable in the url
            //      This is used to redirect to the page they were trying to view before they logged in.
            Response.Redirect("LogIn.aspx?target=" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) +"&"+ Request.QueryString.ToString());
        }
    }
}