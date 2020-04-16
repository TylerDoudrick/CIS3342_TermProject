using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using TermProject;

namespace TP_WebAPI.Controllers
{
    [Route("api/datingservice/interactions/")]
    [ApiController]
    public class InteractionsController : ControllerBase
    {
        DBConnect objDB = new DBConnect();

        [HttpPost("insertPreferences/")]
        public int insertPreferences([FromBody] Preferences p)
        { // inserts empty serialized lists to the db
            SqlCommand objInsertPref = new SqlCommand();
            objInsertPref.CommandText = "TP_InsertPreferences";
            objInsertPref.CommandType = CommandType.StoredProcedure;
            objInsertPref.Parameters.AddWithValue("@userID", p.id);
            objInsertPref.Parameters.AddWithValue("@likes", p.mLikes);
            objInsertPref.Parameters.AddWithValue("@dislikes", p.mDislikes);
            objInsertPref.Parameters.AddWithValue("@blocks", p.mBlocks);
            int result = objDB.DoUpdateUsingCmdObj(objInsertPref, out string error);

            return result;
        }

        [HttpGet("getPreferences/{userID}")]
        public DataSet GetPreferences(int userID)
        { // gets user preferences upon login
            SqlCommand objGetPref = new SqlCommand();
            objGetPref.CommandType = CommandType.StoredProcedure;
            objGetPref.CommandText = "TP_GetPreferences";
            objGetPref.Parameters.AddWithValue("@userID", userID);
            DataSet result = objDB.GetDataSetUsingCmdObj(objGetPref);
            return result;
        }

        [HttpPut("blockUser")]
        public int BlockUser([FromBody] IDictionary<string, string> newValues)
        { // removes all interactions between 2 users when blocked
            int userID = Convert.ToInt16(newValues["userID"]);
            int memID = Convert.ToInt16(newValues["memID"]);

            SqlCommand objBlock = new SqlCommand();
            objBlock.CommandType = CommandType.StoredProcedure;
            objBlock.CommandText = "TP_Block";
            objBlock.Parameters.AddWithValue("@userID", userID);
            objBlock.Parameters.AddWithValue("@memID", memID);
            int result = objDB.DoUpdateUsingCmdObj(objBlock, out string error);
            return result;
        }

        [HttpPut("updatePreferences")]
        public int UpdatePreferences([FromBody] Preferences p)
        { // updates preferences for user
            SqlCommand objUpdatePref = new SqlCommand();
            objUpdatePref.CommandType = CommandType.StoredProcedure;
            objUpdatePref.CommandText = "TP_UpdatePreferences";
            objUpdatePref.Parameters.AddWithValue("@userID", p.id);
            objUpdatePref.Parameters.AddWithValue("@likes", p.mLikes);
            objUpdatePref.Parameters.AddWithValue("@dislikes", p.mDislikes);
            objUpdatePref.Parameters.AddWithValue("@blocks", p.mBlocks);
            int result = objDB.DoUpdateUsingCmdObj(objUpdatePref, out string error);

            return result;
        }


        [HttpPost("addDateReq")]
        public DataSet AddDateReq([FromBody] IDictionary<string, string> vals)
        { // adds a date request to the db
            int sendingID = Convert.ToInt16(vals["sendingID"]);
            int recID = Convert.ToInt16(vals["recID"]);
            string message = vals["message"];
            DateTime now = DateTime.Now;

            SqlCommand objDateReq = new SqlCommand();
            objDateReq.CommandType = CommandType.StoredProcedure;
            objDateReq.CommandText = "TP_SendDateRequest";
            objDateReq.Parameters.AddWithValue("@userID", sendingID);
            objDateReq.Parameters.AddWithValue("@memID", recID);
            objDateReq.Parameters.AddWithValue("@now", now);
            objDateReq.Parameters.AddWithValue("@message", message);
            DataSet result = objDB.GetDataSetUsingCmdObj(objDateReq);
            return result;
        } // end add date req

        [HttpGet("getAllDates/{userID}")]
        public DataSet GetAllDates(int userID)
        { // gets all dating reqs
            SqlCommand objGetDates = new SqlCommand();
            objGetDates.CommandType = CommandType.StoredProcedure;
            objGetDates.CommandText = "TP_GetAllDates";
            objGetDates.Parameters.AddWithValue("@userID", userID);
            DataSet result = objDB.GetDataSetUsingCmdObj(objGetDates);
            return result;
        } // end get all dates

        [HttpPut("deleteDateReq")]
        public int DeleteDateRequest([FromBody] IDictionary<string, string> vals)
        { // deletes a dating request
            int sendingID = Convert.ToInt16(vals["sendingID"]);
            int recievingID = Convert.ToInt16(vals["recID"]);

            SqlCommand objDelReq = new SqlCommand();
            objDelReq.CommandType = CommandType.StoredProcedure;
            objDelReq.CommandText = "TP_DeleteDateReq";
            objDelReq.Parameters.AddWithValue("@sendingID", sendingID);
            objDelReq.Parameters.AddWithValue("@recID", recievingID);
            int res = objDB.DoUpdateUsingCmdObj(objDelReq, out string err);
            return res;
        } // end delete date req

        [HttpPut("acceptReq")]
        public int AccceptReq([FromBody] IDictionary<string, string> vals)
        {
            int sendingID = Convert.ToInt16(vals["sendingID"]);
            int recievingID = Convert.ToInt16(vals["recID"]);

            SqlCommand objAcceptReq = new SqlCommand();
            objAcceptReq.CommandType = CommandType.StoredProcedure;
            objAcceptReq.CommandText = "TP_AcceptReq";
            objAcceptReq.Parameters.AddWithValue("@sendingID", sendingID);
            objAcceptReq.Parameters.AddWithValue("@recID", recievingID);

            int res = objDB.DoUpdateUsingCmdObj(objAcceptReq, out string err);
            return res;
        } // end accept req

        [HttpPut("denyReq")]
        public int DenyReq([FromBody] IDictionary<string, string> vals)
        {
            int sendingID = Convert.ToInt16(vals["sendingID"]);
            int recievingID = Convert.ToInt16(vals["recID"]);

            SqlCommand objDenyReq = new SqlCommand();
            objDenyReq.CommandType = CommandType.StoredProcedure;
            objDenyReq.CommandText = "TP_AcceptReq";
            objDenyReq.Parameters.AddWithValue("@senderID", sendingID);
            objDenyReq.Parameters.AddWithValue("@recID", recievingID);

            int res = objDB.DoUpdateUsingCmdObj(objDenyReq, out string err);
            return res;
        }

        [HttpGet("getAcceptedDates/{userID}")]
        public DataSet GetAcceptedReqs(int userID)
        {
            SqlCommand objAccepted = new SqlCommand();

            objAccepted.CommandType = CommandType.StoredProcedure;
            objAccepted.CommandText = "TP_GetAcceptedDates";
            objAccepted.Parameters.AddWithValue("@userID", userID);

            DataSet res = objDB.GetDataSetUsingCmdObj(objAccepted);
            return res;
        } // end getacceptedreqs

        [HttpPost("insertDate")]
        public int InsertDate([FromBody] IDictionary<string, string> vals)
        { // this inserts a date to the db
            int sendingID = Convert.ToInt16(vals["sendingID"]);
            int recID = Convert.ToInt16(vals["recID"]);
            DateTime dt = Convert.ToDateTime(vals["dt"]);
            string location = vals["location"];
            string desc = vals["desc"];

            SqlCommand objInsertDt = new SqlCommand();
            objInsertDt.CommandType = CommandType.StoredProcedure;
            objInsertDt.CommandText = "TP_InsertDateDetails";
            objInsertDt.Parameters.AddWithValue("@sendingID", sendingID);
            objInsertDt.Parameters.AddWithValue("@recID", recID);
            objInsertDt.Parameters.AddWithValue("@dt", dt);
            objInsertDt.Parameters.AddWithValue("@location", location);
            objInsertDt.Parameters.AddWithValue("@description", desc);

            int res = objDB.DoUpdateUsingCmdObj(objInsertDt, out string err);
            return res;
        } // end insert date

        [HttpPut("updateDate/")]
        public int UpdateDate([FromBody] IDictionary<string, string> vals)
        { // updates date details when the user edits it
            int sendingID = Convert.ToInt16(vals["sendingID"]);
            int recID = Convert.ToInt16(vals["recID"]);
            DateTime dt = Convert.ToDateTime(vals["dt"]);
            string location = vals["location"];
            string desc = vals["desc"];

            SqlCommand objUpdateDate = new SqlCommand();
            objUpdateDate.CommandType = CommandType.StoredProcedure;
            objUpdateDate.CommandText = "TP_UpdateDate";
            objUpdateDate.Parameters.AddWithValue("@sendingID", sendingID);
            objUpdateDate.Parameters.AddWithValue("@recID", recID);
            objUpdateDate.Parameters.AddWithValue("@dt", dt);
            objUpdateDate.Parameters.AddWithValue("@location", location);
            objUpdateDate.Parameters.AddWithValue("@description", desc);

            int res = objDB.DoUpdateUsingCmdObj(objUpdateDate, out string err);
            return res;
        } // end update date
    }
}