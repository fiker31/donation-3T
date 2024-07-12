<%@ Page Language="C#" MasterPageFile="~/Credit.master" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs"
    Inherits="ErrorPage" Title="Error Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 52px;
        }

        .style2 {
            width: 165px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td class="style1"></td>
            <td style="width: auto" class="title">An Error has occured Please contact your Administrator.
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td class="style2"></td>
            <td>
                <asp:Label ID="lblMessage" runat="server" Style="text-align: center"
                    Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
