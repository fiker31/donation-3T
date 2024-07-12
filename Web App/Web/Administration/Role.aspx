<%@ Page Language="C#" MasterPageFile="~/CreditEditPage.master" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="Administration_Role" Title="Role " MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/CreditEditPage.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BootstrapContentPlaceHolder" runat="Server">
    <title>Roles</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td colspan="2">

                        <div class="input-group">
                            <label style="width: 100%;">Role Name</label>
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-user-lock"></i></span>
                            </div>


                            <asp:TextBox ID="txtRoleName" class="form-control" placeholder="Role Name" runat="server"></asp:TextBox>
                        </div>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">Grant rights for this role.</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Table ID="tblCapabilities" runat="server"></asp:Table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="lblUserHeader" runat="server"
                                        Text="Select the users for this role."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUsers" runat="server" Text="Users"></asp:Label></td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Users In Role"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ListBox ID="lstUnselectedUsers" class="list-group" runat="server" Rows="10" SelectionMode="Multiple"></asp:ListBox>
                                </td>
                                <td style="padding-left: 20px; padding-right: 20px;">
                                    <div style="margin-bottom: 10px;">

                                        <asp:LinkButton ID="btnMoveToSelected" Width="42px" CssClass="btn btn-secondary" runat="server" Text=">" OnClick="btnMoveToSelected_Click"><i class="fa fa-caret-right" ></i></asp:LinkButton>
                                    </div>


                                    <asp:LinkButton ID="btnMoveAllToSelected" CssClass="btn btn-secondary" runat="server" Text=">>"
                                        OnClick="btnMoveAllToSelected_Click"> <i class="fa fa-forward" ></i>  </asp:LinkButton>


                                    <br />
                                    <br />
                                    <div style="margin-bottom: 10px;">
                                        <asp:LinkButton ID="btnMoveToUnselected" Width="42px" CssClass="btn btn-secondary" runat="server" Text="<" OnClick="btnMoveToUnselected_Click"><i class="fa fa-caret-left" ></i></asp:LinkButton>
                                    </div>
                                    <asp:LinkButton ID="btnMoveAllToUnselected" CssClass="btn btn-secondary" runat="server" Text="<<"
                                        OnClick="btnMoveAllToUnselected_Click"><i class="fa fa-backward" ></i></asp:LinkButton>
                                    <br />
                                </td>
                                <td>
                                    <asp:ListBox ID="lstSelectedUsers" runat="server" Rows="10" SelectionMode="Multiple"></asp:ListBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
