using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        DBConnect dbConnection = new DBConnect();
        SqlCommand commandObj = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string passwordConfirm = txtConfirmPassword.Text;
            string firstName = txtFName.Text;
            string lastName = txtLName.Text;
            string AddressOne = txtAddressOne.Text;
            string AddressTwo = txtAddressTwo.Text;
            string city = txtCity.Text;
            string state = txtState.Text;
            string zip = txtZip.Text;

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
            txtState.CssClass = txtState.CssClass.Replace("is-invalid", "").Trim();
            txtZip.CssClass = txtZip.CssClass.Replace("is-invalid", "").Trim();


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
                txtState.CssClass += " is-invalid";
            }
            if (zip.Length <= 0 || !regexZip.IsMatch(zip))
            {
                trigger = true;
                txtZip.CssClass += " is-invalid";
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

                SqlParameter outputUsernameExists = new SqlParameter("@UsernameExists", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                SqlParameter outputEmailExists = new SqlParameter("@EmailExists", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                commandObj.Parameters.Add(inputUsername);
                commandObj.Parameters.Add(inputPassword);
                commandObj.Parameters.Add(inputSalt);
                commandObj.Parameters.Add(inputEmail);
                commandObj.Parameters.Add(inputFirstName);
                commandObj.Parameters.Add(inputLastName);

                commandObj.Parameters.Add(outputEmailExists);
                commandObj.Parameters.Add(outputUsernameExists);


                if (dbConnection.DoUpdateUsingCmdObj(commandObj, out string exception) == -2)
                {
                    Response.Write("exception");
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
                    divCreateAccount.Visible = false;
                    divValidate.Visible = true;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "StartCountdown", "redirectCountdown()", true);
                }
            }



            //string email = txtEmail.Text.Trim();
            //string sendAdd = "querydating@gmail.com";
            //MailMessage msg = new MailMessage();
            //msg.To.Add(new MailAddress(@email));
            //msg.Subject = "QUERY Verification Email";
            //msg.From = new MailAddress(sendAdd);
            //msg.IsBodyHtml = true;
            //msg.Body = "<div> Thank you for signing up for Query.com! <Br><BR> Please click the below link to verify your account status." +
            //    "<Br><BR> <div>";
            //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            //smtp.Credentials = new System.Net.NetworkCredential(sendAdd, "CIS3342TermProject");
            //smtp.EnableSsl = true;

            //smtp.Send(msg);
            //Session["email"] = email;

            //Response.Redirect("Verification.aspx");
        }
    }
}