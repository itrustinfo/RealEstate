<%@ Page Title="" Language="C#" MasterPageFile="~/_master_pages/modal.Master" AutoEventWireup="true" CodeBehind="add-parentitem.aspx.cs" Inherits="ProjectManagementTool._modal_pages.add_parentitem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="modal_master_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="modal_master_body" runat="server">
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-md-12 col-lg-4 form-group" id="MainItem" runat="server" visible="true">
                <label class="sr-only" for="DDLMainMainItem">Item</label>
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text">Item</span>
                    </div>
                    <asp:DropDownList ID="DDLMainItem" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLMainMainItem_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-12 col-lg-4 form-group" id="SubItem1" runat="server" visible="false">
                <label class="sr-only" for="DDLSubItem1">SubItem</label>
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text">SubItem</span>
                    </div>
                    <asp:DropDownList ID="DDLSubItem1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLSubItem1_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">

            <div class="col-md-12 col-lg-4 form-group" id="SubItem2" runat="server" visible="false">
                <label class="sr-only" for="DDLSubItem2">SubItem</label>
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text">SubItem</span>
                    </div>
                    <asp:DropDownList ID="DDLSubItem2" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLSubItem2_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-12 col-lg-4 form-group" id="SubItem3" runat="server" visible="false">
                <label class="sr-only" for="DDLSubItem3">SubItem</label>
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text">SubItem</span>
                    </div>
                    <asp:DropDownList ID="DDLSubItem3" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLSubItem3_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-md-12 col-lg-4 form-group" id="SubItem4" runat="server" visible="false">
                <label class="sr-only" for="DDLSubItem4">SubItem</label>
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text">SubItem</span>
                    </div>
                    <asp:DropDownList ID="DDLSubItem4" runat="server" CssClass="form-control" AutoPostBack="true" Visible="false"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </form>
</asp:Content>
