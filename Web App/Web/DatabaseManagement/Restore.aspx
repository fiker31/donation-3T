<%@ Page Language="C#" MasterPageFile="~/Credit.master" AutoEventWireup="true" CodeFile="Restore.aspx.cs" Inherits="DatabaseManagement_Restore" Title="Restore Page" %>

<%@ Register Assembly="FrameworkControls" Namespace="FrameworkControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Restore Database</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="card card-secondary card-outline">
                    <div class="card-header">
                        <h3 class="card-title"><i class="fa fa-info-circle"></i>&nbsp; You can restore the Database below!</h3>
                        <div class="card-tools">
                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                <i class="fas fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="card-body">


                        <table>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <cc1:ValidationErrorMessages ID="ValidationErrorMessages1" runat="server" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>List of Avialable Backup Files:</td>

                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td rowspan="3">
                                    <hr />
                                    <br />
                                    <asp:ListBox ID="lstBackupFiles" runat="server" AutoPostBack="True"
                                        Height="235px" OnSelectedIndexChanged="lstBackupFiles_SelectedIndexChanged"
                                        Width="190px"></asp:ListBox>
                                </td>
                                <td></td>
                                <td colspan="2">
                                    <label>Database:</label>
                                    <br />

                                    <asp:DropDownList ID="ddlDatabase" CssClass="form-control select2" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDatabase_SelectedIndexChanged" Width="400px">
                                        <asp:ListItem Value="CBEProjectManagement">Main DataBase</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td colspan="2">

                                    <div class="input-group">
                                        <label style="width: 100%;">Selected File:</label>
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class="fas fa-file-alt"></i></span>
                                        </div>

                                        <asp:TextBox ID="txtSelectedFile" CssClass="form-control" runat="server" ReadOnly="True" Width="270px"></asp:TextBox>

                                    </div>

                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>
                                    <div>
                                        <div class="button">
                                            <asp:LinkButton ID="btnRestore" CssClass="btn btn-secondary" runat="server" OnClientClick="return confirm('Are you sure you want to restore the database');"
                                                Text="Restore Database" Width="173px" OnClick="btnRestore_Click"><i class="fa fa-undo-alt" ></i>  Restore Database</asp:LinkButton>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
