<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Verification.aspx.cs" Inherits="TermProject.Verification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
     <div class="my-5">
        <asp:Linkbutton runat="server" id="lbSendAgain" OnClick="lbSendAgain_Click"> Click here to send the email again</asp:Linkbutton>
    </div>
    <h5 class=" text-center my-5 font-weight-bold">
        An email has been sent for verification. Please follow the instructions in that email to verify your account
    </h5>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
</asp:Content>
