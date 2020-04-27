using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject.UserControls
{
    public partial class Notifier : System.Web.UI.UserControl
    {
        //Grab the userid and the token for use in the uc
        public string UserID = HttpContext.Current.Session["UserID"].ToString();
        public string token = HttpContext.Current.Session["token"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}