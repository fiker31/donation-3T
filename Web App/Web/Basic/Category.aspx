<%@ Page Language="C#" MasterPageFile="~/CreditEditPage.master" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Basic_Category" Title="Category" %>

<%@ MasterType VirtualPath="~/CreditEditPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BootstrapContentPlaceHolder" runat="Server">
    <title>Category</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">

    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-12 col-xl-6">
            <div class="input-group">
                <label style="width: 100%;">Category Name</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="txtCategoryName" runat="server" placeholder="Category Name"></asp:TextBox>

            </div>
        </div>
    </div>


</asp:Content>
