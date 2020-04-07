using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TermProject
{
    public partial class LogIn : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/interactions/";
        string profileWebAPI = "https://localhost:44375/api/profile/";
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
            if (email.Length <= 0)
            {
                txtLogInEmail.CssClass += " is-invalid";
                trigger = true;
            }
            if (password.Length <= 0)
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
                // validation login
                WebRequest request = WebRequest.Create(profileWebAPI + "checkLogin/"+email+"/"+password);
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();  response.Close();
                JavaScriptSerializer js = new JavaScriptSerializer();
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(data);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    Session["LoggedIn"] = "true";
                    Session["email"] = ds.Tables[0].Rows[0]["emailAddress"].ToString();
                    Response.Redirect("Dashboard.aspx");

                    // get preferences
                    int userID = Convert.ToInt32(ds.Tables[0].Rows[0]["userID"].ToString());
                    request = WebRequest.Create(interactionsWebAPI + "getPreferences/" + userID);
                    response = request.GetResponse();
                    theDataStream = response.GetResponseStream();
                    reader = new StreamReader(theDataStream);
                    data = reader.ReadToEnd();
                    reader.Close(); response.Close();
                    ds = JsonConvert.DeserializeObject<DataSet>(data);

                    List<int> memberLikes = js.Deserialize<List<int>>(ds.Tables[0].Rows[0]["memberLikes"].ToString());
                    List<int> memberDislikes = js.Deserialize<List<int>>(ds.Tables[0].Rows[0]["memberDislikes"].ToString());
                    List<int> memberBlocks = js.Deserialize<List<int>>(ds.Tables[0].Rows[0]["memberBlocks"].ToString());

                    Session["memberLikes"] = memberLikes;
                    Session["memberDislikes"] = memberDislikes;
                    Session["memberBlocks"] = memberBlocks;
                }
                else
                { 
                    // invalid login

                }
            }
            
        }
    }
}