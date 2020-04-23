using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class AccountRecovery : System.Web.UI.Page
    {
        string interactionsWebAPI = "https://localhost:44375/api/datingservice/interactions/";
        string profileWebAPI = "https://localhost:44375/api/datingservice/profile/";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmitVerification_Click(object sender, EventArgs e)
        {
            string code = txtVerificationCode.Text;
            string email = txtEmailAddress.Text;
            try
            {


                SqlCommand commandObj = new SqlCommand();
                commandObj.Parameters.Clear();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "TP_CheckVerification";

                commandObj.Parameters.AddWithValue("@EmailAddress", email);
                commandObj.Parameters.AddWithValue("@Verification", code);
                commandObj.Parameters.Add("@UserID", SqlDbType.Int, 50).Direction = ParameterDirection.Output;

                DBConnect OBJ = new DBConnect();
                DataSet ds = OBJ.GetDataSetUsingCmdObj(commandObj);

                if (ds.Tables.Count == 1)
                {
                    divInvalidCode.Visible = false;
                    Session["VerifyingID"] = commandObj.Parameters["@UserID"].Value.ToString();
                    divSecurityQuestion.Visible = true;
                    divVerificationCode.Visible = false;

                    lblSecurityQuestion.Text = ds.Tables[0].Rows[0]["SecurityQuestionText"].ToString();
                    Session["SQAnswer"] = ds.Tables[0].Rows[0]["questionAnswer"].ToString();
                }
                else
                {
                    divInvalidCode.Visible = true;
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);
            }
        }

        protected void btnSubmitQuestion_Click(object sender, EventArgs e)
        {
            if (txtAnswer.Text.Length > 0 && txtAnswer.Text == Session["SQAnswer"].ToString())
            {
                divSecurityQuestion.Visible = false;
                divChangePassword.Visible = true;
                divInvalidAnswer.Visible = true;
            }
            else
            {
                divInvalidAnswer.Visible = true;
            }
        }
        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            string password = txtNewPass.Text;
            string passwordConfirm = txtConfirmPass.Text;

            Regex regexPassword = new Regex(@"^(?=.*\d).{7,20}$");

            bool trigger = false;

            if (password.Length <= 0 || !regexPassword.IsMatch(password))
            {
                trigger = true;
                txtNewPass.CssClass += " is-invalid";
                txtNewPass.Text = "";
            }
            if (password != passwordConfirm)
            {
                trigger = true;
                txtConfirmPass.CssClass += " is-invalid";
                txtConfirmPass.Text = "";
            }
            if (!trigger)
            {
                //Password Salting & Hashing
                byte[] saltArray = CryptoUtilities.GenerateSalt();
                byte[] hashPassword = CryptoUtilities.CalculateMD5Hash(saltArray, password);
                try
                {


                    SqlCommand commandObj = new SqlCommand();
                    commandObj.Parameters.Clear();
                    commandObj.CommandType = CommandType.StoredProcedure;
                    commandObj.CommandText = "TP_UpdatePassword";

                    commandObj.Parameters.AddWithValue("@userID", Session["VerifyingID"].ToString());
                    commandObj.Parameters.AddWithValue("@pass", hashPassword);
                    commandObj.Parameters.AddWithValue("@salt", saltArray);

                    DBConnect OBJ = new DBConnect();
                    if (OBJ.DoUpdateUsingCmdObj(commandObj, out string err) == -2)
                    {
                        //Response.Write(err);
                    }
                    else
                    {
                        divSuccess.Visible = true;
                        divChangePassword.Visible = false;
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "FailureToast", "showDBError();", true);
                }
            }
            else
            {
                divInvalidPassword.Visible = true;
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx");
        }
    }
}