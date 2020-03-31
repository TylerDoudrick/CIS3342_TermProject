using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoginSubmit_Click(object sender, EventArgs e)
        {
            string email = txtLogInEmail.Text;
            string password = txtLogInPassword.Text;

            bool trigger = false;

            txtLogInEmail.CssClass = txtLogInEmail.CssClass.Replace("is-invalid", "").Trim();
            txtLogInPassword.CssClass = txtLogInPassword.CssClass.Replace("is-invalid", "").Trim();
            if(email.Length <= 0)
            {
                txtLogInEmail.CssClass += " is-invalid";
                trigger = true;
            }
            if(password.Length <= 0)
            {
                txtLogInPassword.CssClass += " is-invalid";
                trigger = true;
            }

            if (trigger)
            {
                //Failed, do nothing
            }
            else
            {
 
                //Do something
            }
            Session["LoggedIn"] = "true";
            Response.Redirect("Dashboard.aspx");
        }
    }
}