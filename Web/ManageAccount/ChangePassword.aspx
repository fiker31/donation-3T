<%@ Page Language="C#" MasterPageFile="~/Credit.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs"
    Inherits="ManageAccount_ChangePassword" Title="Change Password Page" %>

<%@ Register Assembly="FrameworkControls" Namespace="FrameworkControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Credit.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="card card-secondary card-outline">
            <div class="card-header">
                <h3 class="card-title"><i class="fa fa-info-circle"></i>&nbsp; Please enter the record below!</h3>
                <div class="card-tools">
                    <button type="button" class="btn btn-tool" data-card-widget="collapse">
                        <i class="fas fa-minus"></i>
                    </button>
                </div>
            </div>
            <div class="card-body">
                <div class="row">
                    <cc1:ValidationErrorMessages ID="ValidationErrorMessages1" runat="server" />
                    <div class="col-md-4">


                        <div class="input-group">
                            <label style="width: 100%;">Old Password</label>
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-lock"></i></span>
                            </div>


                            <asp:TextBox class="form-control" ID="txtOldPassword" placeholder="Old Password" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                        </div>


                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPassword" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">

                        <div class="input-group">
                            <label style="width: 100%;">New Password</label>
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-lock"></i></span>
                            </div>


                            <asp:TextBox class="form-control" placeholder="Password" ID="txtPassword" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                        </div>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">

                        <div class="input-group">
                            <label style="width: 100%;">Confirm Password</label>
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-lock"></i></span>
                            </div>


                            <asp:TextBox class="form-control" ID="txtConfirmPassword" placeholder="Confirm Password" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                        </div>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <asp:LinkButton ID="btnApply" Width="50%" CssClass="btn btn-secondary" runat="server" Text="Apply" OnClick="btnApply_Click"> <i class="fa fa-lock" ></i>  Apply </asp:LinkButton>
                        <asp:Button ID="btnCancel" Width="50%" CssClass="btn btn-secondary" runat="server" Text="Cancel" OnClick="btnCancel_Click" Visible="False" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
