<%@ Page Title="" Language="C#" MasterPageFile="~/_master_pages/default.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ProjectManagementTool._content_pages.vendor_items_search._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="default_master_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="default_master_body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-lg-4 form-group">Vendor Items Search</div>
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
                        <div class="row">
                            <div class="col-md-12 col-lg-4 form-group" id="vendorDiv" runat="server">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Vendor</span>
                                    </div>
                                    <asp:TextBox ID="txtVendorSearch" runat="server" CssClass="form-control" Placeholder="type here..."></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12 col-lg-4 form-group" id="itemDiv" runat="server">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Item</span>
                                    </div>
                                    <asp:TextBox ID="txtItemSearch" runat="server" CssClass="form-control" Placeholder="type here..."></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-lg-4 form-group" id="divsubmit" runat="server">

                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                &nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear Search" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                                <br />
                            </div>
                        </div>
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
                                        <asp:Label ID="ActivityHeading" CssClass="text-uppercase font-weight-bold" runat="server" Text="Vendor-Items List" />
                                    </h6>
                                    </div>
                            </div>
                        <div class="table-responsive">
                            <asp:GridView ID="grdItemSearch" runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="true" PageSize="10" CssClass="table table-bordered" OnPageIndexChanging="grdItemSearch_PageIndexChanging" OnRowDataBound="grdItemSearch_RowDataBound" Visible="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Serial No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VendorUniqueNo" HeaderText="Vendor ID"></asp:BoundField>
                                    <asp:BoundField DataField="CompanyName" HeaderText="Vendor Name"></asp:BoundField>
                                    <asp:BoundField DataField="UniqueItemCode" HeaderText="Item ID">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GenericName" HeaderText="Item Name">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MinQuantity" HeaderText="Quantity">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                   <%-- <asp:TemplateField>
                                        <ItemTemplate>
                                            <a id="EditVendorItemsMaster" href="/_modal_pages/add-vendoritemsmaster.aspx?&ItemID=<%#Eval("ItemID")%>&UniqueItemCode=<%#Eval("UniqueItemCode")%>&VendorID=<%#Eval("VendorID")%>&GenericName=<%#Eval("GenericName")%>&Description=<%#Eval("Description")%>&Unit=<%#Eval("Unit")%>&MinQuantity=<%#Eval("MinQuantity")%>&Rate=<%#Eval("Rate")%>&ValidFrom=<%#Eval("ValidFrom")%>&ValidUntil=<%#Eval("ValidUntil")%>&ItemLevel=<%#Eval("ItemLevel")%>&Vendor_specific_Name=<%#Eval("Vendor_specific_Name")%>&Parent_Item_code=<%#Eval("Parent_Item_code")%>" class="showeditVendorItemModal"><span title="Edit" class="fas fa-edit"></span></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
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
</asp:Content>
