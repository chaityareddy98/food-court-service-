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

public partial class reg : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bal b = new bal();
        b.Entername1=txtname.Text;
        b.Enterdob1 = txtDob.Text;
        b.Enterage1 = txtAge.Text;
        b.Selectgender1 = RadioButtonList1.SelectedValue;
        b.Phoneno1 = txtphoneNo.Text;
        b.Email1 = txtemailid.Text;
        b.Address1 = txtaddress.Text;
        b.Username1 = txtusername.Text;
        b.Password1 = txtpassword.Text;
        b.ConformPassword1 = txtpassword2.Text;
        SqlConnection con1 = new SqlConnection("Data Source=.;Initial Catalog=dining services;Integrated Security=True");

        SqlCommand cmd1 = new SqlCommand("select COUNT(*) from custmerreg where Username='" + txtusername.Text + "' and Password='" + txtpassword.Text + "'",con1);
        con1.Open();

        int a = (int)cmd1.ExecuteScalar();
        con1.Close();
        if (a != 0)
        {
            lbl.Visible = true;
            lbl.Text = "UserName is Exist";
            //RequiredFieldValidator8.ErrorMessage = "UserName is Exist";
        }
        else
        {
            int i = b.insert();
            if (i > 0)
            {
                lbl.Visible = true;


                lbl.Text = "Successfully Registered";
                txtaddress.Text = "";
                txtAge.Text = "";
                txtDob.Text = "";
                txtemailid.Text = "";
                txtname.Text = "";
                txtpassword.Text = "";
                txtpassword2.Text = "";
                txtphoneNo.Text = "";
                txtusername.Text = "";

                // RadioButtonList1.SelectedValue = Convert.ToString(false);


            }
            else
            {
                Response.Redirect("reg.aspx");
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("custologin.aspx");
    }
}
