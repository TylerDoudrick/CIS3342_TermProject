using Classess;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Settings : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";
        int userID;
        List<int> memberBlocks = new List<int>();
        DBConnect obj = new DBConnect(); SqlCommand objCMD = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblSuccess.Text = ""; lblError.Text = ""; lblSearchUpdate.Text = "";
            if (Session["UserID"] == null) Response.Redirect("LogIn.aspx?target=Settings");

            else
            {
                userID = Convert.ToInt32(Session["UserID"]); // get user id

                // call web api method to get the data for this page
                WebRequest request = WebRequest.Create(profileWebAPI + "GetSettings/" + userID);
                request.Headers.Add("Authorization", "Bearer " + Session["token"].ToString());

                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                string data = reader.ReadToEnd();
                reader.Close(); response.Close();
                JavaScriptSerializer js = new JavaScriptSerializer();

                DataSet ds = JsonConvert.DeserializeObject<DataSet>(data); // ds contains username + password, hide profile pref, blocked

                // set text of controls to the data recieved
                txtCurrentUsername.Text = ds.Tables[0].Rows[0][0].ToString();
                txtCurrentPassword.Text = ds.Tables[0].Rows[0][1].ToString();

                txtStAddresses.Text = ds.Tables[0].Rows[0][2].ToString();
                txtCity.Text = ds.Tables[0].Rows[0][3].ToString();
                ddlState.SelectedValue = ds.Tables[0].Rows[0][4].ToString();
                txtZip.Text = ds.Tables[0].Rows[0][5].ToString();

                if (ds.Tables[1].Rows.Count == 0)
                { // if there were no results in the out out of search, that means they want to stay visible
                    rbNo.Checked = true;
                }
                else rbYes.Checked = true;

                memberBlocks = (List<int>) Session["memberBlocks"]; // get blocks list from                
                bindDL(); // bind the memberlikes data list
                if (!IsPostBack)
                {
                    Session["remain"] = 1; // variable used for security question attempts
                    Random random = new Random(); int r = random.Next(0, 3); // get random number --> will choose security question
                    Session["secAns"] = ds.Tables[2].Rows[r]["questionAnswer"].ToString().ToLower();
                    lblSecurityQuestion.Text = ds.Tables[2].Rows[r]["SecurityQuestionText"].ToString();
                } // end if
            }
        } //end pageload

        protected void lbChangeUsername_Click(object sender, EventArgs e)
        { 
            // when this link button is clicked, the div that allows the user to change their username will be made visibile

            divOPTOut.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:none");
            divAddress.Attributes.Add("style", "display:none");
            Session["changeType"] = "username";

            // only show the username div if user has entered the correct answer within 3 attempts
            if (Session["secTrigger"] == null )
            {
                divSecurityQuestions.Attributes.Add("style", "display:block");
            }
            else if (!(Convert.ToBoolean(Session["secTrigger"].ToString())))
            {
                divSecurityQuestions.Attributes.Add("style", "display:block");
            }
            else lblError.Text = "Cannot change username. Wrong security answer entered too many times."; 
        } // end change username

        protected void lbChangePassword_Click(object sender, EventArgs e)
        {// when this link button is clicked, the div that allows the user to change their username will be made visibile
            divChangeUsername.Attributes.Add("style", "display:none");
            divOPTOut.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:none");
            divAddress.Attributes.Add("style", "display:none");
            Session["changeType"] = "password";

            // only show the password div if user has entered the correct answer within 3 attempts
            if (Session["secTrigger"]==null)
            {
                divSecurityQuestions.Attributes.Add("style", "display:block");
            }
            else if (!(Convert.ToBoolean(Session["secTrigger"].ToString())))
            {
                divSecurityQuestions.Attributes.Add("style", "display:block");
            }
            else lblError.Text = "Cannot change password. Wrong security answer entered too many times.";
            //divChangePassword.Attributes.Add("style", "display:block");
        } // end change password

        protected void lbOptOut_Click(object sender, EventArgs e)
        {// when this link button is clicked, the div that allows the user to change their username will be made visibile
            divChangeUsername.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:none");
            divAddress.Attributes.Add("style", "display:none");
            divOPTOut.Attributes.Add("style", "display:block");
            divSecurityQuestions.Attributes.Add("style", "display:none");
        } // end opt out

        protected void lbBlockedUsers_Click(object sender, EventArgs e)
        {// when this link button is clicked, the div that allows the user to change their username will be made visibile
            divChangeUsername.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:none");
            divOPTOut.Attributes.Add("style", "display:none");
            divAddress.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:flex");
            divSecurityQuestions.Attributes.Add("style", "display:none");
        } // end blocked users

        protected void btnUpdateUsername_Click(object sender, EventArgs e)
        { // validate user input for username
            Session["remain"] = 1;
            if (txtNewUsername.Text=="")
            {
                txtNewUsername.CssClass += " is-invalid";
                return;
            } // end check
            else
            {
                SqlCommand objUpdateUserName = new SqlCommand();
                objUpdateUserName.CommandType = CommandType.StoredProcedure;
                objUpdateUserName.CommandText = "TP_UpdateUsername";
                SqlParameter outputUsernameExists = new SqlParameter("@UsernameExists", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                objUpdateUserName.Parameters.AddWithValue("@userID", userID);
                objUpdateUserName.Parameters.AddWithValue("username", txtNewUsername.Text);
                objUpdateUserName.Parameters.Add(outputUsernameExists);
                if (obj.DoUpdateUsingCmdObj(objUpdateUserName, out string exception) == -2)
                {
                   // Response.Write(exception);
                }
                if (Int32.Parse(outputUsernameExists.Value.ToString()) == 1)
                {
                    //Response.Write("Fail, username exists");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessToast", "showSuccess();", true);

                    //   ScriptManager.RegisterClientScriptBlock(this, GetType(),"alertMessage", @"alert('Successfully updated username ')", true);
                    txtCurrentUsername.Text = txtNewUsername.Text;
                    txtNewUsername.CssClass = txtNewUsername.CssClass.Replace("is-invalid", "").Trim();
                }
                txtNewUsername.Text = "";
            }
        } // end update username btn click

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        { // validate user input for password
            Session["remain"] = 1;

            Regex regexPassword = new Regex(@"^(?=.*\d).{7,20}$");

            // make sure entered password matches criteria
            if (txtNewPassword.Text == "" || !regexPassword.IsMatch(txtNewPassword.Text))
            {
                lblPasswordError.Text = "Please enter your new password."; txtNewPassword.Text = "";
                txtNewPassword.CssClass += " is-invalid"; return;
            } // end check
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                txtConfirmPassword.CssClass += " is-invalid";
                txtConfirmPassword.Text = ""; return;
            }
            else
            {
                // has password
                byte[] saltArray = CryptoUtilities.GenerateSalt();
                byte[] hashPassword = CryptoUtilities.CalculateMD5Hash(saltArray, txtNewPassword.Text);

                SqlCommand objUpdatePass = new SqlCommand();
                objUpdatePass.CommandType = CommandType.StoredProcedure;
                objUpdatePass.CommandText = "TP_UpdatePassword";
                objUpdatePass.Parameters.AddWithValue("@userID", userID);
                objUpdatePass.Parameters.AddWithValue("@pass", hashPassword);
                objUpdatePass.Parameters.AddWithValue("@salt", saltArray);
                obj.DoUpdateUsingCmdObj(objUpdatePass, out string error);
                if (String.IsNullOrEmpty(error))
                {
                    txtConfirmPassword.Text = ""; txtNewPassword.Text = "";
                    txtNewPassword.CssClass = txtNewPassword.CssClass.Replace("is-invalid", "").Trim();
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessToast", "showSuccess();", true);

                    // ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('Successfully updated password')", true);
                }
            }
        } // end update password btn click

        protected void lblAddress_Click(object sender, EventArgs e)
        { // this wil make the address div visible
            divChangeUsername.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:none");
            divOPTOut.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:none");
            divAddress.Attributes.Add("style", "display:flex");
            btnSave.Attributes.Add("style", "display:none");
            divSecurityQuestions.Attributes.Add("style", "display:none");

            //disable all the controls
            txtStAddresses.ReadOnly = true;
            txtCity.ReadOnly = true;
            ddlState.Enabled = false;
            txtZip.ReadOnly = true;
        } // end address eventhandler

        protected void btnEditAddress_Click(object sender, EventArgs e)
        { // will enable all textboxes and allow user to make changes
            txtStAddresses.ReadOnly = false;
            txtCity.ReadOnly = false;
            ddlState.Enabled = true;
            txtZip.ReadOnly = false;
            btnSave.Attributes.Add("style", "display:flex");
            btnEditAddress.Enabled = false;
        } // end edit address

        protected void btnSave_Click(object sender, EventArgs e)
        { // this will update the information after validation
            Regex regexZip = new Regex(@"(^\d{5}$)|(^\d{5}-\d{4}$)");

            // remove any validation from the controls
            txtStAddresses.CssClass = txtStAddresses.CssClass.Replace("is-invalid", "").Trim();
            txtZip.CssClass = txtZip.CssClass.Replace("is-invalid", "").Trim();
            txtCity.CssClass = txtCity.CssClass.Replace("is-invalid", "").Trim();
            ddlState.CssClass = ddlState.CssClass.Replace("is-invalid", "").Trim();

            Boolean trigger = false;
            // validate input
            if (txtStAddresses.Text.Length<=0)
            {
                trigger = true;
                txtStAddresses.CssClass += " is-invalid";
            }
            if (txtCity.Text.Length<=0)
            {
                trigger = true;
                txtCity.CssClass += " is-invalid";
            }
            if(txtZip.Text.Length <= 0 || !regexZip.IsMatch(txtZip.Text.Trim()))
            {
                trigger = true;
                txtZip.CssClass += " is-invalid";
            }
            if (ddlState.SelectedValue== "-1")
            {
                trigger = true;
                ddlState.CssClass += "is-invalid";
            }
            if (trigger)
            {
            }
            else
            { // everything was entered correctly..

                // create dictionary with input values
                IDictionary<string, string> a = new Dictionary<string, string>
                {
                    ["id"] = userID.ToString(),
                    ["billingAddress"] = txtStAddresses.Text,
                    ["city"] = txtCity.Text,
                    ["state"] = ddlState.SelectedValue,
                    ["zipCode"] = txtZip.Text
                };

                // serialize dictionary
                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonA = js.Serialize(a);

                // call web api method to update address
                WebRequest request = WebRequest.Create(profileWebAPI + "/updateAddress");
                request.Headers.Add("Authorization", "Bearer " + Session["token"].ToString());

                request.Method = "PUT";
                request.ContentLength = jsonA.Length;
                request.ContentType = "application/json";

                // Write the JSON data to the Web Request           
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(jsonA);
                writer.Flush();
                writer.Close();

                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close(); response.Close();

                // show success message, hide save, and enable edit
                lblSuccess.Text = "Successfully updated address.";
                btnSave.Attributes.Add("style", "display:none");
                btnEditAddress.Enabled = true;
                txtStAddresses.ReadOnly = true;
                txtCity.ReadOnly = true;
                ddlState.Enabled = false;
                txtZip.ReadOnly = true;
            }
        } // end save eventhandler

        protected void btnSecurity_Click(object sender, EventArgs e)
        { 
            // validates security question answer
            int remain = Convert.ToInt16(Session["remain"].ToString());
            if (txtSecurityQuestion.Text.Trim().ToLower() != Session["secAns"].ToString().Trim()) {}
            else
            { // if its the right answer
                divSecurityQuestions.Attributes.Add("style", "display:none"); lblError.Text = "";
                if (Session["changeType"].ToString() == "username")
                {
                    divChangeUsername.Attributes.Add("style", "display:block");
                }
                else
                {
                    divChangePassword.Attributes.Add("style", "display:block");
                }
                Session["secTrigger"] = false;
                Session["remain"] = 1; txtSecurityQuestion.Text = "";
                return;
            }
            if (remain == 3)
            { // if max incorrect answers reached
                divSecurityQuestions.Attributes.Add("style", "display:none");
                lblError.Text = "You have entered the wrong answer too many times.";
                Session["secTrigger"] = true;
            }
            else
            { // else increment variable
                int ch = 3 - remain;
                lblError.Text = "Wrong answer. " + ch + " chances remaining";
                Session["remain"] = remain+1;
            }
        }

        protected void btnSaveOptOut_Click(object sender, EventArgs e)
        {
            SqlCommand objUpdateVisiblity = new SqlCommand();
            objUpdateVisiblity.CommandType = CommandType.StoredProcedure;
            objUpdateVisiblity.CommandText = "TP_UpdateSearchPref";
            objUpdateVisiblity.Parameters.AddWithValue("@userID", userID);
            if (rbYes.Checked)
            {
                objUpdateVisiblity.Parameters.AddWithValue("@optOut", "yes");
                DateTime now = DateTime.Now;
                objUpdateVisiblity.Parameters.AddWithValue("@time", now);
            }
            else
            {
                objUpdateVisiblity.Parameters.AddWithValue("@optIn", "no");
            }
            obj.DoUpdateUsingCmdObj(objUpdateVisiblity, out string error);
            if (String.IsNullOrEmpty(error))
            {
                showSuccessToast();
               // lblSearchUpdate.Text = "Profile preferences have been updated. You can change these settings at any time.";
            }
        }
        

        protected void Unnamed_Command(object sender, CommandEventArgs e)
        {
            // will remove user from the blocked list
            int unblockUserID = Convert.ToInt32(e.CommandName);
            memberBlocks.Remove(unblockUserID); // remove the user's id from the blocked list
            Session["memberBlocks"] = memberBlocks;

            // serialize list
            BinaryFormatter bf = new BinaryFormatter(); MemoryStream mStream = new MemoryStream();
            Byte[] mBlocks;
            bf.Serialize(mStream, memberBlocks); mBlocks = mStream.ToArray();

            // update the db
            SqlCommand objUpdatePref = new SqlCommand();
            objUpdatePref.CommandType = CommandType.StoredProcedure;
            objUpdatePref.CommandText = "TP_UpdatePreferences";
            objUpdatePref.Parameters.AddWithValue("@userID", userID);
            objUpdatePref.Parameters.AddWithValue("@blocks", mBlocks );
            obj.DoUpdateUsingCmdObj(objUpdatePref, out string error);

            if (String.IsNullOrEmpty(error))
            {
                bindDL(); // rebind the datalist
            }
        }

        protected void bindDL()
        {
            // binds the blocked users data list to the list 
            List<User> blckUsersBinding = new List<User>();

            DataTable dtBlocks = new DataTable();
            dtBlocks.Columns.Add("UserId", typeof(int));
            foreach (int id in memberBlocks)
            {
                DataRow dr = dtBlocks.NewRow();
                dr["UserId"] = id;
                dtBlocks.Rows.Add(dr);
            }
            // call stored procedure
            objCMD.Parameters.Clear();
            objCMD.CommandType = CommandType.StoredProcedure;
            objCMD.CommandText = "TP_GetUsersFromList";
            objCMD.Parameters.AddWithValue("@UserList", dtBlocks);
            DataSet dsUser = obj.GetDataSetUsingCmdObj(objCMD);
            DataTable dt = dsUser.Tables[0];

            for (int row = 0; row < dt.Rows.Count; row++)
            { // loop through dataset, deserialize img, create user object --> add to array that will be bound to datalist
                Byte[] imgArray = (Byte[])dt.Rows[row]["profileImage"];
                MemoryStream memorystreamd = new MemoryStream(imgArray);
                BinaryFormatter bfd = new BinaryFormatter();
                string url = (bfd.Deserialize(memorystreamd)).ToString();

                User u = new User();
                u.userID = Convert.ToInt16(dt.Rows[row]["userID"]);
                
                u.tagline = dt.Rows[row]["tagline"].ToString();
                u.imageSRC = url;
                u.city= dt.Rows[row]["city"].ToString();
                u.state= dt.Rows[row]["state"].ToString();

                if (dt.Rows[row]["gender"].ToString().Trim().ToLower() == "female")
                {
                    u.gender = "F";
                }
                else
                {
                    u.gender = "M";
                }

                u.occuption= dt.Rows[row]["occupation"].ToString();

                DateTime now = DateTime.Now;
                DateTime birthday = Convert.ToDateTime(dt.Rows[row]["birthday"].ToString());
                TimeSpan timelived = now.Subtract(birthday);
                int age = timelived.Days / 365;
                u.age = age;

                string heading = (dt.Rows[row]["name"].ToString()) + " (" + u.gender + ") , " + u.age;
                u.heading = heading;
                u.occuption = dt.Rows[row]["occupation"].ToString();
                blckUsersBinding.Add(u);
            }
            // set datasource, databind, and show 3 records per row
            dlBlockedUsers.DataSource = blckUsersBinding;
            dlBlockedUsers.DataBind();
            dlBlockedUsers.RepeatColumns = 3; dlBlockedUsers.RepeatDirection = RepeatDirection.Horizontal;
        }


        protected void showSuccessToast()
        { // show success message
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessToast", "showSuccess();", true);
        }

        protected void showFailureToast()
        { // show failure message
            ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showFailed();", true);

        }

    } // end class
} // end namesapce