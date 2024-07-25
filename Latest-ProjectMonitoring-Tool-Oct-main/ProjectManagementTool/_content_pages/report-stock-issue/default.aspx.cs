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

namespace ProjectManagementTool._content_pages.report_stock_issue
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
                dt.Columns.Add("IssueNo");
                dt.Columns.Add("IssueDate");
                dt.Columns.Add("Equipment_Vehicle");
                dt.Columns.Add("IRRNO");
                dt.Columns.Add("IRRDate");
                dt.Columns.Add("ItemDescription");
                dt.Columns.Add("IssuedQty");
                dt.Columns.Add("Rate");
                dt.Columns.Add("Amount");
                dt.Columns.Add("Remarks");
                dt.Columns.Add("CreatedBy");

                DataRow r1 = dt.NewRow();
                r1["IssueNo"] = "BG/HO/SIE/3";
                r1["IssueDate"] = "12/08/2015";
                r1["Equipment_Vehicle"] = "TT/MP 20DA 1276";
                r1["IRRNO"] = "Irr01";               
                r1["IRRDate"] = "08/07/2015";
                r1["ItemDescription"] = "80100000-ADMIXTURE";
                r1["IssuedQty"] = "20.00";
                r1["Rate"] = "10.00";
                r1["Amount"] = "200.0000";
                r1["Remarks"] = "Not Dispatched";
                r1["CreatedBy"] = "Billing";
                dt.Rows.Add(r1);
                //
                r1 = dt.NewRow();
                r1["IssueNo"] = "BG/HO/Head Office/SIE/2";
                r1["IssueDate"] = "08/07/2015";
                r1["Equipment_Vehicle"] = "Bike/MP 20MU 3764";
                r1["IRRNO"] = "Irr02";
                r1["IRRDate"] = "08/07/2015";
                r1["ItemDescription"] = "80100000-ADMIXTURE";
                r1["IssuedQty"] = "15.00";
                r1["Rate"] = "10.00";
                r1["Amount"] = "150.0000";
                r1["Remarks"] = "Dispatched";
                r1["CreatedBy"] = "Billing";
                dt.Rows.Add(r1);
                //
                
                r1 = dt.NewRow();
                r1["IssueNo"] = "BG/HO/Head Office/SIE/2";
                r1["IssueDate"] = "08/07/2015";
                r1["Equipment_Vehicle"] = "Bike/MP 20MU 3764";
                r1["IRRNO"] = "Irr03";
                r1["IRRDate"] = "08/07/2015";
                r1["ItemDescription"] = "CUT-STONES";
                r1["IssuedQty"] = "20.00";
                r1["Rate"] = "1.00";
                r1["Amount"] = "20.0000";
                r1["Remarks"] = "Dispatched";
                r1["CreatedBy"] = "Billing";
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