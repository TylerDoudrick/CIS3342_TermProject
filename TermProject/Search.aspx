<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TermProject.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <br />
    <br />
    <div class="row justify-content-center align-items-center">
        <div class="col-2">
            <asp:Button runat="server" ID="btnReligion" Text="Religion" CssClass="btn w-100 border-bottom" OnClientClick="btnReligionToggle()" OnClick="btnReligion_Click" />
            <br />
            <br />
            <asp:ListBox runat="server" ID="lbReligion" CssClass="form-control w-100 " SelectionMode="Single"></asp:ListBox>
        </div>
        <div class="col-2">
            <asp:LinkButton runat="server" ID="lbCommitment" Text="Commitment" CssClass="btn w-100  border-bottom" OnClick="lbCommitment_Click"></asp:LinkButton>
            <br />
            <br />
            <asp:ListBox runat="server" ID="lbCommittment" CssClass="form-control w-100" SelectionMode="Single"></asp:ListBox>
        </div>
        <div class="col-2">
            <asp:Button runat="server" ID="btnLikes" Text="Likes" CssClass="btn w-100 border-bottom" OnClick="btnLikes_Click" />
            <br />
            <br />
            <div >
                <asp:ListBox runat="server" ID="lbLikes" CssClass="form-control w-100" SelectionMode="Multiple" style="max-height:10em; overflow-y:scroll;"></asp:ListBox>
            </div>

        </div>
        <div class="col-2">
            <asp:Button runat="server" ID="btnDislikes" Text="Dislikes" CssClass="btn w-100 border-bottom" OnClick="btnDislikes_Click" />
            <br />
            <br />
            <asp:ListBox runat="server" ID="lbDislikes" CssClass="form-control w-100 " SelectionMode="Multiple"></asp:ListBox>
        </div>
        <div class="col-2">
            <asp:Button runat="server" ID="btnInterests" Text="Interests" CssClass="btn w-100 border-bottom" OnClick="btnInterests_Click" />
            <br />
            <br />
            <asp:ListBox runat="server" ID="lbInterests" CssClass="form-control w-100 " SelectionMode="Multiple"></asp:ListBox>

        </div>
    </div>



    <br />
    <div runat="server" class="row justify-content-center align-items-center hidden" id="divSearch">
        <div>
            <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />
        </div>
    </div>

    <hr />
    <div runat="server" id="divResults" class="hidden">
        <h4 class="text-info font-weight-bold">Result Set</h4>

        <!-- Result set could be carousal that's in dashboard  -->

    </div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
    <script>
        $(document).ready(function () {
            $("#navlinkDashboard").removeClass("active");
            $("#navlinkSearch").addClass("active");

            $('[id*=lbInterests]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select atleast 1 interest...',
                    maxHeight:200
                });
            $('[id*=lbLikes]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select atleast 1 like...',
         maxHeight: 200     
                });
            $('[id*=lbDislikes]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select atleast 1 dislike...', 
                             maxHeight: 200     
                });
            $('[id*=lbCommittment]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select commitment type...', 
                         maxHeight: 200     
});
            $('[id*=lbReligion]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select a religion...', 
                             maxHeight: 200     
                });


        });

    </script>
</asp:Content>
