<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="TermProject.CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script>
        var ttr = 10;

        function redirectCountdown() {
            ttr = ttr - 1;
            if (ttr <= 0) {
                window.location.replace("Registration.aspx");
            } else {
                document.getElementById("countdown").innerHTML = ttr;
                setTimeout("redirectCountdown()", 1000);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

    <div class="row justify-content-center my-5" id="divCreateAccount" runat="server">
        <div class="col-4">
            <div class="card p-3">
                <h5 class="card-title text-center">Create Account</h5>
                <div class="card-body">
                                        <div class="form-group alert-danger p-4 rounded" runat="server" id="divUsernameExists" visible="false">
                            Username already exists, please try again.
                        </div>
                                        <div class="form-group alert-danger p-4 rounded" runat="server" id="divEmailExists" visible="false">
                            Email already exists, please try again.
                        </div>
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
                        <label for="<%= ddlState.ClientID %>" class="col-sm-4 col-form-label">State</label>
                        <div class="col-sm-8">
                            <asp:DropDownList runat="server" ID="ddlState" CssClass="form-control">
                                <asp:ListItem Value="-1">Select State... </asp:ListItem>
                                <asp:ListItem Value="AL">Alabama</asp:ListItem>
                                <asp:ListItem Value="AK">Alaska</asp:ListItem>
                                <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                                <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                                <asp:ListItem Value="CA">California</asp:ListItem>
                                <asp:ListItem Value="CO">Colorado</asp:ListItem>
                                <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                                <asp:ListItem Value="DE">Delaware</asp:ListItem>
                                <asp:ListItem Value="FL">Florida</asp:ListItem>
                                <asp:ListItem Value="GA">Georgia</asp:ListItem>
                                <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                                <asp:ListItem Value="ID">Idaho</asp:ListItem>
                                <asp:ListItem Value="IL">Illinois</asp:ListItem>
                                <asp:ListItem Value="IN">Indiana</asp:ListItem>
                                <asp:ListItem Value="IA">Iowa</asp:ListItem>
                                <asp:ListItem Value="KS">Kansas</asp:ListItem>
                                <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                                <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                                <asp:ListItem Value="ME">Maine</asp:ListItem>
                                <asp:ListItem Value="MD">Maryland</asp:ListItem>
                                <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                                <asp:ListItem Value="MI">Michigan</asp:ListItem>
                                <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                                <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                                <asp:ListItem Value="MO">Missouri</asp:ListItem>
                                <asp:ListItem Value="MT">Montana</asp:ListItem>
                                <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                                <asp:ListItem Value="NV">Nevada</asp:ListItem>
                                <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                                <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                                <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                                <asp:ListItem Value="NY">New York</asp:ListItem>
                                <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                                <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                                <asp:ListItem Value="OH">Ohio</asp:ListItem>
                                <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                                <asp:ListItem Value="OR">Oregon</asp:ListItem>
                                <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                                <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                                <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                                <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                                <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                                <asp:ListItem Value="TX">Texas</asp:ListItem>
                                <asp:ListItem Value="UT">Utah</asp:ListItem>
                                <asp:ListItem Value="VT">Vermont</asp:ListItem>
                                <asp:ListItem Value="VA">Virginia</asp:ListItem>
                                <asp:ListItem Value="WA">Washington</asp:ListItem>
                                <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                                <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                                <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="<%= txtZip.ClientID %>" class="col-sm-4 col-form-label">Zip Code</label>
                        <div class="col-sm-8">
                            <asp:TextBox type="text" CssClass="form-control" ID="txtZip" placeholder="ZIP Code" runat="server" />
                        </div>
                    </div>
                    <hr />
                    <h5 class="text-center my-3">Security Questions</h5>
                    <p>Please select three security questions and answer them. They will be used if you forget your password.</p>
                    <div class="form-group">
                        <label for="<%= ddlSecurityQOne.ClientID %>">First Security Question:</label>
                        <asp:DropDownList ID="ddlSecurityQOne" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtSecurityQOne.ClientID %>">Answer:</label>

                        <asp:TextBox ID="txtSecurityQOne" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= ddlSecurityQTwo.ClientID %>">Second Security Question:</label>
                        <asp:DropDownList ID="ddlSecurityQTwo" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtSecurityQTwo.ClientID %>">Answer:</label>

                        <asp:TextBox ID="txtSecurityQTwo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= ddlSecurityQThree.ClientID %>">Third Security Question:</label>
                        <asp:DropDownList ID="ddlSecurityQThree" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtSecurityQThree.ClientID %>">Answer:</label>

                        <asp:TextBox ID="txtSecurityQThree" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:Button CssClass="btn btn-primary" Text="Create Account" runat="server" ID="btnCreateAccount" OnClick="btnCreateAccount_Click" />
                    <hr class="py-2" />
                    <div>
                        <h5>Already have an account?</h5>
                        <asp:LinkButton type="submit" class="btn btn-primary my-3" runat="server" Text="Click Here to Sign In" href="LogIn.aspx" />
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
                        Good news! You're account has been validated because we don't know if we can do validation!
                    </p>
                    <p>You'll be redirected to account creation in... <span id="countdown"></span></p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
</asp:Content>
