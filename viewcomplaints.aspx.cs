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


public partial class admin_viewcomplaints : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dining services;Integrated Security=True");
        
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            Get();
        }
        
        
        
    }
    public void Get()
    {
        //SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter("viewcomp", con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Command(object sender, CommandEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int i = Convert.ToInt32(e.CommandArgument.ToString());
            SqlCommand cmd = new SqlCommand("delete from complaint where id='" + i + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();

            con.Close();




        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Get();
    }
}
