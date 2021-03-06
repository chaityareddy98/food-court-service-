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
public partial class Admin_frmPartialPublic : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DocumentManagementSystemConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetData();
        }
    }
    private void GetData()
    {
        // throw new NotImplementedException();
        string filetype = "private";
        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [tbl_DocUploadMaster] WHERE ([DocFileStatus] = '" + filetype + "')", con);
        DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        grdPartialPublicDocs.DataSource = ds.Tables[0];
        grdPartialPublicDocs.DataBind();

    }

    protected void grdPartialPublicDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int DocId = Convert.ToInt32(e.CommandArgument);
        Server.Transfer("~/Admin/frmDocumentDetails.aspx?Id=" + DocId);
    }
}
