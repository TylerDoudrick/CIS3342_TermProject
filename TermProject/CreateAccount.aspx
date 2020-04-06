<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="TermProject.CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script>
    var ttr = 10;

    function redirectCountdown() {
        ttr = ttr - 1;
        if (ttr <= 0) {
            window.location.replace("Registration.aspx");
        }else {
            document.getElementById("countdown").innerHTML = ttr;
            setTimeout("redirectCountdown()", 1000);
        }
    }

</script>
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
          <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Submit</button>
                </div>
            </div>
        </div>
    </div>
    <div class="row justify-content-center my-5" id="divCreateAccount" runat="server">
        <div class="col-4">
            <div class="card p-3">
                <h5 class="card-title text-center">Create Account</h5>
                <div class="card-body">
                    <div class="form-group row">
                        <label for="<%= txtUsername.ClientID %>" class="col-sm-4 col-form-label">Username</label>
                        <div class="col-sm-8">
                            <asp:TextBox type="text" CssClass="form-control" ID="txtUsername" placeholder="Username" runat="server" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="<%= txtEmail.ClientID %>" class="col-sm-4 col-form-label">Email</label>
                        <div class="col-sm-8">
                            <asp:TextBox type="text" CssClass="form-control" ID="txtEmail" placeholder="email@example.com" runat="server" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="<%= txtPassword.ClientID %>" class="col-sm-4 col-form-label">Password</label>
                        <div class="col-sm-8">
                            <asp:TextBox type="password" CssClass="form-control" ID="txtPassword" placeholder="Password" runat="server" />
                            <small id="passwordHelpBlock" class="form-text text-muted">Your password must be 8-20 characters long, contain letters and numbers, and must not contain spaces or emoji.
                            </small>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="<%= txtConfirmPassword.ClientID %>" class="col-sm-4 col-form-label">Confirm Password</label>
                        <div class="col-sm-8">
                            <asp:TextBox type="password" CssClass="form-control" ID="txtConfirmPassword" placeholder="Confirm Password" runat="server" />
                        </div>
                    </div>
                    <hr />
                    <div class="form-group row">
                        <label for="<%= txtFName.ClientID %>" class="col-sm-4 col-form-label">First Name</label>
                        <div class="col-sm-8">
                            <asp:TextBox type="text" CssClass="form-control" ID="txtFName" placeholder="John" runat="server" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="<%= txtLName.ClientID %>" class="col-sm-4 col-form-label">Last Name</label>
                        <div class="col-sm-8">
                            <asp:TextBox type="text" CssClass="form-control" ID="txtLName" placeholder="Doe" runat="server" />
                        </div>
                    </div>
                    <hr />

                    <h5 class="text-center my-3">Billing Address</h5>

                    <div class="form-group row">
                        <label for="<%= txtAddressOne.ClientID %>" class="col-sm-4 col-form-label">Address Line One</label>
                        <div class="col-sm-8">
                            <asp:TextBox type="text" CssClass="form-control" ID="txtAddressOne" placeholder="1234 N Main St" runat="server" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="<%= txtAddressTwo.ClientID %>" class="col-sm-4 col-form-label">Address Line Two</label>
                        <div class="col-sm-8">
                            <asp:TextBox type="text" CssClass="form-control" ID="txtAddressTwo" placeholder="Apt, Studio, Unit, etc" runat="server" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="<%= txtCity.ClientID %>" class="col-sm-4 col-form-label">City</label>
                        <div class="col-sm-8">
                            <asp:TextBox type="text" CssClass="form-control" ID="txtCity" placeholder="City" runat="server" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="<%= txtState.ClientID %>" class="col-sm-4 col-form-label">State</label>
                        <div class="col-sm-8">
                            <asp:TextBox type="text" CssClass="form-control" ID="txtState" placeholder="State" runat="server" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="<%= txtZip.ClientID %>" class="col-sm-4 col-form-label">Zip Code</label>
                        <div class="col-sm-8">
                            <asp:TextBox type="text" CssClass="form-control" ID="txtZip" placeholder="ZIP Code" runat="server" />
                        </div>
                    </div>
                    <asp:Button CssClass="btn btn-primary" Text="Create Account" runat="server" ID="btnCreateAccount" OnClick="btnCreateAccount_Click" />
                    <hr class="py-2" />
                    <div>
                        <h5>Already have an account?</h5>
                        <asp:LinkButton type="submit" class="btn btn-primary my-3" runat="server" Text="Click Here to Sign In" href="CreateAccount.aspx" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row justify-content-center my-5" id="divValidate" runat="server" visible="false">
        <div class="col-4">
            <div class="card p-3">
                <h5 class="card-title text-center">Validate Account</h5>
                <div class="card-body">
                    <p>
                        Good news! You're account has been validated because we don't know if we can do validation!</p>
                       <p> You'll be redirected to account creation in... <span id="countdown"></span></p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">

</asp:Content>
