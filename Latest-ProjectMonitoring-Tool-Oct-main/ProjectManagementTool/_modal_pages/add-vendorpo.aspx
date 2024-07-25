<%@ Page Title="" Language="C#" MasterPageFile="~/_master_pages/modal.Master" AutoEventWireup="true" CodeBehind="add-vendorpo.aspx.cs" Inherits="ProjectManagementTool._modal_pages.add_vendorpo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="modal_master_head" runat="server">
        <script type="text/javascript">
 $( function() {
    $("input[id$='dtInvoiceDate']").datepicker({
      changeMonth: true,
        changeYear: true,
      dateFormat:'dd/mm/yy'
      });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="modal_master_body" runat="server">
        <form id="frmAddInvoiceModal" runat="server">
        <div class="container-fluid" style="max-height:80vh; overflow-y:auto;">
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label class="lblCss" for="txtponumber">PO Number</label> &nbsp;<span style="color:red; font-size:1.1rem;">*</span>
                         <asp:TextBox ID="txtponumber" runat="server" CssClass="form-control" autocomplete="off" required></asp:TextBox>
                    </div>                   
                    <div class="form-group">
                        <label class="lblCss" for="dtExpiry">PO Date</label> &nbsp;<span style="color:red; font-size:1.1rem;">*</span>
                        <asp:TextBox ID="dtInvoiceDate" CssClass="form-control" runat="server" required placeholder="dd/mm/yyyy" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
            </div> 
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                </div>
    </form>
</asp:Content>
