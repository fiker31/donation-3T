<%@ Page Language="C#" MasterPageFile="~/Credit.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Home Page" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Home</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" runat="server" id="ParentPanel">
        <div class="col-lg-3 col-md-6 col-sm-12">
            <div class="small-box bg-light">
                <div class="inner">
                    <h3><sup style="font-size: 20px">21</sup></h3>
                    <p><i class="fa fa-pause-circle"></i>&nbsp; Ongoing Projects</p>
                </div>
                <div class="icon">
                    <i class="fa fa-pause-circle mr-2" style="color: orange;"></i>
                </div>
                <a href="../Home/Default.aspx" style="color: #3C8DBC !important;" class="small-box-footer">View Detail <i class="fas fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <div class="col-lg-3 col-md-6 col-sm-12">
            <div class="small-box bg-light">
                <div class="inner">
                    <h3><sup style="font-size: 20px">21</sup></h3>
                    <p><i class="fa fa-play-circle"></i>&nbsp; Pending Projects</p>
                </div>
                <div class="icon">
                    <i class="fa fa-play-circle mr-2" style="color: grey;"></i>
                </div>
                <a href="../Home/Default.aspx" style="color: #3C8DBC !important;" class="small-box-footer">View Detail <i class="fas fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <div class="col-lg-3 col-md-6 col-sm-12">
            <div class="small-box bg-light">
                <div class="inner">
                    <h3><sup style="font-size: 20px">21</sup></h3>
                    <p><i class="fa fa-check-circle"></i>&nbsp; Closed Projects</p>
                </div>
                <div class="icon">
                    <i class="fa fa-check-circle mr-2" style="color: green;"></i>
                </div>
                <a href="../Home/Default.aspx" style="color: #3C8DBC !important;" class="small-box-footer">View Detail <i class="fas fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <div class="col-lg-3 col-md-6 col-sm-12">
            <div class="small-box bg-light">
                <div class="inner">
                    <h3><sup style="font-size: 20px">21</sup></h3>
                    <p><i class="fa fa-tasks"></i>&nbsp; All Projects</p>
                </div>
                <div class="icon">
                    <i class="fa  fa-tasks" style="color: #3C8DBC;"></i>
                </div>
                <a href="../Home/Default.aspx" style="color: #3C8DBC !important;" class="small-box-footer">View Detail <i class="fas fa-arrow-circle-right"></i></a>
            </div>
        </div>


    </div>

    <asp:TreeView ID="tvMenuDescriptions" runat="server" Visible="false">
    </asp:TreeView>
</asp:Content>
