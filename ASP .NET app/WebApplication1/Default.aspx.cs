using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Default : System.Web.UI.Page
    {
        private DateTime data;
       
        public Default()
        {
            data = new DateTime();
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            setDateToLabel();
        }
        
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            setDateToLabel();
        }
        
        private void setDateToLabel()
        {
            data = DateTime.Now;
            Label1.Text = data.Year + "-" +
            data.Month + "-" +
            data.Day + " " +
            data.Hour + ":" +
            data.Minute + ":" +
            data.Second + ":" +
            data.Millisecond;
        }


    }
}