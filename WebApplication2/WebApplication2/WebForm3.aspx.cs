using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace WebApplication2
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        ObjectDataSource1.SelectParameters.Clear();//add this line
        ObjectDataSource1.SelectParameters.Add("MOIS", Session["SelectMonth"].ToString());
        ObjectDataSource1.SelectParameters.Add("ANNEE", Session["SelectYear"].ToString());

        }


}
}