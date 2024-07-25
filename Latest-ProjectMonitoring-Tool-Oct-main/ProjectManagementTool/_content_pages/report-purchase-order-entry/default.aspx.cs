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

namespace ProjectManagementTool._content_pages.report_purchase_order_entry
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
                dt.Columns.Add("Created In");
                dt.Columns.Add("Site");
                dt.Columns.Add("PO No.");
                dt.Columns.Add("PO Date");
                dt.Columns.Add("Vendor");
                dt.Columns.Add("Total Qty");
                dt.Columns.Add("Total");
                dt.Columns.Add("Excise");
                dt.Columns.Add("Local Tax");
                dt.Columns.Add("CST");
                dt.Columns.Add("Packing & Forwarding Charges");
                dt.Columns.Add("Total Amount");
                dt.Columns.Add("Advance Amount");
                dt.Columns.Add("Mode of Payment");
                dt.Columns.Add("Status");

                DataRow r1 = dt.NewRow();
                r1["Created In"] = "Bangalore";
                r1["Site"] = "Ramenahalli";
                r1["PO No."] = "MG/Phase-II/PO/48";
                r1["PO Date"] = "29/10/2015";               
                r1["Vendor"] = "O.M.ELECTRICALS & MACHINERY STORE";
                r1["Total Qty"] = "2.0";
                r1["Total"] = "110.00";
                r1["Excise"] = "0.00";
                r1["Local Tax"] = "0.00";
                r1["CST"] = "0.00";
                r1["Packing & Forwarding Charges"] = "0.00";
                r1["Total Amount"] = "110.00";
                r1["Advance Amount"] = "0.00";
                r1["Mode of Payment"] = "";
                r1["Status"] = "Authorized";

                dt.Rows.Add(r1);
                //
                r1 = dt.NewRow();
                r1["Created In"] = "Bangalore";
                r1["Site"] = "Ramenahalli";
                r1["PO No."] = "MG/Phase-II/PO/47";
                r1["PO Date"] = "29/10/2015";
                r1["Vendor"] = "O.M.ELECTRICALS & MACHINERY STORE";
                r1["Total Qty"] = "64.00";
                r1["Total"] = "9560.06";
                r1["Excise"] = "191.22";
                r1["Local Tax"] = "292.57";
                r1["CST"] = "97.52";
                r1["Packing & Forwarding Charges"] = "0.00";
                r1["Total Amount"] = "10142.37";
                r1["Advance Amount"] = "101420.00";
                r1["Mode of Payment"] = "Cash";
                r1["Status"] = "Authorized";
                dt.Rows.Add(r1);
                //
                //
                r1 = dt.NewRow();
                r1["Created In"] = "Bangalore";
                r1["Site"] = "Ramenahalli";
                r1["PO No."] = "MG/Phase-II/PO/46";
                r1["PO Date"] = "13/10/2015";
                r1["Vendor"] = "Bangalore GLASS CENTRE";
                r1["Total Qty"] = "4.00";
                r1["Total"] = "8000.00";
                r1["Excise"] = "0.00";
                r1["Local Tax"] = "1120.00";
                r1["CST"] = "160.0";
                r1["Packing & Forwarding Charges"] = "0.00";
                r1["Total Amount"] = "9280.00";
                r1["Advance Amount"] = "0.00";
                r1["Mode of Payment"] = "Cash";
                r1["Status"] = "Authorized";
                dt.Rows.Add(r1);
                //
                //
                r1 = dt.NewRow();
                r1["Created In"] = "Bangalore";
                r1["Site"] = "Ramenahalli";
                r1["PO No."] = "MG/Pahse-II/PO/45";
                r1["PO Date"] = "10/10/2015";
                r1["Vendor"] = "Q.S.MOTORS";
                r1["Total Qty"] = "15.00";
                r1["Total"] = "3750.00";
                r1["Excise"] = "0.00";
                r1["Local Tax"] = "450.00";
                r1["CST"] = "75.00";
                r1["Packing & Forwarding Charges"] = "0.00";
                r1["Total Amount"] = "4275.00";
                r1["Advance Amount"] = "0.00";
                r1["Mode of Payment"] = "";
                r1["Status"] = "Authorized";
                dt.Rows.Add(r1);
                //
                //
                r1 = dt.NewRow();
                r1["Created In"] = "Bangalore";
                r1["Site"] = "Ramenahalli";
                r1["PO No."] = "MG/Phase-II/PO/44";
                r1["PO Date"] = "10/10/201";
                r1["Vendor"] = "S.K TRADING";
                r1["Total Qty"] = "5.00";
                r1["Total"] = "1250.00";
                r1["Excise"] = "0.00";
                r1["Local Tax"] = "0.00";
                r1["CST"] = "0.00";
                r1["Packing & Forwarding Charges"] = "0.00";
                r1["Total Amount"] = "1250.00";
                r1["Advance Amount"] = "0.00";
                r1["Mode of Payment"] = "Cheque";
                r1["Status"] = "Authorized";
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