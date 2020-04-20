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
            if(Session["UserID"] == null)
            {
                navLoggedIn.Visible = false;
                navLoggedOut.Visible = true;
            }
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
            Response.Redirect("LogIn.aspx?target=" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) +"&"+ Request.QueryString.ToString());
        }
    }
}