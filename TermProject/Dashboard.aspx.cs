using Classess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null) Response.Redirect("Default.aspx");

            else
            {
                
                

            }
        } // end page load

        protected void lbGoToProfile_Command(object sender, CommandEventArgs e)
        { // transfers you to the profile you clicked on
            int uID = Convert.ToInt32(e.CommandName);
            Response.Redirect("MemberProfile.aspx?memberID=" + uID);
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberProfile.aspx?memberID=5");
        }
    }
}