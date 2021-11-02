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

public partial class Customer_myprofile : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dining services;Integrated Security=True");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from custmerreg where Username='" + Session["username"].ToString() + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            TextBox1.Text = ds.Tables[0].Rows[0]["Entername"].ToString();
            TextBox2.Text = ds.Tables[0].Rows[0]["Enterdob"].ToString();
            TextBox3.Text = ds.Tables[0].Rows[0]["Enterage"].ToString();
            string re=ds.Tables[0].Rows[0]["Selectgender"].ToString();
            if (re == "true")
            {
                TextBox11.Text = "FeMale";
            }
            else
            {
                TextBox11.Text = "Male";
            }
            TextBox5.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            TextBox6.Text = ds.Tables[0].Rows[0]["Phoneno"].ToString();
            TextBox7.Text = ds.Tables[0].Rows[0]["Email"].ToString();
            TextBox8.Text = ds.Tables[0].Rows[0]["Username"].ToString();
            TextBox9.Text = ds.Tables[0].Rows[0]["Password"].ToString();
            TextBox10.Text = ds.Tables[0].Rows[0]["ConformPassword"].ToString();
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("update custmerreg set Entername='" + TextBox1.Text + "',Enterdob='" + TextBox2.Text + "',Enterage='" + TextBox3.Text + "' ,Selectgender='" + TextBox11.Text + "',Address='" + TextBox5.Text + "',Phoneno='" + TextBox6.Text + "',Email='" + TextBox7.Text + "',Username='" + TextBox8.Text + "',Password='" + TextBox9.Text + "',ConformPassword='" + TextBox10.Text + "'",con);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        if (i > 0)
        {
            lbl.Visible = true;
            lbl.Text="Successfully updated" ;
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox11.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";

        }
        else
        {
            lbl.Text="failed";
        }
        con.Close();
    }

}
