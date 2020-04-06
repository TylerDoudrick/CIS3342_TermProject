<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TermProject.Search" %>

<%@ Register Src="~/UserControls/ddl.ascx" TagPrefix="uc1" TagName="ddl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

    <uc1:ddl runat="server" ID="ddl" />

    <div runat="server" class="row justify-content-center align-items-center" id="divSearch">
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
                    maxHeight: 200
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
