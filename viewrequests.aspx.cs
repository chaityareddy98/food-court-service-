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


public partial class admin_viewreq : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dining services;Integrated Security=True");
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Get();
        }
        
        


    }

    private void Get()
    {
        //SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter("viewreq", con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
      
        
        //Response.Redirect("~/admin/worktostaff.aspx");
    }
  
    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int i = Convert.ToInt32(e.CommandArgument.ToString());
            SqlCommand cmd = new SqlCommand("delete from request where id='" + i + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            
            con.Close();




        }
        //Get();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Get();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
