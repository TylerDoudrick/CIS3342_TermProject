using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    // there's a div with contact info - that's hidden until date request is approved!!

    public partial class MemberProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null)
            { 
                divPrivateBasic.Attributes.Add("style", "display:flex"); // show private info in the basic info category
                divFavThings.Attributes.Add("style", "display:flex"); // show fav things
                // enable buttons
                btnBlock.Enabled = true; btnLike.Enabled = true; btnPass.Enabled = true; btnDM.Enabled = true;
            } // end if 
            else
            {
                btnBlock.Enabled = false; btnLike.Enabled = false; btnPass.Enabled = false; btnDM.Enabled = false;
            }

        } // end page load

        protected void btnLike_Click(object sender, EventArgs e)
        {

        } // end btn like event handler

        protected void btnPass_Click(object sender, EventArgs e)
        {

        } // end btn pass event handler

        protected void btnBlock_Click(object sender, EventArgs e)
        {

        } // end btn block event handler

        protected void btnDM_Click(object sender, EventArgs e)
        {

        } // end btnDM event handler
    } // end class
} // end namespace