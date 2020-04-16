using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TP_WebAPI.Controllers
{
    [Route("api/datingservice/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public string searchUsers([FromBody] SearchOptions options)
        {
            return options.Religions; 
        }
    }

    public class Profile
    {
        public string ProfileID { get; set; }

    }
    
    public class SearchOptions
    {
        
        public string Religions { get; set; }
        public string Commitments { get; set; }
        public string Interests { get; set; }
        public string Likes { get; set; }
        public string Dislikes { get; set; }
    }
}