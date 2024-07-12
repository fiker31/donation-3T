<%@ Page Language="C#" MasterPageFile="~/Credit.master" AutoEventWireup="true" CodeFile="PurgeAuditLog.aspx.cs"
    Inherits="Administration_PurgeAuditLog" Title="Untitled Page" %>

<%@ Register Assembly="FrameworkControls" Namespace="FrameworkControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function ConfirmPurge() {
            return confirm("Do you want to permanently delete log records before the specified date?");
        }
    </script>
    <style type="text/css">
        .style2 {
            width: 107px;
            height: 40px;
        }

        .style3 {
            height: 40px;
        }

        .style4 {
            width: 297px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="PanelDateRange" runat="server" GroupingText="Purge audit data before"
        Width="323px">
        <table>
            <tr>
                <td>
                    <cc2:ValidationErrorMessages ID="ValidationErrorMessages1" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td class="style2">Event Date :
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtDate" runat="server" Enabled="true"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="txtDate_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtDate"></cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="txtDate"></cc1:CalendarExtender>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table>
        <tr>
            <td>
                <asp:Button ID="btnPurge" runat="server" Text="Purge"
                    OnClick="btnPurge_Click" OnClientClick="return ConfirmPurge();" />
            </td>
            <td>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                    CausesValidation="False" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
