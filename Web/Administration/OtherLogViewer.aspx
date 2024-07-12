<%@ Page Language="C#" MasterPageFile="~/Credit.master" AutoEventWireup="true" CodeFile="OtherLogViewer.aspx.cs"
    Inherits="Administration_OtherLogViewer" Title="Untitled Page" %>

<%@ Register Assembly="FrameworkControls" Namespace="FrameworkControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
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
    <table>
        <tr>
            <td>
                <asp:Panel ID="PanelType" runat="server">
                    <table>
                        <tr>
                            <td>
                                <%--<asp:RadioButtonList ID="rblDisplay" runat="server" AutoPostBack="False" 
            Width="171px">
            <asp:ListItem Selected="True" Value="LH">Login History</asp:ListItem>
            <asp:ListItem Value="PL">Process Log</asp:ListItem>
        </asp:RadioButtonList>--%>
                                <asp:CheckBox ID="chkUserId" runat="server" Text="User Id : " AutoPostBack="True"
                                    OnCheckedChanged="chkUserId_CheckedChanged" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserId" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkUserHost" runat="server" Text="User Host: " AutoPostBack="True"
                                    OnCheckedChanged="chkUserHost_CheckedChanged" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserHost" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td>
                <asp:Panel ID="PanelDateRange" runat="server" GroupingText="Date Range" Width="323px">
                    <table>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkDateAfter" runat="server" Text="Occured After : " AutoPostBack="True"
                                    OnCheckedChanged="chkDateAfter_CheckedChanged" TabIndex="1" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateAfter" runat="server" Enabled="False" TabIndex="1"></asp:TextBox>
                                <cc2:MaskedEditExtender ID="txtDateAfter_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDateAfter"
                                    UserDateFormat="DayMonthYear"></cc2:MaskedEditExtender>
                                <cc2:CalendarExtender ID="txtDateAfter_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd/MM/yyyy" TargetControlID="txtDateAfter"></cc2:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkDateBefore" runat="server" Text="Occured Before  : " AutoPostBack="True"
                                    OnCheckedChanged="chkDateBefore_CheckedChanged" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateBefore" runat="server" Enabled="False" TabIndex="1"></asp:TextBox>
                                <cc2:MaskedEditExtender ID="txtDateBefore_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDateBefore"
                                    UserDateFormat="DayMonthYear"></cc2:MaskedEditExtender>
                                <cc2:CalendarExtender ID="txtDateBefore_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd/MM/yyyy" TargetControlID="txtDateBefore"></cc2:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
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
