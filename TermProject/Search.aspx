<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TermProject.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <br />
    <br />
    <div class="row justify-content-center align-items-center">
        <div class="col-2">
            <asp:Button runat="server" ID="btnReligion" Text="Religion" CssClass="btn  w-75" OnClick="btnReligion_Click" />
        </div>
        <div class="col-2">
            <asp:LinkButton runat="server" ID="lbCommitment" Text="Commitment" CssClass="btn  w-75" OnClick="lbCommitment_Click"></asp:LinkButton>
        </div>
        <div class="col-2">
            <asp:Button runat="server" ID="btnLikes" Text="Likes" CssClass="btn w-75" OnClick="btnLikes_Click" />
        </div>
        <div class="col-2">
            <asp:Button runat="server" ID="btnDislikes" Text="Dislikes" CssClass="btn w-75" OnClick="btnDislikes_Click" />
        </div>
        <div class="col-2">
            <asp:Button runat="server" ID="btnInterests" Text="Interests" CssClass="btn w-75" OnClick="btnInterests_Click" />
        </div>
    </div>

    <div class="row justify-content-center align-items-center">
        <div class="col-2 hidden" id="divReligion" runat="server">
            <asp:DropDownList runat="server" ID="ddlReligion" CssClass="form-control" AppendDataBoundItems="true">
                <asp:ListItem Value="-1"> Select Religion...</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-2 hidden" id="divCommittment" runat="server">
            <asp:DropDownList runat="server" ID="ddlCommittment" CssClass="form-control w-100" AppendDataBoundItems="true">
                <asp:ListItem Value="-1"> Select Commitment...</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-2 hidden" id="divLikes" runat="server">
            <asp:ListBox runat="server" ID="lbLikes" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
        </div>
        <div class="col-2 hidden" id="divDislikes" runat="server">
            <asp:ListBox runat="server" ID="lbDislikes" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
        </div>
         <div class="col-2 hidden" id="divInterests" runat="server">
            <asp:ListBox runat="server" ID="lbInterests" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
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
                    nonSelectedText: 'Select atleast 1 interest...' // Here you can change with your desired text as per your requirement.
                });
            $('[id*=lbLikes]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select atleast 1 like...' // Here you can change with your desired text as per your requirement.
                });
            $('[id*=lbDislikes]').multiselect
                ({
                    includeSelectAllOption: false,
                    nonSelectedText: 'Select atleast 1 dislike...' // Here you can change with your desired text as per your requirement.
                });
        });

    </script>
</asp:Content>
