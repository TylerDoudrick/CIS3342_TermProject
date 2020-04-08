using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Messages : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/interactions/";
        string profileWebAPI = "https://localhost:44375/api/profile/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] == null) Response.Redirect("Default.aspx");

        }

        protected void showMessage(object sender, EventArgs e)
        {
            divMessageList.Visible = false;
            divMessageListControls.Visible = false;
            divViewMessageControls.Visible = true;
            divViewMessage.Visible = true;
        }
        protected void ViewMessageList(object sender, EventArgs e)
        {
            divMessageList.Visible = true;
            divMessageListControls.Visible = true;
            divViewMessageControls.Visible = false;
            divViewMessage.Visible = false;
        }
    }
}