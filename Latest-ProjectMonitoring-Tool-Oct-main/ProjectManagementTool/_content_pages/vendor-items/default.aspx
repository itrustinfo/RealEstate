<%@ Page Title="" Language="C#" MasterPageFile="~/_master_pages/default.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ProjectManagementTool._content_pages.vendor_items._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="default_master_head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".showVendoreItemModal").click(function (e) {
                e.preventDefault();
                var url = $(this).attr("href");
                $("#ModAddVendorItem iframe").attr("src", url);
                $("#ModAddVendorItem").modal("show");
            });
            $(".showeditVendorItemModal").click(function (e) {
                e.preventDefault();
                var url = $(this).attr("href");
                $("#ModEditVendorItem iframe").attr("src", url);
                $("#ModEditVendorItem").modal("show");
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="default_master_body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-lg-4 form-group">Vendor Items</div>
            <div class="col-md-6 col-lg-4 form-group">
                <label class="sr-only" for="DDLProject">Project</label>
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text">Project</span>
                    </div>
                    <asp:DropDownList ID="DDlProject" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDlProject_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-lg-4 form-group">
                <label class="sr-only" for="DDLWorkPackage">Work Package</label>
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text">Work Package</span>
                    </div>
                    <asp:DropDownList ID="DDLWorkPackage" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLWorkPackage_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-xl-12 form-group">
                <div class="card mb-4">
                    <div class="card-body">
                        <asp:UpdatePanel ID="Update1" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-12 col-lg-4 form-group" id="MainItem" runat="server">
                                        <label class="sr-only" for="DDLMainItem">Main Item</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Main Item</span>
                                            </div>
                                            <asp:DropDownList ID="DDLMainItem" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLMainItem_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-lg-4 form-group" id="SubItem1" runat="server" visible="false">
                                        <label class="sr-only" for="DDLSubItem1">Sub Item</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Sub Item</span>
                                            </div>
                                            <asp:DropDownList ID="DDLSubItem1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLSubItem1_SelectedIndexChanged" visible="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-lg-4 form-group" id="SubItem2" runat="server" visible="false">
                                        <label class="sr-only" for="DDLSubItem2">Sub Item</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Sub Item</span>
                                            </div>
                                            <asp:DropDownList ID="DDLSubItem2" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLSubItem2_SelectedIndexChanged" visible="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-lg-4 form-group" id="SubItem3" runat="server" visible="false">
                                        <label class="sr-only" for="DDLSubItem3">Sub Item</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Sub Item</span>
                                            </div>
                                            <asp:DropDownList ID="DDLSubItem3" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLSubItem3_SelectedIndexChanged" visible="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-lg-4 form-group" id="SubItem4" runat="server" visible="false">
                                        <label class="sr-only" for="DDLSubItem4">Sub Item</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Sub Item</span>
                                            </div>
                                            <asp:DropDownList ID="DDLSubItem4" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLSubItem4_SelectedIndexChanged" visible="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 col-lg-4 form-group" id="divsubmit" runat="server">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                        &nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                                        <br />
                                        <asp:Label ID="LblMessage" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-xl-12 form-group">
                <div class="card mb-4">
                    <div class="card-body">
                            <div class="card-title">
                                <div class="d-flex justify-content-between">
                                    <h6 class="text-muted">
                                        <asp:Label ID="ActivityHeading" CssClass="text-uppercase font-weight-bold" runat="server" Text="Master Vendor-Items List" />
                                    </h6>
                                    <div>
                                        <a id="HLAdd" runat="server" href="/_modal_pages/add-vendoritemsmaster.aspx" class="showVendoreItemModal">
                                            <asp:Button ID="Button1" runat="server" Text="+ Add Item" align="end" CssClass="btn btn-primary"></asp:Button></a>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="grdVendorItem" runat="server" AutoGenerateColumns="False" Width="100%" PageSize="15" CssClass="table table-bordered" OnPageIndexChanging="grdVendors_PageIndexChanging" OnRowDataBound="grdVendors_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Serial No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UniqueItemCode" HeaderText="Item Code"></asp:BoundField>
                                        <asp:BoundField DataField="GenericName" HeaderText="Item Name"></asp:BoundField>
                                        <asp:BoundField DataField="CategoryName" HeaderText="Item Category"></asp:BoundField>
                                        <asp:BoundField DataField="Unit" HeaderText="Unit">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ValidFrom" HeaderText="ValidFrom" DataFormatString="{0:dd/MM/yyyy}">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ValidUntil" HeaderText="ValidUntil" DataFormatString="{0:dd/MM/yyyy}">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Vendor_specific_Name" HeaderText="Vendor_specific_Name">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MinQuantity" HeaderText="Min_Quantity">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Unit" HeaderText="Unit">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a id="EditVendorItemsMaster" href="/_modal_pages/add-vendoritemsmaster.aspx?&ItemID=<%#Eval("ItemID")%>&UniqueItemCode=<%#Eval("UniqueItemCode")%>&ItemCategoryUID=<%#Eval("ItemCategoryUID")%>&VendorID=<%#Eval("VendorID")%>&GenericName=<%#Eval("GenericName")%>&Description=<%#Eval("Description")%>&Unit=<%#Eval("Unit")%>&MinQuantity=<%#Eval("MinQuantity")%>&Rate=<%#Eval("Rate")%>&ValidFrom=<%#Eval("ValidFrom")%>&ValidUntil=<%#Eval("ValidUntil")%>&ItemLevel=<%#Eval("ItemLevel")%>&Vendor_specific_Name=<%#Eval("Vendor_specific_Name")%>&Parent_Item_code=<%#Eval("Parent_Item_code")%>" class="showeditVendorItemModal"><span title="Edit" class="fas fa-edit"></span></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found !                  
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                    </div>
                     </div>
                 </div>
             </div>
           </div>

                    <%--add vendor-item modal--%>
                    <div id="ModAddVendorItem" class="modal it-modal fade">
                        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Add Vendor Item</h5>
                                    <button aria-label="Close" class="close" data-dismiss="modal" type="button"><span aria-hidden="true">&times;</span></button>
                                </div>
                                <div class="modal-body">
                                    <iframe class="border-0 w-100" style="height: 460px;" loading="lazy"></iframe>
                                </div>

                            </div>
                        </div>
                    </div>
                    <%--edit vendor-item modal--%>
                    <div id="ModEditVendorItem" class="modal it-modal fade">
                        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Edit Vendor Item</h5>
                                    <button aria-label="Close" class="close" data-dismiss="modal" type="button"><span aria-hidden="true">&times;</span></button>
                                </div>
                                <div class="modal-body">
                                    <iframe class="border-0 w-100" style="height: 460px;" loading="lazy"></iframe>
                                </div>

                            </div>
                        </div>
                    </div>
</asp:Content>
