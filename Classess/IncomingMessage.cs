using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
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
}
