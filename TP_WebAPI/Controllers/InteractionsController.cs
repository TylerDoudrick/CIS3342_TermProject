﻿using System;
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
            int result = objDB.DoUpdateUsingCmdObj(objInsertPref);
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
            int result = objDB.DoUpdateUsingCmdObj(objUpdatePref);
            return result;
        }

    }
}