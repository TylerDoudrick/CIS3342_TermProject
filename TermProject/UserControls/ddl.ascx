<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ddl.ascx.cs" Inherits="TermProject.UserControls.ddl" %>

<asp:Panel runat="server" ID="pnlHolder">
    <div class="row justify-content-center align-items-center text-center my-5">
        <div class="col-2">
            <asp:Label runat="server" ID="lblReligion" CssClass="w-100 border-bottom  d-block"> Religion</asp:Label><br />
            <asp:ListBox runat="server" ID="lbReligion" CssClass="form-control w-100 " SelectionMode="Single"></asp:ListBox>


        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblCommitment" CssClass="w-100 border-bottom d-block"> Commitment</asp:Label><br />
            <asp:ListBox runat="server" ID="lbCommittment" CssClass="form-control w-100" SelectionMode="Single"></asp:ListBox>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblInterests" CssClass="w-100 border-bottom d-block"> Interests</asp:Label><br />
            <asp:ListBox runat="server" ID="lbInterests" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblLikes" CssClass="w-100 border-bottom d-block"> Likes</asp:Label><br />
            <asp:ListBox runat="server" ID="lbLikes" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
        </div>
        <div class="col-2">
            <asp:Label runat="server" ID="lblDislikes" CssClass="w-100 border-bottom d-block"> Dislikes</asp:Label><br />
            <asp:ListBox runat="server" ID="lbDislikes" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
        </div>
    </div>
</asp:Panel>



