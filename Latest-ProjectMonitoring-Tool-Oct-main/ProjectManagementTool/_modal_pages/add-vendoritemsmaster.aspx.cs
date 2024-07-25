using ProjectManager.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectManagementTool._modal_pages
{
    public partial class add_vendoritemsmaster : System.Web.UI.Page
    {
        MeasurementUpdate getdt = new MeasurementUpdate();
        DBGetData getdata = new DBGetData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack || ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                if (Session["Username"] == null)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>parent.location.href=parent.location.href;</script>");
                }

                // Call your function to retrieve data for binding the DropDownList(Item Category , Vendors and Unit )
                BindItemCategory();
                // Add the first entry as "<--select-->"
                DDLItemCategory.Items.Insert(0, new ListItem("<--select-->", "0"));
                BindVendorData();
                // Add the first entry as "<--select-->"
                DDLVendorID.Items.Insert(0, new ListItem("<--select-->", "0"));

                //for displaying selected Parent Item
                if (Session["ParentUID"] != null)
                {
                    LinkParentItem.Visible = false;
                    btnchoose.Visible = false;
                    lblParentItemName.Visible = true;
                    lblParentItemName.Text = Session["ParentDataText"].ToString();
                }
                    BindUnit();

                    // Retrieve query parameters for Edit/update
                    if (Request.QueryString["ItemID"] != null)
                {
                    Guid itemID = new Guid(Request.QueryString["ItemID"]);
                    Guid itemCategoryUID =new Guid(Request.QueryString["ItemCategoryUID"]);
                    string uniqueItemCode = Request.QueryString["UniqueItemCode"];
                    string vendorID = Request.QueryString["VendorID"];
                    string genericName = Request.QueryString["GenericName"];
                    string description = Request.QueryString["Description"];
                    string unit = Request.QueryString["Unit"];
                    string minQuantity = Request.QueryString["MinQuantity"];
                    string rate = Request.QueryString["Rate"];
                    string itemLevel = Request.QueryString["ItemLevel"];
                    string vendor_specific_Name = Request.QueryString["Vendor_specific_Name"];
                    string parent_Item_code = Request.QueryString["Parent_Item_code"];
                    string validFrom = Request.QueryString["ValidFrom"];
                    string validUntil = Request.QueryString["ValidUntil"];
                    string cgst = Request.QueryString["CGST"];
                    string sgst = Request.QueryString["SGST"];
                    string gst = Request.QueryString["GST"];

                    DateTime registrationDate;
                    string validFromDate = " ";
                    string validUntilDate = " ";

                    if (DateTime.TryParse(validFrom, out registrationDate))
                    {
                        // Format the DateTime object to display only the date portion
                         validFromDate = registrationDate.ToString("dd/MM/yyyy");
                    }
                    if (DateTime.TryParse(validUntil, out registrationDate))
                    {
                        // Format the DateTime object to display only the date portion
                         validUntilDate = registrationDate.ToString("dd/MM/yyyy");
                    }
                    // Set textbox values
                    DDLItemCategory.SelectedValue = itemCategoryUID.ToString();
                    txtitemcode.Text = uniqueItemCode;
                    txtgenricName.Text = genericName;
                    txtDescription.Text = description;
                    DDLVendorID.SelectedValue = vendorID;
                    DDLUnit.SelectedItem.Text = unit;
                    txtMinQuantity.Text = decimal.Parse(minQuantity).ToString("0.00");
                    txtRate.Text = decimal.Parse(rate).ToString("0.00");
                    txtValidFrom.Text = validFromDate;
                    txtValidUntil.Text = validUntilDate;
                    txtVSPname.Text = vendor_specific_Name;
                    txtCGST.Text = decimal.Parse(cgst).ToString("0.00");
                    txtSGST.Text = decimal.Parse(sgst).ToString("0.00");
                    txtGST.Text = decimal.Parse(gst).ToString("0.00");

                }
            }
        }

       
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (DDLVendorID.SelectedValue != "0" && DDLItemCategory.SelectedValue !="0")
                {
                    Guid itemID;

                    if (Request.QueryString["ItemID"] != null)
                    {
                        itemID = new Guid(Request.QueryString["ItemID"]);

                    }
                    else
                    {
                        itemID = Guid.NewGuid();
                    }
                    string sDate1 = "";
                    DateTime CDate1 = DateTime.Now;
                    if (txtValidFrom.Text != "")
                    {
                        sDate1 = txtValidFrom.Text;
                        sDate1 = getdt.ConvertDateFormat(sDate1);
                        CDate1 = Convert.ToDateTime(sDate1);
                    }
                    string sDate2 = "";
                    DateTime CDate2 = DateTime.Now;
                    if (txtValidUntil.Text != "")
                    {
                        sDate2 = txtValidUntil.Text;
                        sDate2 = getdt.ConvertDateFormat(sDate2);
                        CDate2 = Convert.ToDateTime(sDate2);
                    }
                    decimal minQuantity;
                    if (decimal.TryParse(txtMinQuantity.Text, out minQuantity)) { }
                    decimal rate;
                    if (decimal.TryParse(txtRate.Text, out rate)) { }
                    decimal cgst;
                    if (decimal.TryParse(txtCGST.Text, out cgst)) { }
                    decimal sgst;
                    if (decimal.TryParse(txtSGST.Text, out sgst)) { }
                    decimal gst;
                    if (decimal.TryParse(txtGST.Text, out gst)) { }
                    //check uniqueness of UniqueItemCode
                    DataSet ds = getdt.getVendorItemList();
                    string userEnteredValue = txtitemcode.Text.ToString();

                    // If a matching value is found   while edit skip this foreach loop and update table

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        // Get the value of "UniqueItemCode" column for the current row
                        string itemUniqueNo = row["UniqueItemCode"].ToString();

                       string requestedUniqueItemCode = Request.QueryString["UniqueItemCode"]?.ToString();
                        if (!string.IsNullOrEmpty(requestedUniqueItemCode) && requestedUniqueItemCode == itemUniqueNo)
                        {
                            if (requestedUniqueItemCode == txtitemcode.Text.ToString()){
                                break;
                            }
                        }

                        // Compare the user entered value with the value from the DataSet
                        else if (itemUniqueNo.Equals(userEnteredValue, StringComparison.OrdinalIgnoreCase))
                        {

                            // If a matching value is found, return with an error message
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Alert: Unique ItemCode already exists!');", true);
                            return; // Exit the method or return appropriate response

                        }
                    }
                    int itemLevel = -1;
                    Guid parent_item_code = Guid.Empty;
                    if (Session["ParentUID"] != null)
                    {
                        DataSet dsParent = getdt.getParentItemDetails(new Guid(Session["ParentUID"].ToString()));
                        if (dsParent.Tables.Count > 0 && dsParent.Tables[0].Rows.Count > 0)
                        {
                            object itemLevelObj = dsParent.Tables[0].Rows[0]["ItemLevel"];
                            object parentItemCodeObj = dsParent.Tables[0].Rows[0]["ItemID"];
                            if (itemLevelObj != DBNull.Value)
                            {
                                // Convert the value to integer
                                itemLevel = Convert.ToInt32(itemLevelObj)+1;
                            }
                            if (parentItemCodeObj != DBNull.Value)
                            {
                                // Convert the value to GUID
                                parent_item_code = (Guid)parentItemCodeObj;
                            }
                        }
                    }
                    else
                    {
                        itemLevel = 1;
                        
                    }

                    int count = getdt.insertORUpdateVendorsItems(itemID, txtitemcode.Text.ToString(), new Guid(DDLVendorID.SelectedValue.ToString()), txtgenricName.Text.ToString(), txtDescription.Text.ToString(),
                            DDLUnit.SelectedItem.Text.ToString(), minQuantity, rate, CDate1, CDate2, itemLevel, txtVSPname.Text.ToString(), parent_item_code, new Guid(DDLItemCategory.SelectedValue.ToString()), cgst,sgst,gst);

                    if (count > 0)
                    {
                        Session["ParentUID"] = null;
                        Session["ParentDataText"] = null;
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>parent.location.href=parent.location.href;</script>");

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('Error Occured. Please contact system admin.);</script>");

                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Alert: Please Select a Valid ItemCategory And/OR Vendor Number');", true);

                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('Error Occured. Please contact system admin.);</script>");

            }


        }

        private void BindVendorData()
        {
            // Assuming you have a method to retrieve vendor data, replace this with your actual implementation
            DDLVendorID.DataSource = getdt.getVendorsList();

            // Bind the vendor data to the DropDownList

            DDLVendorID.DataTextField = "VendorUniqueNo"; 
            DDLVendorID.DataValueField = "VendorID"; 
            DDLVendorID.DataBind();
        }

        private void BindUnit()
        {
            DDLUnit.DataTextField = "Unit_Name";
            DDLUnit.DataValueField = "Unit_UID";
            DDLUnit.DataSource = getdata.getUnitMaster_List();
            DDLUnit.DataBind();
        }

        protected void LnkChangeItem_Click(object sender, EventArgs e)
        {
            Session["ParentData"]= null;
            LinkParentItem.Visible = true;
            btnchoose.Visible = true;
            lblParentItemName.Visible = false;
        }

        private void BindItemCategory()
        {
            DDLItemCategory.DataTextField = "CategoryName";
            DDLItemCategory.DataValueField = "ItemCategoryUID";
            DDLItemCategory.DataSource = getdt.getItemCategory_List();
            DDLItemCategory.DataBind();
        }

    }


}