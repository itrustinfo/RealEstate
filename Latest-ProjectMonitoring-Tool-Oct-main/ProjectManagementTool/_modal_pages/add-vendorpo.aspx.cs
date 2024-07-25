using ProjectManagementTool.DAL;
using ProjectManager.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectManagementTool._modal_pages
{
    public partial class add_vendorpo : System.Web.UI.Page
    {
        Invoice invoice = new Invoice();
        DBGetData getdata = new DBGetData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>parent.location.href=parent.location.href;</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["VendorID"] != null)
                    {
                        //DataSet ds = new DataSet();
                        //ds = getdata.GetWorkPackages_By_ProjectUID(new Guid(Request.QueryString["PrjUID"]));

                    }
                }
            }
        }

       
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string sDate1 = "";
                DateTime CDate1 = DateTime.Now;

                if (dtInvoiceDate.Text != "")
                {
                    sDate1 = dtInvoiceDate.Text;
                }
                else
                {
                    sDate1 = DateTime.Now.ToString("dd/MM/yyyy");
                }
                
                sDate1 = getdata.ConvertDateFormat(sDate1);
                CDate1 = Convert.ToDateTime(sDate1);
                DataSet ds = new DataSet();
                ds = getdata.GetWorkPackages_By_ProjectUID(new Guid(Request.QueryString["PrjUID"]));

                Guid PO_OrderID = Guid.NewGuid();
                Guid vendorId = new Guid(Request.QueryString["VendorID"]);
                Guid workPckUID = new Guid(ds.Tables[0].Rows[0]["WorkPackageUID"].ToString());
                string po_Num = txtponumber.Text.ToString();
                int cnt = invoice.PO_Order_Details_InsertorUpdate(PO_OrderID, vendorId, workPckUID, po_Num, CDate1);
                if (cnt > 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>parent.location.href=parent.location.href;</script>");
                }
                else if (cnt == -1)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('PO Number alreday exists. Try with different PO Number');</script>");
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('Error Code : AIM-01 there is a problem with these feature. please contact system admin.');</script>");
            }
        }
    }
}