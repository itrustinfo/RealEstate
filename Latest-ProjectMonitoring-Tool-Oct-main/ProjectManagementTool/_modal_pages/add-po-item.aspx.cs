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
    public partial class add_po_item : System.Web.UI.Page
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
                    if (Request.QueryString["PO_OrderID"] != null && Request.QueryString["VendorID"] != null)
                    {
                        //Load fill in Items DDL
                        BindItemDropDownList(new Guid (Request.QueryString["VendorID"]));
                    }
                }
            }
        }

       
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet ds = new DataSet();
                ds = getdata.GetWorkPackages_By_ProjectUID(new Guid(Request.QueryString["PrjUID"]));

                Guid PO_ItemID = Guid.NewGuid();
                Guid PO_OrderID = new Guid(Request.QueryString["PO_OrderID"]);
                Guid workPckUID = new Guid(ds.Tables[0].Rows[0]["WorkPackageUID"].ToString());
                int PO_Item_Quantity = int.Parse(txtQty.Text);
                Guid ItemID = new Guid(DDLtItem.SelectedValue.ToString());
                if(DDLtItem.SelectedValue == "0")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('Please Select a Proper Item');</script>");

                }
                int cnt = invoice.PO_Item_Quantity_InsertorUpdate(PO_ItemID, PO_OrderID, ItemID,PO_Item_Quantity);
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
        private void BindItemDropDownList(Guid VendorID)
        {
            // Sample data fetching method - replace with your actual data fetching logic
            DataSet dt = invoice.getAllItems(VendorID);

            // Clear existing items
            DDLtItem.Items.Clear();


            // Bind data to the DropDownList
            DDLtItem.DataSource = dt;
            DDLtItem.DataTextField = "GenericName"; // Replace with your actual data text field
            DDLtItem.DataValueField = "ItemID";  // Replace with your actual data value field
            DDLtItem.DataBind();

            // Insert the default item
            DDLtItem.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }

        protected void DDLtItem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}