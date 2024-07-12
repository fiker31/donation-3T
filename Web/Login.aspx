<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" EnableEventValidation="false" Inherits="Login" %>

<%@ Register Assembly="FrameworkControls" Namespace="FrameworkControls" TagPrefix="cc1" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Log in</title>
    <link href="Images/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="Content/plugins/fontawesome-free/css/all.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="Content/dist/css/style.min.css">
    <link href="Content/Style.css" rel="stylesheet" />
</head>
<body class="hold-transition login-page">
    <script type="text/javascript">
        function GetKeyCode(evt) {
            var keyCode;
            if (evt.keyCode == 13) {
                __doPostBack('btnLogin', '');
            }
        }
    </script>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server">
            <div class="login-box">
                <div class="login-box">
                    <div class="login-logo">
                    </div>
                    <!-- /.login-logo -->
                    <div class="card card-purple card-outline bg-gradient-light">
                        <div class="card-body login-card-body">
                            <img alt="Commercial Bank of Ethiopia" style="width: 100%" src="Images/Logo Header.png" />
                            <asp:Label ID="Label2" runat="server" Style="color: #000066; font-size: 18px; text-align: center;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  CBE - Digital Ticketing System</asp:Label>
                            <hr />
                            <asp:Label ID="Label3" runat="server" Style="color: #000066; text-align: center;">&nbsp;&nbsp;&nbsp;&nbsp; Login here by providing your credentials</asp:Label>
                            <br />
                            <br />
                            <form>
                                <div class="col-md-12">

                                    <div class="input-group mb-3">
                                        <asp:TextBox class="form-control" onkeypress="GetKeyCode(event)" ID="txtLogin" placeholder="Username" runat="server"></asp:TextBox>
                                        <div class="input-group-append">
                                            <div class="input-group-text">
                                                <span class="fas fa-user"></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="input-group mb-3">
                                        <asp:TextBox class="form-control" onkeypress="GetKeyCode(event)" ID="txtPassword" placeholder="Password" type="Password" runat="server"></asp:TextBox>
                                        <div class="input-group-append">
                                            <div class="input-group-text">
                                                <span class="fas fa-lock"></span>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <br />
                                <div class="row">
                                    <div class="col-6">
                                        <div class="icheck-info">
                                            <asp:CheckBox ID="chkFullLoad" runat="server" />
                                            <label for="chkFullLoad">
                                                Full Load
                                            </label>
                                        </div>
                                    </div>
                                    <!-- /.col -->
                                    <div class="col-6">
                                        <asp:LinkButton ID="btnLogin" runat="server" class="btn btn-secondary btn-block" OnClick="btnLogin_Click"> <i class="fa fa-sign-in-alt" ></i> Sign In</asp:LinkButton>
                                    </div>
                                    <!-- /.col -->
                                    <cc1:ValidationErrorMessages ID="ValidationErrorMessages1" runat="server" />
                                </div>
                            </form>
                        </div>
                        <!-- /.login-card-body -->
                    </div>
                </div>
                <!-- /.login-box -->
            </div>
        </asp:Panel>
        <asp:Panel ID="panaelChangePassword" runat="server" Visible="false">
            <div class="login-box">
                <div class="login-box">
                    <!-- /.login-logo -->
                    <div class="card card-purple card-outline bg-gradient-light">
                        <div class="card-body login-card-body">
                            <img alt="Commercial Bank of Ethiopia" style="width: 100%" src="Images/Logo Header.png" />
                            <asp:Label runat="server" Style="color: #000066; text-align: center;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  CBE - Digital Ticketing System</asp:Label>
                            <hr />
                            <form>
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                <span class="login-box-msg">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Change Password</span>
                                <hr />
                                <div class="col-md-12">

                                    <div class="input-group mb-3">
                                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" MaxLength="10" class="form-control" placeholder="New Password"> </asp:TextBox>
                                        <div class="input-group-append">
                                            <div class="input-group-text">
                                                <span class="fas fa-lock"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="input-group mb-3">
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="10" class="form-control" placeholder="Confirm Password"></asp:TextBox>
                                        <div class="input-group-append">
                                            <div class="input-group-text">
                                                <span class="fas fa-lock"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-8">
                                        <asp:LinkButton ID="btnApply" runat="server" class="btn btn-secondary btn-block" OnClick="btnApply_Click"><i class="fa fa-check-circle" ></i> Apply</asp:LinkButton>
                                    </div>
                                    <!-- /.col -->
                                    <div class="col-4">
                                        <asp:LinkButton ID="btnCancel" runat="server" class="btn btn-secondary btn-block" OnClick="btnCancel_Click"> <i class="fa fa-times-circle" ></i>  Cancel</asp:LinkButton>
                                    </div>
                                    <!-- /.col -->
                                    <cc1:ValidationErrorMessages ID="ValidationErrorMessages2" runat="server" />
                                </div>
                            </form>
                        </div>
                        <!-- /.login-card-body -->
                    </div>
                </div>
                <!-- /.login-box -->
            </div>
        </asp:Panel>
        <div>
            <br />
            <hr />
            <asp:Label ID="Label1" runat="server" Style="color: #000066">© 
                                2022 | <i class="fa fa-code"></i> System Development &amp; Customization - Information System</asp:Label>
            <asp:Label ID="lblVersion" Style="color: #000066" Visible="true" runat="server"></asp:Label>

        </div>
    </form>
</body>
</html>
