using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        } // end page load

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Boolean check = false;

            if (!check)
            { // if everything was entered correctly, transfer to participant profile
                Server.Transfer("/Dashboard.aspx");
            } // end if

        } // end save event handler

    } // end class
} // end namespace