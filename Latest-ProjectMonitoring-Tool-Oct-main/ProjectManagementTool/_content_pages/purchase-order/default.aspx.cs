using ProjectManagementTool.DAL;
using ProjectManager.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ProjectManagementTool._content_pages.purchase_order
{
    public partial class _default : System.Web.UI.Page
    {
        DBGetData getdt = new DBGetData();
        TaskUpdate gettk = new TaskUpdate();
        Invoice invoice = new Invoice();
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
                    SelectedProject();
                   
                   
                    DDlProject_SelectedIndexChanged(sender, e);
                  
                }

            }
        }

        private void SelectedProject()
        {
            if (!IsPostBack)
            {
                if (Session["Project_Workpackage"] != null)
                {
                    string[] selectedValue = Session["Project_Workpackage"].ToString().Split('_');
                    if (selectedValue.Length > 1)
                    {
                        DDlProject.SelectedValue = selectedValue[0];
                    }
                    else
                    {
                        DDlProject.SelectedValue = Session["Project_Workpackage"].ToString();
                    }

                }
            }

        }
        private void BindProject()
        {
            DataSet ds = new DataSet();
            if (Session["TypeOfUser"].ToString() == "U" || Session["TypeOfUser"].ToString() == "MD" || Session["TypeOfUser"].ToString() == "VP" || Session["TypeOfUser"].ToString() == "NJSD")
            {
                ds = gettk.GetAllProjects();
            }
            else if (Session["TypeOfUser"].ToString() == "PA")
            {
                ds = getdt.GetAssignedProjects_by_UserUID(new Guid(Session["UserUID"].ToString()));
            }
            else
            {
                ds = getdt.GetAssignedProjects_by_UserUID(new Guid(Session["UserUID"].ToString()));
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDlProject.DataTextField = "ProjectName";
                DDlProject.DataValueField = "ProjectUID";
                DDlProject.DataSource = ds;
                DDlProject.DataBind();
            }
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
                    InvoiceDiv.Visible = true;
                    TreeView1.Nodes.Clear();
                    PopulateTreeView(ds, null, "", 0);
                    TreeView1.Nodes[0].Selected = true;
                    TreeView1.CollapseAll();
                    TreeView1.Nodes[0].Expand();
                    TreeView1_SelectedNodeChanged(sender, e);
                }
                else
                {
                    TreeView1.Nodes.Clear();
                    InvoiceDiv.Visible = false;

                }
                Session["Project_Workpackage"] = DDlProject.SelectedValue;
            }
            else
            {
                btnaddpo.Visible = false;
            }

        }


        public void PopulateTreeView(DataSet dtParent, TreeNode treeNode, string ParentUID, int Level)
        {
            foreach (DataRow row in dtParent.Tables[0].Rows)
            {
                //string RABillNumber = "";

                TreeNode child = new TreeNode
                {
                    Text = Level == 0 ? LimitCharts(row["Name"].ToString()) : Level == 1 ? LimitCharts(row["VendorUniqueNo"].ToString()) : Level == 2 ? LimitCharts(row["PO_Number"].ToString()) : Level == 3 ? LimitCharts(row["GenericName"].ToString()) : "",
                    Value = Level == 0 ? row["WorkPackageUID"].ToString() : Level == 1 ? row["VendorID"].ToString() : Level == 2 ? row["PO_OrderID"].ToString() : row["PO_ItemID"].ToString(),
                    Target = Level == 0 ? "WorkPackage" : Level == 1 ? "CompanyName" : Level == 2 ? "PO_Number" : Level == 3 ? "GenericName" : "",
                    ToolTip = Level == 0 ? LimitCharts(row["Name"].ToString()) : Level == 1 ? LimitCharts(row["CompanyName"].ToString()) : Level == 2 ? LimitCharts(row["PO_Number"].ToString()) : Level == 3 ? LimitCharts(row["GenericName"].ToString()):""
                };

                if (ParentUID == "")
                {
                    TreeView1.Nodes.Add(child);
                    //DataSet dschild = invoice.GetInvoiceMaster_by_WorkpackageUID(new Guid(child.Value));
                    DataSet dschild = invoice.GetVendorDetails_by_WorkpackageUID(new Guid(child.Value));
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        PopulateTreeView(dschild, child, child.Value, 1);
                    }

                }
                else if (Level == 1)
                {
                    treeNode.ChildNodes.Add(child);
                    //DataSet dschild = invoice.GetInvoiceMaster_by_WorkpackageUID(new Guid(child.Value));
                    DataSet dschild = invoice.GetPoOrderList_by_VendorID(new Guid(child.Value));

                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        PopulateTreeView(dschild, child, child.Value, 2);
                    }
                }
                else if (Level == 2)
                {
                    treeNode.ChildNodes.Add(child);
                    DataSet dssubchild = invoice.GetPoItemList_by_PO_OrderID(new Guid(child.Value));
                    if (dssubchild.Tables[0].Rows.Count > 0)
                    {
                        PopulateTreeView(dssubchild, child, child.Value, 3);
                    }
                }
               
                else
                {
                    treeNode.ChildNodes.Add(child);

                }
            }

        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindAllInvoiceTotal(string WorkpackageUID)
        {
            DataSet ds = invoice.GetAllInvoiceTotalAmount_by_WorkpackageUID(new Guid(WorkpackageUID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                //LblAllInvoiceTotal.Text = ds.Tables[0].Rows[0]["Currency"].ToString() + " " + Convert.ToDouble(ds.Tables[0].Rows[0]["TotalAmount"].ToString()).ToString("#,##.00", CultureInfo.CreateSpecificCulture(ds.Tables[0].Rows[0]["Currency_CultureInfo"].ToString()));
                //
                //LblAllInvoiceTotalVendor.Text = ds.Tables[0].Rows[0]["Currency"].ToString() + " " + Convert.ToDouble(ds.Tables[0].Rows[0]["TotalAmount"].ToString()).ToString("#,##.00", CultureInfo.CreateSpecificCulture(ds.Tables[0].Rows[0]["Currency_CultureInfo"].ToString()));
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TotalDeductionAmount"].ToString()))
                {
                    //LblAllInvoiceDeductionTotal.Text = ds.Tables[0].Rows[0]["Currency"].ToString() + " " + Convert.ToDouble(ds.Tables[0].Rows[0]["TotalDeductionAmount"].ToString()).ToString("#,##.00", CultureInfo.CreateSpecificCulture(ds.Tables[0].Rows[0]["Currency_CultureInfo"].ToString()));
                    //
                    //LblAllInvoiceDeductionTotalVendor.Text = ds.Tables[0].Rows[0]["Currency"].ToString() + " " + Convert.ToDouble(ds.Tables[0].Rows[0]["TotalDeductionAmount"].ToString()).ToString("#,##.00", CultureInfo.CreateSpecificCulture(ds.Tables[0].Rows[0]["Currency_CultureInfo"].ToString()));
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TotalNetAmount"].ToString()))
                {
                    //LblAllInvoiceNetTotal.Text = ds.Tables[0].Rows[0]["Currency"].ToString() + " " + Convert.ToDouble(ds.Tables[0].Rows[0]["TotalNetAmount"].ToString()).ToString("#,##.00", CultureInfo.CreateSpecificCulture(ds.Tables[0].Rows[0]["Currency_CultureInfo"].ToString()));
                    //
                    //LblAllInvoiceNetTotalVednor.Text = ds.Tables[0].Rows[0]["Currency"].ToString() + " " + Convert.ToDouble(ds.Tables[0].Rows[0]["TotalNetAmount"].ToString()).ToString("#,##.00", CultureInfo.CreateSpecificCulture(ds.Tables[0].Rows[0]["Currency_CultureInfo"].ToString()));
                }
            }
            else
            {
                //LblAllInvoiceTotal.Text = "-";
                //LblAllInvoiceDeductionTotal.Text = "-";
                //LblAllInvoiceNetTotal.Text = "-";
            }
        }

        public string LimitCharts(string Desc)
        {
            if (Desc.Length > 100)
            {
                return Desc.Substring(0, 100) + "  . . .";
            }
            else
            {
                return Desc;
            }
        }
        private void BindData()
        {
            if (TreeView1.SelectedNode.Target == "WorkPackage")
            {
                VendorDiv.Visible = true;
                InvoiceDiv.Visible = false;
                PoTaxes.Visible = false;

                PoItemsDiv.Visible = false;
                PoDetails.Visible = false;
                DataSet ds = invoice.GetVendorDetails_by_WorkpackageUID(new Guid(TreeView1.SelectedNode.Value));
                if (ds.Tables[1].Rows.Count > 0)
                {
                    LblGrossWorkPackage.Text = ds.Tables[1].Rows[0]["TotalGrossAmount"].ToString();
                    LblTaxWorkPackage.Text = ds.Tables[1].Rows[0]["TotalTaxAmount"].ToString();
                    LblNetWorkPackage.Text = ds.Tables[1].Rows[0]["TotalNetAmount"].ToString();
                }

                GrdVendors.DataSource = ds;
                GrdVendors.DataBind();
                ActivityHeadingVendor.Text = "WorkPackage Details";// for " + TreeView1.SelectedNode.Text;
                BindAllInvoiceTotal(TreeView1.SelectedNode.Value);
                btnRABillPrint.Visible = false;

            }
            else if (TreeView1.SelectedNode.Target == "CompanyName")
            {
                InvoiceDiv.Visible = true;
                PoTaxes.Visible = false;
                VendorDiv.Visible = false;
                PoItemsDiv.Visible = false;
                PoDetails.Visible = false;
                DataSet dsvendor = invoice.GetVendorDetails_by_VendorUID(new Guid(TreeView1.SelectedNode.Value));
                lblVendorID.Text = dsvendor.Tables[0].Rows[0]["VendorUniqueNo"].ToString();
                lblVednorName.Text = dsvendor.Tables[0].Rows[0]["CompanyName"].ToString();
                DateTime vendorRegDate = Convert.ToDateTime(dsvendor.Tables[0].Rows[0]["RegistrationDate"]);
                string formattedDate = vendorRegDate.ToString("dd/MM/yyyy");
                lblVednorRegdate.Text = formattedDate;
                lblVednorAddr.Text = dsvendor.Tables[0].Rows[0]["Address1"].ToString();
                if (dsvendor.Tables[1].Rows.Count > 0)
                {
                    LblVendorGrossAmount.Text = dsvendor.Tables[1].Rows[0]["GrossAmount"].ToString();
                    LblVendorTaxAmount.Text = dsvendor.Tables[1].Rows[0]["TaxAmount"].ToString();
                    LblVendorNetAmount.Text = dsvendor.Tables[1].Rows[0]["NetAmount"].ToString();
                }

                //DataSet ds = invoice.GetInvoiceMaster_by_WorkpackageUID(new Guid(TreeView1.SelectedNode.Value));
                DataSet ds = invoice.GetPoOrderList_by_VendorID(new Guid(TreeView1.SelectedNode.Value));

                GrdInvoice.DataSource = ds;
                GrdInvoice.DataBind();
                var addPoLink = (HtmlAnchor)InvoiceDiv.FindControl("AddPO");
                if (addPoLink != null)
                {
                    addPoLink.HRef = "/_modal_pages/add-vendorpo.aspx?PrjUID=" + DDlProject.SelectedValue + "&VendorID=" + TreeView1.SelectedNode.Value ;
                }
                ActivityHeading.Text = "Vendor Details"; //for " + TreeView1.SelectedNode.Text;
                BindAllInvoiceTotal(TreeView1.SelectedNode.Value);
                btnRABillPrint.Visible = false;

               
            }
            else if (TreeView1.SelectedNode.Target == "PO_Number")
            {

                PoTaxes.Visible = false;
                PoItemsDiv.Visible = true;
                InvoiceDiv.Visible = false;
                PoDetails.Visible = true;
                VendorDiv.Visible = false;
                DataSet ds = invoice.GetPoItemList_by_PO_OrderID(new Guid(TreeView1.SelectedNode.Value));
                LblPONumber.Text = ds.Tables[1].Rows[0]["PO_Number"].ToString();
                DateTime po_Date = Convert.ToDateTime(ds.Tables[1].Rows[0]["PO_Date"]);
                string formattedDate = po_Date.ToString("dd/MM/yyyy");
                LblPODate.Text = formattedDate;
                LblPOTotalAmount.Text = ds.Tables[1].Rows[0]["PO_TotalAmount"].ToString();
                LblPOTaxAmount.Text = ds.Tables[1].Rows[0]["PO_TotalTaxes"].ToString();
                LblPONetAmount.Text = ds.Tables[1].Rows[0]["PO_NetAmount"].ToString();
                GrdRABillItems.DataSource = ds.Tables[0];
                GrdRABillItems.DataBind();
                var addPoItemLink = (HtmlAnchor)PoItemsDiv.FindControl("AddPoItem");
                if (addPoItemLink != null)
                {
                    string vendorUID = TreeView1.SelectedNode.Parent?.Value ?? "UnknownValue"; // Fetch parent node value (CompanyName)
                    addPoItemLink.HRef = "/_modal_pages/add-po-item.aspx?PrjUID=" + DDlProject.SelectedValue + "&PO_OrderID=" + TreeView1.SelectedNode.Value + "&VendorID=" + vendorUID;
                }
                ActivityHeading.Text = "PO-Items List for " + TreeView1.SelectedNode.Text;
                //for Taxes
                var addPoTaxesLink = (HtmlAnchor)PoTaxes.FindControl("AddInvoiceDeductions");
                if (addPoTaxesLink != null)
                {
                    addPoTaxesLink.HRef = "/_modal_pages/add-po-taxes.aspx?PrjUID=" + DDlProject.SelectedValue + "&PO_OrderID=" + TreeView1.SelectedNode.Value;
                }
                //BindInvoiceMaster(TreeView1.SelectedNode.Value);
                //BindRAbills(TreeView1.SelectedNode.Value);

                btnRABillPrint.Visible = false;
            }
            //else if (TreeView1.SelectedNode.Target == "GenericName")
            //{

            //    Taxes.Visible = true;
            //    PoItemsDiv.Visible = true;
            //    InvoiceDiv.Visible = false;
            //    PoDetails.Visible = true;
            //    VendorDiv.Visible = false;
            //    DataSet ds = invoice.GetPoItemList_by_PO_OrderID(new Guid(TreeView1.SelectedNode.Value));
            //    GrdRABillItems.DataSource = ds;
            //    GrdRABillItems.DataBind();
            //    //var addPoItemLink = (HtmlAnchor)InvoiceDiv.FindControl("AddPoItem");
            //    //if (addPoItemLink != null)
            //    //{
            //    //    addPoItemLink.HRef = "/_modal_pages/add-po-item.aspx?PrjUID=" + DDlProject.SelectedValue + "&PO_OrderID=" + TreeView1.SelectedNode.Value;
            //    //}
            //    //ActivityHeading.Text = "PO-Items List for " + TreeView1.SelectedNode.Text;
            //    //
            //    BindInvoiceMaster(TreeView1.SelectedNode.Value);
            //    BindRAbills(TreeView1.SelectedNode.Value);
            //    //
            //    btnRABillPrint.Visible = false;
            //}
            else
            {
                PoTaxes.Visible = false;

                PoItemsDiv.Visible = true;
                InvoiceDiv.Visible = false;
                PoDetails.Visible = false;
                PoItemsDiv.Visible = false;

                DataTable dts = new DataTable();
                

                DataSet ds = invoice.GetInvoiceRAbills_by_InvoiceRABill_UID(new Guid(TreeView1.SelectedNode.Value));
                GrdRABillItems.DataSource = ds;
                GrdRABillItems.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnRABillPrint.Visible = true;
                }
            }
        }

        private void BindInvoiceMaster(string InvoiceMaster_UID)
        {
            DataSet ds = invoice.GetInvoiceMaster_by_InvoiceMaster_UID(new Guid(InvoiceMaster_UID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                LblPONumber.Text = ds.Tables[0].Rows[0]["Invoice_Number"].ToString();
                if (ds.Tables[0].Rows[0]["Invoice_Date"].ToString() != "")
                {
                    LblPODate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Invoice_Date"].ToString()).ToString("dd MMM yyyy");
                }
               

               
                LblPOTotalAmount.Text = ds.Tables[0].Rows[0]["Currency"].ToString() + " " + Convert.ToDouble(ds.Tables[0].Rows[0]["Invoice_TotalAmount"].ToString()).ToString("#,##.00", CultureInfo.CreateSpecificCulture(ds.Tables[0].Rows[0]["Currency_CultureInfo"].ToString()));
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Invoice_DeductionAmount"].ToString()))
                {
                    LblPOTaxAmount.Text = ds.Tables[0].Rows[0]["Currency"].ToString() + " " + Convert.ToDouble(ds.Tables[0].Rows[0]["Invoice_DeductionAmount"].ToString()).ToString("#,##.00", CultureInfo.CreateSpecificCulture(ds.Tables[0].Rows[0]["Currency_CultureInfo"].ToString()));
                }
               
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Invoice_DeductionAmount"].ToString()) && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Invoice_AdditionAmount"].ToString()))
                {
                    LblPONetAmount.Text = ds.Tables[0].Rows[0]["Currency"].ToString() + " " + (Convert.ToDouble(ds.Tables[0].Rows[0]["Invoice_TotalAmount"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[0]["Invoice_AdditionAmount"].ToString()) - Convert.ToDouble(ds.Tables[0].Rows[0]["Invoice_DeductionAmount"].ToString())).ToString("#,##.00", CultureInfo.CreateSpecificCulture(ds.Tables[0].Rows[0]["Currency_CultureInfo"].ToString()));
                }
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void BindRAbills(string InvoiceMaster_UID)
        {
            DataSet ds = invoice.GetInvoiceRABills_by_InvoiceMaster_UID(new Guid(InvoiceMaster_UID));
            GrdRABillItems.DataSource = ds;
            GrdRABillItems.DataBind();
        }

        public string GetDeductionMaster(string UID)
        {
            return getdt.GetDeductionMasterName_by_UID(new Guid(UID));
        }

        //protected void GrdRABillItems_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Cells[2].Text = "Numbers";
        //        e.Row.Cells[3].Text = "10000";
        //        e.Row.Cells[4].Text = "1000";
        //    }

        //}

        //protected void GrdVendors_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Cells[1].Text = "V-1001";
        //        e.Row.Cells[2].Text = "Vendor-1 ";
               
        //    }
        //}
    }
}