using ProjectManager.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectManagementTool._content_pages.vendor_items_search
{
    public partial class _default : System.Web.UI.Page
    {
        DBGetData getdata = new DBGetData();
        MeasurementUpdate getdt = new MeasurementUpdate();
        TaskUpdate gettk = new TaskUpdate();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    BindProject();
                    SelectedProjectWorkpackage("Project");
                    DDlProject_SelectedIndexChanged(sender, e);
                }
            }
        }

        protected void grdItemSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridData();
        }
        private void BindGridData()
        {
            string vendorInput = string.IsNullOrEmpty(txtVendorSearch.Text) ? null : txtVendorSearch.Text;
            string itemInput = string.IsNullOrEmpty(txtItemSearch.Text) ? null : txtItemSearch.Text;
            DataSet ds = getdt.getSerachVendorItems(vendorInput, itemInput);
            grdItemSearch.Visible = true;
            grdItemSearch.DataSource = ds;
            grdItemSearch.DataBind();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            grdItemSearch.Visible = false;
            Response.Redirect("default.aspx");
        }

        protected void grdItemSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdItemSearch.PageIndex = e.NewPageIndex;
            BindGridData();
        }
        private void BindProject()
        {
            DataTable ds = new DataTable();
            if (Session["TypeOfUser"].ToString() == "U" || Session["TypeOfUser"].ToString() == "MD" || Session["TypeOfUser"].ToString() == "VP" || Session["TypeOfUser"].ToString() == "NJSD")
            {
                ds = gettk.GetProjects();
            }
            else if (Session["TypeOfUser"].ToString() == "PA")
            {
                //ds = gettk.GetProjects_by_UserUID(new Guid(Session["UserUID"].ToString()));
                ds = gettk.GetAssignedProjects_by_UserUID(new Guid(Session["UserUID"].ToString()));
            }
            else
            {
                //ds = gettk.GetProjects();
                ds = gettk.GetAssignedProjects_by_UserUID(new Guid(Session["UserUID"].ToString()));
            }
            DDlProject.DataTextField = "ProjectName";
            DDlProject.DataValueField = "ProjectUID";
            DDlProject.DataSource = ds;
            DDlProject.DataBind();
            DDlProject.Items.Insert(0, new ListItem("-- Select Project --", ""));
            DDLWorkPackage.Items.Insert(0, new ListItem("-- Select Workpackage --", ""));
        }
        internal void SelectedProjectWorkpackage(string pType)
        {
            if (!IsPostBack && Session["Project_Workpackage"] != null)
            {
                string[] selectedValue = Session["Project_Workpackage"].ToString().Split('_');
                if (selectedValue.Length > 1)
                {
                    if (pType == "Project")
                    {
                        DDlProject.SelectedValue = selectedValue[0];
                    }
                    else
                    {
                        DDLWorkPackage.SelectedValue = selectedValue[1];
                    }
                }
                else
                {
                    if (pType == "Project")
                    {
                        DDlProject.SelectedValue = Session["Project_Workpackage"].ToString();
                    }
                }
            }

        }
        protected void DDlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDlProject.SelectedValue != "")
            {
                //DataSet ds = getdata.GetWorkPackages_By_ProjectUID(new Guid(DDlProject.SelectedValue));
                DataSet ds = new DataSet();
                if (Session["TypeOfUser"].ToString() == "U" || Session["TypeOfUser"].ToString() == "MD" || Session["TypeOfUser"].ToString() == "VP" || Session["TypeOfUser"].ToString() == "NJSD")
                {
                    ds = getdata.GetWorkPackages_By_ProjectUID(new Guid(DDlProject.SelectedValue));
                }
                else if (Session["TypeOfUser"].ToString() == "PA")
                {
                    ds = getdata.GetWorkPackages_ForUser_by_ProjectUID(new Guid(Session["UserUID"].ToString()), new Guid(DDlProject.SelectedValue));
                }
                else
                {
                    ds = getdata.GetWorkPackages_ForUser_by_ProjectUID(new Guid(Session["UserUID"].ToString()), new Guid(DDlProject.SelectedValue));
                }
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DDLWorkPackage.DataTextField = "Name";
                    DDLWorkPackage.DataValueField = "WorkPackageUID";
                    DDLWorkPackage.DataSource = ds;
                    DDLWorkPackage.DataBind();

                    SelectedProjectWorkpackage("Workpackage");

                   // BindVendorItems();
                    
                    DDLWorkPackage_SelectedIndexChanged(sender, e);

                }
                else
                {
                    
                    DDLWorkPackage.DataSource = null;
                    DDLWorkPackage.DataBind();
                }
            }

        }
        protected void DDLWorkPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //WorkPackageUID
            if (DDLWorkPackage.SelectedValue != "")
            {
               // BindVendorItems();
                
            }
        }


    }
}