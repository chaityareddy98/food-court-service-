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
using System.IO;
public partial class Admin_frmUplaodDoc : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!IsPostBack)
            {
                GetDocTypeNames();
                DisplayUserDetailsOnGrid();
                Session["i"] = 0;
                Session["UserLoginIds"] = "0";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void ddlPermission_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlPermission.SelectedIndex == 2)
        {
            DisplayUserDetailsOnGrid();
            GrvUsers.Visible = true;
            panel1.Visible = true;
        }
        else
        {
            GrvUsers.Visible = false;
            panel1.Visible = false;
        }
    }
    public void GetDocTypeNames()
    {
        
        DataSet dsDocType = clsDocuments.GetAllDocTypeMaster();
        if (dsDocType.Tables[0].Rows.Count > 0)
        {
            ddlDocTypeId.DataTextField = "DocTypeDesc";
            ddlDocTypeId.DataValueField = "DocTypeId";
            ddlDocTypeId.DataSource = dsDocType.Tables[0];
            ddlDocTypeId.DataBind();
            ddlDocTypeId.Items.Insert(0, "--Select One--");
        }
    }
   
    public void DisplayUserDetailsOnGrid()
    {
        DataSet dsUsers = clsUsers.GetAllUserDetails();
        if (dsUsers.Tables[0].Rows.Count > 0)
        {
            GrvUsers.DataSource = dsUsers.Tables[0];
            GrvUsers.DataBind();
        }
        else
        {
            GrvUsers.EmptyDataText = "No records found";
            GrvUsers.DataBind();
        }
    }
    string str = "";
    protected void GrvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow dRow in GrvUsers.Rows)
        {
            chk = (CheckBox)dRow.FindControl("Cbx_MailId1");
            if (chk.Checked == true)
            {
                if (str == "")
                {
                    str = chk.Text;
                }
                else
                {
                    string strold = (string)Session["UserLoginIds"];
                    if (!strold.Contains(chk.Text))
                    {
                        str = str + "," + chk.Text;
                    }
                }
            }
            else
            {
                string strold = (string)Session["UserLoginIds"];
                string[] splitOld = strold.Split(',');
                string strNew = "";
                for (int i = 0; i <= splitOld.Length - 1; i++)
                {
                    if (splitOld.Contains(chk.Text))
                    {
                        splitOld[i] = "";
                    }
                    else
                    {
                        if (strNew == "")
                            strNew = splitOld[i];
                        else
                            strNew = strNew + "," + splitOld[i];
                    }
                }
                Session["UserLoginIds"] = strNew;
            }
        }
        if (Session["UserLoginIds"].ToString() == "0")
        {
            Session["UserLoginIds"] = str;
        }
        else
        {
            string strold = (string)Session["UserLoginIds"];
            Session["UserLoginIds"] = str + "," + strold;
        }
        GrvUsers.PageIndex = e.NewPageIndex;
        DisplayUserDetailsOnGrid();
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            clsUsers.intUserId = Convert.ToInt32(Session["UserId"]);
            clsUsers.intDocTypeId = Convert.ToInt32(ddlDocTypeId.SelectedValue);
            clsUsers.strDocDesc = Convert.ToString(txtDesc.Text);
            clsUsers.strDocShortInfoText = Convert.ToString(txtShortInfo.Text);
            clsUsers.strSearchKeys = Convert.ToString(txtSearchKeyWords.Text);
            clsUsers.intFileSize = Convert.ToInt32(lblFileSize.Text);
            string UserList = "";
            CheckBox chk;
            foreach (GridViewRow drow in GrvUsers.Rows)
            {
                chk = (CheckBox)drow.FindControl("Cbx_MailId1");
                if (chk.Checked == true)
                {
                    Label lbl = (Label)drow.FindControl("lblUserId");
                    if (UserList == "")
                        UserList = lbl.Text;
                    else
                        UserList = UserList + "," + lbl.Text;
                }
            }
            Session["UserLoginIds"] = UserList;
            clsUsers.strUserIdsForPrivate = Convert.ToString(UserList);
            clsUsers.strDocName = Convert.ToString(Session["FileName"]);
            clsUsers.byteDocFile = (byte[])Session["FileContent"];
            clsUsers.strFileSharedStatus = Convert.ToString(ddlPermission.SelectedValue);
            lblMsg.Text = clsUsers.InsertUploadDocumentDetails();
            lblMsg.Visible = true;
            ClearFields();
        }
        catch (Exception ex)
        {
            lblMsg.Visible = true;
            lblMsg.Text = ex.Message.ToString();
        }
    }
    void ClearFields()
    {
        txtDesc.Text = "";
        txtSearchKeyWords.Text = "";
        txtShortInfo.Text = "";
        ddlPermission.SelectedIndex = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            HttpPostedFile File = FileUpload1.PostedFile;
            int FileSize = Convert.ToInt32(File.ContentLength);
            string FileName=FileUpload1.PostedFile.ToString();
            int i = (FileSize) / (1024 * 1024);
            if (i <= 10)
            {
                
                string[] a = new string[] { ".doc", ".docx", ".txt", ".xl", ".jpeg", ".mpeg", ".png", ".gif", ".jpg", ".bmp", ".ppt", ".xml", ".pdf" };
                FileInfo info = new FileInfo(FileUpload1.PostedFile.FileName);
                string strExt = info.Extension;
                if (a.Contains(strExt.ToLower()))
                {
                    Session["ActivityStaringTime"] = DateTime.Now.ToString();
                    Byte[] Data = clsUtilityClass.ReadImageFile(FileUpload1.PostedFile.FileName, a);
                    if (Data != null)
                    {
                        Session["FileContent"] = Data;
                        Session["FileName"] = FileUpload1.FileName.Trim();
                        lblFileSize.Text = FileSize.ToString();
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Selected file data not supporting to attach.";
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "File Format Not Supported Choose Another one";
                }
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "File Lengh Too Long";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Visible = true;
            lblMsg.Text = ex.Message.ToString();
        }
    }
   
    protected void GrvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string strUserIds = (string)Session["UserLoginIds"];
        CheckBox chk;
        Label lblId;
        foreach (GridViewRow dRow in GrvUsers.Rows)
        {
            if (strUserIds != null)
            {
                lblId = (Label)dRow.FindControl("lblUserId");
                chk = (CheckBox)dRow.FindControl("Cbx_MailId1");
                if (strUserIds.Contains(lblId.Text))
                {
                    chk.Checked = true;
                }
            }
        }
    }

    protected void ddlDocTypeId_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GrvUsers_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

