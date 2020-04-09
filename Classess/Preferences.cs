using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_WebAPI.Models
{
    public class Preferences
    {
        public int id { get; set; }
        public Byte[] mLikes { get; set; }
        public Byte[] mDislikes { get; set; }
        public Byte[] mBlocks { get; set; }
    }
}
