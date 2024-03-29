﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="TermProject.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- Modal -->
    <div class="modal fade" id="modalForgotPassword" tabindex="-1" role="dialog" aria-labelledby="modalForgotPasswordLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalForgotPasswordLabel">Forgot Password?</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Enter your email address. If it's on file, you'll receive an email with a password recovery link.
          <asp:TextBox runat="server" CssClass="form-control" ID="txtPasswordRecoveryEmail"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSendRecovery_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="row justify-content-center my-5">
        <div class="col-4">
            <div class="card p-3">
                <h5 class="card-title text-center">Log In</h5>
                <div class="card-body">
                    <div class="form-group alert-danger p-4 rounded" runat="server" id="divInvalidInputs" visible="false">
                            Please enter a username and password.
                        </div>
                                            <div class="form-group alert-danger p-4 rounded" runat="server" id="divWrongCredentials" visible="false">
Username and password do not match what is on file. Please try again.                        </div>
                        <div class="form-group">
                            <label for="<%= txtLogInUsername.ClientID %>">Username</label>
                            <asp:TextBox type="text" CssClass="form-control" ID="txtLogInUsername" placeholder="Username" runat="server" />

                        </div>
                        <div class="form-group">
                            <label for="<%= txtLogInPassword.ClientID %>">Password</label>
                            <asp:TextBox type="password" CssClass="form-control" ID="txtLogInPassword" placeholder="Password" runat="server" />
                        </div>
                        <div class="form-group">
                            <a href="#" class="" data-toggle="modal" data-target="#modalForgotPassword">Forgot Password?</a>
                        </div>
                        <div class="form-check">
                            <asp:CheckBox type="checkbox" CssClass="form-check-input" ID="chkLogInRemember" runat="server" />
                            <label class="form-check-label noselect" for="<%= chkLogInRemember.ClientID %>">Remember Me</label>
                        </div>
                        <asp:Button type="submit" class="btn btn-primary my-3" runat="server" Text="Log In" OnClick="btnLoginSubmit_Click" />
                        <hr class="py-2" />
                        <asp:Button type="submit" CssClass="btn btn-secondary my-3" runat="server" Text="Debug: Login as Typed Username" OnClick="btnDebug3_Click" />
                        <asp:Button type="submit" CssClass="btn btn-secondary my-3" runat="server" Text="Debug: Login as Samantha" OnClick="btnDebug1_Click" />
                        <asp:Button type="submit" CssClass="btn btn-secondary my-3" runat="server" Text="Debug: Login as Thomas" OnClick="btnDebug2_Click" />
                        <hr class="py-2" />
                        <div>
                            <h5>Don't have an account?</h5>
                            <asp:LinkButton type="submit" class="btn btn-primary my-3" runat="server" Text="Create Account" href="CreateAccount.aspx" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
</asp:Content>
