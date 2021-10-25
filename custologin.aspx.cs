using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class custologin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dining services;Integrated Security=True");
        SqlDataAdapter da = new SqlDataAdapter("select Username,Password from custmerreg where username='"+TextBox1.Text+"' and Password='"+TextBox2.Text+"'",con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["Username"].ToString() == TextBox1.Text)
            {
                if (ds.Tables[0].Rows[0]["Password"].ToString() == TextBox2.Text)
                {
                    Session["username"] = TextBox1.Text;
                    Response.Redirect("~/Customer/Default.aspx");
                }
            }
           
        }
        else
        {
            Response.Redirect("custologin.aspx");
        }
        
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("reg.aspx");

    }
}
