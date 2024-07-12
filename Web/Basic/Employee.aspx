<%@ Page Language="C#" MasterPageFile="~/CreditEditPage.master" AutoEventWireup="true" CodeFile="Employee.aspx.cs" Inherits="Basic_Employee" Title="Employee" %>

<%@ MasterType VirtualPath="~/CreditEditPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BootstrapContentPlaceHolder" runat="Server">
    <title>Employee</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">

    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-12 col-xl-6">
            <div class="input-group">
                <label style="width: 100%;">First Name</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="txtFirstName" runat="server" placeholder="First Name"></asp:TextBox>

            </div>


             <div class="input-group">
                <label style="width: 100%;">Last Name</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="txtLastname" runat="server" placeholder="Last Name"></asp:TextBox>

            </div>

            <div class="input-group">
                <label style="width: 100%;">Department</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="txtDepartment" runat="server" placeholder="Department"></asp:TextBox>

            </div>
            <div class="input-group">
                <label style="width: 100%;">Position</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="txtPosition" runat="server" placeholder="Position"></asp:TextBox>

            </div>

        </div>
    </div>


</asp:Content>
