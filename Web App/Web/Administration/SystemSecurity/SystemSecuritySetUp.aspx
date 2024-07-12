<%@ Page Title="" Language="C#" MasterPageFile="~/Credit.master" AutoEventWireup="true"
    CodeFile="SystemSecuritySetUp.aspx.cs" Inherits="Administration_SystemSecurity_SystemSecuritySetUp" %>

<%@ Register Assembly="FrameworkControls" Namespace="FrameworkControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <cc1:ValidationErrorMessages ID="ValidationErrorMessages1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:ValidationWarnings_Info ID="ValidationWarnings_Info1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lnkBuildSystemSecurityTables" runat="server" OnClick="lnkBuildSystemSecurityTables_Click">Build System Security Tables</asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelSystemSecurity" runat="server">
        <asp:Panel ID="PanelPassword" GroupingText="Password Strength"
            runat="server" Width="618px">
            <table>
                <tr>
                    <td>Minimum Login Id Length :
                    </td>
                    <td>
                        <asp:TextBox ID="txtMinLoginIdLength" runat="server" MaxLength="2"></asp:TextBox>
                        <cc2:FilteredTextBoxExtender ID="txtMinLoginIdLength_FilteredTextBoxExtender"
                            runat="server" Enabled="True" FilterType="Numbers"
                            TargetControlID="txtMinLoginIdLength"></cc2:FilteredTextBoxExtender>
                    </td>
                    <td>Password History No :
                    </td>
                    <td>
                        <asp:TextBox ID="txtPasswordHistory" runat="server" MaxLength="2"></asp:TextBox>
                        <cc2:FilteredTextBoxExtender ID="txtPasswordHistory_FilteredTextBoxExtender"
                            runat="server" Enabled="True" FilterType="Numbers"
                            TargetControlID="txtPasswordHistory"></cc2:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>Allowed Unsuccessful Attempts :
                    </td>
                    <td>
                        <asp:TextBox ID="txtUnsuccessfulAttempt" runat="server" MaxLength="2"
                            ToolTip="Enter 0 to disable."></asp:TextBox>
                        <cc2:FilteredTextBoxExtender ID="txtUnsuccessfulAttempt_FilteredTextBoxExtender"
                            runat="server" Enabled="True" FilterType="Numbers"
                            TargetControlID="txtUnsuccessfulAttempt"></cc2:FilteredTextBoxExtender>
                    </td>
                    <td>Password Interval Days:             </td>
                    <td>
                        <asp:TextBox ID="txtPasswordInterval" runat="server" MaxLength="2"
                            ToolTip="Password Expiry Intervals, enter 0 if password never expires"></asp:TextBox>
                        <cc2:FilteredTextBoxExtender ID="txtPasswordInterval_FilteredTextBoxExtender"
                            runat="server" Enabled="True" FilterType="Numbers"
                            TargetControlID="txtPasswordInterval"></cc2:FilteredTextBoxExtender>
                    </td>
                </tr>
            </table>
            <table title="Password">
                <tr>
                    <td>Minimum Length :
                    </td>
                    <td>
                        <asp:TextBox ID="txtMinPassowrdLength" runat="server" MaxLength="2"></asp:TextBox>
                        <cc2:FilteredTextBoxExtender ID="txtMinPassowrdLength_FilteredTextBoxExtender"
                            runat="server" Enabled="True" FilterType="Numbers"
                            TargetControlID="txtMinPassowrdLength"></cc2:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>Password Must Have :
                    </td>
                    <td>
                        <asp:CheckBox ID="chkDigit" runat="server" Width="238px" Text="Digit" />
                        <asp:CheckBox ID="chkLowerCase" runat="server" Width="238px" Text="Lower Case Character" /><br />
                        <asp:CheckBox ID="chkSpecial" runat="server" Width="238px" Text="Special Character" />
                        <asp:CheckBox ID="chkUpperCase" runat="server" Width="238px" Text="Upper Case Character" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server" GroupingText="Working Hour Definition">
            <table>
                <tr>
                    <td>From :
                    </td>
                    <td>
                        <asp:TextBox ID="txtWorkingHourFrom" runat="server"></asp:TextBox>
                    </td>
                    <td>To :
                    </td>
                    <td>
                        <asp:TextBox ID="txtWorkingHourTo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>For Saturday :
                    </td>
                    <td>
                        <asp:TextBox ID="txtWorkingHourToSat" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkAllowWorkOnHoliday" runat="server" />
                    </td>
                    <td>Allow Work on Holiday :
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkAllowWorkOnWeekend" runat="server" />
                    </td>
                    <td>Allow Work on Weekend :
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkAllowWorkAfterWorkingHours" runat="server" />
                    </td>
                    <td>Allow Work After Working Hour :
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnEdit" runat="server" Text="Change" OnClick="btnEdit_Click" />
                </td>
                <td>
                    <asp:Button ID="btnApply" runat="server" Text="Apply" Enabled="false"
                        OnClick="btnApply_Click" />
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Enabled="false"
                        OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
