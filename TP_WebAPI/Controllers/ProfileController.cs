using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TermProject;

namespace TP_WebAPI.Controllers
{
    [Route("api/profile/")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet("searchCriteria")]
        public DataSet GetSearchCriteria()
        { // returns tables associated with the search criteria
            DBConnect obj = new DBConnect();
            SqlCommand objSearchCriteria = new SqlCommand();
            objSearchCriteria.CommandType = CommandType.StoredProcedure;
            objSearchCriteria.CommandText = "TP_GetSearchCriteria";
            DataSet ds = obj.GetDataSetUsingCmdObj(objSearchCriteria);
            return ds;
        }

        [HttpGet("checkLogin/{username}/{password}")]
        public DataSet checkLogin(string username, string password)
        {
            DBConnect obj = new DBConnect();
            SqlCommand objLogin = new SqlCommand();
            objLogin.CommandType= CommandType.StoredProcedure;
            objLogin.CommandText = "TP_CheckLogin";
            objLogin.Parameters.AddWithValue("@username", username);
            objLogin.Parameters.AddWithValue("@password", password);
            SqlParameter returnP = new SqlParameter("@count", DbType.Int32);
            returnP.Direction = ParameterDirection.ReturnValue;
            objLogin.Parameters.Add(returnP);
            DataSet myDS = obj.GetDataSetUsingCmdObj(objLogin);
            return myDS;
        }
        [HttpGet("{id}")]
       public DataSet grabPublicProfile(string id)
        {
            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_LookupPersonalProfile";
            commandObj.Parameters.AddWithValue("@UserId", id);
            DataSet myDS = databaseObj.GetDataSetUsingCmdObj(commandObj);
            return myDS;
        }

    }

}