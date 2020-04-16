<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TermProject.Search" %>

<%@ Register Src="~/UserControls/ddl.ascx" TagPrefix="uc1" TagName="ddl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

    <uc1:ddl runat="server" ID="ddl" />

    <div runat="server" class="row justify-content-center align-items-center" id="divSearch">
        <div>
            <button type="button" id="btnSearch" class="btn btn-info">Search<i class="fas fa-search pl-2"></i></button>
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


            $("#btnSearch").click(function () {
                var searchOptions = {
                    "Religions": $('.ddlReligion').selectpicker('val').toString(),
                    "Commitments": $('.ddlCommitment').selectpicker('val').toString(),
                    "Interests": $('.ddlInterests').selectpicker('val').toString(),
                    "Likes": $('.ddlLikes').selectpicker('val').toString(),
                    "Dislikes": $('.ddlDislikes').selectpicker('val').toString()
                };
                console.log(JSON.stringify(searchOptions));

                $.ajax({
                    type: 'POST',
                    url: 'https://localhost:44375/api/datingservice/search/',
                    accepts: 'application/json',
                    contentType: 'application/json',
                    data: JSON.stringify(searchOptions),
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(errorThrown);
                    },
                    success: function (result) {
                        console.log(result);
                    }
                });
            });


        });

    </script>
</asp:Content>
