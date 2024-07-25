<%@ Page Title="" Language="C#" MasterPageFile="~/_master_pages/modal.Master" AutoEventWireup="true" CodeBehind="add-vendorsmaster.aspx.cs" Inherits="ProjectManagementTool._modal_pages.add_vendorsmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="modal_master_head" runat="server">
 <script type="text/javascript">
     $(function () {
         $("input[id$='txtRegistrationDate']").datepicker({
             changeMonth: true,
             changeYear: true,
             dateFormat: 'dd/mm/yy'
         });
     });
     //check if contact number is number
     $(function() {
    $('#txtcontactNum').keypress(function(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            alert("Only numbers allowed!");
            return false;
        }
        return true;
    });
});
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="modal_master_body" runat="server">
    <form id="frmVendorsMaster" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid" style="max-height: 80vh; overflow-y: auto; margin-top: 15px;">
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="lblCss" for="txtVUNumber">Vendor Unique Number</label> &nbsp;<span style="color:red; font-size:1rem;">*</span>
                        <asp:TextBox ID="txtVUNumber" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="lblCss" for="txtCompanyName">Company Name</label>
                        <asp:TextBox ID="txtCompanyName" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="lblCss" for="txtAddress1">Address1</label> &nbsp;<span style="color:red; font-size:1rem;">*</span>
                        <asp:TextBox ID="txtAddress1" CssClass="form-control" TextMode="MultiLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="lblCss" for="txtAddress2">Address2</label> &nbsp;<span style="color:red; font-size:1rem;">*</span>
                        <asp:TextBox ID="txtAddress2" CssClass="form-control" TextMode="MultiLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="lblCss" for="txtCity">City</label> &nbsp;<span style="color:red; font-size:1rem;">*</span>
                        <asp:TextBox ID="txtCity" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtState">State</label> &nbsp;<span style="color:red; font-size:1rem;">*</span>
                        <asp:TextBox ID="txtState" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtCounty">Country</label> &nbsp;<span style="color:red; font-size:1rem;">*</span>
                        <asp:TextBox ID="txtCounty" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtRegID">Registration ID</label> &nbsp;<span style="color:red; font-size:1rem;">*</span>
                        <asp:TextBox ID="txtRegID" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>



                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="lblCss" for="txtPANNum">PAN Number</label>
                        <asp:TextBox ID="txtPANNum" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtVATNum">VAT Number</label>
                        <asp:TextBox ID="txtVATNum" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtRegistrationDate">Registration Date</label> &nbsp;<span style="color:red; font-size:1rem;">*</span>
                      
                        <asp:TextBox ID="txtRegistrationDate" CssClass="form-control" runat="server" placeholder="dd/mm/yyyy" autocomplete="off" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtcontactNum">Contact Number</label> &nbsp;<span style="color:red; font-size:1rem;">*</span>
                        <asp:TextBox ID="txtcontactNum" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true" MaxLength="10" autocomplete="off" onkeypress="return isNumber(event)"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtBnkAccNum">Bank Account Number</label> &nbsp;<span style="color:red; font-size:1rem;">*</span>
                  <asp:TextBox ID="txtBnkAccNum" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtBnkname">Bank Name</label> &nbsp;<span style="color:red; font-size:1rem;">*</span>
                        <asp:TextBox ID="txtBnkname" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtIFSC">IFSCCode</label> &nbsp;<span style="color:red; font-size:1rem;">*</span>
                        <asp:TextBox ID="txtIFSC" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtBranchName">Branch Name</label> &nbsp;<span style="color:red; font-size:1rem;">*</span>
                        <asp:TextBox ID="txtBranchName" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>


                </div>
            </div>
        </div>

        <div class="modal-footer">
            <asp:Button ID="btnAdd" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
        </div>
    </form>
</asp:Content>
