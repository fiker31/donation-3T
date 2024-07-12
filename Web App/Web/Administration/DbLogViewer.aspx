<%@ Page Language="C#" MasterPageFile="~/Credit.master" AutoEventWireup="true" CodeFile="DbLogViewer.aspx.cs"
    Inherits="Administration_DbLogViewer" Title="Data Audit Log Viewer" %>

<%@ Register Assembly="FrameworkControls" Namespace="FrameworkControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td>
                <cc1:ValidationErrorMessages ID="ValidationErrorMessages1" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelFilter" runat="server" GroupingText="Filter">
        <table style="width: 693px">
            <tr id="MainContainer">
                <td id="leftContainer">
                    <table>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkTable" runat="server" Text="Object : "
                                    OnCheckedChanged="chkTable_CheckedChanged" AutoPostBack="True" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTable" runat="server" Enabled="false" Width="234px"
                                    OnSelectedIndexChanged="ddlTable_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkColumn" runat="server" Text="Field : " Enabled="False"
                                    AutoPostBack="True" OnCheckedChanged="chkColumn_CheckedChanged" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlColumn" runat="server" Enabled="false" Width="235px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkUserName" runat="server" Text="User Name : "
                                    AutoPostBack="True" OnCheckedChanged="chkUserName_CheckedChanged" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUserName" runat="server" Enabled="false" Width="233px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Row Key :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRowKey" runat="server" Width="234px" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td id="rightContainer">
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="PanelEvent" runat="server" GroupingText="Event Type" Width="323px">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBoxList ID="cblEvent" runat="server" Height="25px" RepeatDirection="Horizontal"
                                                    Width="303px">
                                                    <asp:ListItem Selected="True">Insert</asp:ListItem>
                                                    <asp:ListItem Selected="True">Update</asp:ListItem>
                                                    <asp:ListItem Selected="True">Delete</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="PanelDateRange" runat="server" GroupingText="Date Range" Width="323px">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkDateAfter" runat="server" Text="Occured After : "
                                                    AutoPostBack="True" OnCheckedChanged="chkDateAfter_CheckedChanged" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDateAfter" runat="server" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkDateBefore" runat="server" Text="Occured Before  : "
                                                    AutoPostBack="True" OnCheckedChanged="chkDateBefore_CheckedChanged" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDateBefore" runat="server" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table>
        <tr>
            <td>
                <asp:Button ID="btnView" runat="server" Text="Run" OnClick="btnView_Click" />
            </td>
            <td>
                <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click"
                    CausesValidation="False" />
            </td>
            <td>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False"
                    OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:GridView ID="gvQuery" runat="server" AllowPaging="True" CellPadding="3" PageSize="25"
                    EmptyDataText="Matching record not Found." OnPageIndexChanged="gvQuery_PageIndexChanged"
                    OnPageIndexChanging="gvQuery_PageIndexChanging">
                    <RowStyle CssClass="gridViewRow"></RowStyle>
                    <PagerStyle CssClass="gridViewPager"></PagerStyle>
                    <HeaderStyle CssClass="gridViewHeader"></HeaderStyle>
                    <AlternatingRowStyle CssClass="gridViewAlternatingRow"></AlternatingRowStyle>
                    <SelectedRowStyle BackColor="LightBlue" Font-Bold="true" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
