﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Trash the session when the log out button is clicked.
            Session.Abandon();
            Response.Redirect("Dashboard.aspx");
        }
    }
}