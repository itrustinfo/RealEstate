using ProjectManager.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectManagementTool._content_pages.vendor_items
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
                    HLAdd.Visible = false;
                    DDlProject_SelectedIndexChanged(sender, e);
                    

                }
            }
        }
        private void BindProject()
        {
            DataTable ds = new DataTable();
            if (Session["TypeOfUser"].ToString() == "U" || Session["TypeOfUser"].ToString() =="MD" || Session["TypeOfUser"].ToString() == "VP" || Session["TypeOfUser"].ToString() == "NJSD")
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

                    //if (Session["SelectedWorkpakage"] != null)
                    //{
                    //    DDLWorkPackage.SelectedValue = Session["SelectedWorkpakage"].ToString().Split('_')[1];
                    //}
                    HLAdd.Visible = true;
                    BindVendorItems();
                    HLAdd.HRef = "/_modal_pages/add-vendoritemsmaster.aspx.aspx?ProjectUID=" + DDlProject.SelectedValue + "&WorkPackageUID=" + DDLWorkPackage.SelectedValue;
                    Session["Project_Workpackage"] = DDlProject.SelectedValue + "_" + DDLWorkPackage.SelectedValue;
                    DDLWorkPackage_SelectedIndexChanged(sender, e);
                    
                }
                else
                {
                    HLAdd.Visible = false;
                    DDLWorkPackage.DataSource = null;
                    DDLWorkPackage.DataBind();
                }
            }
        }
        private void BindVendorItems()
        {
            //fetch ItemLevel=1 from DB
            DataSet ds = getdt.getMainItems();

            //Bind Data to Main Item DDL
            DDLMainItem.DataTextField = "GenericName";
            DDLMainItem.DataValueField = "ItemID";
            DDLMainItem.DataSource = ds;
            DDLMainItem.DataBind();
            // Add the first entry as "--select--"
            DDLMainItem.Items.Insert(0, new ListItem("--select--", "0"));

            // Bind Data to Grid
            grdVendorItem.DataSource = ds; 
            grdVendorItem.DataBind();


        }
        protected void DDLWorkPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //WorkPackageUID
            if (DDLWorkPackage.SelectedValue != "")
            {
                BindVendorItems();
                HLAdd.HRef = "/_modal_pages/add-vendoritemsmaster.aspx?ProjectUID=" + DDlProject.SelectedValue + "&WorkPackageUID=" + DDLWorkPackage.SelectedValue;
                Session["SelectedWorkpakage"] = DDlProject.SelectedValue + "_" + DDLWorkPackage.SelectedValue;
                Session["Project_Workpackage"] = DDlProject.SelectedValue + "_" + DDLWorkPackage.SelectedValue;
            }
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
        protected void grdVendors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Set the Serial No. based on the row index
                int serialNo = (grdVendorItem.PageIndex * grdVendorItem.PageSize) + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = serialNo.ToString();
            }
        }


        protected void grdVendors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdVendorItem.PageIndex = e.NewPageIndex;
            BindVendorItems();

        }
        protected void DDLMainItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLMainItem.SelectedValue != "0")
            {
                DataSet ds = new DataSet();
                ds = getdt.getSubItems(new Guid(DDLMainItem.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SubItem1.Visible = true;
                    DDLSubItem1.Visible = true;
                    DDLSubItem1.DataTextField = "GenericName";
                    DDLSubItem1.DataValueField = "ItemID";
                    DDLSubItem1.DataSource = ds;
                    DDLSubItem1.DataBind();
                    // Add the first entry as "--select--"
                    DDLSubItem1.Items.Insert(0, new ListItem("--select--", "0"));
                }
                else
                {
                    // Clear the previously loaded values & hide next DDL
                    DDLSubItem1.DataSource = null;
                    DDLSubItem1.DataBind();
                    SubItem1.Visible = false;
                    DDLSubItem1.Visible = false;
                }
            }
            else
            {
                // Clear the previously loaded values & hide next DDL
                DDLSubItem1.DataSource = null;
                DDLSubItem1.DataBind();
                SubItem1.Visible = false;
                DDLSubItem1.Visible = false;
            }

        }
        protected void DDLSubItem1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLSubItem1.SelectedValue != "0")
            {
                DataSet ds = new DataSet();
                ds = getdt.getSubItems(new Guid(DDLSubItem1.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SubItem2.Visible = true;
                    DDLSubItem2.Visible = true;
                    DDLSubItem2.DataTextField = "GenericName";
                    DDLSubItem2.DataValueField = "ItemID";
                    DDLSubItem2.DataSource = ds;
                    DDLSubItem2.DataBind();
                    // Add the first entry as "--select--"
                    DDLSubItem2.Items.Insert(0, new ListItem("--select--", "0"));
                }
                else
                {
                    // Clear the previously loaded values
                    DDLSubItem2.DataSource = null;
                    DDLSubItem2.DataBind();
                    SubItem2.Visible = false;
                    DDLSubItem2.Visible = false;
                }
            }
            else
            {
                // Clear the previously loaded values
                DDLSubItem2.DataSource = null;
                DDLSubItem2.DataBind();
                SubItem2.Visible = false;
                DDLSubItem2.Visible = false;
            }

        }

        protected void DDLSubItem2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLSubItem2.SelectedValue != "0")
            {
                DataSet ds = new DataSet();
                ds = getdt.getSubItems(new Guid(DDLSubItem2.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SubItem3.Visible = true;
                    DDLSubItem3.Visible = true;
                    DDLSubItem3.DataTextField = "GenericName";
                    DDLSubItem3.DataValueField = "ItemID";
                    DDLSubItem3.DataSource = ds;
                    DDLSubItem3.DataBind();
                    // Add the first entry as "--select--"
                    DDLSubItem3.Items.Insert(0, new ListItem("--select--", "0"));
                }
                else
                {
                    // Clear the previously loaded values
                    DDLSubItem3.DataSource = null;
                    DDLSubItem3.DataBind();
                    SubItem3.Visible = false;
                    DDLSubItem3.Visible = false;
                }
            }
            else
            {
                // Clear the previously loaded values
                DDLSubItem3.DataSource = null;
                DDLSubItem3.DataBind();
                SubItem3.Visible = false;
                DDLSubItem3.Visible = false;
            }

        }

        protected void DDLSubItem3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLSubItem3.SelectedValue != "0")
            {
                DataSet ds = new DataSet();
                ds = getdt.getSubItems(new Guid(DDLSubItem3.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SubItem4.Visible = true;
                    DDLSubItem4.Visible = true;
                    DDLSubItem4.DataTextField = "GenericName";
                    DDLSubItem4.DataValueField = "ItemID";
                    DDLSubItem4.DataSource = ds;
                    DDLSubItem4.DataBind();
                    // Add the first entry as "--select--"
                    DDLSubItem4.Items.Insert(0, new ListItem("--select--", "0"));
                }
                else
                {
                    // Clear the previously loaded values
                    DDLSubItem4.DataSource = null;
                    DDLSubItem4.DataBind();
                    SubItem4.Visible = false;
                    DDLSubItem4.Visible = false;
                }
            }
            else
            {
                // Clear the previously loaded values
                DDLSubItem4.DataSource = null;
                DDLSubItem4.DataBind();
                SubItem4.Visible = false;
                DDLSubItem4.Visible = false;
            }

        }

        protected void DDLSubItem4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DDLSubItem4.SelectedValue) && DDLSubItem4.SelectedValue != "0")
            {
                DataSet ds = new DataSet();
                ds = getdt.getSubItems(new Guid(DDLSubItem4.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdVendorItem.DataSource = ds;
                    grdVendorItem.DataBind();
                }
                else
                {
                    // Clear any previous data
                    grdVendorItem.DataSource = null;
                    grdVendorItem.DataBind();

                }
            }
            else if (!string.IsNullOrEmpty(DDLSubItem3.SelectedValue) && DDLSubItem3.SelectedValue != "0")
            {
                DataSet ds = new DataSet();
                ds = getdt.getSubItems(new Guid(DDLSubItem3.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdVendorItem.DataSource = ds;
                    grdVendorItem.DataBind();
                }
                else
                {
                    // Clear any previous data
                    grdVendorItem.DataSource = null;
                    grdVendorItem.DataBind();
                }
            }
            else if (!string.IsNullOrEmpty(DDLSubItem2.SelectedValue) && DDLSubItem2.SelectedValue != "0")
            {
                DataSet ds = new DataSet();
                ds = getdt.getSubItems(new Guid(DDLSubItem2.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdVendorItem.DataSource = ds;
                    grdVendorItem.DataBind();
                }
                else
                {
                    // Clear any previous data
                    grdVendorItem.DataSource = null;
                    grdVendorItem.DataBind();
                }
            }
            else if (!string.IsNullOrEmpty(DDLSubItem1.SelectedValue) && DDLSubItem1.SelectedValue != "0")
            {
                DataSet ds = new DataSet();
                ds = getdt.getSubItems(new Guid(DDLSubItem1.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdVendorItem.DataSource = ds;
                    grdVendorItem.DataBind();
                }
                else
                {
                    // Clear any previous data
                    grdVendorItem.DataSource = null;
                    grdVendorItem.DataBind();

                }
            }
            else if (!string.IsNullOrEmpty(DDLMainItem.SelectedValue) && DDLMainItem.SelectedValue != "0")
            {
                DataSet ds = new DataSet();
                ds = getdt.getSubItems(new Guid(DDLMainItem.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdVendorItem.DataSource = ds;
                    grdVendorItem.DataBind();
                }
                else
                {
                    // Clear any previous data
                    grdVendorItem.DataSource = null;
                    grdVendorItem.DataBind();
                }
              
            }
            else
            {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Alert: Invalid Input, Please select atleast one Item!');", true);

            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
       
    }
}