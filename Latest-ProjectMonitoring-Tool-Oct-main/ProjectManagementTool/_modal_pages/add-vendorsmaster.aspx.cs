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
    public partial class add_vendorsmaster : System.Web.UI.Page
    {
        MeasurementUpdate getdt = new MeasurementUpdate();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack || ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                if (Session["Username"] == null)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>parent.location.href=parent.location.href;</script>");
                }
                // Retrieve query parameters for Edit/update
                if (Request.QueryString["VendorID"] != null)
                {
                    Guid vendorID = new Guid(Request.QueryString["VendorID"]);
                    string vendorUniqueNo = Request.QueryString["VendorUniqueNo"];
                    string companyName = Request.QueryString["CompanyName"];

                    string address1 = Request.QueryString["Address1"];
                    string address2 = Request.QueryString["Address2"];
                    string city = Request.QueryString["City"];
                    string state = Request.QueryString["State"];
                    string country = Request.QueryString["Country"];
                    string regID = Request.QueryString["RegistrationId"];
                    string pan = Request.QueryString["PANNumber"];
                    string vat = Request.QueryString["VATNumber"];
                    string regDate = Request.QueryString["RegistrationDate"];
                    DateTime registrationDate;
                    string formattedDate = " ";
                    if (DateTime.TryParse(regDate, out registrationDate))
                    {
                        // Format the DateTime object to display only the date portion
                        formattedDate = registrationDate.ToString("dd/MM/yyyy");
                    }
                    string contactNum = Request.QueryString["ContactNumber"];
                    string bankAccountNum = Request.QueryString["BankAccountNumber"];
                    string bankName = Request.QueryString["BankName"];
                    string ifsc = Request.QueryString["IFSCCode"];
                    string branchName = Request.QueryString["BranchName"];


                    // Set textbox values
                    txtVUNumber.Text = vendorUniqueNo;
                    txtCompanyName.Text = companyName;
                    txtAddress1.Text = address1;

                    txtAddress2.Text = address2;
                    txtCity.Text = city;
                    txtState.Text = state;
                    txtCounty.Text = country;
                    txtRegID.Text = regID;

                    txtPANNum.Text = pan;

                    txtVATNum.Text = vat;
                    txtRegistrationDate.Text = formattedDate;
                    txtcontactNum.Text = contactNum;
                    txtBnkAccNum.Text = bankAccountNum;
                    txtBnkname.Text = bankName;
                    txtIFSC.Text = ifsc;
                    txtBranchName.Text = branchName;
                }


            }
        }

       
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                
                    Guid vendorID;

                    if (Request.QueryString["VendorID"] != null)
                    {
                        vendorID = new Guid(Request.QueryString["VendorID"]);

                    }
                    else
                    {
                       vendorID = Guid.NewGuid();
                    }
                    string sDate1 = "";
                    DateTime CDate1 = DateTime.Now;
                    if (txtRegistrationDate.Text != "")
                    {
                        sDate1 = txtRegistrationDate.Text;
                        sDate1 = getdt.ConvertDateFormat(sDate1);
                        CDate1 = Convert.ToDateTime(sDate1);
                    }
                

                DataSet ds = getdt.getVendorMaster(new Guid(Request.QueryString["WorkPackageUID"]));
                string userEnteredValue = txtVUNumber.Text.ToString();

                // If a matching value is found   while edit skip this foreach loop and update table

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        // Get the value of "VendorUniqueNo" column for the current row
                        string vendorUniqueNo = row["VendorUniqueNo"].ToString();

                    string requestedVendorUniqueNo = Request.QueryString["VendorUniqueNo"]?.ToString();
                    if (!string.IsNullOrEmpty(requestedVendorUniqueNo) && requestedVendorUniqueNo == vendorUniqueNo)
                    {
                        if (requestedVendorUniqueNo == txtVUNumber.Text.ToString())
                        {
                            break;
                        }
                        
                    }

                    // Compare the user entered value with the value from the DataSet
                    else if(vendorUniqueNo.Equals(userEnteredValue, StringComparison.OrdinalIgnoreCase))
                        {
                       
                                // If a matching value is found, return with an error message
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Alert: VendorUniqueNo already exists!');", true);
                                return; // Exit the method or return appropriate response
                            
                        }
                    }
                int count = getdt.insertVendorsMasterDetails(vendorID, new Guid(Request.QueryString["ProjectUID"].ToString()), new Guid(Request.QueryString["WorkPackageUID"].ToString()), txtVUNumber.Text.ToString(), txtCompanyName.Text.ToString(), txtAddress1.Text.ToString(),
                        txtAddress2.Text.ToString(), txtCity.Text.ToString(), txtState.Text.ToString(), txtCounty.Text.ToString(), txtRegID.Text.ToString(), txtPANNum.Text.ToString(), txtVATNum.Text.ToString(), CDate1,
                        txtcontactNum.Text.ToString(), txtBnkAccNum.Text.ToString(), txtBnkname.Text.ToString(), txtIFSC.Text.ToString(), txtBranchName.Text.ToString());

                    if (count > 0)
                    {

                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>parent.location.href=parent.location.href;</script>");

                    }
                    else
                    {
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('Error Occured. Please contact system admin.);</script>");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Error Occured. Please contact system admin!');", true);

                }


            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('Error Occured. Please contact system admin.);</script>");

            }


        }

       

    }

        
    }