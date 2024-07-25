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

namespace ProjectManagementTool._content_pages.report_stock
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
                dt.Columns.Add("IRR_No");
                dt.Columns.Add("IRR_Date");
                dt.Columns.Add("ItemDescription");
                dt.Columns.Add("UOM");
                dt.Columns.Add("ValuationRate");
                dt.Columns.Add("Inward_Qty_As_On");
                dt.Columns.Add("Issued_Qty_As_On");
                dt.Columns.Add("Transferred_As_On_Qty");
                dt.Columns.Add("Closing_Stock_As_On");
                dt.Columns.Add("Amount");

                DataRow r1 = dt.NewRow();
                r1["IRR_NO"] = "1212";
                r1["IRR_Date"] = "12/08/2015";
                r1["ItemDescription"] = "80100002-BRICKS";
                r1["UOM"] = "NOS";               
                r1["ValuationRate"] = "4.00";
                r1["Inward_Qty_As_On"] = "20.00";
                r1["Issued_Qty_As_On"] = "0.00";
                r1["Transferred_As_On_Qty"] = "2.00";
                r1["Closing_Stock_As_On"] = "18.00";
                r1["Amount"] = "72.00";
                dt.Rows.Add(r1);
                //
                r1 = dt.NewRow();
                r1["IRR_NO"] = "Irr01";
                r1["IRR_Date"] = "08/07/2015";
                r1["ItemDescription"] = "80100000-ADMIXTURE";
                r1["UOM"] = "KG";
                r1["ValuationRate"] = "10.00";
                r1["Inward_Qty_As_On"] = "100.00";
                r1["Issued_Qty_As_On"] = "35.00";
                r1["Transferred_As_On_Qty"] = "0.00";
                r1["Closing_Stock_As_On"] = "65.00";
                r1["Amount"] = "650.00";
                dt.Rows.Add(r1);
                //
                
                r1 = dt.NewRow();
                r1["IRR_NO"] = "Irr2";
                r1["IRR_Date"] = "08/07/2015";
                r1["ItemDescription"] = "80100001-CUT STONES";
                r1["UOM"] = "MT";
                r1["ValuationRate"] = "1.00";
                r1["Inward_Qty_As_On"] = "100.00";
                r1["Issued_Qty_As_On"] = "20.00";
                r1["Transferred_As_On_Qty"] = "0.00";
                r1["Closing_Stock_As_On"] = "80.0000";
                r1["Amount"] = "80.00";
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