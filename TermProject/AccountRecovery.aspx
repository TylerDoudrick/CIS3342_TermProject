<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AccountRecovery.aspx.cs" Inherits="TermProject.AccountRecovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="row justify-content-center my-5">
        <div class="col-4">
            <div class="card p-3" id="divVerificationCode" runat="server">
                <h5 class="card-title text-center">Account Recovery</h5>
                <div class="card-body">
                    <div class="form-group alert-danger p-4 rounded" runat="server" id="divInvalidCode" visible="false">
                        Invalid code. Please try again.                       
                    </div>
                    <div class="form-group mb-4">
                        <h5>Please enter your email address and your verification code.</h5>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtEmailAddress.ClientID %>">Email Address:</label>
                        <asp:TextBox type="text" CssClass="form-control" ID="txtEmailAddress" placeholder="" runat="server" />
                    </div>

                    <div class="form-group">
                        <label for="<%= txtVerificationCode.ClientID %>">Verification Code:</label>
                        <asp:TextBox type="text" CssClass="form-control" ID="txtVerificationCode" placeholder="" runat="server" />
                    </div>

                    <asp:Button type="submit" class="btn btn-primary my-3" runat="server" Text="Submit" OnClick="btnSubmitVerification_Click" />
                </div>
            </div>
            <div class="card p-3" id="divSecurityQuestion" runat="server" visible="false">
                <h5 class="card-title text-center">Account Recovery</h5>
                <div class="card-body">
                    <div class="form-group alert-danger p-4 rounded" runat="server" id="divInvalidAnswer" visible="false">
                        Please try again.
                    </div>
                    <div class="form-group mb-4">
                        <h5>Please answer the security question below.</h5>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblSecurityQuestion"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtAnswer.ClientID %>">Answer:</label>
                        <asp:TextBox type="text" CssClass="form-control" ID="txtAnswer" placeholder="Answer" runat="server" />
                    </div>

                    <asp:Button type="submit" class="btn btn-primary my-3" runat="server" Text="Submit" OnClick="btnSubmitQuestion_Click" />
                </div>
            </div>
            <div class="card p-3" id="divChangePassword" visible="false" runat="server">
                <h5 class="card-title text-center">Change Password</h5>
                <div class="card-body">
                    <div class="form-group alert-danger p-4 rounded" runat="server" id="divInvalidPassword" visible="false">
                        Please enter a valid password and be sure they match.                       
                    </div>
                    <div class="form-group mb-4">
                        <h5>Please enter a new password</h5>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtNewPass.ClientID %>">New Password:</label>
                        <asp:TextBox type="password" CssClass="form-control" ID="txtNewPass" placeholder="New Password" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="<%= txtConfirmPass.ClientID %>">Confirm Password:</label>
                        <asp:TextBox type="password" CssClass="form-control" ID="txtConfirmPass" placeholder="Confirm Password" runat="server" />
                    </div>

                    <asp:Button type="submit" class="btn btn-primary my-3" runat="server" Text="Change Password" OnClick="btnChangePass_Click" />
                </div>
            </div>
            <div class="card p-3" id="divSuccess" visible="false" runat="server">
                <h5 class="card-title text-center">Success</h5>
                <div class="card-body">
                    <div class="form-group mb-4">
                        <h5>Your password has been updated successfully. You may now log in.</h5>
                    </div>
                    <div class="form-group">
                        <asp:Button CssClass="btn btn-primary stretched-link" OnClick="btnLogin_Click" Text="Continue to Log In" runat="server" />
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
</asp:Content>
