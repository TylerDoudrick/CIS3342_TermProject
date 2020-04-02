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
            { // if user is logged in, show the private info
                divPrivateBasic.Attributes.Add("style", "display:block");
            } // end if 

        } // end page load
    } // end class
} // end namespace