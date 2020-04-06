﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ddl.ascx.cs" Inherits="TermProject.UserControls.ddl" %>

<div class="row justify-content-center align-items-center my-5">
    <div class="col-2">
        <asp:Label runat="server" ID="lblReligion"> Religion</asp:Label><br />
        <asp:ListBox runat="server" ID="lbReligion" CssClass="form-control w-100 " SelectionMode="Single"></asp:ListBox>

    </div>
    <div class="col-2">
        <asp:Label runat="server" ID="lblCommitment"> Commitment</asp:Label><br />
        <asp:ListBox runat="server" ID="lbCommittment" CssClass="form-control w-100" SelectionMode="Single"></asp:ListBox>
    </div>
    <div class="col-2">
        <asp:Label runat="server" ID="lblInterests"> Interests</asp:Label><br />
        <asp:ListBox runat="server" ID="lbInterests" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
    </div>
    <div class="col-2">
        <asp:Label runat="server" ID="lblLikes"> Likes</asp:Label><br />
        <asp:ListBox runat="server" ID="lbLikes" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
    </div>
    <div class="col-2">
        <asp:Label runat="server" ID="lblDislikes"> Dislikes</asp:Label><br />
        <asp:ListBox runat="server" ID="lbDislikes" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
    </div>
</div>