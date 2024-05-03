using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    [NlogTrace]
    public partial class Contact : Page
    {
        public Contact()
        {
            throw new Exception("zaaa");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
         
    }
}