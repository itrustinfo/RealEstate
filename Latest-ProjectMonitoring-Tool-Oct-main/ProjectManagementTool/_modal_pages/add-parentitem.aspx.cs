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
    public partial class add_parentitem : System.Web.UI.Page
    {
        MeasurementUpdate getdt = new MeasurementUpdate();
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
                    bindItems();
                }
            }
        }
        private void bindItems()
        {
            DDLMainItem.DataTextField = "GenericName";
            DDLMainItem.DataValueField = "ItemID";
            DDLMainItem.DataSource = getdt.getMainItems();
            DDLMainItem.DataBind();
            // Add the first entry as "--select--"
            DDLMainItem.Items.Insert(0, new ListItem("--select--", "0"));

        }

        protected void DDLMainMainItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLMainItem.SelectedValue != "0")
            {
                DataSet ds = new DataSet();
                ds = getdt.getSubItems(new Guid(DDLMainItem.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                SubItem1.Visible = true;
                DDLSubItem1.Visible = true;
                DDLSubItem1.DataTextField = "GenericName";
                DDLSubItem1.DataValueField = "ItemID";
                DDLSubItem1.DataSource = ds;
                DDLSubItem1.DataBind();
                // Add the first entry as "--select--"
                DDLSubItem1.Items.Insert(0, new ListItem("--select--", "0"));


                }
                else
                {
                    // Clear the previously loaded values
                    DDLSubItem1.DataSource = null;
                    DDLSubItem1.DataBind();
                    SubItem1.Visible = false;
                    DDLSubItem1.Visible = false;
                }
        }
            else
            {
                // Clear the previously loaded values & hide next DDL
                DDLSubItem1.DataSource = null;
                DDLSubItem1.DataBind();
                SubItem1.Visible = false;
                DDLSubItem1.Visible = false;

            }
        }


        protected void DDLSubItem1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLSubItem1.SelectedValue != "0")
            {
                DataSet ds = new DataSet();
                ds = getdt.getSubItems(new Guid(DDLSubItem1.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SubItem2.Visible = true;
                    DDLSubItem2.Visible = true;
                    DDLSubItem2.DataTextField = "GenericName";
                    DDLSubItem2.DataValueField = "ItemID";
                    DDLSubItem2.DataSource = ds;
                    DDLSubItem2.DataBind();
                    // Add the first entry as "--select--"
                    DDLSubItem2.Items.Insert(0, new ListItem("--select--", "0"));
                }
                else
                {
                    // Clear the previously loaded values
                    DDLSubItem2.DataSource = null;
                    DDLSubItem2.DataBind();
                    SubItem2.Visible = false;
                    DDLSubItem2.Visible = false;
                }
            }
            else
            {
                // Clear the previously loaded values
                DDLSubItem2.DataSource = null;
                DDLSubItem2.DataBind();
                SubItem2.Visible = false;
                DDLSubItem2.Visible = false;
            }

        }

        protected void DDLSubItem2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLSubItem2.SelectedValue != "0")
            {
                DataSet ds = new DataSet();
                ds = getdt.getSubItems(new Guid(DDLSubItem2.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SubItem3.Visible = true;
                    DDLSubItem3.Visible = true;
                    DDLSubItem3.DataTextField = "GenericName";
                    DDLSubItem3.DataValueField = "ItemID";
                    DDLSubItem3.DataSource = ds;
                    DDLSubItem3.DataBind();
                    // Add the first entry as "--select--"
                    DDLSubItem3.Items.Insert(0, new ListItem("--select--", "0"));
                }
                else
                {
                    // Clear the previously loaded values
                    DDLSubItem3.DataSource = null;
                    DDLSubItem3.DataBind();
                    SubItem3.Visible = false;
                    DDLSubItem3.Visible = false;
                }
            }
            else
            {
                // Clear the previously loaded values
                DDLSubItem3.DataSource = null;
                DDLSubItem3.DataBind();
                SubItem3.Visible = false;
                DDLSubItem3.Visible = false;
            }
        }

        protected void DDLSubItem3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLSubItem3.SelectedValue != "0")
            {
                DataSet ds = new DataSet();
                ds = getdt.getSubItems(new Guid(DDLSubItem3.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SubItem4.Visible = true;
                    DDLSubItem4.Visible = true;
                    DDLSubItem4.DataTextField = "GenericName";
                    DDLSubItem4.DataValueField = "ItemID";
                    DDLSubItem4.DataSource = ds;
                    DDLSubItem4.DataBind();
                    // Add the first entry as "--select--"
                    DDLSubItem4.Items.Insert(0, new ListItem("--select--", "0"));
                }
                else
                {
                    // Clear the previously loaded values
                    DDLSubItem4.DataSource = null;
                    DDLSubItem4.DataBind();
                    SubItem4.Visible = false;
                    DDLSubItem4.Visible = false;
                }
            }
            else
            {
                // Clear the previously loaded values
                DDLSubItem4.DataSource = null;
                DDLSubItem4.DataBind();
                SubItem4.Visible = false;
                DDLSubItem4.Visible = false;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DDLSubItem4.SelectedValue) && DDLSubItem4.SelectedValue != "0")
            {
                Session["ParentUID"] = DDLSubItem4.SelectedValue;
                Session["ParentDataText"] = DDLSubItem4.SelectedItem.Text;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>parent.location.href=parent.location.href;</script>");
            }
            else if (!string.IsNullOrEmpty(DDLSubItem3.SelectedValue) && DDLSubItem3.SelectedValue != "0")
            {
                Session["ParentUID"] = DDLSubItem3.SelectedValue;
                Session["ParentDataText"] = DDLSubItem3.SelectedItem.Text;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>parent.location.href=parent.location.href;</script>");
            }
            else if (!string.IsNullOrEmpty(DDLSubItem2.SelectedValue) && DDLSubItem2.SelectedValue != "0")
            {
                Session["ParentUID"] = DDLSubItem2.SelectedValue;
                Session["ParentDataText"] = DDLSubItem2.SelectedItem.Text;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>parent.location.href=parent.location.href;</script>");
            }
            else if (!string.IsNullOrEmpty(DDLSubItem1.SelectedValue) && DDLSubItem1.SelectedValue != "0")
            {
                Session["ParentUID"] = DDLSubItem1.SelectedValue;
                Session["ParentDataText"] = DDLSubItem1.SelectedItem.Text;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>parent.location.href=parent.location.href;</script>");
            }
            else if (!string.IsNullOrEmpty(DDLMainItem.SelectedValue) && DDLMainItem.SelectedValue != "0")
            {
                Session["ParentUID"] = DDLMainItem.SelectedValue;
                Session["ParentDataText"] = DDLMainItem.SelectedItem.Text;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>parent.location.href=parent.location.href;</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "CLOSE", "<script language='javascript'>alert('Please choose a Parent Item')</script>");
            }
        }


    }
}