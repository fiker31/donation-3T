<%@ Page Language="C#" MasterPageFile="~/Credit.master" AutoEventWireup="true" CodeFile="BackUP.aspx.cs" Inherits="BackUP" Title="Back-Up Page" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Backup Database</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%--    <script type="text/javascript">
        var textSeparator = ",  ";
        function updateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(getSelectedItemsText(selectedItems));
        }
        function synchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = getValuesByTexts(texts);
            checkListBox.SelectValues(values);
            updateText(); // for remove non-existing texts
        }
        function getSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function getValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
    </script>--%>

    <div class="col-md-12">
        <div class="card card-secondary card-outline">
            <div class="card-header">
                <h3 class="card-title"><i class="fa fa-info-circle"></i>&nbsp; You can backup the Database below!</h3>
                <div class="card-tools">
                    <button type="button" class="btn btn-tool" data-card-widget="collapse">
                        <i class="fas fa-minus"></i>
                    </button>
                </div>
            </div>
            <div class="card-body">

                <%--                <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ASPxDropDownEdit1" Width="285px" runat="server" Theme="MaterialCompact" AnimationType="None">
                    <DropDownWindowStyle BackColor="#EDEDED" />
                    <DropDownWindowTemplate>
                        <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                            runat="server" Height="200px" EnableSelectAll="true" SelectedIndex="0">
                            <FilteringSettings ShowSearchUI="true" />
                            <Border BorderStyle="None" />
                            <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                            <Items>
                                <dx:ListEditItem Text="Chrome" Value="0" Selected="true" />
                                <dx:ListEditItem Text="Firefox" Value="1" />
                                <dx:ListEditItem Text="IE" Value="2" />
                                <dx:ListEditItem Text="Opera" Value="3" />
                                <dx:ListEditItem Text="Safari" Value="4" Selected="true" />
                                <dx:ListEditItem Text="Safari" Value="4" Selected="true" />
                                <dx:ListEditItem Text="Safari" Value="4" Selected="true" />
                                <dx:ListEditItem Text="Safari" Value="4" Selected="true" />
                                <dx:ListEditItem Text="Safari" Value="4" Selected="true" />
                                <dx:ListEditItem Text="Safari" Value="4" Selected="true" />
                            </Items>
                            <ClientSideEvents SelectedIndexChanged="updateText" Init="updateText" />
                        </dx:ASPxListBox>
                        <asp:SqlDataSource runat="server" ID="TeamMemberSqlDataSource" ConnectionString='<%$ ConnectionStrings:AppConnectionString %>' SelectCommand="SELECT [UserAccountName], [UserAccountId], FROM [EntUserAccount]"></asp:SqlDataSource>
                        <table style="width: 100%">
                            <tr>
                                <td style="padding: 4px">
                                    <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                        <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </DropDownWindowTemplate>
                    <ClientSideEvents TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
                </dx:ASPxDropDownEdit>--%>


                <table>
                    <tr>
                        <td colspan="4">
                            <table>
                                <tr>
                                    <td></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">

                                        <div class="input-group">
                                            <label style="width: 100%;">File Name</label>
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fas fa-file-alt"></i></span>
                                            </div>

                                            <asp:TextBox ID="txtFileName" CssClass="form-control" placeholder="File Name" runat="server" Width="400px"></asp:TextBox>

                                        </div>

                                    </td>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td colspan="2">

                                        <br />
                                        <div class="input-group">
                                            <label style="width: 100%;">Path</label>
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fas fa-folder"></i></span>
                                            </div>

                                            <asp:TextBox ID="txtPath" CssClass="form-control" placeholder="Path" runat="server" Width="400px"></asp:TextBox>

                                        </div>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <div class="form-group">
                                            <label>Select Database</label>
                                            <br />
                                            <asp:DropDownList ID="ddlDatabase" CssClass="form-control select2" Width="508px" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlDatabase_SelectedIndexChanged">
                                                <asp:ListItem Value="CBEProjectManagement">Main Database</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <div>
                                <div class="button">
                                    <br />
                                    <asp:LinkButton ID="btnCreateBackup" CssClass="btn btn-secondary" runat="server" Text="Create Backup"
                                        Width="150px" OnClick="btnCreateBackup_Click">
                                        <i class="fa fa-download"></i>Create Backup 
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
