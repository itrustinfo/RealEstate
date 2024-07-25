using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography;
using System.Web.Configuration;
using System.Globalization;
using System.Net;
using ProjectManager;

namespace ProjectManager.DAL
{
    public class MeasurementUpdate
    {
        DBUtility db = new DBUtility();

        public string ConvertDateFormat(string dFormat)
        {
            if (WebConfigurationManager.AppSettings["ServerDateFormat"] == "MM/dd/yyyy")
            {
                dFormat = dFormat.Split('/')[1] + "/" + dFormat.Split('/')[0] + "/" + dFormat.Split('/')[2];
            }
            else if (WebConfigurationManager.AppSettings["ServerDateFormat"] == "MM-dd-yyyy")
            {
                dFormat = dFormat.Split('-')[1] + "-" + dFormat.Split('-')[0] + "-" + dFormat.Split('-')[2];
                dFormat = dFormat.Replace("-", "/");
            }
            else
            {
                dFormat = dFormat.Replace("-", "/");
            }
            return dFormat;
        }

        public int InsertIntoMeasurementBook_FirstEntry(Guid UID, Guid TaskUID, string UnitforProgress, string Quantity, string Description,Guid CreatedByUID,string Remarks,DateTime Achieved_Date)
        {
            int cnt = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {

                    using (SqlCommand cmd = new SqlCommand("usp_InsertIntoMeasurementBook_FirstEntry"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@UID", UID);
                        cmd.Parameters.AddWithValue("@TaskUID", TaskUID);
                        cmd.Parameters.AddWithValue("@UnitforProgress", UnitforProgress);
                        cmd.Parameters.AddWithValue("@Quantity", Quantity);
                        cmd.Parameters.AddWithValue("@Description", Description);
                        cmd.Parameters.AddWithValue("@CreatedByUID", CreatedByUID);
                        cmd.Parameters.AddWithValue("@Remarks", Remarks);
                        cmd.Parameters.AddWithValue("@Achieved_Date", Achieved_Date);
                        con.Open();
                        cnt = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return cnt;
            }
            catch (Exception ex)
            {
                return cnt;
            }
        }

        public int InsertMeasurementHistory(Guid UID, Guid MeasurementUID, decimal Value, Guid UserUID, string UserType)
        {
            int cnt = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {

                    using (SqlCommand cmd = new SqlCommand("usp_InsertintoMeasurementHistory"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@UID", UID);
                        cmd.Parameters.AddWithValue("@MeasurementUID", MeasurementUID);
                        cmd.Parameters.AddWithValue("@Value", Value);
                        cmd.Parameters.AddWithValue("@UserUID", UserUID);
                        cmd.Parameters.AddWithValue("@UserType", UserType);
                        con.Open();
                        cnt = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return cnt;
            }
            catch (Exception ex)
            {
                return cnt;
            }
        }

        public string GetMeasurementApprovalDetails(Guid UserUID)
        {
            DataSet ds = new DataSet();
            string approvertype = string.Empty;
            try
            {
                SqlConnection con = new SqlConnection(db.GetConnectionString());
                SqlDataAdapter cmd = new SqlDataAdapter("usp_GetMeasurementApprovalDetails", con);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.SelectCommand.Parameters.AddWithValue("@UserUID", UserUID);
                cmd.Fill(ds);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    approvertype = ds.Tables[0].Rows[0]["ApproverType"].ToString();
                }
            }
            catch (Exception ex)
            {
                approvertype = "";
            }
            return approvertype;
        }

        internal int UpdateMeasurementBookDataforContractor(Guid MeasurementUID, string @Quantity, string Remarks, DateTime Achieved_Date)
        {
            int cnt = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {

                    using (SqlCommand cmd = new SqlCommand("usp_UpdateMeasurementBookDataforContractor"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@MeasurementUID", MeasurementUID);
                        cmd.Parameters.AddWithValue("@Quantity", Quantity);
                        cmd.Parameters.AddWithValue("@Remarks", Remarks);
                        cmd.Parameters.AddWithValue("@Achieved_Date", Achieved_Date);
                        con.Open();
                        cnt = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return cnt;
            }
            catch (Exception ex)
            {
                return cnt;
            }
        }

        public int InsertMeasurementBook_WithoutTaskgrouping_FinalApproval(Guid UID, Guid TaskUID, string Quantity, DateTime SelectedDate)
        {
            int sresult = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_InsertMeasurementBook_WithoutTaskgrouping_FinalApproval"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@UID", UID);
                        cmd.Parameters.AddWithValue("@TaskUID", TaskUID);
                        cmd.Parameters.AddWithValue("@Quantity", Quantity);
                        cmd.Parameters.AddWithValue("@SelectedDate", SelectedDate);
                        sresult = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                sresult = 0;
            }
            return sresult;
        }

        public decimal GetContractorMeasurementData(Guid TaskUID,int sMonth,int sYear)
        {
            decimal Value = 0;
            SqlConnection con = new SqlConnection(db.GetConnectionString());
            try
            {

                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand("usp_GetContractorMeasurementData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskUID", TaskUID);
                cmd.Parameters.AddWithValue("@sMonth", sMonth);
                cmd.Parameters.AddWithValue("@sYear", sYear);
                Value = (decimal)cmd.ExecuteScalar();
                con.Close();
            }
            catch (Exception ex)
            {
                Value = 0;
                if (con.State == ConnectionState.Open) con.Close();
            }
            return Value;
        }

        internal int updateMeasurementBookDash(Guid UID, string Approver1_Quantity, string Approver2_Quantity, string Approver3_Quantity, string Quantity)
        {
            int cnt = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {

                    using (SqlCommand cmd = new SqlCommand("usp_updateMeasurementBookDash"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@UID", UID);
                        cmd.Parameters.AddWithValue("@Approver1_Quantity", Approver1_Quantity);
                        cmd.Parameters.AddWithValue("@Approver2_Quantity", Approver2_Quantity);
                        cmd.Parameters.AddWithValue("@Approver3_Quantity", Approver3_Quantity);
                        cmd.Parameters.AddWithValue("@Quantity", Quantity);

                        con.Open();
                        cnt = Convert.ToInt32(cmd.ExecuteNonQuery());
                        con.Close();

                    }
                }

            }
            catch (Exception ex)
            {

            }
            return cnt;

        }

        public decimal GetApproversMeasurementData(Guid TaskUID, int sMonth, int sYear, string userDesignation)
        {
            decimal Value = 0;
            SqlConnection con = new SqlConnection(db.GetConnectionString());
            try
            {

                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand("usp_GetApproversMeasurementData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskUID", TaskUID);
                cmd.Parameters.AddWithValue("@sMonth", sMonth);
                cmd.Parameters.AddWithValue("@sYear", sYear);
                cmd.Parameters.AddWithValue("@userDesignation", userDesignation);

                Value = (decimal)cmd.ExecuteScalar();
                con.Close();
            }
            catch (Exception ex)
            {
                Value = 0;
                if (con.State == ConnectionState.Open) con.Close();
            }
            return Value;
        }

        public int InsertorUpdateResourceVendorSchdeule(Guid ResourceDeploymentUIDVendor, Guid WorkpackageUID, Guid ResourceUID, DateTime StartDate,
           DateTime EndDate, string DeploymentType, float Planned, DateTime PlannedDate,Guid VendorUID)
        {
            int sresult = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {

                    using (SqlCommand cmd = new SqlCommand("usp_InsertorUpdateResourceVendorSchdeule"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@ResourceDeploymentUIDVendor", ResourceDeploymentUIDVendor);
                        cmd.Parameters.AddWithValue("@WorkpackageUID", WorkpackageUID);
                        cmd.Parameters.AddWithValue("@ResourceUID", ResourceUID);
                        cmd.Parameters.AddWithValue("@StartDate", StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", EndDate);
                        cmd.Parameters.AddWithValue("@DeploymentType", DeploymentType);
                        cmd.Parameters.AddWithValue("@Planned", Planned);
                        cmd.Parameters.AddWithValue("@PlannedDate", PlannedDate);
                        cmd.Parameters.AddWithValue("@VendorUID", VendorUID);
                        sresult = (int)cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
                return sresult;
            }
            catch (Exception ex)
            {
                return sresult = 0;
            }
        }

        public int ResourceDeploymentVendorUpdate(Guid UID, Guid ResourceDeploymentUIDVendor, float Deployed, DateTime DeployedDate, string Remarks)
        {
            int sresult = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {

                    using (SqlCommand cmd = new SqlCommand("usp_ResourceDeploymentVendorUpdate"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@UID", UID);
                        cmd.Parameters.AddWithValue("@ResourceDeploymentUIDVendor", ResourceDeploymentUIDVendor);
                        cmd.Parameters.AddWithValue("@Deployed", Deployed);
                        cmd.Parameters.AddWithValue("@DeployedDate", DeployedDate);
                        cmd.Parameters.AddWithValue("@Remarks", Remarks);
                        sresult = (int)cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
                return sresult;
            }
            catch (Exception ex)
            {
                return sresult = 0;
            }
        }

        //
        public int insertVendorsMasterDetails(Guid VendorID, Guid ProjectUID, Guid WorkPackageUID, string VendorUniqueNo, string CompanyName, string Address1,
                    string Address2, string City, string State, string Country, string RegistrationID, string PANNumber, string VATNumber, DateTime RegistrationDate,
                    string ContactNumber, string BankAccountNumber, string BankName, string IFSCCode, string BranchName)
        {
            int cnt = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {

                    using (SqlCommand cmd = new SqlCommand("usp_insertORUpdateVendorsMasterDetails"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@VendorID", VendorID);
                        cmd.Parameters.AddWithValue("@ProjectUID", ProjectUID);
                        cmd.Parameters.AddWithValue("@WorkPackageUID", WorkPackageUID);
                        cmd.Parameters.AddWithValue("@VendorUniqueNo", VendorUniqueNo);
                        cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                        cmd.Parameters.AddWithValue("@Address1", Address1);
                        cmd.Parameters.AddWithValue("@Address2", Address2);
                        cmd.Parameters.AddWithValue("@City", City);
                        cmd.Parameters.AddWithValue("@State", State);
                        cmd.Parameters.AddWithValue("@Country", Country);
                        cmd.Parameters.AddWithValue("@RegistrationID", RegistrationID);
                        cmd.Parameters.AddWithValue("@PANNumber", PANNumber);
                        cmd.Parameters.AddWithValue("@VATNumber", VATNumber);
                        cmd.Parameters.AddWithValue("@RegistrationDate", RegistrationDate);
                        cmd.Parameters.AddWithValue("@ContactNumber", ContactNumber);
                        cmd.Parameters.AddWithValue("@BankAccountNumber", BankAccountNumber);
                        cmd.Parameters.AddWithValue("@BankName", BankName);
                        cmd.Parameters.AddWithValue("@IFSCCode", IFSCCode);
                        cmd.Parameters.AddWithValue("@BranchName", BranchName);


                        con.Open();
                        cnt = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return cnt;
            }
            catch (Exception ex)
            {
                return cnt;
            }
        }

        public DataSet getVendorMaster(Guid sWorkPackageUID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(db.GetConnectionString());
                SqlDataAdapter cmd = new SqlDataAdapter("usp_getVendorMaster", con);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.SelectCommand.Parameters.AddWithValue("@WorkPackageUID", sWorkPackageUID);
                cmd.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet getVendorItemMaster(Guid sWorkPackageUID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(db.GetConnectionString());
                SqlDataAdapter cmd = new SqlDataAdapter("usp_getVendorItemMaster", con);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.SelectCommand.Parameters.AddWithValue("@WorkPackageUID", sWorkPackageUID);
                cmd.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        //public int insertORUpdateVendorsItems(Guid ItemID, string UniqueItemCode, Guid VendorID, string GenericName, string Description,
        //    string Unit, decimal MinQuantity, decimal Rate, DateTime ValidFrom, DateTime ValidUntil, string Vendor_specific_Name)
        //{
        //    int cnt = 0;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
        //        {

        //            using (SqlCommand cmd = new SqlCommand("usp_insertORUpdateVendorsItems"))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Connection = con;
        //                cmd.CommandTimeout = 120;
        //                cmd.Parameters.AddWithValue("@ItemID", ItemID);
        //                cmd.Parameters.AddWithValue("@UniqueItemCode", UniqueItemCode);
        //                cmd.Parameters.AddWithValue("@VendorID", VendorID);
        //                cmd.Parameters.AddWithValue("@GenericName", GenericName);
        //                cmd.Parameters.AddWithValue("@Description", Description);
        //                cmd.Parameters.AddWithValue("@Unit", Unit);
        //                cmd.Parameters.AddWithValue("@MinQuantity", MinQuantity);
        //                cmd.Parameters.AddWithValue("@Rate", Rate);
        //                cmd.Parameters.AddWithValue("@ValidFrom", ValidFrom);
        //                cmd.Parameters.AddWithValue("@ValidUntil", ValidUntil);
        //                //cmd.Parameters.AddWithValue("@ItemLevel", ItemLevel);
        //                cmd.Parameters.AddWithValue("@Vendor_specific_Name", Vendor_specific_Name);
        //                //cmd.Parameters.AddWithValue("@Parent_Item_code", Parent_Item_code);

        //                con.Open();
        //                cnt = cmd.ExecuteNonQuery();
        //                con.Close();
        //            }
        //        }
        //        return cnt;
        //    }
        //    catch (Exception ex)
        //    {
        //        return cnt;
        //    }
        //}



      
        public DataSet getVendorsList()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(db.GetConnectionString());
                SqlDataAdapter cmd = new SqlDataAdapter("usp_getVendorsList", con);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet getVendorItemList()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(db.GetConnectionString());
                SqlDataAdapter cmd = new SqlDataAdapter("usp_getVendorItemList", con);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet getMainItems()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(db.GetConnectionString());
                SqlDataAdapter cmd = new SqlDataAdapter("usp_getMainItems", con);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet getSubItems(Guid ParentItemUID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(db.GetConnectionString());
                SqlDataAdapter cmd = new SqlDataAdapter("usp_GetSubitems", con);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.SelectCommand.Parameters.AddWithValue("@ParentItemUID",ParentItemUID);
                cmd.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet getParentItemDetails(Guid ParentItemUID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(db.GetConnectionString());
                SqlDataAdapter cmd = new SqlDataAdapter("usp_getParentItemDetails", con);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.SelectCommand.Parameters.AddWithValue("@ParentItemUID", ParentItemUID);
                cmd.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet getSerachVendorItems(string vendorInput, string itemInput)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(db.GetConnectionString());
                SqlDataAdapter cmd = new SqlDataAdapter("usp_SearchVendorItem", con);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.SelectCommand.Parameters.AddWithValue("@vendorInput", vendorInput);
                cmd.SelectCommand.Parameters.AddWithValue("@itemInput", itemInput);
                cmd.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        //added for nikhil on 10/04/2024
        public DataSet getItemCategory_List()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(db.GetConnectionString());
                SqlDataAdapter cmd = new SqlDataAdapter("usp_getItemCategory_List", con);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }



        public int insertORUpdateVendorsItems(Guid ItemID, string UniqueItemCode, Guid VendorID, string GenericName, string Description,
            string Unit, decimal MinQuantity, decimal Rate, DateTime ValidFrom, DateTime ValidUntil, int ItemLevel, string Vendor_specific_Name, Guid Parent_Item_code, Guid ItemCategoryUID)
        {
            int cnt = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {

                    using (SqlCommand cmd = new SqlCommand("usp_insertORUpdateVendorsItems"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@UniqueItemCode", UniqueItemCode);
                        cmd.Parameters.AddWithValue("@VendorID", VendorID);
                        cmd.Parameters.AddWithValue("@GenericName", GenericName);
                        cmd.Parameters.AddWithValue("@Description", Description);
                        cmd.Parameters.AddWithValue("@Unit", Unit);
                        cmd.Parameters.AddWithValue("@MinQuantity", MinQuantity);
                        cmd.Parameters.AddWithValue("@Rate", Rate);
                        cmd.Parameters.AddWithValue("@ValidFrom", ValidFrom);
                        cmd.Parameters.AddWithValue("@ValidUntil", ValidUntil);
                        cmd.Parameters.AddWithValue("@ItemLevel", ItemLevel);
                        cmd.Parameters.AddWithValue("@Vendor_specific_Name", Vendor_specific_Name);
                        cmd.Parameters.AddWithValue("@Parent_Item_code", Parent_Item_code);
                        cmd.Parameters.AddWithValue("@ItemCategoryUID", ItemCategoryUID);
                        con.Open();
                        cnt = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return cnt;
            }
            catch (Exception ex)
            {
                return cnt;
            }
        }

       public DataSet getItemsByCategory(Guid CategoryUID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(db.GetConnectionString());
                SqlDataAdapter cmd = new SqlDataAdapter("usp_getItemsByCategory", con);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.SelectCommand.Parameters.AddWithValue("@CategoryUID", CategoryUID);
                cmd.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

       public DataSet getResourceMaster(Guid sWorkPackageUID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(db.GetConnectionString());
                SqlDataAdapter cmd = new SqlDataAdapter("usp_GetResourceMasterItem", con);
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.SelectCommand.Parameters.AddWithValue("@WorkPackageUID", sWorkPackageUID);
                cmd.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        public int insertORUpdateVendorsItems(Guid ItemID, string UniqueItemCode, Guid VendorID, string GenericName, string Description,
            string Unit, decimal MinQuantity, decimal Rate, DateTime ValidFrom, DateTime ValidUntil, int ItemLevel, string Vendor_specific_Name, Guid Parent_Item_code, Guid ItemCategoryUID, decimal CGST, decimal SGST, decimal GST)
        {
            int cnt = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {

                    using (SqlCommand cmd = new SqlCommand("usp_insertORUpdateVendorsItems"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@UniqueItemCode", UniqueItemCode);
                        cmd.Parameters.AddWithValue("@VendorID", VendorID);
                        cmd.Parameters.AddWithValue("@GenericName", GenericName);
                        cmd.Parameters.AddWithValue("@Description", Description);
                        cmd.Parameters.AddWithValue("@Unit", Unit);
                        cmd.Parameters.AddWithValue("@MinQuantity", MinQuantity);
                        cmd.Parameters.AddWithValue("@Rate", Rate);
                        cmd.Parameters.AddWithValue("@ValidFrom", ValidFrom);
                        cmd.Parameters.AddWithValue("@ValidUntil", ValidUntil);
                        cmd.Parameters.AddWithValue("@ItemLevel", ItemLevel);
                        cmd.Parameters.AddWithValue("@Vendor_specific_Name", Vendor_specific_Name);
                        cmd.Parameters.AddWithValue("@Parent_Item_code", Parent_Item_code);
                        cmd.Parameters.AddWithValue("@ItemCategoryUID", ItemCategoryUID);
                        cmd.Parameters.AddWithValue("@CGST", CGST);
                        cmd.Parameters.AddWithValue("@SGST", SGST);
                        cmd.Parameters.AddWithValue("@GST", GST);

                        con.Open();
                        cnt = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return cnt;
            }
            catch (Exception ex)
            {
                return cnt;
            }
        }

    }
}