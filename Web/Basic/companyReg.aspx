<%@ Page Language="C#" MasterPageFile="~/CreditEditPage.master" AutoEventWireup="true" CodeFile="companyReg.aspx.cs" Inherits="Basic_companyReg" Title="Company Registration" %>

<%@ MasterType VirtualPath="~/CreditEditPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BootstrapContentPlaceHolder" runat="Server">
    <title>Company Registration</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">

    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-12 col-xl-6">
            <div class="input-group">
                <label style="width: 100%;">Companyname</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox9" runat="server" placeholder="Companyname"></asp:TextBox>

            </div>


             <div class="input-group">
                <label style="width: 100%;">Company Description
</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox0" runat="server" placeholder="Company Description"></asp:TextBox>

            </div>
             <div class="input-group">
                <label style="width: 100%;">Location
</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox1" runat="server" placeholder="Location"></asp:TextBox>

            </div>
             <div class="input-group">
                <label style="width: 100%;">Phone
</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox2" runat="server" placeholder="Phone
"></asp:TextBox>

            </div>
             <div class="input-group">
                <label style="width: 100%;">POBOX
</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox3" runat="server" placeholder="POBOX
e"></asp:TextBox>

            </div>
             <div class="input-group">
                <label style="width: 100%;">GeneralManager</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox4" runat="server" placeholder="GeneralManager"></asp:TextBox>

            </div>
             <div class="input-group">
                <label style="width: 100%;">Tillnumber</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox5" runat="server" placeholder="Tillnumber"></asp:TextBox>

            </div>

        </div>
    </div>


</asp:Content>
