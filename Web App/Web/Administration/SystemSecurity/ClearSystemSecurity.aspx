<%@ Page Title="" Language="C#" MasterPageFile="~/Credit.master" AutoEventWireup="true"
    CodeFile="ClearSystemSecurity.aspx.cs" Inherits="Administration_SystemSecurity_ClearSystemSecurity" %>

<%@ Register Assembly="FrameworkControls" Namespace="FrameworkControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function hasLogTableToClear() {
            document.getElementById("ButtonYes").value = "Yes";
            document.getElementById("ButtonNo").value = "No";
            document.getElementById("ButtonYes").style.visibility = "visible";
        }
        function noLogTableToClear() {
            document.getElementById("ButtonYes").style.visibility = "hidden";
            document.getElementById("ButtonYes").disabled = 1;
            document.getElementById("ButtonYes").value = "";
            document.getElementById("ButtonNo").value = "Ok";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    </table>
    <table>
        <tr>
            <td>
                <asp:Button ID="btnClearSystemSecurity" runat="server" Text="Clear System Security" OnClick="btnClearSystemSecurity_Click" />
                <br />
                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnClearSystemSecurity"
                    OnClientCancel="cancelClick" DisplayModalPopupID="ModalPopupExtender2" />
                <br />
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnClearSystemSecurity"
                    PopupControlID="PNLClear" OkControlID="ButtonYes" CancelControlID="ButtonNo"
                    BackgroundCssClass="modalBackground" />
                <asp:Panel ID="PNLClear" runat="server" Style="display: none; width: 500px; background-color: White; border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                    <asp:Label ID="lblConfirmMessage2" runat="server" Font-Size="Medium" ForeColor="#FF3300"></asp:Label>
                    <br />
                    <br />
                    <div style="text-align: right;">
                        <input id="ButtonYes" type="button" />
                        <%--<asp:Button ID="ButtonYes" runat="server" Text="Yes" />--%>
                        <%--<asp:Button ID="ButtonNo" runat="server" Text="No" />--%>
                        <input id="ButtonNo" type="button" />
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
