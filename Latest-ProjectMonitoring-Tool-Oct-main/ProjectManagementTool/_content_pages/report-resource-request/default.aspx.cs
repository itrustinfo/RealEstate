using ProjectManager.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectManagementTool._content_pages.report_resource_request
{
    public partial class _default : System.Web.UI.Page
    {
        DBGetData getdt = new DBGetData();
        TaskUpdate gettk = new TaskUpdate();
        DataSet ds = new DataSet();
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
                   
                }
            }
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
                ds = gettk.GetAssignedProjects_by_UserUID(new Guid(Session["UserUID"].ToString()));
            }
            else
            {
                ds = gettk.GetAssignedProjects_by_UserUID(new Guid(Session["UserUID"].ToString()));
            }

            DDlProject.DataTextField = "ProjectName";
            DDlProject.DataValueField = "ProjectUID";
            DDlProject.DataSource = ds;
            DDlProject.DataBind();

            DDlProject.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", ""));
            DDLWorkPackage.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", ""));

        }

        protected void DDlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDlProject.SelectedValue != "")
            {
                DataSet ds = new DataSet();
                if (Session["TypeOfUser"].ToString() == "U" || Session["TypeOfUser"].ToString() == "MD" || Session["TypeOfUser"].ToString() == "VP" || Session["TypeOfUser"].ToString() == "NJSD")
                {
                    ds = getdt.GetWorkPackages_By_ProjectUID(new Guid(DDlProject.SelectedValue));
                }
                else if (Session["TypeOfUser"].ToString() == "PA")
                {
                    ds = getdt.GetWorkPackages_ForUser_by_ProjectUID(new Guid(Session["UserUID"].ToString()), new Guid(DDlProject.SelectedValue));
                }
                else
                {
                    ds = getdt.GetWorkPackages_ForUser_by_ProjectUID(new Guid(Session["UserUID"].ToString()), new Guid(DDlProject.SelectedValue));
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DDLWorkPackage.DataTextField = "Name";
                    DDLWorkPackage.DataValueField = "WorkPackageUID";
                    DDLWorkPackage.DataSource = ds;
                    DDLWorkPackage.DataBind();

                    DDLWorkPackage_SelectedIndexChanged(sender, e);
                }
            }

        }

        protected void DDLWorkPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLWorkPackage.SelectedValue != "")
            {
                divTabular.Visible = true;
                BindDataforDemo();
                // BindIssues(DDLWorkPackage.SelectedValue);
                // IssueCountLoad(DDLWorkPackage.SelectedValue);
            }
        }

        private void BindDataforDemo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("RequestID");
                dt.Columns.Add("RequestDate");
                dt.Columns.Add("ItemCode");
                dt.Columns.Add("ItemDescription");
                dt.Columns.Add("UOM");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Status");
                dt.Columns.Add("Remarks");

                DataRow r1 = dt.NewRow();
                r1["RequestID"] = "Rq01";
                r1["RequestDate"] = "12/08/2015";
                r1["ItemCode"] = "80100002-BRICKS";
                r1["ItemDescription"] = "Bricks";               
                r1["UOM"] = "NOS";
                r1["Quantity"] = "20.00";
                r1["Status"] = "Dispatched";
                r1["Remarks"] = "Test Request";
                dt.Rows.Add(r1);
                //
                r1 = dt.NewRow();
                r1["RequestID"] = "Rq02";
                r1["RequestDate"] = "12/08/2015";
                r1["ItemCode"] = "80100002-BRICKS";
                r1["ItemDescription"] = "Bricks";
                r1["UOM"] = "NOS";
                r1["Quantity"] = "20.00";
                r1["Status"] = "Processing";
                r1["Remarks"] = "Test Request";
                dt.Rows.Add(r1);
                //
                
                r1 = dt.NewRow();
                r1["RequestID"] = "Rq03";
                r1["RequestDate"] = "12/08/2015";
                r1["ItemCode"] = "80100002-BRICKS";
                r1["ItemDescription"] = "Bricks";
                r1["UOM"] = "NOS";
                r1["Quantity"] = "20.00";
                r1["Status"] = "Not Dispatched";
                r1["Remarks"] = "Test Request";
                dt.Rows.Add(r1);
                //                
                grdDataList.DataSource = dt;
                grdDataList.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}