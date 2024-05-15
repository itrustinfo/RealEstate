<%@ Page Title="" Language="C#" MasterPageFile="~/_master_pages/default.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ProjectManagementTool._content_pages.vendors._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="default_master_head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            

            $(".showVendoreModal").click(function (e) {
                e.preventDefault();
                var url = $(this).attr("href");
                $("#ModAddVendor iframe").attr("src", url);
                $("#ModAddVendor").modal("show");
            });
            $(".editVendorModal").click(function (e) {
                e.preventDefault();
                var url = $(this).attr("href");
                $("#ModEditVendor iframe").attr("src", url);
                $("#ModEditVendor").modal("show");
            });

           
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="default_master_body" runat="server">
    <%--<div id="loader"></div>--%>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-lg-4 form-group">Vendors</div>
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
                        <div class="card-title">
                            <div class="d-flex justify-content-between">
                                <h6 class="text-muted">
                                    <asp:Label ID="ActivityHeading" CssClass="text-uppercase font-weight-bold" runat="server" Text="Master Vendor List" />
                                </h6>
                                <div>
                                    <a id="HLAdd" runat="server" href="/_modal_pages/add-vendorsmaster.aspx" class="showVendoreModal">
                                        <asp:Button ID="Button1" runat="server" Text="+ Add Vendor" align="end" CssClass="btn btn-primary"></asp:Button></a>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="grdVendors" runat="server" AutoGenerateColumns="False" Width="100%" PageSize="15" CssClass="table table-bordered" OnPageIndexChanging="grdVendors_PageIndexChanging" OnRowDataBound="grdVendors_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Serial No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VendorUniqueNo" HeaderText="Vendor Number"></asp:BoundField>
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name"></asp:BoundField>
                                    <asp:BoundField DataField="City" HeaderText="City">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="State" HeaderText="State">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Country" HeaderText="Country">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                   <asp:BoundField DataField="RegistrationDate" HeaderText="Registration Date" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>                                           
<a id="EditVendorsMaster" href="/_modal_pages/add-vendorsmaster.aspx?WorkPackageUID=<%#Eval("WorkPackageUID")%>&ProjectUID=<%#Eval("ProjectUID")%>&VendorID=<%#Eval("VendorID")%>&VendorUniqueNo=<%#Eval("VendorUniqueNo")%>&CompanyName=<%#Eval("CompanyName")%>&Address1=<%#Eval("Address1")%>&Address2=<%#Eval("Address2")%>&City=<%#Eval("City")%>&State=<%#Eval("State")%>&Country=<%#Eval("Country")%>&RegistrationId=<%#Eval("RegistrationId")%>&PANNumber=<%#Eval("PANNumber")%>&VATNumber=<%#Eval("VATNumber")%>&RegistrationDate=<%#Eval("RegistrationDate")%>&ContactNumber=<%#Eval("ContactNumber")%>&BankAccountNumber=<%#Eval("BankAccountNumber")%>&BankName=<%#Eval("BankName")%>&IFSCCode=<%#Eval("IFSCCode")%>&BranchName=<%#Eval("BranchName")%>" class="editVendorModal"><span title="Edit" class="fas fa-edit"></span></a>
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

    <%--add vendor modal--%>
    <div id="ModAddVendor" class="modal it-modal fade">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add Vendor</h5>
                    <button aria-label="Close" class="close" data-dismiss="modal" type="button"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <iframe class="border-0 w-100" style="height: 460px;" loading="lazy"></iframe>
                </div>

            </div>
        </div>
    </div>
        <%--edit vendor modal--%>
    <div id="ModEditVendor" class="modal it-modal fade">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edit Vendor</h5>
                    <button aria-label="Close" class="close" data-dismiss="modal" type="button"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <iframe class="border-0 w-100" style="height: 460px;" loading="lazy"></iframe>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
