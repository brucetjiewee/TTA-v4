<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="Header" runat="server">
    <header id="top" class="header shieght" style="min-height: 5px;margin-top:50px">
        <div class="text-vertical-center" style="margin-top: 10px">
            <h1 class="text-center" style="font-family: 'Segoe UI'; font-size: 50px; font-style: normal; color: white">TimeTable Assistant</h1>
            <hr style="width: 60%">
            <p class="subtitle" style="font-size: 16px; font-weight: 900; background-color: rgba(0, 0, 255, 0.2); color: tomato; margin-top: -10px">A minimalistic, free TimeTable generator tool for the University of Pretoria.</p>
            <br />
            <a href="/../Wizard.aspx" class="btn btn-default btn-lg" style="border: 1px solid salmon; color: white; background-color: tomato">Let's Get Started! <span class="glyphicon glyphicon-chevron-right"></span></a>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2 col-md-10 col-md-offset-1">
                    <div style="margin-top: 30px" id="sharebuttons" class="text-center">
                        <p class="outlineblack" style="margin-bottom: 15px; color: white;">Share this page</p>
                        <span class='st_facebook_large' displaytext='Facebook'></span>
                        <span class='st_twitter_large' displaytext='Tweet'></span>
                        <span class='st_googleplus_large' displaytext='Google +'></span>
                        <span class='st_linkedin_large' displaytext='LinkedIn'></span>
                        <span class='st_pinterest_large' displaytext='Pinterest'></span>
                        <span class='st_email_large' displaytext='Email'></span>

                        <script type="text/javascript">var switchTo5x = true;</script>
                        <script type="text/javascript" src="http://w.sharethis.com/button/buttons.js"></script>
                        <script type="text/javascript">stLight.options({ publisher: "982523b1-f745-4da1-a991-53edadd4aa80", doNotHash: false, doNotCopy: false, hashAddressBar: false });</script>
                    </div>
                </div>
            </div>
        </div>
    </header>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
