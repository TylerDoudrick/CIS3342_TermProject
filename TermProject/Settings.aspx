<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="TermProject.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style>
        .divHidden {
            display: none;
        }

        .required {
            color: red;
        }

        .card-img-top {
            height: auto;
            width: 100%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="row">
        <div class="col-4">
            <asp:LinkButton runat="server" ID="lbChangeUsername" OnClick="lbChangeUsername_Click"> Change Username</asp:LinkButton><br />
            <asp:LinkButton runat="server" ID="lbChangePassword" OnClick="lbChangePassword_Click"> Change Password</asp:LinkButton>
            <br />
            <asp:LinkButton runat="server" ID="lbOptOut" OnClick="lbOptOut_Click"> Hide Profile</asp:LinkButton><br />
            <asp:LinkButton runat="server" ID="lbBlockedUsers" OnClick="lbBlockedUsers_Click"> Blocked Users</asp:LinkButton><br />
        </div>

        <div>
            <div runat="server" id="divChangeUsername" class="divHidden">
                <div class="col">
                    <div class="row">
                        <asp:Label runat="server" ID="lblCurrentUsername" for="<%= txtCurrentUsername.ClientID %>"> Current Username </asp:Label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCurrentUsername" ReadOnly="true"> </asp:TextBox>
                    </div>
                    <br />

                    <div class="row">
                        <asp:Label runat="server" ID="lblUsernameError" CssClass="text-danger font-weight-bold"></asp:Label>
                    </div>

                    <div class="row">
                        <asp:Label runat="server" ID="lblNewUsername" for="<%= txtNewUsername.ClientID %>"> New Username </asp:Label>
                        <span class="required">*</span>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNewUsername"> </asp:TextBox>
                    </div>
                    <br />

                    <div class="row">
                        <asp:Button runat="server" ID="btnUpdateUsername" Text="Update" CssClass="btn btn-success" OnClick="btnUpdateUsername_Click" />
                    </div>

                </div>

            </div>


            <div runat="server" id="divChangePassword" class="divHidden">
                <div class="col">
                    <div class="row">
                        <asp:Label runat="server" ID="lblCurrentPassword" for="<%= txtCurrentPassword.ClientID %>"> Current Password </asp:Label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCurrentPassword" ReadOnly="true"> </asp:TextBox>
                    </div>
                    <br />
                    <div class="row">
                        <asp:Label runat="server" ID="lblPasswordError" CssClass="text-danger font-weight-bold"></asp:Label>
                    </div>
                    <div class="row">
                        <asp:Label runat="server" ID="lblNewPassword" for="<%= txtNewPassword.ClientID %>"> New Password </asp:Label>
                        <span class="required">*</span>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNewPassword"> </asp:TextBox>
                    </div>
                    <br />

                    <div class="row">
                        <asp:Button runat="server" ID="btnUpdatePassword" Text="Update" CssClass="btn btn-success" OnClick="btnUpdatePassword_Click" />
                    </div>
                </div>

            </div>
            <div runat="server" id="divOPTOut" class="divHidden">
                <div class="col">
                    <div class="row">
                        <asp:Label runat="server" ID="lOptOut"> Hide my profile from other users</asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton runat="server" ID="rbYes" Text="Yes" />&nbsp;&nbsp;
                        <asp:RadioButton runat="server" ID="rbNo" Text="No" Checked="true" />
                    </div>
                </div>
            </div>

            <div runat="server" id="divBlockedUsers" class="divHidden">
                <div class="col ">
                    <asp:LinkButton runat="server" class="row">
                        <div class="card w-50">
                            <asp:Image runat="server" CssClass="card-img-top img-thumbnail" ImageURL="https://www.skymania.com/wp/wp-content/uploads/2011/06/sun_with_prominence.jpg"/>
                            <div class="card-body ">
                                <h5 class="card-title text-capitalize"> Sam Smith, 25</h5>
                                <p class="card-text"> This is the tagline</p>
                            </div>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>

        </div>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
</asp:Content>
