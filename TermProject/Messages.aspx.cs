﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Messages : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null) Response.Redirect("LogIn.aspx?target=Messages");

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