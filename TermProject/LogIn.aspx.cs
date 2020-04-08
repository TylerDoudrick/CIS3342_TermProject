using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TermProject
{
    public partial class LogIn : System.Web.UI.Page
    {
        DBConnect dbConnection = new DBConnect();
        SqlCommand commandObj = new SqlCommand();
        string interactionsWebAPI = "https://localhost:44375/api/interactions/";
        string profileWebAPI = "https://localhost:44375/api/profile/";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoginSubmit_Click(object sender, EventArgs e)
        {
            string username = txtLogInUsername.Text;
            string password = txtLogInPassword.Text;

            bool trigger = false;

            txtLogInUsername.CssClass = txtLogInUsername.CssClass.Replace("is-invalid", "").Trim();
            txtLogInPassword.CssClass = txtLogInPassword.CssClass.Replace("is-invalid", "").Trim();
            if (username.Length <= 0)
            {
                txtLogInUsername.CssClass += " is-invalid";
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

                commandObj.Parameters.Clear();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "TP_LookupUserRecord";

                SqlParameter inputUsername = new SqlParameter("@username", username)
                {
                    Direction = ParameterDirection.Input,

                    SqlDbType = SqlDbType.VarChar
                };

                commandObj.Parameters.Add(inputUsername);


                DataSet dsUser = dbConnection.GetDataSetUsingCmdObj(commandObj);
                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    DataRow drUserRecord = dsUser.Tables[0].Rows[0];
                    byte[] salt = (byte[])drUserRecord["salt"];
                    byte[] hashedPassword = (byte[])drUserRecord["password"];


                    if (CryptoUtilities.comparePassword(hashedPassword, salt, password))
                    {
                        Session["UserID"] = drUserRecord["userID"].ToString();
                        Response.Redirect("Dashboard.aspx");
                    }
                    else
                    {
                        Response.Write("Failed password check");
                        //Invalid password :(
                    }
                }
                else
                {
                    Response.Write("Failed username check");
                    //Profile not found
                }

            }

        }
        protected void btnDebug1_Click(object sender, EventArgs e)
        {
            Session["FirstName"] = "Mary";
            Session["LastName"] = "Poppins";
            Session["UserID"] = "20";
            Response.Redirect("Dashboard.aspx");
        }
        protected void btnDebug2_Click(object sender, EventArgs e)
        {
            Session["FirstName"] = "John";
            Session["LastName"] = "Doe";
            Session["UserID"] = "19";
            Response.Redirect("Dashboard.aspx");
               /*
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
            */
        }
    }
}