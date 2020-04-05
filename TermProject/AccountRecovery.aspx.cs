using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class AccountRecovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmitQuestion_Click(object sender, EventArgs e)
        {
            if(txtAnswer.Text.Length > 0)
            {
                divSecurityQuestion.Visible = false;
                divChangePassword.Visible = true;
            }
        }
        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            if(txtNewPass.Text.Length > 0 && txtConfirmPass.Text.Length > 0)
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}