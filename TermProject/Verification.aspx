<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Verification.aspx.cs" Inherits="TermProject.Verification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
                <div class="card p-3" id="divVerificationCode" runat="server">
                <h5 class="card-title text-center">Account Recovery</h5>
                <div class="card-body">
                    <div class="form-group mb-4">

                        <h5>Please enter your email address and your verification code.</h5>
                            <asp:Linkbutton runat="server" id="lbSendAgain" OnClick="lbSendAgain_Click"> Click here to send the email again</asp:Linkbutton>

                    </div>
                    <div class="form-group">
                        <label for="<%= lblEmail.ClientID %>">Email Address:</label>
                        <asp:Label type="text" CssClass="form-control" ID="lblEmail" placeholder="" runat="server" />
                    </div>

                    <div class="form-group">
                        <label for="<%= txtVerificationCode.ClientID %>">Verification Code:</label>
                        <asp:TextBox type="text" CssClass="form-control" ID="txtVerificationCode" placeholder="" runat="server" />
                    </div>

                    <asp:Button type="submit" class="btn btn-primary my-3" runat="server" Text="Submit" OnClick="btnSubmitVerification_Click" />
                </div>
            </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
</asp:Content>
