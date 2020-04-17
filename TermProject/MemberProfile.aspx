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
                    <asp:Button CssClass="btn btn-primary" ID="btnSendMessage" Text="Send Date Request" runat="server" OnClick="btnDateRequest_Click" />
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
                <asp:Label class="text-info font-weight-bold" runat="server" ID="Name">John Johnson, 25</asp:Label><br />
                <asp:Label runat="server" ID="lblLocation"> Philadelphia, PA</asp:Label>
                <br /><br />
                <asp:Label runat="server" ID="lblTagline"> This is John's Tagline</asp:Label>
            </div>
            <div class="col-2">
                <div class="row">
                    <asp:Button runat="server" ID="btnLike" CssClass="btn btn-primary w-75" Text="Like" OnClick="btnLike_Click" data-toggle="modal" data-target="#modalSuccess" />
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
                <div class="row"> <!-- data-toggle="modal" data-target="#modalSendMessage" -->
                    <asp:Button runat="server" ID="btnDateReq" CssClass="btn btn-primary w-75"  Text="Send Date Request" OnClick="btnDateRequest_Click" />
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
                    <asp:Label runat="server" ID="lblNumber"> </asp:Label>

                </div>
                <br />
                <div class="row">
                    <asp:Label runat="server" for="<%= lblEmail.ClientID %>" CssClass="font-weight-bold">Email:  </asp:Label>
                    &nbsp;&nbsp; &nbsp;
                    <asp:Label runat="server" ID="lblEmail">  </asp:Label>
                </div>
            </div>
        </div>
        <hr />
    </div>

    <div>
<div class="col text-center">
            <div class="font-weight-bold text-info h5 my-auto">Basic Information</div>
        </div>        <div class="row justify-content-center my-5">
            <div class="col-md-9">
                <asp:Label runat="server" CssClass="font-weight-bold"> Bio </asp:Label><br />
                <asp:Label runat="server" ID="lblBio">Lorem ipsum dolor sit amet, usu appetere officiis eu, vix ex sensibus postulant. Ut civibus vivendum vis, ut iusto moderatius dissentias mel, viderer temporibus per id. At pro scaevola partiendo consulatu. Menandri salutandi et mei, duo debet maiorum corpora te, eos ex zril salutandi prodesset. Viris consul epicurei quo te, partem inimicus an vel.
                    </asp:Label>
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
            <asp:Label runat="server" ID="lblCommitment"> </asp:Label>
            </div>
            <div class="col-2">
                <asp:Label runat="server" for="<%= lblOccupation.ClientID %>" CssClass="font-weight-bold"> Occupation</asp:Label>
                &nbsp; &nbsp;
                <asp:Label runat="server" ID="lblOccupation"> </asp:Label>
            </div>
            <div class="col-2">
                <asp:Label runat="server" for="<%= lblSeekingGender.ClientID %>" CssClass="font-weight-bold"> Seeking Gender</asp:Label>
                &nbsp; &nbsp;
                <asp:Label runat="server" ID="lblSeekingGender"> 
                </asp:Label>
            </div>

        </div>
        <div class="row justify-content-center my-5 hidden " id="divPrivateBasic" runat="server">
                <div class="col-2">
                    <asp:Label runat="server" for="<%= lblNumKids.ClientID %>" CssClass="font-weight-bold"> Number of Kids</asp:Label>
                    &nbsp; &nbsp;
            <asp:Label runat="server" ID="lblNumKids"> </asp:Label>
                </div>
                <div class="col-2">
                    <asp:Label runat="server"  CssClass="font-weight-bold"> Do you want kids? </asp:Label>
             <asp:Label runat="server" ID="lblWantKids">   </asp:Label>
                </div>
               
        </div>
      
    </div>

    <hr />

    <div>
    <div runat="server" id="divFavThings" class="hidden">
<div class="col text-center">
            <div class="font-weight-bold text-info h5 my-auto">About Me</div>
        </div>          <div class="justify-content-center align-items-center w-75 my-5 mx-auto " id="divPrivateBasic2" runat="server">
              <div class="row">
                  <div class="col-4">
                    <asp:Label runat="server" CssClass="font-weight-bold w-100 border-bottom"> Favorite Restuarants </asp:Label><br />
                    <asp:Label runat="server" ID="lblFavRestaurants">  </asp:Label>
                </div>
                   <div class="col-4">
                    <asp:Label runat="server"  CssClass="font-weight-bold w-100 border-bottom"> Favorite Songs </asp:Label><br />
             <asp:Label runat="server" ID="lblFavSongs">  </asp:Label>
                </div>
                <div class="col-4">
                    <asp:Label runat="server"  CssClass="font-weight-bold w-100 border-bottom"> Favorite Books </asp:Label> <br />
                    <asp:Label runat="server" ID="lblFavBooks">  </asp:Label>
                    
                </div>
              </div>
              <div class="row justify-content-center align-items-center w-75 my-5 mx-auto ">
                  <div class="col-5">
                    <asp:Label runat="server"  CssClass="font-weight-bold w-100 border-bottom"> Favorite Sayings </asp:Label><br />
                    <asp:Label runat="server" ID="lblFavSayings">  </asp:Label>
                </div>

                 <div class="col-5">
                     <asp:Label runat="server"  CssClass="font-weight-bold w-100 border-bottom"> Favorite Movies </asp:Label><br />
                    <asp:Label runat="server" ID="lblFavMovies">  </asp:Label>
                </div>
              </div>
             
        </div>
         <div class="row justify-content-center align-items-center w-75 my-5 mx-auto">
            <div class="col-4">
            <asp:Label runat="server"  CssClass="w-100 border-bottom font-weight-bold"> Interests </asp:Label><br />
            <asp:Label runat="server" ID="lblInterests"> Photography ,Swimming, Sports</asp:Label>
            </div>
            
            <div class="col-4">
            <asp:Label runat="server"  CssClass="font-weight-bold w-100 border-bottom"> Likes </asp:Label><br />
            <asp:Label runat="server" ID="lblLikes"> Photography , Video Games , Fantasy Football </asp:Label>
            </div>
            <div class="col-4">
            <asp:Label runat="server" CssClass="font-weight-bold w-100 border-bottom"> Dislikes </asp:Label><br>
            <asp:Label runat="server" ID="lblDislikes"> Meditation, Philosophy</asp:Label>
            </div>

        </div>
    </div>
            </div>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="EndBodyPlaceHolder" runat="server">

</asp:Content>
