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

namespace ProjectManagementTool._content_pages.report_item_master
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
                dt.Columns.Add("Item_Code");
                dt.Columns.Add("Description");
                dt.Columns.Add("Category");
                dt.Columns.Add("Sub_Category");
                dt.Columns.Add("Unit");
                dt.Columns.Add("Last_Rate");
                dt.Columns.Add("Min_stock");
                DataRow r1 = dt.NewRow();
                r1["Item_Code"] = "IT10001";
                r1["Description"] = "CRANE";
                r1["Category"] = "Consumables";
                r1["Sub_Category"] = "Machinary";
               
                r1["Unit"] = "NUMBERS";
                r1["Last_Rate"] = "34500";
                r1["Min_stock"] = "10";
                dt.Rows.Add(r1);
                //
                r1 = dt.NewRow();
                r1["Item_Code"] = "IT10002";
                r1["Description"] = "JCB";
                r1["Category"] = "Consumables";
                r1["Sub_Category"] = "Machinary";

                r1["Unit"] = "NUMBERS";
                r1["Last_Rate"] = "30500";
                r1["Min_stock"] = "30";
                dt.Rows.Add(r1);
                //
                //
                r1 = dt.NewRow();
                r1["Item_Code"] = "IT10003";
                r1["Description"] = "Excavators";
                r1["Category"] = "Consumables";
                r1["Sub_Category"] = "Machinary";

                r1["Unit"] = "NUMBERS";
                r1["Last_Rate"] = "24500";
                r1["Min_stock"] = "20";
                dt.Rows.Add(r1);
                //
                //
                r1 = dt.NewRow();
                r1["Item_Code"] = "IT10021";
                r1["Description"] = "ADMIXTURE";
                r1["Category"] = "Consumables";
                r1["Sub_Category"] = "CIVIL MATERIALS";

                r1["Unit"] = "KG";
                r1["Last_Rate"] = "5";
                r1["Min_stock"] = "20";
                dt.Rows.Add(r1);
                //
                //
                r1 = dt.NewRow();
                r1["Item_Code"] = "IT10031";
                r1["Description"] = "CEMENT OPC";
                r1["Category"] = "Consumables";
                r1["Sub_Category"] = "CIVIL MATERIALS";

                r1["Unit"] = "BAGS";
                r1["Last_Rate"] = "400";
                r1["Min_stock"] = "10";
                dt.Rows.Add(r1);

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