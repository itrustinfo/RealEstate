<%@ Page Title="" Language="C#" MasterPageFile="~/_master_pages/modal.Master" AutoEventWireup="true" CodeBehind="add-vendoritemsmaster.aspx.cs" Inherits="ProjectManagementTool._modal_pages.add_vendoritemsmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="modal_master_head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("input[id$='txtValidFrom']").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy'
            });
            $("input[id$='txtValidUntil']").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy'
            });
        });
</script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".showParentItemModal").click(function (e) {
                e.preventDefault();
                jQuery.noConflict();
                var url = $(this).attr("href");
                $("#ModParentItemData iframe").attr("src", url);
                $("#ModParentItemData").modal("show");
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
                    <div class="form-group" id="ChooseParentItem" runat="server">
                        <label class="lblCss" for="lblParentItem">Choose Parent Item</label>
                        &nbsp;<asp:LinkButton ID="LnkChangeItem" runat="server" Text="Change Item" OnClick="LnkChangeItem_Click"></asp:LinkButton>
                        <br />
                        <asp:Label ID="lblParentItemName" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                        <a id="LinkParentItem" runat="server" href="/_modal_pages/add-parentitem.aspx" class="showParentItemModal">
                            <asp:Button ID="btnchoose" runat="server" CausesValidation="false" Text="Choose Parent Item" CssClass="form-control btn-link" />
                        </a>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="DDLItemCategory">Item Category</label>
                        &nbsp;<span style="color: red; font-size: 1rem;">*</span>
                        <asp:DropDownList ID="DDLItemCategory" runat="server" class="form-control" AutoPostBack="false"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label class="lblCss" for="txtitemcode">Unique ItemCode</label>
                        &nbsp;<span style="color: red; font-size: 1rem;">*</span>
                        <asp:TextBox ID="txtitemcode" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="DDLVendorID">Vendor Number</label>
                        &nbsp;<span style="color: red; font-size: 1rem;">*</span>
                        <asp:DropDownList ID="DDLVendorID" runat="server" class="form-control" AutoPostBack="false"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtgenricName">Generic Name</label>
                        &nbsp;<span style="color: red; font-size: 1rem;">*</span>
                        <asp:TextBox ID="txtgenricName" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtDescription">Description</label>
                        &nbsp;<span style="color: red; font-size: 1rem;">*</span>
                        <asp:TextBox ID="txtDescription" CssClass="form-control" TextMode="MultiLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="lblCss" for="DDLUnit">Unit</label>
                        &nbsp;<span style="color: red; font-size: 1rem;">*</span>
                        <asp:DropDownList ID="DDLUnit" runat="server" class="form-control" AutoPostBack="false"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtMinQuantity">MinQuantity</label>
                        &nbsp;<span style="color: red; font-size: 1rem;">*</span>
                        <asp:TextBox ID="txtMinQuantity" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtRate">Rate</label>
                        &nbsp;<span style="color: red; font-size: 1rem;">*</span>
                        <asp:TextBox ID="txtRate" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtValidFrom">ValidFrom</label>
                        &nbsp;<span style="color: red; font-size: 1rem;">*</span>
                        <asp:TextBox ID="txtValidFrom" CssClass="form-control" runat="server" placeholder="dd/mm/yyyy" autocomplete="off" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="lblCss" for="txtValidUntil">ValidUntil</label>
                        &nbsp;<span style="color: red; font-size: 1rem;">*</span>
                        <asp:TextBox ID="txtValidUntil" CssClass="form-control" runat="server" placeholder="dd/mm/yyyy" autocomplete="off" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <%--<div class="form-group">
                        <label class="lblCss" for="TextBox8">ItemLevel</label>
                        <asp:TextBox ID="TextBox8" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>--%>
                    <div class="form-group">
                        <label class="lblCss" for="txtVSPname">Vendor_specific_Name</label>
                        &nbsp;<span style="color: red; font-size: 1rem;">*</span>
                        <asp:TextBox ID="txtVSPname" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static" required="true"></asp:TextBox>
                    </div>
                    <%--<div class="form-group">
                        <label class="lblCss" for="TextBox11">Parent_Item_code</label>
                        <asp:TextBox ID="TextBox11" CssClass="form-control" TextMode="SingleLine" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </div>--%>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnAdd" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
        </div>
        <%--Link Parent Item modal--%>
        <div id="ModParentItemData" class="modal it-modal fade">
            <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Link Parent Item</h5>
                        <button aria-label="Close" class="close" data-dismiss="modal" type="button"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <iframe class="border-0 w-100" style="height: 340px;" loading="lazy"></iframe>
                    </div>

                </div>
            </div>
        </div>
    </form>
</asp:Content>
