using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["LoggedIn"] == null)
            {
                navLoggedIn.Visible = false;
                navLoggedOut.Visible = true;
            }
            else
            {
                navLoggedIn.Visible = true;
                navLoggedOut.Visible = false;
            }
        }
    }
}