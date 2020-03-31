<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="TermProject.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="row justify-content-center my-5">
        <div class="col-4">
            <div class="card p-3">
                <h5 class="card-title text-center">Log In</h5>
                <div class="card-body">
                    <div class="form-group">
                        <label for="<%= txtLogInEmail.ClientID %>">Username</label>
                        <asp:TextBox type="email" CssClass="form-control" ID="txtLogInEmail" placeholder="johndoe@gmail.com" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="<%= txtLogInPassword.ClientID %>">Password</label>
                        <asp:TextBox type="password" CssClass="form-control" ID="txtLogInPassword" placeholder="Password" runat="server" />
                    </div>
                    <div class="form-check">
                        <asp:CheckBox type="checkbox" CssClass="form-check-input" id="chkLogInRemember" runat="server"/>
                        <label class="form-check-label noselect" for="<%= chkLogInRemember.ClientID %>">Remember Me</label>
                    </div>
                      <asp:Button type="submit" class="btn btn-primary my-3" runat="server" Text="Submit" OnClick="btnLoginSubmit_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
</asp:Content>
