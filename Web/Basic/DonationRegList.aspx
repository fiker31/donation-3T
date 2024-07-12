<%@ Page Language="C#" MasterPageFile="~/CreditEditPage.master" AutoEventWireup="true" CodeFile="DonationRegList.aspx.cs" Inherits="Basic_DonationRegList" Title="Donation List Registration" %>

<%@ MasterType VirtualPath="~/CreditEditPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BootstrapContentPlaceHolder" runat="Server">
    <title>Donation Registration</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">

    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-12 col-xl-6">
            <div class="input-group">
                <label style="width: 100%;">Company ID</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="txtCompanyID" runat="server" placeholder="Company ID"></asp:TextBox>

            </div>
            </div>
         <div class="col-6">
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 col-xl-6">
                    <div class="input-group">
                        <label style="width: 100%;">Poster</label>
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fas fa-file-alt"></i></span>
                        </div>
                        <asp:FileUpload runat="server" class="form-control" ID="filePoster" />
                    </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 col-xl-6">
                    <div class="input-group">
                        <label id="txtImageStatus" runat="server" style="width: 100%;">Click here to Upload</label>

                        <asp:LinkButton runat="server" ID="btnUpload" OnClick="btnUpload_Click" CssClass="btn btn-secondary btn-block"><i class="fa fa-upload" ></i> Upload</asp:LinkButton>
                    </div>
                </div>
            </div>

        </div>
        <div class="col-6">
            <asp:Image ID="imgposter" runat="server" Height="100%" AlternateText="Image will be displayed here." Width="100%" />
        </div>

            <div class="col-lg-6 col-md-6 col-sm-12 col-xl-6">
            <div class="input-group">
                <label style="width: 100%;">Donation Title</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox9" runat="server" placeholder="Donation Title"></asp:TextBox>

            

            </div>
             <div class="input-group">
                <label style="width: 100%;">Donation Description

</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox1" runat="server" placeholder="Donation Description
"></asp:TextBox>

            </div>
             <div class="input-group">
                <label style="width: 100%;">Donation Amount Required

</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox2" runat="server" placeholder="Donation Amount Required

"></asp:TextBox>

            </div>
             <div class="input-group">
                <label style="width: 100%;">Donation End Date

</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox3" runat="server" placeholder="Donation End Date

e"></asp:TextBox>

            </div>
             <div class="input-group">
                <label style="width: 100%;">Special Short Code
</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox4" runat="server" placeholder="Special Short Code
"></asp:TextBox>

            </div>
             <div class="input-group">
                <label style="width: 100%;">Donation Related Links
</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox5" runat="server" placeholder="Donation Related Links
"></asp:TextBox>

            </div>
            <div class="input-group">
                <label style="width: 100%;">SMS Message For Donator

</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox6" runat="server" placeholder="SMS Message For Donator

"></asp:TextBox>

            </div>
             <div class="input-group">
                <label style="width: 100%;">Email Message for Donator


</label>
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="fas fa-tasks"></i></span>
                </div>

                <asp:TextBox class="form-control" ID="TextBox7" runat="server" placeholder="Email Message for Donator


"></asp:TextBox>

            </div>

        </div>
    </div>


</asp:Content>
