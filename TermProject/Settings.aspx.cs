using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null) Response.Redirect("Default.aspx");
           
            else
            {
                lblSuccess.Text = "";
                userID = Convert.ToInt32(Session["UserID"]);
                WebRequest request = WebRequest.Create(profileWebAPI + "/GetSettings");
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                string data = reader.ReadToEnd();
                reader.Close(); response.Close();

                DataSet ds = JsonConvert.DeserializeObject<DataSet>(data); // ds contains username + password, hide profile pref, 
            }
        } //end pageload

        protected void lbChangeUsername_Click(object sender, EventArgs e)
        { // when this link button is clicked, the div that allows the user to change their username will be made visibile
            divOPTOut.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:none");
            divAddress.Attributes.Add("style", "display:none");
            divChangeUsername.Attributes.Add("style", "display:block");
        } // end change username

        protected void lbChangePassword_Click(object sender, EventArgs e)
        {// when this link button is clicked, the div that allows the user to change their username will be made visibile
            divChangeUsername.Attributes.Add("style", "display:none");
            divOPTOut.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:none");
            divAddress.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:block");
        } // end change password

        protected void lbOptOut_Click(object sender, EventArgs e)
        {// when this link button is clicked, the div that allows the user to change their username will be made visibile
            divChangeUsername.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:none");
            divAddress.Attributes.Add("style", "display:none");
            divOPTOut.Attributes.Add("style", "display:block");
        } // end opt out

        protected void lbBlockedUsers_Click(object sender, EventArgs e)
        {// when this link button is clicked, the div that allows the user to change their username will be made visibile
            divChangeUsername.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:none");
            divOPTOut.Attributes.Add("style", "display:none");
            divAddress.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:flex");
        } // end blocked users

        protected void btnUpdateUsername_Click(object sender, EventArgs e)
        { // validate user input for username
            if (txtNewUsername.Text=="")
            {
                lblUsernameError.Text = "Please enter your new username."; return;
            } // end check
        } // end update username btn click

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        { // validate user input for password
            if (txtNewPassword.Text == "")
            {
                lblPasswordError.Text = "Please enter your new password."; return;
            } // end check
        } // end update password btn click

        protected void lblAddress_Click(object sender, EventArgs e)
        { // this wil make the address div visible
            divChangeUsername.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:none");
            divOPTOut.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:none");
            divAddress.Attributes.Add("style", "display:flex");
            btnSave.Attributes.Add("style", "display:none");

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

    } // end class
} // end namesapce