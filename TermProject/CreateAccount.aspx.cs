using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace TermProject
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";
        DBConnect dbConnection = new DBConnect();
        SqlCommand commandObj = new SqlCommand();
        int userID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { 
            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_LookupAllSecurityQuestions";
            DataSet myDS = databaseObj.GetDataSetUsingCmdObj(commandObj);
            DataTable firstSet = myDS.Tables[0];
            DataTable secondSet = myDS.Tables[1];
            DataTable thirdSet = myDS.Tables[2];

            ddlSecurityQOne.DataSource = firstSet;
            ddlSecurityQTwo.DataSource = secondSet;
            ddlSecurityQThree.DataSource = thirdSet;

            ddlSecurityQOne.DataTextField = "SecurityQuestionText";
            ddlSecurityQOne.DataValueField = "SecurityQuestionID";
            ddlSecurityQTwo.DataTextField = "SecurityQuestionText";
            ddlSecurityQTwo.DataValueField = "SecurityQuestionID";
            ddlSecurityQThree.DataTextField = "SecurityQuestionText";
            ddlSecurityQThree.DataValueField = "SecurityQuestionID";

            ddlSecurityQOne.DataBind();
            ddlSecurityQTwo.DataBind();
            ddlSecurityQThree.DataBind();

            ddlSecurityQOne.Items.Insert(0, new ListItem("Please Select a Question...", String.Empty));
            ddlSecurityQTwo.Items.Insert(0, new ListItem("Please Select a Question...", String.Empty));
            ddlSecurityQThree.Items.Insert(0, new ListItem("Please Select a Question...", String.Empty));

            ddlSecurityQOne.SelectedIndex = 0;
            ddlSecurityQTwo.SelectedIndex = 0;
            ddlSecurityQThree.SelectedIndex = 0;
            }
        }


        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text;
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string passwordConfirm = txtConfirmPassword.Text;
            string firstName = txtFName.Text;
            string lastName = txtLName.Text;
            string AddressOne = txtAddressOne.Text;
            string AddressTwo = txtAddressTwo.Text;
            string city = txtCity.Text;
            string state = ddlState.SelectedValue;
            string zip = txtZip.Text;

            string SQOne = txtSecurityQOne.Text;
            string SQTwo = txtSecurityQTwo.Text;
            string SQThree = txtSecurityQThree.Text;
            //
            //Regular Expressions sourced from http://regexlib.com
            //

            Regex regexEmail = new Regex(@"^([\w\d\-\.]+)@{1}(([\w\d\-]{1,67})|([\w\d\-]+\.[\w\d\-]{1,67}))\.(([a-zA-Z\d]{2,4})(\.[a-zA-Z\d]{2})?)$");
            Regex regexPassword = new Regex(@"^(?=.*\d).{7,20}$");
            Regex regexZip = new Regex(@"(^\d{5}$)|(^\d{5}-\d{4}$)");

            txtUsername.CssClass = txtUsername.CssClass.Replace("is-invalid", "").Trim();
            txtEmail.CssClass = txtEmail.CssClass.Replace("is-invalid", "").Trim();
            txtPassword.CssClass = txtPassword.CssClass.Replace("is-invalid", "").Trim();
            txtConfirmPassword.CssClass = txtConfirmPassword.CssClass.Replace("is-invalid", "").Trim();
            txtFName.CssClass = txtFName.CssClass.Replace("is-invalid", "").Trim();
            txtLName.CssClass = txtLName.CssClass.Replace("is-invalid", "").Trim();
            txtAddressOne.CssClass = txtAddressOne.CssClass.Replace("is-invalid", "").Trim();
            txtAddressTwo.CssClass = txtAddressTwo.CssClass.Replace("is-invalid", "").Trim();
            txtCity.CssClass = txtCity.CssClass.Replace("is-invalid", "").Trim();
            ddlState.CssClass = ddlState.CssClass.Replace("is-invalid", "").Trim();
            txtZip.CssClass = txtZip.CssClass.Replace("is-invalid", "").Trim();

            txtSecurityQOne.CssClass = txtSecurityQOne.CssClass.Replace("is-invalid", "").Trim();
            txtSecurityQTwo.CssClass = txtSecurityQTwo.CssClass.Replace("is-invalid", "").Trim();
            txtSecurityQThree.CssClass = txtSecurityQThree.CssClass.Replace("is-invalid", "").Trim();

            ddlSecurityQOne.CssClass = ddlSecurityQOne.CssClass.Replace("is-invalid", "").Trim();
            ddlSecurityQTwo.CssClass = ddlSecurityQTwo.CssClass.Replace("is-invalid", "").Trim();
            ddlSecurityQThree.CssClass = ddlSecurityQThree.CssClass.Replace("is-invalid", "").Trim();


            Boolean trigger = false;
            if (username.Length <= 0)
            {
                trigger = true;
                txtUsername.CssClass += " is-invalid";
            }
            if (email.Length <= 0 || !regexEmail.IsMatch(email))
            {
                trigger = true;
                txtEmail.CssClass += " is-invalid";
            }
            if (password.Length <= 0 || !regexPassword.IsMatch(password))
            {
                trigger = true;
                txtPassword.CssClass += " is-invalid";
                txtPassword.Text = "";
            }
            if (password != passwordConfirm)
            {
                txtConfirmPassword.CssClass += " is-invalid";
                txtConfirmPassword.Text = "";
            }
            if (firstName.Length <= 0)
            {
                trigger = true;
                txtFName.CssClass += " is-invalid";
            }
            if (lastName.Length <= 0)
            {
                trigger = true;
                txtLName.CssClass += " is-invalid";
            }
            if (AddressOne.Length <= 0)
            {
                trigger = true;
                txtAddressOne.CssClass += " is-invalid";
            }
            if (city.Length <= 0)
            {
                trigger = true;
                txtCity.CssClass += " is-invalid";
            }
            if (state.Length <= 0)
            {
                trigger = true;
                ddlState.CssClass += " is-invalid";
            }
            if (zip.Length <= 0 || !regexZip.IsMatch(zip))
            {
                trigger = true;
                txtZip.CssClass += " is-invalid";
            }
            if (SQOne.Length <= 0)
            {
                trigger = true;
                txtSecurityQOne.CssClass += " is-invalid";
            }
            if (SQTwo.Length <= 0)
            {
                trigger = true;
                txtSecurityQTwo.CssClass += " is-invalid";
            }
            if (SQThree.Length <= 0)
            {
                trigger = true;
                txtSecurityQThree.CssClass += " is-invalid";
            }
            if(ddlSecurityQOne.SelectedValue == "")
            {
                trigger = true;
                ddlSecurityQOne.CssClass += " is-invalid";
            }
            if (ddlSecurityQTwo.SelectedValue == "")
            {
                trigger = true;
                ddlSecurityQTwo.CssClass += " is-invalid";
            }
            if (ddlSecurityQThree.SelectedValue == "")
            {
                trigger = true;
                ddlSecurityQThree.CssClass += " is-invalid";
            }

            if (trigger)
            {
            }
            else
            {
                //Password Salting & Hashing
                byte [] saltArray = CryptoUtilities.GenerateSalt();
                byte[] hashPassword = CryptoUtilities.CalculateMD5Hash(saltArray, password);

                commandObj.Parameters.Clear();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "TP_CreateUser";

                SqlParameter inputUsername = new SqlParameter("@username", username)
                {
                    Direction = ParameterDirection.Input,

                    SqlDbType = SqlDbType.VarChar
                };
                SqlParameter inputPassword = new SqlParameter("@password", hashPassword)
                {
                    Direction = ParameterDirection.Input,

                    SqlDbType = SqlDbType.VarBinary
                };
                SqlParameter inputSalt = new SqlParameter("@salt", saltArray)
                {
                    Direction = ParameterDirection.Input,

                    SqlDbType = SqlDbType.VarBinary
                };
                SqlParameter inputEmail = new SqlParameter("@emailAddress", email) 
                {
                    Direction = ParameterDirection.Input,

                    SqlDbType = SqlDbType.VarChar
                };
                SqlParameter inputFirstName = new SqlParameter("@firstName", firstName)
                {
                    Direction = ParameterDirection.Input,

                    SqlDbType = SqlDbType.VarChar
                };
                SqlParameter inputLastName = new SqlParameter("@lastName", lastName)
                {
                    Direction = ParameterDirection.Input,

                    SqlDbType = SqlDbType.VarChar
                };

                SqlParameter inputBilling = new SqlParameter("@billing", AddressOne)
                {
                    Direction = ParameterDirection.Input,

                    SqlDbType = SqlDbType.VarChar
                };

                SqlParameter inputCity = new SqlParameter("@city", city)
                {
                    Direction = ParameterDirection.Input,

                    SqlDbType = SqlDbType.VarChar
                };
                SqlParameter inputState = new SqlParameter("@state", state)
                {
                    Direction = ParameterDirection.Input,

                    SqlDbType = SqlDbType.VarChar
                };
                SqlParameter inputZip = new SqlParameter("@zip", Convert.ToInt32(zip))
                {
                    Direction = ParameterDirection.Input,

                    SqlDbType = SqlDbType.VarChar
                };
                SqlParameter outputUsernameExists = new SqlParameter("@UsernameExists", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                SqlParameter outputEmailExists = new SqlParameter("@EmailExists", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                SqlParameter outputNewUserID = new SqlParameter("@NewUserID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                commandObj.Parameters.Add(inputUsername);
                commandObj.Parameters.Add(inputPassword);
                commandObj.Parameters.Add(inputSalt);
                commandObj.Parameters.Add(inputEmail);
                commandObj.Parameters.Add(inputFirstName);
                commandObj.Parameters.Add(inputLastName);
                commandObj.Parameters.Add(inputBilling);
                commandObj.Parameters.Add(inputCity);
                commandObj.Parameters.Add(inputState);
                commandObj.Parameters.Add(inputZip);
                commandObj.Parameters.Add(outputNewUserID);

                commandObj.Parameters.Add(outputEmailExists);
                commandObj.Parameters.Add(outputUsernameExists);


                if (dbConnection.DoUpdateUsingCmdObj(commandObj, out string exception) == -2)
                {
                    Response.Write(exception);
                }
                else
                {
                    if (Int32.Parse(outputUsernameExists.Value.ToString()) == 1)
                    {
                        Response.Write("Fail, username exists");
                    }
                    if (Int32.Parse(outputEmailExists.Value.ToString()) == 1)
                    {
                        Response.Write("Fail, email exists");
                    }
                    if (!(Int32.Parse(outputUsernameExists.Value.ToString()) == 1 && (Int32.Parse(outputEmailExists.Value.ToString()) == 1)))
                    {


                        commandObj.Parameters.Clear();
                        commandObj.CommandType = CommandType.StoredProcedure;
                        commandObj.CommandText = "TP_UpdateSecurityQuestions";
                        DataTable dtSecurityQuestions = new DataTable();
                        dtSecurityQuestions.Columns.Add("UserId", typeof(int));
                        dtSecurityQuestions.Columns.Add("QuestionID", typeof(int));
                        dtSecurityQuestions.Columns.Add("QuestionAnswer", typeof(string));

                        DataRow newRow = dtSecurityQuestions.NewRow();
                        newRow["UserId"] = Int32.Parse(outputNewUserID.Value.ToString());
                        newRow["QuestionId"] = Int32.Parse(ddlSecurityQOne.SelectedValue);
                        newRow["QuestionAnswer"] = txtSecurityQOne.Text;
                        dtSecurityQuestions.Rows.Add(newRow);

                        newRow = dtSecurityQuestions.NewRow();
                        newRow["UserId"] = Int32.Parse(outputNewUserID.Value.ToString());
                        newRow["QuestionId"] = Int32.Parse(ddlSecurityQTwo.SelectedValue);
                        newRow["QuestionAnswer"] = txtSecurityQTwo.Text;
                        dtSecurityQuestions.Rows.Add(newRow);

                        newRow = dtSecurityQuestions.NewRow();
                        newRow["UserId"] = Int32.Parse(outputNewUserID.Value.ToString());
                        newRow["QuestionId"] = Int32.Parse(ddlSecurityQThree.SelectedValue);
                        newRow["QuestionAnswer"] = txtSecurityQThree.Text;
                        dtSecurityQuestions.Rows.Add(newRow);

                        commandObj.Parameters.AddWithValue("@SecurityQuestions", dtSecurityQuestions);
                        commandObj.Parameters.AddWithValue("@UserID", outputNewUserID.Value.ToString());
                        if (dbConnection.DoUpdateUsingCmdObj(commandObj, out exception) == -2)
                        {
                            Response.Write(exception);
                        }
                        else
                        {
                            // insert empty list of prefs
                            insertPreferences(Convert.ToInt32(outputNewUserID.Value.ToString()));
                            Session["RegisteringUserID"] = outputNewUserID;
                            Session["token"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1ODcyNzY2Nzd9.nUnarRJiy26XQjw9AFE986rYRTvykpLJs8483vX91wE";

                            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                            byte[] random = new byte[16];
                            rng.GetBytes(random);

                            string rngString = Convert.ToBase64String(random);
                            string trimmed = rngString.Substring(0, rngString.Length - 2);

                            // send email
                            string sendAdd = "querydating@gmail.com";
                            MailMessage msg = new MailMessage();
                            msg.To.Add(new MailAddress(@email));
                            msg.Subject = "QUERY Welcome Email";
                            msg.From = new MailAddress(sendAdd);
                            msg.IsBodyHtml = true;
                            msg.Body = "<div> Thank you for signing up for Query.com! <Br><BR> You have successfully " +
                                "created an account. To verify, please enter the verification code: <strong>" + trimmed +
                                "</strong><Br><BR> <div>";
                            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                            smtp.Credentials = new System.Net.NetworkCredential(sendAdd, "CIS3342TermProject");
                            smtp.EnableSsl = true;

                            smtp.Send(msg);
                            Session["email"] = email;



                            commandObj.Parameters.Clear();
                            commandObj.CommandType = CommandType.StoredProcedure;
                            commandObj.CommandText = "TP_InsertVerification";

                            commandObj.Parameters.AddWithValue("@UserID", outputNewUserID.Value.ToString());
                            commandObj.Parameters.AddWithValue("@code", trimmed);


                            DBConnect OBJ = new DBConnect();
                            if(OBJ.DoUpdateUsingCmdObj(commandObj, out string err) != -2)
                            {
                                Response.Redirect("Verification.aspx");

                            }
                            else
                            {
                                Response.Write(err);
                            }

                        }

                    }
                }
            }

        }

        protected void insertPreferences(int id)
        { // calls web api to insert empty lists
            List<int> memberLikes = new List<int>();
            List<int> memberDislikes = new List<int>();
            List<int> memberBlocks = new List<int>();

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();
            Byte[] mLikes; Byte[] mDislikes; Byte[] mBlocks;

            bf.Serialize(mStream, memberLikes); mLikes = mStream.ToArray();
            bf.Serialize(mStream, memberDislikes); mDislikes = mStream.ToArray();
            bf.Serialize(mStream, memberBlocks); mBlocks = mStream.ToArray();
            
           // int userID = 100;  // this needs to be changed to the userID of the new user

            Preferences p = new Preferences();
            p.id = id;
            p.mLikes = mLikes; p.mDislikes = mDislikes; p.mBlocks = mBlocks;
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonP = js.Serialize(p);

            WebRequest request = WebRequest.Create(interactionsWebAPI + "insertPreferences/");
            request.Method = "POST";
            request.ContentLength = jsonP.Length;
            request.ContentType = "application/json";
            

            // Write the JSON data to the Web Request           
            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(jsonP);
            writer.Flush();
            writer.Close();

            // Read the data from the Web Response, which requires working with streams.
            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            Session["memberLikes"] = memberLikes;
            Session["memberDislikes"] = memberDislikes;
            Session["memberBlocks"] = memberBlocks;
        }
    }
}