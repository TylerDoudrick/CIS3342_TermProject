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
    [Route("api/datingservice/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost]
        public User TryLogin([FromBody] LoginCredentials cred)
        {
            //Do something
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
                DataRow drUserRecord = dsUser.Tables[0].Rows[0];

                byte[] salt = (byte[])drUserRecord["salt"];
                byte[] hashedPassword = (byte[])drUserRecord["password"];


                if (CryptoUtilities.comparePassword(hashedPassword, salt, cred.password))
                {
                    User foundUser = new User();
                    foundUser.userID = drUserRecord["userID"].ToString();
                    foundUser.firstName = drUserRecord["firstName"].ToString();
                    foundUser.lastName = drUserRecord["lastName"].ToString();
                    foundUser.emailAddress = drUserRecord["emailAddress"].ToString();
                    foundUser.seekingGender = drUserRecord["seekingGender"].ToString();
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

        [HttpGet]
        public string debugToken()
        {
            return GenerateJSONWebToken();
        }

        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretserversidesecret"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

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

        public class User
        {
            public string userID { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string emailAddress { get; set; }
            public string seekingGender { get; set; }
            public string token { get; set; }
        }
    }
}