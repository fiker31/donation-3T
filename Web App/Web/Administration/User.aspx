<%@ Page Language="C#" MasterPageFile="~/CreditEditPage.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="Administration_User" Title="User Registration" %>

<%@ MasterType VirtualPath="~/CreditEditPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BootstrapContentPlaceHolder" runat="Server">
    <title>Users</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-12 col-xl-3">
            <label class="control-label">Select Branch</label>
            <asp:DropDownList class="form-control select2" ID="cbobranch" runat="server">
            </asp:DropDownList>
        </div>

        <div class="col-lg-3 col-md-3 col-sm-12 col-xl-3">
            <div class="input-group">
                <label style="width: 100%;">Username</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-user-alt"></i></span>
                </div>

                <asp:TextBox class="form-control" placeholder="Username" ID="txtUserAName" runat="server" MaxLength="50"></asp:TextBox>

            </div>

        </div>
    </div>

    <br />

    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-12 col-xl-3">

            <div class="input-group">
                <label style="width: 100%;">First Name</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-user-alt"></i></span>
                </div>

                <asp:TextBox class="form-control" placeholder="First Name" ID="txtFirstName" runat="server" MaxLength="50"></asp:TextBox>

            </div>
        </div>

        <div class="col-lg-3 col-md-3 col-sm-12 col-xl-3">
            <div class="input-group">
                <label style="width: 100%;">Father Name</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-user-alt"></i></span>
                </div>

                <asp:TextBox class="form-control" placeholder="Father Name" ID="txtFatherName" runat="server" MaxLength="50"></asp:TextBox>

            </div>
        </div>
    </div>
    <br />

    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-12 col-xl-3">

            <div class="input-group">
                <label style="width: 100%;">Position</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-user-graduate"></i></span>
                </div>


                <asp:TextBox class="form-control" placeholder="Position" ID="txtPosition" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="col-lg-3 col-md-3 col-sm-12 col-xl-3">
            <div class="input-group">
                <label style="width: 100%;">Department</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-user-alt"></i></span>
                </div>


                <asp:TextBox class="form-control" placeholder="Department" ID="txtDepartment" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>
    <br />

    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-12 col-xl-3">
            <div class="input-group">
                <label style="width: 100%;">E-Mail</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-envelope-square"></i></span>
                </div>


                <asp:TextBox class="form-control" placeholder="E-Mail" ID="txtEmail" runat="server" MaxLength="100"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-12 col-xl-3">
            <div class="input-group">
                <label style="width: 100%;">&nbsp;</label>

                <label>Is Active User :&nbsp;&nbsp;</label>

                <asp:CheckBox ID="chkActive" runat="server" Text="  " TextAlign="Left" />

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <label>Is Locked:   &nbsp;  &nbsp;  </label>

                <asp:CheckBox ID="chkLocked" runat="server" Text="  " TextAlign="Left" />
            </div>
        </div>
    </div>

    <br />

    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-12 col-xl-3">
            <div class="input-group">
                <label style="width: 100%;">Password</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-lock"></i></span>
                </div>


                <asp:TextBox class="form-control" placeholder="Password" autocomplete="new-password" ID="txtPWD" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
            </div>
        </div>

        <div class="col-lg-3 col-md-3 col-sm-12 col-xl-3">
            <div class="input-group">
                <label style="width: 100%;">Confirm Password</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-lock"></i></span>
                </div>

                <asp:TextBox class="form-control" placeholder="Confirm Password" ID="txtConfirmPassword" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>

            </div>
        </div>
    </div>
</asp:Content>
