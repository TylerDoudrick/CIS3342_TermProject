using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStoreLibrary;

namespace TPWebAPI.Controllers
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
            objSearchCriteria.CommandType = System.Data.CommandType.StoredProcedure;
            objSearchCriteria.CommandText = "TP_GetSearchCriteria";
            DataSet ds = obj.GetDataSetUsingCmdObj(objSearchCriteria);
            return ds;
        }
    }

}