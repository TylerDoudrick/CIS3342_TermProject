using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TermProject;

namespace TP_WebAPI.Controllers
{
    //
    //This controller handles user logins and token generation for authentication
    //
    //
    //



    [Route("api/datingservice/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost]
        public User TryLogin([FromBody] LoginCredentials cred)
        {
            //User is trying to log in

            try { 
            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();
            commandObj.Parameters.Clear();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_LookupUserRecord";

            SqlParameter inputUsername = new SqlParameter("@username", cred.username)
            {
                Direction = ParameterDirection.Input,

                SqlDbType = SqlDbType.VarChar
            };

            commandObj.Parameters.Add(inputUsername);


            DataSet dsUser = databaseObj.GetDataSetUsingCmdObj(commandObj);
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                //
                //We grab the record from the given username
                //
                //And compare the passwords using the CryptoUtilities class
                //
                DataRow drUserRecord = dsUser.Tables[0].Rows[0];

                byte[] salt = (byte[])drUserRecord["salt"];
                byte[] hashedPassword = (byte[])drUserRecord["password"];

                if (CryptoUtilities.comparePassword(hashedPassword, salt, cred.password))
                {
                    //
                    //If the password matches, we send the account back to the codebehind for storing in the session
                    //


                    User foundUser = new User();
                    foundUser.userID = drUserRecord["userID"].ToString();
                    foundUser.firstName = drUserRecord["firstName"].ToString();
                    foundUser.lastName = drUserRecord["lastName"].ToString();
                    foundUser.emailAddress = drUserRecord["emailAddress"].ToString();
                    if((dsUser.Tables[1].Rows.Count > 0)) foundUser.seekingGender = dsUser.Tables[1].Rows[0]["seekingGender"].ToString();
                    foundUser.isVerified = drUserRecord["isVerified"].ToString();
                    foundUser.finishedRegistration = drUserRecord["finishedRegistration"].ToString();
                    //
                    //Here the token is generated and appended to the user account
                    //
                    foundUser.token = GenerateJSONWebToken();
                    return foundUser;

                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
            }
            catch
            {
                return null;
            }

        }
        [HttpPost("verify")]
        public User TryVerify([FromBody] VerificationCredentials cred)
        {
            //
            //User is trying to verify
            //      Works in a similar way to logging in. Returns the userid and token if the code was correct
            //

            try { 
            User registeringUser = new User();
            SqlCommand commandObj = new SqlCommand();
            commandObj.Parameters.Clear();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_CheckVerification";

            commandObj.Parameters.AddWithValue("@EmailAddress", cred.email);
            commandObj.Parameters.AddWithValue("@Verification", cred.code);
            commandObj.Parameters.Add("@UserID", SqlDbType.Int, 50).Direction = ParameterDirection.Output;

            DBConnect OBJ = new DBConnect();
            DataSet ds = OBJ.GetDataSetUsingCmdObj(commandObj);

            if (ds.Tables.Count == 1)
            {
                registeringUser.userID = commandObj.Parameters["@UserID"].Value.ToString();
                registeringUser.token = GenerateJSONWebToken();
                return registeringUser;

            }
            else
            {
                return null;
            }
            }
            catch
            {
                return null;
            }
        }

        private string GenerateJSONWebToken()
        {
            //
            //Generate a token with a secret and sign it with sha256
            //
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretserversidesecret"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //
            //Token lasts 2 hours
            //

            var token = new JwtSecurityToken(
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




        public class LoginCredentials
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class VerificationCredentials
        {
            public string email { get; set; }
            public string code { get; set; }
        }

        public class User
        {
            public string userID { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string emailAddress { get; set; }
            public string seekingGender { get; set; }
            public string finishedRegistration { get; set; }
            public string isVerified { get; set; }
            public string token { get; set; }

        }
    }
}