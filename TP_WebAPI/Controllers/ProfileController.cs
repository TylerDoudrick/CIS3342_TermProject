using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Data;
using TermProject;
using Classess;
using Microsoft.AspNetCore.Authorization;

namespace TP_WebAPI.Controllers
{
    [Authorize]
    [Route("api/datingservice/profile/")]
    [ApiController]
    public class ProfileController : ControllerBase
    {

        [HttpGet("searchCriteria")]
        public DataSet GetSearchCriteria()
        { // returns tables associated with the search criteria
            try
            {
                DBConnect obj = new DBConnect();
                SqlCommand objSearchCriteria = new SqlCommand();
                objSearchCriteria.CommandType = CommandType.StoredProcedure;
                objSearchCriteria.CommandText = "TP_GetSearchCriteria";
                DataSet ds = obj.GetDataSetUsingCmdObj(objSearchCriteria);
                return ds;
            }
            catch { return null; }
        }

        [HttpGet("checkLogin/{username}/{trueword}")]
        public DataSet checkLogin(string username, string trueword)
        {
            try
            {
                DBConnect obj = new DBConnect();
                SqlCommand objLogin = new SqlCommand();
                objLogin.CommandType = CommandType.StoredProcedure;
                objLogin.CommandText = "TP_CheckLogin";
                objLogin.Parameters.AddWithValue("@username", username);
                objLogin.Parameters.AddWithValue("@trueword", trueword);
                SqlParameter returnP = new SqlParameter("@count", DbType.Int32);
                returnP.Direction = ParameterDirection.ReturnValue;
                objLogin.Parameters.Add(returnP);
                DataSet myDS = obj.GetDataSetUsingCmdObj(objLogin);
                return myDS;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public DataSet grabMemberProfile(string id)
        {
            try
            {
                DBConnect databaseObj = new DBConnect();
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "TP_LookupPersonalProfile";
                commandObj.Parameters.AddWithValue("@UserId", id);
                DataSet myDS = databaseObj.GetDataSetUsingCmdObj(commandObj);
                return myDS;
            }
            catch
            {
                return null;
            }
        }

        [AllowAnonymous]
        [HttpGet("public/{id}")]
        public DataSet grabPublicProfile(string id)
        {
            try { 
            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_LookupPublicProfile";
            commandObj.Parameters.AddWithValue("@UserId", id);
            DataSet myDS = databaseObj.GetDataSetUsingCmdObj(commandObj);
            return myDS;
            }
            catch
            {
                return null;
            }
        }

        [HttpPut("updateAddress")]
        public void UpdateAddress([FromBody] IDictionary<string, string> newValues)
        {
            try
            {

            int userID = Convert.ToInt16(newValues["id"]);
            int zipCode = Convert.ToInt16(newValues["zipCode"]);
            string billingAddress = (newValues["billingAddress"]);
            string city = newValues["city"];
            string state = newValues["state"];

            DBConnect obj = new DBConnect();
            SqlCommand objUpdateAdd = new SqlCommand();
            objUpdateAdd.CommandType = CommandType.StoredProcedure;
            objUpdateAdd.CommandText = "TP_UpdateAddress";
            objUpdateAdd.Parameters.AddWithValue("@userID", userID);
            objUpdateAdd.Parameters.AddWithValue("@stAddress", billingAddress);
            objUpdateAdd.Parameters.AddWithValue("@city", city);
            objUpdateAdd.Parameters.AddWithValue("@state", state);
            objUpdateAdd.Parameters.AddWithValue("@zip", zipCode);
            obj.DoUpdateUsingCmdObj(objUpdateAdd, out string erro);
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("GetSettings/{userID}")]
        public DataSet GetSettings(int userID)
        {
            try { 
            DBConnect obj = new DBConnect();
            SqlCommand objGetAddress = new SqlCommand();
            objGetAddress.CommandType = CommandType.StoredProcedure;
            objGetAddress.CommandText = "TP_GetSettings";
            objGetAddress.Parameters.AddWithValue("@userID", userID);
            DataSet dsAddress = obj.GetDataSetUsingCmdObj(objGetAddress);
            return dsAddress;
            }
            catch
            {
                return null;
            }
        }
        //
        //BEGIN simple update post methods
        //
        [HttpPost("update/tagline/{id}")]
        public string UpdateTagLine(string id, [FromBody] IDictionary<string, string> newValues)
        {
            try { 
            string tagLine = newValues["tagLine"];


            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_UpdateTagline";
            commandObj.Parameters.AddWithValue("@UserId", id);
            commandObj.Parameters.AddWithValue("@TagLine", tagLine);

            //return myDS;
            if (databaseObj.DoUpdateUsingCmdObj(commandObj, out string exception) == -2)
            {
                return exception;
            }
            else
            {
                return "true";
            }
            }
            catch
            {
                return null;
            }
        }
        [HttpPost("update/contact/{id}")]
        public string UpdateContactInformation(string id, [FromBody] IDictionary<string, string> newValues)
        {
            string phone = newValues["phone"];
            string email = newValues["email"];
            try { 

            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_UpdateContactInfo";
            commandObj.Parameters.AddWithValue("@UserId", id);
            commandObj.Parameters.AddWithValue("@phone", phone);
            commandObj.Parameters.AddWithValue("@email", email);

            //return myDS;
            if (databaseObj.DoUpdateUsingCmdObj(commandObj, out string exception) == -2)
            {
                return exception;
            }
            else
            {
                return "true";
            }
            }
            catch
            {
                return null;
            }
        }

        [HttpPost("update/basic/{id}")]
        public string UpdateBasicInformation(string id, [FromBody] IDictionary<string, string> newValues)
        {
            string bio = newValues["bio"];
            string numChildren = newValues["numChildren"];
            string seeking = newValues["seeking"];
            string wantChildren = newValues["wantChildren"];
            string occupation = newValues["occupation"];

            try { 
            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_UpdateBasicInfo";
            commandObj.Parameters.AddWithValue("@UserId", id);
            commandObj.Parameters.AddWithValue("@bio", bio);
            commandObj.Parameters.AddWithValue("@numChildren", numChildren);
            commandObj.Parameters.AddWithValue("@wantChildren", wantChildren);
            commandObj.Parameters.AddWithValue("@occupation", occupation);
            commandObj.Parameters.AddWithValue("@seeking", seeking);

            if (databaseObj.DoUpdateUsingCmdObj(commandObj, out string exception) == -2)
            {
                return exception;
            }
            else
            {
                return "true";
            }
            }
            catch
            {
                return null;
            }
        }

        [HttpPost("update/about/{id}")]
        public string UpdateAboutYouInformation(string id, [FromBody] IDictionary<string, string> newValues)
        {
            string songs = newValues["songs"];
            string sayings = newValues["sayings"];
            string restuarants = newValues["restuarants"];
            string movies = newValues["movies"];
            string books = newValues["books"];

            try
            {
                DBConnect databaseObj = new DBConnect();
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "TP_UpdateAboutYouInfo";
                commandObj.Parameters.AddWithValue("@UserId", id);
                commandObj.Parameters.AddWithValue("@favMovies", movies);
                commandObj.Parameters.AddWithValue("@favSayings", sayings);
                commandObj.Parameters.AddWithValue("@favRestuarants", restuarants);
                commandObj.Parameters.AddWithValue("@favBooks", books);
                commandObj.Parameters.AddWithValue("@favSongs", songs);

                //return myDS;
                if (databaseObj.DoUpdateUsingCmdObj(commandObj, out string exception) == -2)
                {
                    return exception;
                }
                else
                {
                    return "true";
                }
            }
            catch
            {
                return null;
            }
        }

        [HttpPost("insert/registrationInfo")]
        public string InsertRegistrationInfo([FromBody] RegistrationObj r)
        { // inserts registration info
            try { 
            DBConnect obj = new DBConnect();
            SqlCommand objReg = new SqlCommand();
            objReg.CommandType = CommandType.StoredProcedure;
            objReg.CommandText = "TP_InsertRegistration";

            objReg.Parameters.AddWithValue("@userID", r.id);
            objReg.Parameters.AddWithValue("@photo", r.photo);
            objReg.Parameters.AddWithValue("@phone", r.phone);
            // if there are valid values for height and weight, add them to the command
            if (r.height != 0)
            {
                objReg.Parameters.AddWithValue("@height", r.height);
            }
            if (r.weight != 0)
            {
                objReg.Parameters.AddWithValue("@weight", r.weight);
            }
            objReg.Parameters.AddWithValue("@numChildren", r.numChildren);
            objReg.Parameters.AddWithValue("@wantKids", r.wantKids);
            objReg.Parameters.AddWithValue("@bio", r.bio);
            objReg.Parameters.AddWithValue("@movies", r.movies);
            objReg.Parameters.AddWithValue("@sayings", r.sayings);
            objReg.Parameters.AddWithValue("@restaurants", r.restaurants);
            objReg.Parameters.AddWithValue("@books", r.books);
            objReg.Parameters.AddWithValue("@songs", r.songs);
            objReg.Parameters.AddWithValue("@birthday", r.birthday);
            objReg.Parameters.AddWithValue("@gender", r.gender);
            objReg.Parameters.AddWithValue("@occupation", r.occupation);
            objReg.Parameters.AddWithValue("@seekingGender", r.seekingGender);
            objReg.Parameters.AddWithValue("@tagline", r.tagline);

            if (obj.DoUpdateUsingCmdObj(objReg, out string exception) == -2)
            {
                throw new Exception(exception);
                return exception;
            }
            else
            {
                return "pass";
            }
            }
            catch
            {
                return null;
            }
        }


        [HttpPost("update/details/{id}")]
        public string UpdateDetails(string id, [FromBody] IDictionary<string, List<string>> newValues)
        {
            try { 
            List<string> religions = newValues["religions"];
            List<string> commitments = newValues["commitments"];
            List<string> interests = newValues["interests"];
            List<string> likes = newValues["likes"];
            List<string> dislikes = newValues["dislikes"];

            DBConnect databaseObj = new DBConnect();
            SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_UpdateDetails";

            DataTable dtReligions = new DataTable();
            dtReligions.Columns.Add("UserId", typeof(int));
            dtReligions.Columns.Add("TypeId", typeof(int));

            DataTable dtCommitments = new DataTable();
            dtCommitments.Columns.Add("UserId", typeof(int));
            dtCommitments.Columns.Add("TypeId", typeof(int));

            DataTable dtInterests = new DataTable();
            dtInterests.Columns.Add("UserId", typeof(int));
            dtInterests.Columns.Add("TypeId", typeof(int));

            DataTable dtLikes = new DataTable();
            dtLikes.Columns.Add("UserId", typeof(int));
            dtLikes.Columns.Add("TypeId", typeof(int));

            DataTable dtDislikes = new DataTable();
            dtDislikes.Columns.Add("UserId", typeof(int));
            dtDislikes.Columns.Add("TypeId", typeof(int));

            foreach(string str in religions)
            {
                DataRow newRow = dtReligions.NewRow();
                newRow["UserId"] = Int32.Parse(id);
                newRow["TypeId"] = Int32.Parse(str);
                dtReligions.Rows.Add(newRow);
            }
            foreach (string str in commitments)
            {
                DataRow newRow = dtCommitments.NewRow();
                newRow["UserId"] = Int32.Parse(id);
                newRow["TypeId"] = Int32.Parse(str);
                dtCommitments.Rows.Add(newRow);
            }
            foreach (string str in interests)
            {
                DataRow newRow = dtInterests.NewRow();
                newRow["UserId"] = Int32.Parse(id);
                newRow["TypeId"] = Int32.Parse(str);
                dtInterests.Rows.Add(newRow);
            }
            foreach (string str in likes)
            {
                DataRow newRow = dtLikes.NewRow();
                newRow["UserId"] = Int32.Parse(id);
                newRow["TypeId"] = Int32.Parse(str);
                dtLikes.Rows.Add(newRow);
            }
            foreach (string str in dislikes)
            {
                DataRow newRow = dtDislikes.NewRow();
                newRow["UserId"] = Int32.Parse(id);
                newRow["TypeId"] = Int32.Parse(str);
                dtDislikes.Rows.Add(newRow);
            }

            commandObj.Parameters.AddWithValue("@UserId", id);
            commandObj.Parameters.AddWithValue("@Religions", dtReligions);
            commandObj.Parameters.AddWithValue("@Commitments", dtCommitments);
            commandObj.Parameters.AddWithValue("@Interests", dtInterests);
            commandObj.Parameters.AddWithValue("@Likes", dtLikes);
            commandObj.Parameters.AddWithValue("@Dislikes", dtDislikes);

            if (databaseObj.DoUpdateUsingCmdObj(commandObj, out string exception) == -2)
            {
                return exception;
            }
            else
            {
                return "true";
            }
            }
            catch
            {
                return null;
            }
        }


        //
        //END simple update post methods
        //
    }

}