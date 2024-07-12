<%@ Page Language="C#" MasterPageFile="~/CreditEditPage.master" AutoEventWireup="true" CodeFile="Donator_p.aspx.cs" Inherits="Basic_Donator_p" Title="Donator Profile" %>

<%@ MasterType VirtualPath="~/CreditEditPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BootstrapContentPlaceHolder" runat="Server">
    <title>Donator Profile</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">

    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-12 col-xl-6">
            <div class="input-group">
                <label style="width: 100%;">Full name</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="txtFullname" runat="server" placeholder="Full name"></asp:TextBox>

            </div>


             <div class="input-group">
                <label style="width: 100%;">Customer phone
</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="txtCustomerphone" runat="server" placeholder="Customer phone
"></asp:TextBox>

            </div>
             <div class="input-group">
                <label style="width: 100%;">Recursive donations
</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox1" runat="server" placeholder="Recursivedonations"></asp:TextBox>

            </div>
             

        </div>
    </div>


</asp:Content>
