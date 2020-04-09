using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStoreLibrary;
using Models;

namespace TP_WebAPI.Controllers
{
    [Route("api/datingservice/profile/")]
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

        [HttpPut("updateAddress")]
        public void UpdateAddress([FromBody] UserAddress ua)
        {
            DBConnect obj = new DBConnect();
            SqlCommand objUpdateAdd = new SqlCommand();
            objUpdateAdd.CommandType = CommandType.StoredProcedure;
            objUpdateAdd.CommandText = "TP_UpdateAddress";
            objUpdateAdd.Parameters.AddWithValue("@userID", ua.id);
            objUpdateAdd.Parameters.AddWithValue("@stAddress", ua.billingAddress);
            objUpdateAdd.Parameters.AddWithValue("@city", ua.city);
            objUpdateAdd.Parameters.AddWithValue("@state", ua.state);
            objUpdateAdd.Parameters.AddWithValue("@zip", ua.zipCode);
            obj.DoUpdateUsingCmdObj(objUpdateAdd);
        }

        [HttpGet("GetSettings/{userID}")]
        public DataSet GetSettings(int userID)
        {
            DBConnect obj = new DBConnect();
            SqlCommand objGetAddress = new SqlCommand();
            objGetAddress.CommandType = CommandType.StoredProcedure;
            objGetAddress.CommandText = "TP_GetSettings";
            objGetAddress.Parameters.AddWithValue("@userID", userID);
            DataSet dsAddress = obj.GetDataSetUsingCmdObj(objGetAddress);
            return dsAddress;
        }
       
    }

}