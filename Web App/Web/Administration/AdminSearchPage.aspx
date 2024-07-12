<%@ Page Language="C#" MasterPageFile="~/CreditSearchPage.master" AutoEventWireup="true" CodeFile="AdminSearchPage.aspx.cs" Inherits="Administration_AdminSearchPage" Title="Administration Search Page" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="FrameworkControls" Namespace="FrameworkControls" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="row">

        <div class="col-md-12">

            <dx:ASPxGridView ID="dxgvSearch" runat="server" Width="100%" Theme="MaterialCompact">
                <SettingsPager PageSize="10" AlwaysShowPager="true"></SettingsPager>

                <Styles>
                    <FocusedRow BackColor="#3C8DBC">
                    </FocusedRow>
                </Styles>
                <Settings ShowFilterBar="Visible" ShowHeaderFilterButton="True" ShowFilterRow="True" ShowFilterRowMenu="True" HorizontalScrollBarMode="Visible" ColumnMinWidth="1" ShowPreview="True" />
                <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" AllowFocusedRow="True" AllowDragDrop="False" AllowSort="true" />
                <SettingsResizing ColumnResizeMode="Control" />
                <SettingsPopup>
                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                </SettingsPopup>
                <SettingsSearchPanel Visible="True" Delay="120" />
                <SettingsLoadingPanel Mode="ShowAsPopup" />
            </dx:ASPxGridView>
        </div>
        <div class="col-md-7">
            <br />
            <asp:LinkButton ID="btnLoad" CssClass="btn btn-secondary" runat="server" Text="Load Data" OnClick="btnLoad_Click">
                    <i class="fa fa-arrow-alt-circle-left" ></i>&nbsp;&nbsp;Load
            </asp:LinkButton>
            <asp:LinkButton ID="btnCancel" CssClass="btn btn-secondary" runat="server" Text="Load Data" OnClick="btnCancel_Click">
                    <i class="fa fa-times-circle" ></i>&nbsp;&nbsp;Cancel
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
