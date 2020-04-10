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
        DBConnect obj = new DBConnect(); SqlCommand objCMD = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblSuccess.Text = ""; lblError.Text = "";
            if (Session["UserID"] == null) Response.Redirect("Default.aspx");

            else
            {
                userID = Convert.ToInt32(Session["UserID"]);
                WebRequest request = WebRequest.Create(profileWebAPI + "GetSettings/" + userID);
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                string data = reader.ReadToEnd();
                reader.Close(); response.Close();
                JavaScriptSerializer js = new JavaScriptSerializer();

                DataSet ds = JsonConvert.DeserializeObject<DataSet>(data); // ds contains username + password, hide profile pref, blocked

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

                List<int> memberBlocks = (List<int>) Session["memberBlocks"]; // get blocks list from session
                List<User> blckUsersBinding = new List<User>();
                foreach ( int i in memberBlocks)
                {
                    objCMD.CommandType = CommandType.StoredProcedure;
                    objCMD.CommandText = "TP_GetUser";
                    objCMD.Parameters.AddWithValue("@userID", userID);
                    DataSet dsUser = obj.GetDataSetUsingCmdObj(objCMD);
                    User u = new User();
                    u.name = (dsUser.Tables[0].Rows[0]["firstName"].ToString() + " "  + dsUser.Tables[0].Rows[0]["lastName"].ToString());
                    u.tagline = dsUser.Tables[0].Rows[0]["tagline"].ToString();
                    blckUsersBinding.Add(u);
                }
                rptBlockedUsers.DataSource = blckUsersBinding;
                rptBlockedUsers.DataBind();

                if (!IsPostBack)
                {
                    Session["remain"] = 1;
                    Random random = new Random(); int r = random.Next(0, 3); // get random number --> will choose security question
                    Session["secAns"] = ds.Tables[3].Rows[r]["questionAnswer"].ToString().ToLower();
                    lblSecurityQuestion.Text = ds.Tables[3].Rows[r]["SecurityQuestionText"].ToString();
                }
            }
        } //end pageload

        protected void lbChangeUsername_Click(object sender, EventArgs e)
        { // when this link button is clicked, the div that allows the user to change their username will be made visibile
            divOPTOut.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:none");
            divAddress.Attributes.Add("style", "display:none");
            Session["changeType"] = "username";
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
                lblUsernameError.Text = "Please enter your new username.";
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
                    Response.Write(exception);
                }
                if (Int32.Parse(outputUsernameExists.Value.ToString()) == 1)
                {
                    Response.Write("Fail, username exists");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(),"alertMessage", @"alert('Successfully updated username ')", true);
                    txtCurrentUsername.Text = txtNewUsername.Text;
                }
                txtNewUsername.Text = "";
            }
        } // end update username btn click

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        { // validate user input for password
            Session["remain"] = 1;
            Regex regexPassword = new Regex(@"^(?=.*\d).{7,20}$");

            if (txtNewPassword.Text == "" || !regexPassword.IsMatch(txtNewPassword.Text))
            {
                lblPasswordError.Text = "Please enter your new password."; txtNewPassword.Text = "";
                txtNewPassword.CssClass += " is-invalid"; return;
            } // end check
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                lblPasswordError.Text = "Passwords don't match.";
                txtConfirmPassword.CssClass += " is-invalid";
                txtConfirmPassword.Text = ""; return;
            }
            else
            {
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
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('Successfully updated password')", true);
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
            txtStAddresses.CssClass = txtStAddresses.CssClass.Replace("is-invalid", "").Trim();
            txtZip.CssClass = txtZip.CssClass.Replace("is-invalid", "").Trim();
            txtCity.CssClass = txtCity.CssClass.Replace("is-invalid", "").Trim();
            ddlState.CssClass = ddlState.CssClass.Replace("is-invalid", "").Trim();

            Boolean trigger = false;
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
            if(txtZip.Text.Length <= 0 || !regexZip.IsMatch(txtZip.Text))
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
            {
                
                UserAddress a = new UserAddress();
                a.id = userID; a.billingAddress = txtStAddresses.Text; a.city = txtCity.Text; a.state = ddlState.SelectedValue; a.zipCode = Convert.ToInt32(txtZip.Text);
                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonA = js.Serialize(a);

                WebRequest request = WebRequest.Create(profileWebAPI + "/updateAddress");
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
        { // validates security question answer
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
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('Successfully updated profile visibility. 
                    Your profile will not longer show up on the site. You can change this preference at any time.')", true);
            }
        }
    } // end class
} // end namesapce