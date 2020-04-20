using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using TermProject;

namespace TP_WebAPI.Controllers
{
    [Authorize]
    [EnableCors("AllowOrigin")]
    [Route("api/datingservice/interactions/")]
    [ApiController]
    public class InteractionsController : ControllerBase
    {
        //
        //"for any and all interactions so updating the liked lists, pass list, blocked lists, dates, and messages"
        //
        DBConnect objDB = new DBConnect();
        Notifier notifier = new Notifier();

        [AllowAnonymous]
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
        public int AddDateReq([FromBody] IDictionary<string, string> vals)
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
            objDateReq.Parameters.Add("@SenderName", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;

            int result = objDB.DoUpdateUsingCmdObj(objDateReq, out string err);

            notifier.NotifyDate(recID, "New date request from " + objDateReq.Parameters["@SenderName"].Value + "!");
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
            objAcceptReq.Parameters.Add("@SenderName", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;

            int res = objDB.DoUpdateUsingCmdObj(objAcceptReq, out string err);
            notifier.NotifyDate(recievingID, objAcceptReq.Parameters["@SenderName"].Value + " accepted your date!");

            return res;
        } // end accept req

        [HttpPut("denyReq")]
        public int DenyReq([FromBody] IDictionary<string, string> vals)
        {
            int sendingID = Convert.ToInt16(vals["sendingID"]);
            int recievingID = Convert.ToInt16(vals["recID"]);

            SqlCommand objDenyReq = new SqlCommand();
            objDenyReq.CommandType = CommandType.StoredProcedure;
            objDenyReq.CommandText = "TP_DenyReq";
            objDenyReq.Parameters.AddWithValue("@senderID", sendingID);
            objDenyReq.Parameters.AddWithValue("@recID", recievingID);
            objDenyReq.Parameters.Add("@SenderName", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;

            int res = objDB.DoUpdateUsingCmdObj(objDenyReq, out string err);

            notifier.NotifyDate(recievingID, objDenyReq.Parameters["@SenderName"].Value + " denied your date!");

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
            objInsertDt.Parameters.Add("@SenderName", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;

            int res = objDB.DoUpdateUsingCmdObj(objInsertDt, out string err);

            notifier.NotifyDate(recID, "New date request from " + objInsertDt.Parameters["@SenderName"].Value);


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

        [HttpPut("ignoreReq")]
        public int IgnoreRequest([FromBody] IDictionary<string, string> vals)
        { // this ignores a request
            int sendingID = Convert.ToInt16(vals["sendingID"]);
            int recID = Convert.ToInt16(vals["recID"]);

            SqlCommand objIgnoreReq = new SqlCommand();
            objIgnoreReq.CommandType = CommandType.StoredProcedure;
            objIgnoreReq.CommandText = "TP_IgnoreReq";
            objIgnoreReq.Parameters.AddWithValue("@senderID", sendingID);
            objIgnoreReq.Parameters.AddWithValue("@recID", recID);

            int res = objDB.DoUpdateUsingCmdObj(objIgnoreReq, out string err);
            return res;
        } // end ignore request


        [HttpPost("GetUserInbox")]
        public List<IncomingMessage> GetUserInbox([FromBody] User user)
        {
            List<IncomingMessage> messages = new List<IncomingMessage>();
            SqlCommand commandObj = new SqlCommand();

            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_GetUserInbox";
            commandObj.Parameters.AddWithValue("@UserID", user.id);

            DataSet res = objDB.GetDataSetUsingCmdObj(commandObj);

            if (res.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (DataRow row in res.Tables[0].Rows)
                {
                    IncomingMessage temp = new IncomingMessage();
                    temp.messageid = row["messageID"].ToString();
                    temp.senderid = row["senderID"].ToString();
                    temp.message = row["messageBody"].ToString();
                    DateTime sentOn = DateTime.Parse(row["timeStamp"].ToString());
                    temp.timestamp = sentOn.ToString("dddd, MMMM dd'th' yyyy '@' hh:mm tt");
                    temp.readreceipt = row["readReceipt"].ToString();
                    temp.sendername = row["name"].ToString();
                    Byte[] imgArray = (Byte[])row["profileImage"];
                    MemoryStream memorystreamd = new MemoryStream(imgArray);
                    BinaryFormatter bfd = new BinaryFormatter();
                    temp.senderimage = bfd.Deserialize(memorystreamd).ToString();
                    messages.Add(temp);
                }
            }

            return messages;
        }


        [HttpPost("GetUserOutbox")]
        public List<OutgoingMessage> GetUserOutbox([FromBody] User user)
        {
            List<OutgoingMessage> messages = new List<OutgoingMessage>();
            SqlCommand commandObj = new SqlCommand();

            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_GetUserOutbox";
            commandObj.Parameters.AddWithValue("@UserID", user.id);

            DataSet res = objDB.GetDataSetUsingCmdObj(commandObj);

            if (res.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (DataRow row in res.Tables[0].Rows)
                {
                    OutgoingMessage temp = new OutgoingMessage();
                    temp.receiverid = row["receiverID"].ToString();
                    temp.message = row["messageBody"].ToString();
                    DateTime sentOn = DateTime.Parse(row["timeStamp"].ToString());
                    temp.timestamp = sentOn.ToString("dddd, MMMM dd'th' yyyy '@' hh:mm tt");
                    temp.readreceipt = row["readReceipt"].ToString();
                    temp.receivername = row["name"].ToString();
                    Byte[] imgArray = (Byte[])row["profileImage"];
                    MemoryStream memorystreamd = new MemoryStream(imgArray);
                    BinaryFormatter bfd = new BinaryFormatter();
                    temp.receiverimage = bfd.Deserialize(memorystreamd).ToString();
                    messages.Add(temp);
                }
            }

            return messages;
        }



        [HttpPost("ProfileSnippet")]
        public Recipient ProfileSnippet([FromBody] User user)
        {
            Recipient found = new Recipient();
            SqlCommand commandObj = new SqlCommand();

            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_GetProfileSnippet";
            commandObj.Parameters.AddWithValue("@UserID", user.id);

            DataSet res = objDB.GetDataSetUsingCmdObj(commandObj);

            if (res.Tables[0].Rows.Count != 1)
            {
                return null;
            }
            else
            {
                DataRow row = res.Tables[0].Rows[0];
                found.userID = row["userID"].ToString();
                found.name = row["name"].ToString();
                Byte[] imgArray = (Byte[])row["image"];
                MemoryStream memorystreamd = new MemoryStream(imgArray);
                BinaryFormatter bfd = new BinaryFormatter();
                found.image = bfd.Deserialize(memorystreamd).ToString();
                found.location = row["location"].ToString();

            }


            return found;
        }


        [HttpPut("SendMessage")]
        public Response SendMessage([FromBody] SendingMessage message)
        {
            Response response = new Response();
            SqlCommand commandObj = new SqlCommand();

            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_SendMessage";
            commandObj.Parameters.AddWithValue("@SenderID", message.senderid);
            commandObj.Parameters.AddWithValue("@RecipientID", message.recipientid);
            commandObj.Parameters.AddWithValue("@MessageBody", message.message);
            commandObj.Parameters.Add("@SenderName", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
            commandObj.Parameters.Add("@SenderEmail", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
            if (objDB.DoUpdateUsingCmdObj(commandObj, out string exception) == -2)
            {
                response.result = "fail";
                response.message = exception;
            }
            else
            {

                string email = commandObj.Parameters["@SenderEmail"].Value.ToString();
                string sendAdd = "querydating@gmail.com";
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(@email));
                msg.Subject = "QUERY Verification Email";
                msg.From = new MailAddress(sendAdd);
                msg.IsBodyHtml = true;
                msg.Body = "<div> You got a new message! <br><BR> Sign into your account to view the message of your admirer!" +
                    "<Br><BR> <div>";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential(sendAdd, "CIS3342TermProject");
                smtp.EnableSsl = true;

                smtp.Send(msg);

                notifier.NotifyMessage(Int32.Parse(message.recipientid), "You have a new message from " + commandObj.Parameters["@SenderName"].Value + "!");
                response.result = "success";
                response.message = "Successfully sent Message!";
            }

            return response;
        }
        [HttpPut("UpdateReadReceipt")]
        public Response UpdateReadReceipt([FromBody] MessageInfo message)
        {
            Response response = new Response();
            SqlCommand commandObj = new SqlCommand();

            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "TP_UpdateReadReceipt";
            commandObj.Parameters.AddWithValue("@MessageID", message.id);

            if (objDB.DoUpdateUsingCmdObj(commandObj, out string exception) == -2)
            {
                response.result = "fail";
                response.message = exception;
            }
            else
            {
                //  notifier.NotifyMessage();
                response.result = "success";
                response.message = "Successfully updated readreceipt";
            }

            return response;
        }
        public class User
        {
            public string id { get; set; }
        }
        public class MessageInfo
        {
            public string id { get; set; }
        }
        public class SendingMessage
        {
            public string senderid { get; set; }
            public string recipientid { get; set; }
            public string recipientname { get; set; }
            public string message { get; set; }

        }

        public class IncomingMessage
        {
            public string messageid { get; set; }
            public string senderid { get; set; }
            public string sendername { get; set; }
            public string senderimage { get; set; }
            public string recipientid { get; set; }
            public string message { get; set; }
            public string timestamp { get; set; }
            public string readreceipt { get; set; }
        }

        public class OutgoingMessage
        {
            public string receiverid { get; set; }
            public string receivername { get; set; }
            public string receiverimage { get; set; }
            public string senderid { get; set; }
            public string message { get; set; }
            public string timestamp { get; set; }
            public string readreceipt { get; set; }
        }
        public class Recipient
        {
            public string userID { get; set; }
            public string name { get; set; }
            public string image { get; set; }
            public string location { get; set; }
        }

        public class Response
        {
            public string result { get; set; }
            public string message { get; set; }
        }
    }
}