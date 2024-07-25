<%@ Page Title="" Language="C#" MasterPageFile="~/_master_pages/default.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ProjectManagementTool._content_pages.report_resource_request._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="default_master_head" runat="server">
    <style type="text/css">
        .hideItem {
            display: none;
        }

        .pager span {
            color: green;
            font-weight: bold;
            font-size: 17px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="default_master_body" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-lg-4 form-group">Resource Request Report</div>
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
        <div class="container-fluid" id="divTabular" runat="server" visible="false">
            <div class="row">
                <div class="col-lg-12 col-xl-12 form-group">
                    <div class="card mb-4">
                        <div class="card-body">
                            <div class="card-title">
                                <div class="d-flex justify-content-between">
                                    <h6 class="text-muted">
                                        <%--<asp:Label ID="ActivityHeading" CssClass="text-uppercase font-weight-bold" runat="server" Text="Contract Details" />--%>
                                    </h6>
                                    <div style="float: none; width: 50%; text-align: right">
                                        <asp:Button ID="btnAddResourceRequest" runat="server" Text="+ Add Resource Request" Visible="true" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnexcelexport" runat="server" Text="Export to Excel" Visible="true" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnExportReportPDF" runat="server" Text="Export PDF" Visible="true" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnPrintPDF" runat="server" Text="Print" Visible="true" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <div id="divsummary" runat="server" visible="true">

                                    <div style="overflow: scroll;" id="DivMainContentNew">
                                        <asp:GridView ID="grdDataList" EmptyDataText="No Data Found." runat="server" Width="100%" AutoGenerateColumns="false" CellPadding="6" CellSpacing="16" HeaderStyle-BackColor="#666666" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#F5F5F5"
                                            CssClass="table table-bordered">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="RequestID" HeaderText="Request ID">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RequestDate" HeaderText="Request Date">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ItemDescription" HeaderText="Item Description">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="UOM" HeaderText="UOM">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Status" HeaderText="Status">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
