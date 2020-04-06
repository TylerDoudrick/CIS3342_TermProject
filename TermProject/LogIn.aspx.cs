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
        string webapiURL = "https://localhost:44394/api/profile/";
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
                //Do something
                WebRequest request = WebRequest.Create(webapiURL + "checkLogin/"+email+"/"+password);
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();
                JavaScriptSerializer js = new JavaScriptSerializer();
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(data);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    Session["LoggedIn"] = "true";
                    Session["email"] = ds.Tables[0].Rows[0]["emailAddress"].ToString();
                    Response.Redirect("Dashboard.aspx");
                }
                else
                { 
                    // invalid login

                }
            }
            
        }
    }
}