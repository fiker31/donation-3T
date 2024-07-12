<%@ Page Language="C#" MasterPageFile="~/CreditEditPage.master" AutoEventWireup="true" CodeFile="Donationmade.aspx.cs" Inherits="Basic_Donationmade" Title="Donation made" %>

<%@ MasterType VirtualPath="~/CreditEditPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BootstrapContentPlaceHolder" runat="Server">
    <title>Donation made</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">

    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-12 col-xl-6">
            <div class="input-group">
                <label style="width: 100%;">Donation Id</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="txtDonationId" runat="server" placeholder="Donation Id"></asp:TextBox>

            </div>
               <div class="input-group">
                <label style="width: 100%;">Customer Phone</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox4" runat="server" placeholder="Customer Phone"></asp:TextBox>

            </div>

    
             <div class="input-group">
                <label style="width: 100%;">Customer Name

</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox1" runat="server" placeholder="Customer Name
"></asp:TextBox>

            </div>
             <div class="input-group">
                <label style="width: 100%;">Donation Amount

</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox2" runat="server" placeholder="Donation Amount

"></asp:TextBox>

            </div>
             <div class="input-group">
                <label style="width: 100%;">Donation Date

</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox3" runat="server" placeholder="Donation Date

e"></asp:TextBox>

            </div>
             

        </div>
    </div>


</asp:Content>
