<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AccountRecovery.aspx.cs" Inherits="TermProject.AccountRecovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
        <div class="row justify-content-center my-5">
        <div class="col-4">
            <div class="card p-3" id="divSecurityQuestion" runat="server">
                <h5 class="card-title text-center">Account Recovery</h5>
                <div class="card-body">
                    <div class="form-group mb-4">
                        <h5>Please answer the security question below.</h5>
                    </div>
                    <div class="form-group">
                        <p>Question: What is your mother's maiden name?</p>
                        <label for="<%= txtAnswer.ClientID %>">Answer:</label>
                        <asp:TextBox type="text" CssClass="form-control" ID="txtAnswer" placeholder="Answer" runat="server" />
                    </div>
                  
                    <asp:Button type="submit" class="btn btn-primary my-3" runat="server" Text="Submit"  onClick="btnSubmitQuestion_Click"/>
                </div>
            </div>     
            <div class="card p-3" id="divChangePassword" visible="false" runat="server">
                <h5 class="card-title text-center">Change Password</h5>
                <div class="card-body">
                    <div class="form-group mb-4">
                        <h5>Please enter a new password</h5>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtNewPass.ClientID %>">New Password:</label>
                        <asp:TextBox type="text" CssClass="form-control" ID="txtNewPass" placeholder="New Password" runat="server" />
                    </div>         
                    <div class="form-group">
                        <label for="<%= txtConfirmPass.ClientID %>">Confirm Password:</label>
                        <asp:TextBox type="text" CssClass="form-control" ID="txtConfirmPass" placeholder="Confirm Password" runat="server" />
                    </div>
                  
                    <asp:Button type="submit" class="btn btn-primary my-3" runat="server" Text="Change Password" OnClick="btnChangePass_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
</asp:Content>
