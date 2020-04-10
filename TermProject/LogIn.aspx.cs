﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TermProject
{
    public partial class LogIn : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";
        DBConnect dbConnection = new DBConnect();
        SqlCommand commandObj = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoginSubmit_Click(object sender, EventArgs e)
        {
            
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_GetPreferences";

            SqlParameter uid = new SqlParameter("@userID", 1)
            {
                Direction = ParameterDirection.Input,

                SqlDbType = SqlDbType.VarChar
            };

            commandObj.Parameters.Add(uid);

            DBConnect OBJ = new DBConnect();
            DataSet ds = OBJ.GetDataSetUsingCmdObj(commandObj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow test = ds.Tables[0].Rows[0];
                Response.Write(test["memberLikes"]);
                Byte[] testarray = (Byte[])test["memberLikes"];
                MemoryStream memorystreamd = new MemoryStream(testarray);
                BinaryFormatter bfd = new BinaryFormatter();
                List<int> memberLikes = bfd.Deserialize(memorystreamd) as List<int>;

                Byte[] test2 = (Byte[])ds.Tables[0].Rows[0][1];
                MemoryStream m2 = new MemoryStream(test2);
                BinaryFormatter bfd2 = new BinaryFormatter();
                List<int> memberDislikes = bfd2.Deserialize(m2) as List<int>;

                Byte[] test3 = (Byte[])ds.Tables[0].Rows[0][1];
                MemoryStream m3 = new MemoryStream(test3);
                BinaryFormatter bfd3 = new BinaryFormatter();
                List<int> memberBlocks = bfd3.Deserialize(m3) as List<int>;

                Session["memberLikes"] = memberLikes;
                Session["memberDislikes"] = memberDislikes;
                Session["memberBlocks"] = memberBlocks;
            }

            Session["UserID"] = 1; Response.Redirect("Dashboard.aspx");

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
                 // get preferences
                 WebRequest request = WebRequest.Create(interactionsWebAPI + "getPreferences/" + userID);
                 WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                 StreamReader reader = new StreamReader(theDataStream);
                 string data = reader.ReadToEnd();
                 reader.Close(); response.Close();
                 ds = JsonConvert.DeserializeObject<DataSet>(data);

                 JavaScriptSerializer js = new JavaScriptSerializer();
                 List<int> memberLikes = js.Deserialize<List<int>>(ds.Tables[0].Rows[0][0].ToString());
                 List<int> memberDislikes = js.Deserialize<List<int>>(ds.Tables[0].Rows[0][1].ToString());
                 List<int> memberBlocks = js.Deserialize<List<int>>(ds.Tables[0].Rows[0][1].ToString());

                 Session["userID"] = userID;
                 Session["memberLikes"] = memberLikes;
                 Session["memberDislikes"] = memberDislikes;
                 Session["memberBlocks"] = memberBlocks;

         }
         */
        }
    }
}