using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TermProject
{
    public partial class LogIn : System.Web.UI.Page
    {
        DBConnect dbConnection = new DBConnect();
        SqlCommand commandObj = new SqlCommand();
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
                if(dsUser.Tables[0].Rows.Count > 0)
                {
                    DataRow drUserRecord = dsUser.Tables[0].Rows[0];
                    byte[] salt = (byte[]) drUserRecord["salt"];
                    byte[] hashedPassword = (byte[]) drUserRecord["password"];


                    if (CryptoUtilities.comparePassword(hashedPassword, salt, password)){
                        //User is logged in
                        Session["LoggedIn"] = "true";
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
    }
}