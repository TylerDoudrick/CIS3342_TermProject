using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    // there's a div with contact info - that's hidden until date request is approved!!

    public partial class MemberProfile : System.Web.UI.Page
    {
        int memberUserID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            { 
                divPrivateBasic.Attributes.Add("style", "display:flex"); // show private info in the basic info category
                divFavThings.Attributes.Add("style", "display:flex"); // show fav things
                // enable buttons
                btnBlock.Enabled = true; btnLike.Enabled = true; btnPass.Enabled = true;  btnDateRequest.Enabled = true;
                
            } // end if 
            else
            {
                btnBlock.Enabled = false; btnLike.Enabled = false; btnPass.Enabled = false;btnDateRequest.Enabled = false;
            }
            memberUserID = 0; // this is the user id of the person who's profile we're viewing
        } // end page load

        protected void btnLike_Click(object sender, EventArgs e)
        {
            List<int> memberLikes = (List<int>)Session["memberLikes"];
            memberLikes.Add(memberUserID);
            Session["memberLikes"] = memberLikes;
        } // end btn like event handler

        protected void btnPass_Click(object sender, EventArgs e)
        {
            List<int> memberDislikes = (List<int>)Session["memberDislikes"];
            memberDislikes.Add(memberUserID);
            Session["memberDislikes"] = memberDislikes;
        } // end btn pass event handler

        protected void btnBlock_Click(object sender, EventArgs e)
        {
            List<int> memberBlocks = (List<int>)Session["memberBlocks"];
            memberBlocks.Add(memberUserID);
            Session["memberBlocks"] = memberBlocks;
        } // end btn block event handler
        

        protected void btnDateRequest_Click(object sender, EventArgs e)
        {
           /* string email = Session["email"].ToString(); // this needs to be changed to member's email!
            string sendAdd = "querydating@gmail.com";
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(@email));
            msg.Subject = "QUERY Verification Email";
            msg.From = new MailAddress(sendAdd);
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.Body = "<div> You've gotten a date request! Log into your account to view the request " +
                "and to accept or deny! </div>";
            SmtpClient smtp = new SmtpClient("smtp.temple.edu");
            smtp.EnableSsl = true;

            smtp.Send(msg); */
        } // end date request eventhandler
    } // end class
} // end namespace