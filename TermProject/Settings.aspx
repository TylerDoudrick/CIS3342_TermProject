<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="TermProject.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style>
        

        .required {
            color: red;
        }

        .card-img-top {
            height: auto;
            width: 100%;
        }
        .card {
            border: 1px solid black;
            width: 16em;
            height:auto;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <br />
    <br />
    <br />


    <div class="row h-100">
        <div class="col-4 list-group list-group-flush w-50">
            <asp:LinkButton runat="server" ID="lbChangeUsername" OnClick="lbChangeUsername_Click" CssClass="list-group-item list-group-item-action bg-light w-50"> Change Username</asp:LinkButton><br />
            <asp:LinkButton runat="server" ID="lbChangePassword" OnClick="lbChangePassword_Click" CssClass="list-group-item list-group-item-action bg-light w-50"> Change Password</asp:LinkButton>
            <br />
            <asp:LinkButton runat="server" ID="lbOptOut" OnClick="lbOptOut_Click" CssClass="list-group-item list-group-item-action bg-light w-50"> Hide Profile</asp:LinkButton><br />
            <asp:LinkButton runat="server" ID="lbBlockedUsers" OnClick="lbBlockedUsers_Click" CssClass="list-group-item list-group-item-action bg-light w-50"> Blocked Users</asp:LinkButton><br />

            <asp:LinkButton runat="server" ID="lbAddress" OnClick="lblAddress_Click" CssClass="list-group-item list-group-item-action bg-light w-50"> Address </asp:LinkButton><br />
        </div>

        <div>
            <div class="col">
                    <div runat="server" class="row"> 
                        <asp:Label runat="server" ID="lblSuccess" CssClass="text-success"></asp:Label>
                    </div>
                <br />
                </div>
            <div runat="server" id="divChangeUsername" class="hidden">
                <div class="col">
                    <div class="row">
                        <asp:Label runat="server" ID="lblCurrentUsername" for="<%= txtCurrentUsername.ClientID %>"> Current Username </asp:Label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCurrentUsername" ReadOnly="true" placeholder="john123"> </asp:TextBox>
                    </div>
                    <br />

                    <div class="row">
                        <asp:Label runat="server" ID="lblUsernameError" CssClass="text-danger font-weight-bold"></asp:Label>
                        <br />
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


            <div runat="server" id="divChangePassword" class="hidden">
                <div class="col">
                    <div class="row">
                        <asp:Label runat="server" ID="lblCurrentPassword" for="<%= txtCurrentPassword.ClientID %>"> Current Password </asp:Label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCurrentPassword" ReadOnly="true" type="password"> </asp:TextBox>
                    </div>
                    <br />
                    <div class="row">
                        <asp:Label runat="server" ID="lblPasswordError" CssClass="text-danger font-weight-bold"></asp:Label>
                        <br />
                    </div>
                    <div class="row">
                        <asp:Label runat="server" ID="lblNewPassword" for="<%= txtNewPassword.ClientID %>"> New Password </asp:Label>
                        <span class="required">*</span>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNewPassword" type="password"> </asp:TextBox>
                    </div>
                    <div class="row">
                        <small id="passwordHelpBlock" class="form-text text-muted">Your password must be 8-20 characters long, 
                            contain letters and numbers, and must not contain spaces or emoji.</small>
                    </div>
                    <div class="row">
                        <label for="<%= txtConfirmPassword.ClientID %>" >Confirm Password</label>
                        <asp:TextBox type="password" CssClass="form-control" ID="txtConfirmPassword" placeholder="Confirm Password" runat="server" />
                    </div>
                    <br />

                    <div class="row">
                        <asp:Button runat="server" ID="btnUpdatePassword" Text="Update" CssClass="btn btn-success" OnClick="btnUpdatePassword_Click" />
                    </div>
                </div>

            </div>
            <div runat="server" id="divOPTOut" class="hidden">
                <div class="col">
                    <div runat="server" class="row"> 
                        <asp:Label runat="server" ID="lblSearchUpdate" CssClass="text-success"></asp:Label>
                    </div>
                    <br />
                </div>
                <div class="col">
                    <div class="row">
                        <asp:Label runat="server" ID="lOptOut"> Hide my profile from other users</asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton runat="server" ID="rbYes" Text="Yes" GroupName="rbOptOut" />&nbsp;&nbsp;
                        <asp:RadioButton runat="server" ID="rbNo" Text="No" GroupName="rbOptOut" />
                    </div>
                    <div class="row">
                        <asp:Button runat="server" id="btnSaveOptOut" Text="Save" OnClick="btnSaveOptOut_Click" CssClass="btn btn-secondary" />
                    </div>
                </div>
            </div>

            <div runat="server" id="divBlockedUsers" class="hidden row">
                <asp:DataList runat="server" ID="dlBlockedUsers">
                    <ItemTemplate>
                        <div class="col-sm-3">
                            <asp:LinkButton runat="server" CommandName=' <%#DataBinder.Eval(Container.DataItem, "userID") %>' OnCommand="Unnamed_Command">
                                <div class="card my-2 " >
                                <div runat="server" >
                                    <img class="card-img-top img-thumbnail" src='<%#Eval("imageSRC") %>'> </img>
                                </div>
                                
                                <div class="card-body">
                                    <asp:Label CssClass="card-text font-weight-bold text-center" ID="lblName" runat="server" Text='<%#Eval("name") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="lblTagline" CssClass="card-text text-left" runat="server" Text='<%#Eval("tagline") %>'></asp:Label>
                                </div>
                                <div class="card-footer text-center">
                                    <asp:Button runat="server" ID="btnUnblock" Text="Unblock" CssClass="btn btn-dark" />
                                </div>
                            </div>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:DataList>

                
            </div>
            <div runat="server" id="divAddress" class="hidden">
                
                <div class="col">
                    <div runat="server" class="row">
                        <div class="col-9">
                            <asp:Label runat="server" ID="lblStAddress" for="<%= txtStAddresses.ClientID %>"> Street Address</asp:Label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtStAddresses" ReadOnly="true" Text="1900 Broad Street"> </asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div runat="server" class="row">
                        <div runat="server" class="col-3 ">
                            <asp:Label runat="server" ID="lblCity" for="<%= txtCity.ClientID %>"> City </asp:Label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtCity" ReadOnly="true" Text="Philadelphia"> </asp:TextBox>
                        	<div id="suggestion"></div>
                        </div>
                        <div class="col-3">
                            <asp:Label runat="server" ID="lblState" for="<%= txtState.ClientID %>"> State </asp:Label>
                            <asp:DropDownList runat="server" ID="ddlState" CssClass="form-control disabled">
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
                                <asp:ListItem Value="PA" >Pennsylvania</asp:ListItem>
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
                        <div class="col-3">
                            <asp:Label runat="server" ID="lblZip" for="<%= txtZip.ClientID %>"> Zip Code </asp:Label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtZip" ReadOnly="true" Text="19115"> </asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <asp:Button runat="server" Text="Edit Address" ID="btnEditAddress" OnClick="btnEditAddress_Click" CssClass="btn btn-info" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Button runat="server" Text="Save" ID="btnSave" class="btn btn-success hidden" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>

            <div class="col">
                <div class="row">
                         <asp:Label runat="server" ID="lblError" CssClass="text-danger font-weight-bold"></asp:Label>
                     </div>
            </div>
            <div id="divSecurityQuestions" class="hidden" runat="server">
                <div class="col">
                     
                    <div class="row">
                        <asp:Label runat="server" ID="lblSecurityQuestion"></asp:Label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSecurityQuestion"></asp:TextBox>
                    </div>
                    <br />
                    <div class="row">
                        <asp:Button runat="server" ID="btnSecurity" Text="Check" CssClass="btn btn-secondary" OnClick="btnSecurity_Click" />
                    </div>
                </div>

            </div>
        </div>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
    <script>
        // city link: "https://secure.geobytes.com/AutoCompleteCity?key=7c756203dbb38590a66e01a5a3e1ad96&callback=?&q="
 
        $("#txtCity").autocomplete({

    //       source: function( request, response ) {
    //    $.ajax({
    //      url: "http://gd.geobytes.com/AutoCompleteCity",
    //      dataType: "json",
    //      data: {
    //        filter: "US",
    //          q: request.term,
    //        template: "<geobytes city>"
    //      },
    //      success: function( data ) {
    //        console.log(data);
    //        response( data );
    //      }
    //    });
    //  },
    //  minLength: 2
    //}
    //});
  });
    </script>
</asp:Content>
