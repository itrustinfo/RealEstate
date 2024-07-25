<%@ Page Title="" Language="C#" MasterPageFile="~/_master_pages/default.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ProjectManagementTool._content_pages.purchase_order._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="default_master_head" runat="server">
    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>
    <script type="text/javascript">
              function BindEvents() {
            $(".showAddPoModal").click(function (e) {
                e.preventDefault();
                var url = $(this).attr("href");
                $("#ModAddPO iframe").attr("src", url);
                $("#ModAddPO").modal("show");
            });

            $(".showAddPoItemModal").click(function (e) {
                e.preventDefault();
                var url = $(this).attr("href");
                $("#ModAddPOItem iframe").attr("src", url);
                $("#ModAddPOItem").modal("show");
             });
            $(".showAddPotaxesModal").click(function (e) {
                e.preventDefault();
                var url = $(this).attr("href");
                $("#ModAddPOTaxes iframe").attr("src", url);
                $("#ModAddPOTaxes").modal("show");
            }); 
        }
        $(document).ready(function () {
            BindEvents();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="default_master_body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-lg-8 form-group">Purchase Order</div>
            <div class="col-md-6 col-lg-4 form-group">
                <label class="sr-only" for="DDLProject">Project</label>
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text">Project</span>
                    </div>
                    <asp:DropDownList ID="DDlProject" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDlProject_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <%--        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>--%>
        <div class="row">
            <div class="col-lg-6 col-xl-3 form-group">
                <div class="card h-100" style="max-height: 700px; overflow-y: auto;">
                    <div class="card-body">
                        <h6 class="card-title text-muted text-uppercase font-weight-bold">PO List</h6>
                        <%--<div class="form-group">
                                        <label class="sr-only" for="TxtSearchDocuments">Search</label>
                                        <div class="input-group">
                                            <input id="TxtSearchDocuments" class="form-control" type="text" placeholder="Activity name" />
                                            <div class="input-group-append">
                                                <asp:Button ID="BtnSearchDocuments" CssClass="btn btn-primary" Text="Search" runat="server" />
                                            </div>
                                        </div>
                                    </div>--%>
                        <asp:TreeView runat="server" CssClass="it_tree_view" ID="TreeView1" ImageSet="XPFileExplorer" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" NodeIndent="15" EnableTheming="True" NodeWrap="True">
                            <NodeStyle CssClass="it_tree_view__node" NodeSpacing="2px" />
                            <ParentNodeStyle Font-Bold="False" />
                            <SelectedNodeStyle CssClass="it_tree_view__node__selected" HorizontalPadding="4px" VerticalPadding="2px" />
                        </asp:TreeView>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-xl-9 form-group">
                <div class="card mb-4" id="VendorDiv" runat="server" visible="false">
                    <div class="card-body">
                        <div class="card-title">
                            <div class="d-flex justify-content-between">
                                <h6 class="text-muted">
                                    <asp:Label ID="ActivityHeadingVendor" CssClass="text-uppercase font-weight-bold" runat="server" />
                                </h6>
                                <div>
                                    <asp:Button ID="Button1" runat="server" Text="Back To Dashboard" CssClass="btn btn-primary" PostBackUrl="/_content_pages/dashboard/" Visible="false"></asp:Button>

                                </div>
                            </div>
                        </div>

                        <div class="table-responsive">
                            <table class="table table-borderless">
                                <tr>
                                    <td>Total Amount</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblGrossWorkPackage" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                    <td>Total Tax Amount</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblTaxWorkPackage" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Net Amount</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblNetWorkPackage" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>

                        </div>
                         <div class="card-title">
                            <div class="d-flex justify-content-between">
                                <h6 class="text-muted">
                                    <asp:Label Text="Vendor List" CssClass="text-uppercase font-weight-bold" runat="server" />
                                </h6>
                               
                            </div>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="GrdVendors" runat="server" AutoGenerateColumns="False" EmptyDataText="No Data Found." AllowPaging="true" PageSize="20" Width="100%" CssClass="table table-bordered">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VendorUniqueNo" HeaderText="Vendor ID">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="CompanyName" HeaderText="Vendor Name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="IFSCCode" HeaderText="Amount">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>--%>
                                    <%--<asp:BoundField DataField="InvoiceMaster_UID" HeaderText="InvoiceMaster_UID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>--%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="card mb-4" id="InvoiceDiv" runat="server" visible="false">
                    <div class="card-body">
                        <div class="card-title">
                            <div class="d-flex justify-content-between">
                                <h6 class="text-muted">
                                    <asp:Label ID="ActivityHeading" CssClass="text-uppercase font-weight-bold" runat="server" />
                                </h6>
                                <div>
                                    <asp:Button ID="btnback" runat="server" Text="Back To Dashboard" CssClass="btn btn-primary" PostBackUrl="/_content_pages/dashboard/" Visible="false"></asp:Button>
                                    <a id="AddPO" runat="server" href="/_modal_pages/add-vendorpo.aspx" class="showAddPoModal"><asp:Button ID="btnaddpo" runat="server" Text="+ Add PO" CssClass="btn btn-primary"></asp:Button></a>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <table class="table table-borderless">
                                <tr>
                                    <td>Vendor ID</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblVendorID" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                    <td>Vendor Company Name</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblVednorName" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Vendor Registration Date</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblVednorRegdate" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                    <td>Vendor Address</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblVednorAddr" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                  <tr>
                                    <td>Gross Amount</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblVendorGrossAmount" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                    <td>Tax Amount</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblVendorTaxAmount" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>

                                    <td>Net Amount</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblVendorNetAmount" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>

                        </div>
                        <hr />
                    <%--    <div class="table-responsive">
                            <table class="table table-borderless">
                                <tr>
                                    <td>Total Amount</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblAllInvoiceTotal" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                    <td>Total Tax Amount</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblAllInvoiceDeductionTotal" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Net Amount</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblAllInvoiceNetTotal" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>

                        </div>--%>
                        <div class="card-title">
                            <div class="d-flex justify-content-between">
                                <h6 class="text-muted">
                                    <asp:Label Text="PO List" CssClass="text-uppercase font-weight-bold" runat="server" />
                                </h6>
                               
                            </div>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="GrdInvoice" runat="server" AutoGenerateColumns="False" EmptyDataText="No Data Found." AllowPaging="true" PageSize="20" Width="100%" CssClass="table table-bordered">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PO_Number" HeaderText="PO Number">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="PO_Date" HeaderText="PO Date" DataFormatString="{0:dd MMM yyyy}">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="PO_TotalAmount" HeaderText="Gross Amount">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="PO_TotalTaxes" HeaderText="PO Taxes">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PO_NetAmount" HeaderText="Net Amout">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="card mb-4" id="PoDetails" runat="server" visible="false">
                    <div class="card-body">
                        <div class="card-title">
                            <div class="d-flex justify-content-between">
                                <h6 class="text-muted">
                                    <asp:Label Text="PO Details" CssClass="text-uppercase font-weight-bold" runat="server" />
                                </h6>
                                <div>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" Width="70px" OnClick="btnPrint_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <table class="table table-borderless">
                                <tr>
                                    <td>PO Number</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblPONumber" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>PO Date</td>
                                    <td>:</td>
                                    <td colspan="2">
                                        <asp:Label ID="LblPODate" runat="server" DataFormatString="{0:dd MMM yyyy}"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Gross Amount</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblPOTotalAmount" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                    <td>Tax Amount</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblPOTaxAmount" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>

                                    <td>Net Amount</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblPONetAmount" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>Up to Prev. Bill Gross</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblUptoPrevTotal" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                    <td>Up to Prev. Bill Deduction</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblUptoPrevDeeductionTotal" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                    <td>Up to Prev. Bill Net</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LblUptoPrevNetTotal" ForeColor="#006699" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>

                        </div>

                    </div>
                </div>
                <div class="card mb-4" id="PoItemsDiv" runat="server" visible="false">
                    <div class="card-body">

                        <div class="card-title">
                            <div class="d-flex justify-content-between">
                                <h6 class="text-muted">
                                    <asp:Label Text="PO Items List" CssClass="text-uppercase font-weight-bold" runat="server" />
                                </h6>
                                <div>
                                    <asp:Button ID="btnRABillPrint" runat="server" Text="Print" Visible="false" CssClass="btn btn-primary" Width="70px" />
                                    <a id="AddPoItem" runat="server" href="/_modal_pages/add-po-item.aspx" class="showAddPoItemModal">
                                        <asp:Button ID="btnAddPoItem" runat="server" Text="+ Add PO Items" CssClass="btn btn-primary"></asp:Button></a>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">

                            <asp:GridView ID="GrdRABillItems" runat="server" AutoGenerateColumns="false" PageSize="20"
                                AllowPaging="true" CssClass="table table-bordered" EmptyDataText="No Data"
                                Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <a id="RABillItems" href="/_modal_pages/show-RABills.aspx?RABillUid=<%#Eval("RABillUid")%>&WorkpackageUID=<%#Eval("WorkpackageUID")%>" class="showModalRABills">
                                                <%#Eval("RABillNumber")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="GenericName" HeaderText="PO Item">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PO_Item_Quantity" HeaderText="Quantity">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Item Rate">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UniqueItemCode" HeaderText="Item Code">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Description" HeaderText="Item Description">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PO_Item_Quantity_Amnt" HeaderText="Item Amount">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Item_Taxes" HeaderText="Taxes">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>                                    
                                    <asp:BoundField DataField="Net_Amount" HeaderText="Net Amount">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <strong>No Records Found ! </strong>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="card mb-4" id="PoTaxes" runat="server" visible="false">
                    <div class="card-body">

                        <div class="card-title">
                            <div class="d-flex justify-content-between">
                                <h6 class="text-muted">
                                    <asp:Label Text="Taxes" CssClass="text-uppercase font-weight-bold" runat="server" />
                                </h6>
                                <div>
                                    <a id="AddInvoiceDeductions" runat="server" href="/_modal_pages/add-po-taxes.aspx" class="showAddPotaxesModal">
                                        <asp:Button ID="btnAddInvoiceDeductions" runat="server" Text="+ Add PO Taxes" CssClass="btn btn-primary"></asp:Button></a>
                                </div>
                            </div>
                        </div>

                        <div class="table-responsive">
                            <asp:GridView ID="GrdInvoiceDeductions" runat="server" AllowPaging="false" CssClass="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="Invoice Number">
                                                            <ItemTemplate>
                                                                 <%#GetInvoiceNumber(Eval("InvoiceMaster_UID").ToString())%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Tax Item">
                                        <ItemTemplate>
                                            <%#GetDeductionMaster(Eval("Deduction_UID").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-ForeColor="#006699">
                                        <ItemTemplate>
                                            <span style="color: #006699;"><%#Eval("Currency")%></span>&nbsp;<%#string.Format(new System.Globalization.CultureInfo(Eval("Currency_CultureInfo").ToString()),"{0:N}", Eval("Amount"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Percentage">
                                        <ItemTemplate>
                                            <%#Eval("Percentage")%>%
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a id="EditInvoiceDeduction" href="/_modal_pages/add-invoicededuction.aspx?Invoice_DeductionUID=<%#Eval("Invoice_DeductionUID")%>&InvoiceMaster_UID=<%#Eval("InvoiceMaster_UID")%>&WorkUID=<%#Eval("WorkpackageUID")%>" class="showModalEditInvoiceDeduction"><span title="Edit" class="fas fa-edit"></span></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkdelete" runat="server" CausesValidation="false" OnClientClick="return DeleteItem()" CommandArgument='<%#Eval("Invoice_DeductionUID")%>' CommandName="delete"><span title="Delete" class="fas fa-trash"></span></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Invoice_DeductionUID" HeaderText="Invoice_DeductionUID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <strong>No Records Found ! </strong>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>



            </div>
        </div>

    </div>
    <%--add PO modal--%>
    <div id="ModAddPO" class="modal it-modal fade">
	    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
		    <div class="modal-content">
			    <div class="modal-header">
				    <h5 class="modal-title">Add PO</h5>
                    <button aria-label="Close" class="close" data-dismiss="modal" type="button"><span aria-hidden="true">&times;</span></button>
			    </div>
			    <div class="modal-body">
                    <iframe class="border-0 w-100" style="height: 460px;" loading="lazy"></iframe>
			    </div>
              
		    </div>
	    </div>
    </div>
        <%--add PO item modal--%>
    <div id="ModAddPOItem" class="modal it-modal fade">
	    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
		    <div class="modal-content">
			    <div class="modal-header">
				    <h5 class="modal-title">Add PO Item</h5>
                    <button aria-label="Close" class="close" data-dismiss="modal" type="button"><span aria-hidden="true">&times;</span></button>
			    </div>
			    <div class="modal-body">
                    <iframe class="border-0 w-100" style="height: 460px;" loading="lazy"></iframe>
			    </div>
              
		    </div>
	    </div>
    </div>
            <%--add PO Taxes modal--%>
    <div id="ModAddPOTaxes" class="modal it-modal fade">
	    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
		    <div class="modal-content">
			    <div class="modal-header">
				    <h5 class="modal-title">Add PO Taxes</h5>
                    <button aria-label="Close" class="close" data-dismiss="modal" type="button"><span aria-hidden="true">&times;</span></button>
			    </div>
			    <div class="modal-body">
                    <iframe class="border-0 w-100" style="height: 460px;" loading="lazy"></iframe>
			    </div>
              
		    </div>
	    </div>
    </div>
</asp:Content>
