<%@ Page Language="C#" MasterPageFile="~/Credit.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="_Report" Title="Report" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 300px;
            height: 141px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Welcome to CBE </h1>
    <h3>Automated School Managment System</h3>
    <p>Please visit this page later! We are making changes!</p>
    <p>
        <img alt="Coming Soon!" class="auto-style1" src="../images/underdev.jpg" />
    </p>
    <hr />
    <div style="position: relative;">
    </div>
    <hr />
    <asp:TreeView ID="tvMenuDescriptions" runat="server" Visible="false">
    </asp:TreeView>
</asp:Content>
