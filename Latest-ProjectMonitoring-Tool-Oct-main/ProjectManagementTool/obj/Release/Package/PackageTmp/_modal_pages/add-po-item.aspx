<%@ Page Title="" Language="C#" MasterPageFile="~/_master_pages/modal.Master" AutoEventWireup="true" CodeBehind="add-po-item.aspx.cs" Inherits="ProjectManagementTool._modal_pages.add_po_item" %>

<asp:Content ID="Content1" ContentPlaceHolderID="modal_master_head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("input[id$='dtInvoiceDate']").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy'
            });
        });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="modal_master_body" runat="server">
    <form id="frmAddInvoiceModal" runat="server">
        <div class="container-fluid" style="max-height: 80vh; overflow-y: auto;">
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label class="lblCss" for="DDLtItem">Item</label>
                        &nbsp;<span style="color: red; font-size: 1.1rem;">*</span>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Item</span>
                            </div>
                            <asp:DropDownList ID="DDLtItem" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLtItem_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtQty">Quantity</label>
                        &nbsp;<span style="color: red; font-size: 1.1rem;">*</span>
                        <asp:TextBox ID="txtQty" CssClass="form-control" runat="server" required="true" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </form>
</asp:Content>
