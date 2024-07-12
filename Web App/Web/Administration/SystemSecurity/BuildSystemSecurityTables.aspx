<%@ Page Title="" Language="C#" MasterPageFile="~/Credit.master" AutoEventWireup="true"
    CodeFile="BuildSystemSecurityTables.aspx.cs" Inherits="Administration_SystemSecurity_BuildSystemSecurityTables" %>

<%@ Register Assembly="FrameworkControls" Namespace="FrameworkControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Regeneratechecked() {
            document.getElementById("ButtonOK").disabled = !document.getElementById("chkRegenerate").checked;
        }
        function hasLogTable() {
            document.getElementById("ButtonOK").disabled = 1;
            document.getElementById("chkRegenerate").checked = 0;
            document.getElementById("chkRegenerate").disabled = 0;
            document.getElementById("chkRegenerate").style.visibility = "visible";
            document.getElementById("ButtonOK").value = "Ok";
            document.getElementById("ButtonCancel").value = "Cancel";
        }
        function noLogTable() {
            document.getElementById("ButtonOK").disabled = 0;
            document.getElementById("chkRegenerate").style.visibility = "hidden";
            document.getElementById("ButtonOK").value = "Yes";
            document.getElementById("ButtonCancel").value = "No";
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
                <asp:Button ID="btnBuildSystemSecurityTables" runat="server" Text="Build System Security Tables" OnClick="btnBuildSystemSecurityTables_Click" />
                <br />
                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnBuildSystemSecurityTables"
                    OnClientCancel="cancelClick" DisplayModalPopupID="ModalPopupExtender1" />
                <br />
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnBuildSystemSecurityTables"
                    PopupControlID="PNLBuild" OkControlID="ButtonOK" CancelControlID="ButtonCancel"
                    BackgroundCssClass="modalBackground" />
                <asp:Panel ID="PNLBuild" runat="server" Style="display: none; width: 600px; background-color: White; border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                    <asp:Label ID="lblConfirmMessage1" runat="server" Font-Size="Medium" ForeColor="#FF3300"></asp:Label>
                    <br />
                    <input id="chkRegenerate" type="checkbox" onclick="javacript:Regeneratechecked();" />
                    <asp:Label ID="lblregenerate" runat="server"></asp:Label>
                    <%-- <asp:CheckBox ID="chkRegenerate" runat="server" Text="Re-generate the audit tables" AutoPostBack="false"
                    onclick="javacript:Regeneratechecked();"  OnCheckedChanged="chkRegenerate_CheckedChanged"/>--%>
                    <br />
                    <div style="text-align: right;">
                        <input id="ButtonOK" type="button" />
                        <%--<asp:Button ID="ButtonOK" runat="server" Text="Ok"  />--%>
                        <input id="ButtonCancel" type="button" />
                        <%--<asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />--%>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
