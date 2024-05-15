using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectManager.DAL;

namespace ProjectManagementTool._content_pages.dashboard_measurment
{
    public partial class _default : System.Web.UI.Page
    {
        DBGetData getdata = new DBGetData();
        MeasurementUpdate measureupdate = new MeasurementUpdate();

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
                    if (Request.QueryString["WorkPackageUID"] != null)
                    {
                        BindMeasurementBook(Request.QueryString["WorkPackageUID"]);
                        lblTotalcount.Text = "Total Count : " + grdMeasurementbook.Rows.Count.ToString();
                    }
                }
            }
        }

        public string GetTaskName(string TaskUID)
        {
            if (!string.IsNullOrEmpty(TaskUID))
            {
                return getdata.getTaskNameby_TaskUID(new Guid(TaskUID));
            }
            else
            {
                return "";
            }
        }

        private void BindMeasurementBook(string WorkPackageUID)
        {

            try
            {
                //ActivityHeading.Text = "Task : " + TreeView1.SelectedNode.Text;
                DataSet ds = getdata.GetTaskMeasurementBookForDashboard(new Guid(WorkPackageUID));
                grdMeasurementbook.DataSource = ds;
                grdMeasurementbook.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void grdMeasurementbook_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                // Replace "Edit" button with "Update" and "Cancel" buttons
                Button btnUpdate = (Button)e.Row.FindControl("btnUpdate");
                Button btnCancel = (Button)e.Row.FindControl("btnCancel");

                btnUpdate.Visible = true;
                btnCancel.Visible = true;

                // Hide other buttons or controls as needed
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the "Edit" button in the row
                Button btnEdit = (Button)e.Row.FindControl("btnupdate");

                // Check the user's designation
                string userDesignation = Session["MeasurementUser"]?.ToString();

                // Set the visibility of the "Edit" button based on user designation
                Label lblApprover1_Quantity = (Label)e.Row.FindControl("lblApprover1_Quantity");
                Label lblApprover2_Quantity = (Label)e.Row.FindControl("lblApprover2_Quantity");
                Label lblApprover3_Quantity = (Label)e.Row.FindControl("lblApprover3_Quantity");
                
                btnEdit.Visible = IsUserApprover(userDesignation, lblApprover1_Quantity.Text, lblApprover2_Quantity.Text,lblApprover3_Quantity.Text);
            }
        }

        protected void grdMeasurementbook_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdMeasurementbook.EditIndex = e.NewEditIndex;

            // Call a method to bind the data to the GridView
            BindMeasurementBook(Request.QueryString["WorkPackageUID"]);
            string userDesignation = Session["MeasurementUser"]?.ToString();

            // Find the TextBox control in the "Approver1 Quantity" column of the edited row & // Set the TextBox control as editable

            if (userDesignation == "Approver1")
            {
                TextBox txtApprover1_Quantity = (TextBox)grdMeasurementbook.Rows[e.NewEditIndex].FindControl("txtApprover1_Quantity");
                if (txtApprover1_Quantity != null)
                {
                    txtApprover1_Quantity.Enabled = true;
                }
            }
            else if(userDesignation == "Approver2")
            {
                TextBox txtApprover2_Quantity = (TextBox)grdMeasurementbook.Rows[e.NewEditIndex].FindControl("txtApprover2_Quantity");
                if (txtApprover2_Quantity != null)
                {
                    txtApprover2_Quantity.Enabled = true;
                }
            }
            else if(userDesignation == "Approver3")
            {
                TextBox txtApprover3_Quantity = (TextBox)grdMeasurementbook.Rows[e.NewEditIndex].FindControl("txtApprover3_Quantity");
                if (txtApprover3_Quantity != null)
                {
                    txtApprover3_Quantity.Enabled = true;
                }
            }
            else if(userDesignation == "Contractor")
            {
                TextBox txtContractor_Quantity = (TextBox)grdMeasurementbook.Rows[e.NewEditIndex].FindControl("txtContractor_Quantity");
                if (txtContractor_Quantity != null)
                {
                    txtContractor_Quantity.Enabled = true;
                }
            }

        }
        protected void grdMeasurementbook_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Exit edit mode
            grdMeasurementbook.EditIndex = -1; 

            // Rebind the data to the GridView
            BindMeasurementBook(Request.QueryString["WorkPackageUID"]);
          
        }


        private bool IsUserApprover(string userDesignation, string Approver1Value, string Approver2Value,string Approver3Value)
            {
            if(userDesignation == "Approver1" || userDesignation == "Approver2" || userDesignation == "Approver3" || userDesignation == "Contractor")
            {
                if ((userDesignation == "Approver1") && Approver3Value == "0")
                    return true;
                else if (userDesignation == "Approver2" && Approver1Value !="0" && Approver3Value == "0")
                    return true;
                else if (userDesignation == "Approver3" && Approver2Value != "0")
                    return true;
                else if (userDesignation == "Contractor" && Approver3Value == "0")
                    return true;
                else
                    return false;
            }
            return false;
            }

        protected void grdMeasurementbook_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                // Access the UID from the DataSet
                DataSet ds = getdata.GetTaskMeasurementBookForDashboard(new Guid (Request.QueryString["WorkPackageUID"]));
                Guid rowUID = (Guid)ds.Tables[0].Rows[e.RowIndex]["UID"];
                Guid TaskUID = (Guid)ds.Tables[0].Rows[e.RowIndex]["TaskUID"];

                string userDesignation = Session["MeasurementUser"]?.ToString();
                // Access the updated values from the GridView
                
                string updatedApprover1_Quantity = ((TextBox)grdMeasurementbook.Rows[e.RowIndex].FindControl("txtApprover1_Quantity")).Text;

                string updatedApprover2_Quantity = ((TextBox)grdMeasurementbook.Rows[e.RowIndex].FindControl("txtApprover2_Quantity")).Text;
                string updatedApprover3_Quantity = ((TextBox)grdMeasurementbook.Rows[e.RowIndex].FindControl("txtApprover3_Quantity")).Text;
                string updatedContractor_Quantity = ((TextBox)grdMeasurementbook.Rows[e.RowIndex].FindControl("txtContractor_Quantity")).Text;

                string achvdate = ((Label)grdMeasurementbook.Rows[e.RowIndex].FindControl("Achieved_Date")).Text;
                string sDate1 = "";
                DateTime CDate1 = DateTime.Now;
                sDate1 = achvdate;
                sDate1 = getdata.ConvertDateFormat(achvdate);
                CDate1 = Convert.ToDateTime(sDate1);

                //for validations
                float Targetvalue = 0;
                decimal ContractorCumulative = 0;
                decimal Approver1Cumulative = 0;
                decimal Approver2Cumulative = 0;
                decimal Approver3Cumulative = 0;

                DataSet dsgetdata = getdata.GetTaskSchedule_by_TaksUID_Month_Year(TaskUID, Convert.ToInt32(CDate1.Month), Convert.ToInt32(CDate1.Year));
                if (dsgetdata.Tables[0].Rows.Count > 0)
                {
                    Targetvalue = float.Parse(dsgetdata.Tables[0].Rows[0]["Schedule_Value"].ToString());
                }

                if (userDesignation == "Contractor")
                {
                    ContractorCumulative = measureupdate.GetContractorMeasurementData(TaskUID, Convert.ToInt32(CDate1.Month), Convert.ToInt32(CDate1.Year));

                    if ((float.Parse(updatedContractor_Quantity) + float.Parse(ContractorCumulative.ToString()) > Targetvalue))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('Total Contractor quantity cannot be greater than target quantity " + Targetvalue + "');</script>");

                        return;
                    }
                }
                else if(userDesignation == "Approver1")
                {
                    Approver1Cumulative = measureupdate.GetApproversMeasurementData(TaskUID, Convert.ToInt32(CDate1.Month), Convert.ToInt32(CDate1.Year), userDesignation);

                    if ((float.Parse(updatedApprover1_Quantity) + float.Parse(Approver1Cumulative.ToString()) > Targetvalue))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('Total Approver1 quantity cannot be greater than target quantity " + Targetvalue + "');</script>");

                        return;
                    }
                }
                else if (userDesignation == "Approver2")
                {
                    Approver2Cumulative = measureupdate.GetApproversMeasurementData(TaskUID, Convert.ToInt32(CDate1.Month), Convert.ToInt32(CDate1.Year), userDesignation);

                    if ((float.Parse(updatedApprover2_Quantity) + float.Parse(Approver2Cumulative.ToString()) > Targetvalue))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('Total Approver2 quantity cannot be greater than target quantity " + Targetvalue + "');</script>");

                        return;
                    }
                }
                else if (userDesignation == "Approver3")
                {
                    Approver3Cumulative = measureupdate.GetApproversMeasurementData(TaskUID, Convert.ToInt32(CDate1.Month), Convert.ToInt32(CDate1.Year), userDesignation);

                    if ((float.Parse(updatedApprover3_Quantity) + float.Parse(Approver3Cumulative.ToString()) > Targetvalue))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('Total Approver3 quantity cannot be greater than target quantity " + Targetvalue + "');</script>");

                        return;
                    }
                }
                //
                // Call the function with the updated values
                if (userDesignation != "Approver3")
                {
                    if ((float.Parse(updatedApprover2_Quantity)  > Targetvalue) || (float.Parse(updatedApprover1_Quantity) > Targetvalue))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('Total Apporver quantity cannot be greater than target quantity " + Targetvalue + "');</script>");

                        return;
                    }
                    var rs = measureupdate.updateMeasurementBookDash(rowUID, updatedApprover1_Quantity, updatedApprover2_Quantity, updatedApprover3_Quantity, updatedContractor_Quantity);
                }
                else //if it is Approver 3 i.e final approval
                {
                    if ((float.Parse(updatedApprover3_Quantity) > Targetvalue))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('Total Apporver quantity cannot be greater than target quantity " + Targetvalue + "');</script>");

                        return;
                    }
                        int resultF = measureupdate.InsertMeasurementBook_WithoutTaskgrouping_FinalApproval(rowUID, TaskUID, updatedApprover3_Quantity, CDate1);
                }

                // Exit edit mode
                grdMeasurementbook.EditIndex = -1;

                // Rebind the data to the GridView
                BindMeasurementBook(Request.QueryString["WorkPackageUID"]);


                // Update MeasurementHistory Table
                decimal value = 0;

                if (userDesignation == "Approver1")
                {
                    value = Convert.ToDecimal(updatedApprover1_Quantity);
                }
                else if(userDesignation == "Approver2")
                {
                    value = Convert.ToDecimal(updatedApprover2_Quantity);

                }
                else if (userDesignation == "Approver3")
                {
                    value = Convert.ToDecimal(updatedApprover3_Quantity);

                }
                else if (userDesignation == "Contractor")
                {
                    value = Convert.ToDecimal(updatedContractor_Quantity);

                }

                int result = measureupdate.InsertMeasurementHistory(Guid.NewGuid(), rowUID, value, new Guid(Session["UserUID"].ToString()), Session["MeasurementUser"].ToString());
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                // You might want to log the exception or show an error message to the user
            }
        }

    }
}