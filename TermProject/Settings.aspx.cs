using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
        } //end pageload

        protected void lbChangeUsername_Click(object sender, EventArgs e)
        { // when this link button is clicked, the div that allows the user to change their username will be made visibile
            divOPTOut.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:none");
            divChangeUsername.Attributes.Add("style", "display:block");
        } // end change username

        protected void lbChangePassword_Click(object sender, EventArgs e)
        {// when this link button is clicked, the div that allows the user to change their username will be made visibile
            divChangeUsername.Attributes.Add("style", "display:none");
            divOPTOut.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:block");
        } // end change password

        protected void lbOptOut_Click(object sender, EventArgs e)
        {// when this link button is clicked, the div that allows the user to change their username will be made visibile
            divChangeUsername.Attributes.Add("style", "display:none");
            divBlockedUsers.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:none");
            divOPTOut.Attributes.Add("style", "display:block");
        } // end opt out

        protected void lbBlockedUsers_Click(object sender, EventArgs e)
        {// when this link button is clicked, the div that allows the user to change their username will be made visibile
            divChangeUsername.Attributes.Add("style", "display:none");
            divChangePassword.Attributes.Add("style", "display:none");
            divOPTOut.Attributes.Add("style", "display:none");
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

    } // end class
} // end namesapce