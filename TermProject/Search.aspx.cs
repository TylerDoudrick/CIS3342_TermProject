using MusicStoreLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace TermProject
{
    public partial class Search : System.Web.UI.Page
    {
        DBConnect obj = new DBConnect();
        string webapiURL = "https://localhost:44394/api/profile/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }

        } // end page load

       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            divResults.Attributes.Add("style", "display:flex");
        } // end search

    } // end class
} // end namespace