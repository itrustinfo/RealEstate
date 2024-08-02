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

namespace ProjectManagementTool._content_pages.goods_receipt_notes_with_po
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
                dt.Columns.Add("GRN No.");
                dt.Columns.Add("GRN Date");
                dt.Columns.Add("PO No");
                dt.Columns.Add("Vendor");
                dt.Columns.Add("Total Qty");
                dt.Columns.Add("Total Amount");
                

                DataRow r1 = dt.NewRow();
                //r1["GRN No."] = "BG/GRN/85";
                //r1["GRN Date"] = "12/08/2015";
                //r1["PO No"] = "P0123456";
                //r1["Vendor"] = "Q.S ENTERPRISES";
                //r1["Total Qty"] = "10.000";              
                //r1["Total Amount"] = "990.00";
                //r1["Status"] = "New";
                //dt.Rows.Add(r1);
                ////
                //r1 = dt.NewRow();
                //r1["GRN No."] = "BG/GRN/86";
                //r1["GRN Date"] = "12/08/2015";
                //r1["PO No"] = "P0123456";
                //r1["Vendor"] = "Q.S.MOTORS";
                //r1["Total Qty"] = "20.000";
                //r1["Total Amount"] = "80.00";
                //r1["Status"] = "Authorized";
                //dt.Rows.Add(r1);
                ////
                //r1 = dt.NewRow();
                //r1["GRN No."] = "BG/Head Office/GRN/35";
                //r1["GRN Date"] = "08/07/2015";
                //r1["PO No"] = "P0123456";
                //r1["Vendor"] = "Q.S.MOTORS";
                //r1["Total Qty"] = "100.000";
                //r1["Total Amount"] = "1000.00";
                //r1["Status"] = "Authorized";
                dt.Rows.Add(r1);
                //            
                grdDataList.DataSource = dt;
                grdDataList.DataBind();
                grdDataList.ShowHeader = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}