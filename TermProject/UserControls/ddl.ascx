<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ddl.ascx.cs" Inherits="TermProject.UserControls.ddl" %>

<asp:Panel runat="server" ID="pnlHolder" CssClass="ddlMisc">
    <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
    <div class="col">
        <asp:Label runat="server" ID="lblReligion" CssClass="w-100 border-bottom  d-block"> Religion</asp:Label><br /> 
        <asp:ListBox runat="server" ID="lbReligion" CssClass="selectpicker col-10 ddlReligion" multiple="multiple" Title="Choose a religion..." data-selected-text-format="count > 3"></asp:ListBox>

    </div>
    <div class="col">
        <asp:Label runat="server" ID="lblCommitment" CssClass="w-100 border-bottom d-block"> Commitment</asp:Label><br />
        <asp:ListBox runat="server" ID="lbCommittment" CssClass="selectpicker col-10 ddlCommitment" multiple="multiple" Title="Choose a commitment type..." data-selected-text-format="count > 3"></asp:ListBox>
    </div>
    <div class="col">
        <asp:Label runat="server" ID="lblInterests" CssClass="w-100 border-bottom d-block"> Interests</asp:Label><br /> 
        <asp:ListBox runat="server" ID="lbInterests" CssClass="selectpicker col-10 ddlInterests" multiple="multiple" Title="Choose some interests..." data-selected-text-format="count > 3"></asp:ListBox>
    </div>

</div>
        <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
                <div class="col">
        <asp:Label runat="server" ID="lblLikes" CssClass="w-100 border-bottom d-block"> Likes</asp:Label><br /> 
        <asp:ListBox runat="server" ID="lbLikes" CssClass="selectpicker col-7 ddlLikes" multiple="multiple" Title="Choose some likes..." data-selected-text-format="count > 3"></asp:ListBox>
    </div>
    <div class="col">
        <asp:Label runat="server" ID="lblDislikes" CssClass="w-100 border-bottom d-block"> Dislikes</asp:Label><br /> 
        <asp:ListBox runat="server" ID="lbDislikes" CssClass="selectpicker col-7 ddlDislikes" multiple="multiple" Title="Choose some dislikes..." data-selected-text-format="count > 3"></asp:ListBox>
    </div>
            </div>
</asp:Panel>

