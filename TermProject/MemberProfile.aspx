<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MemberProfile.aspx.cs" Inherits="TermProject.MemberProfile" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

        <!-- Modal -->
    <div class="modal fade" id="modalSendMessage" tabindex="-1" role="dialog" aria-labelledby="modalSendMessageLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalSendMessageLabel">Send Message</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    
                    <div class="form-group">
                        <label for="<%= txtMessage.ClientID %>">Message</label>
                        <asp:TextBox type="text" CssClass="form-control" ID="txtMessage" TextMode="MultiLine" placeholder="Message..." runat="server" />
                    </div>


                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:button CssClass="btn btn-primary" id="btnSendMessage" Text="Send Date Request" runat="server" OnClick="btnDateRequest_Click" />
                </div>
            </div>
        </div>
    </div>


    <br />
    <div>
        <div class="row justify-content-center my-5">
            <div class="col-2">
                <asp:Image runat="server" CssClass="img-thumbnail" ImageUrl="https://www.skymania.com/wp/wp-content/uploads/2011/06/sun_with_prominence.jpg" />
            </div>
            <div class="col-4 ">
                <h5 class="text-info font-weight-bold" runat="server" id="lblName">John Johnson, 25</h5>
                <asp:Label runat="server" ID="lblLocation"> Philadelphia, PA</asp:Label>
                <br />
                <asp:Label runat="server" ID="lblTagline"> This is John's Tagline</asp:Label>
            </div>
            <div class="col-2">
                <div class="row">
                    <asp:Button runat="server" ID="btnLike" CssClass="btn btn-primary w-75" Text="Like" OnClick="btnLike_Click" data-toggle="modal" data-target="#modalSuccess"  />
                </div>
                <br />
                <div class="row">
                    <asp:Button runat="server" ID="btnPass" CssClass="btn btn-primary w-75" Text="Pass" OnClick="btnPass_Click" />
                </div>
                <br />
                <div class="row">
                    <asp:Button runat="server" ID="btnBlock" CssClass="btn btn-primary w-75" Text="Block" OnClick="btnBlock_Click" />
                </div>
                <br />
                <div class="row">
                    <button runat="server" id="btnDateReq" type="button" class="btn btn-primary w-75" data-toggle="modal" data-target="#modalSendMessage">Send Date Request</button>
                </div>
            </div>
        </div>
    </div>

    <hr />

    <div runat="server" class="hidden">
        <div class="row">
            <h5 class="text-info font-weight-bold">Contact Information</h5>
        </div>
        <div class="row justify-content-center my-5">
            <div class="col-4">

                <div class="row">
                    <asp:Label runat="server" ID="lblPhoneNumber" for="<%= lblNumber.ClientID %>" CssClass="font-weight-bold"> Phone Number: </asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" ID="lblNumber"> (215)- 855-9966 </asp:Label>

                </div>
                <br />
                <div class="row">
                    <asp:Label runat="server" for="<%= lblEmail.ClientID %>" CssClass="font-weight-bold">Email:  </asp:Label>
                    &nbsp;&nbsp; &nbsp;
                    <asp:Label runat="server" ID="lblEmail"> absd@gmail.com </asp:Label>
                </div>
            </div>
        </div>
        <hr />
    </div>

    <div>
        <h5 class="text-info font-weight-bold ">Basic Information</h5>
        <div class="row justify-content-center my-5">
            <div class="col-md-9">
                <asp:Label runat="server" CssClass="font-weight-bold"> Bio </asp:Label><br />
                <asp:Label runat="server" ID="lblBio">Lorem ipsum dolor sit amet, usu appetere officiis eu, vix ex sensibus postulant. Ut civibus vivendum vis, ut iusto moderatius dissentias mel, viderer temporibus per id. At pro scaevola partiendo consulatu. Menandri salutandi et mei, duo debet maiorum corpora te, eos ex zril salutandi prodesset. Viris consul epicurei quo te, partem inimicus an vel.

Wisi scripserit qui et, te zril consul forensibus duo, ne sed facilisi legendos assentior. Id sit iudicabit patrioque, no est tale veri. Id velit referrentur est, at has quidam singulis. Ei posse corrumpit duo, ex lorem saperet omnesque vix. Eam te scripta efficiantur, ex est omnis euismod, sea partiendo efficiantur at. Hinc wisi detracto at vim.</asp:Label>
            </div>
        </div>
        <div class="row justify-content-center my-5">
            <div class="col-2 form-group">
                <asp:Label runat="server" for="<%= lblReligion.ClientID %>" CssClass="font-weight-bold"> Religion </asp:Label>
                &nbsp;  &nbsp;
            <asp:Label runat="server" ID="lblReligion"> Christianity </asp:Label>
            </div>
            <div class="col-2">
                <asp:Label runat="server" for="<%= lblCommitment.ClientID %>" CssClass="font-weight-bold">Commitment Type</asp:Label>
                &nbsp;  &nbsp;
            <asp:Label runat="server" ID="lblCommitment"> Casual</asp:Label>
            </div>
            <div class="col-2">
                <asp:Label runat="server" for="<%= lblOccupation.ClientID %>" CssClass="font-weight-bold"> Occupation</asp:Label>
                &nbsp; &nbsp;
                <asp:Label runat="server" ID="lblOccupation"> Dentist</asp:Label>
            </div>
            <div class="col-2">
                <asp:Label runat="server" for="<%= lblSeekingGender.ClientID %>" CssClass="font-weight-bold"> Seeking Gender</asp:Label>
                &nbsp; &nbsp;
                <asp:Label runat="server" ID="lblSeekingGender"> Female
                </asp:Label>
            </div>

        </div>
        <div class=" row justify-content-center my-5 hidden " id="divPrivateBasic" runat="server">
            <div class="col-2">
                <asp:Label runat="server" for="<%= lblNumKids.ClientID %>" CssClass="font-weight-bold"> Number of Kids</asp:Label>
                &nbsp; &nbsp;
            <asp:Label runat="server" ID="lblNumKids"> 0  </asp:Label>
            </div>
            <div class="col-2">
                <asp:Label runat="server" for="<%= lblWantKids.ClientID %>" CssClass="font-weight-bold"> Do you want kids? </asp:Label>
                &nbsp; &nbsp;
             <asp:Label runat="server" ID="lblWantKids"> No  </asp:Label>
            </div>
        </div>
    </div>

    <hr />

    <div runat="server" id="divFavThings" class="hidden">
        <div class="row my-5 w-50">
            <h5 class="text-info font-weight-bold ">Favorite Things</h5>
        </div>
        <div class="row justify-content-center my-5 w-100">
            <asp:Label runat="server" for="<%= lblInterests.ClientID %>" CssClass="font-weight-bold"> Interests </asp:Label>
            &nbsp; &nbsp;
            <asp:Label runat="server" ID="lblInterests"> Photography ,Swimming, Sports</asp:Label>

        </div>
        <div class="row justify-content-center my-5 w-100">
            <asp:Label runat="server" for="<%= lblLikes.ClientID %>" CssClass="font-weight-bold"> Likes </asp:Label>
            &nbsp; &nbsp;
            <asp:Label runat="server" ID="lblLikes"> Photography , Video Games , Fantasy Football </asp:Label>
        </div>
        <div class="row justify-content-center my-5 w-100">
            <asp:Label runat="server" for="<%= lblDislikes.ClientID %>" CssClass="font-weight-bold"> Dislikes </asp:Label>
            &nbsp; &nbsp;
            <asp:Label runat="server" ID="lblDislikes"> Meditation, Philosophy</asp:Label>

        </div>
    </div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">
    <script lang="javascript" type="text/javascript">

    </script>
</asp:Content>
