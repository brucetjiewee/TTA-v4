<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content topSpace"></div>
    <div class="panel-group topSpace" id="accordion" style="margin-top: 25px; margin-bottom: 20px">
        <div class="panel panel-default">
            <div class="panel-heading bkb">
                <h1 class="panel-title wHead">
                    <a data-toggle="collapse" data-parent="#accordion" href="#colpnlDevContactInfo"><span aria-hidden="true" style="font-size: 80%; margin-right: 5px" class="glyphicon glyphicon-user fct"></span>Developers</a>
                </h1>
            </div>
            <div id="colpnlDevContactInfo" class="panel-collapse collapse topSpace">
                <div class="panel-body">
                    <div class="row" style="text-align: left; margin-left: 55px">
                        <div id="thumbBruce" class="col-lg-4 col-sm-4 col-md-4 col-xs-12" style="width: 255px/*; border: 1px solid darkslateblue*/">
                            <img src="Design Graphics/YYLThumbnail.jpg" style="border-radius: 100%; height: 80px; width: 80px; float: left; margin-left: 1px; margin-top: 10px; margin-right: 10px; margin-bottom: 10px; border: 3px solid tomato" />
                            <address>
                                <br />
                                <strong>YI-YU (BRUCE)<br />
                                    LIU</strong><br />
                                FOUNDER<br />
                                <hr />
                                University of Pretoria<br />
                                Hatfield, Pretoria<br />
                                <strong>
                                    <abbr title="Phone">P:</abbr></strong> (072) 379 9000<br />
                                <strong>
                                    <abbr title="E-mail">E:</abbr></strong> <a href="mailto:yiyubruceliu@gmail.com" class="fct">yiyubruceliu@gmail.com</a><br />
                            </address>
                        </div>
                        <div id="thumbLuke" class="col-lg-4 col-sm-4 col-md-4 col-xs-12" style="width: 255px/*; border: 1px solid darkslateblue*/">
                            <img src="Design Graphics/LJVThumbnail.jpg" style="border-radius: 100%; height: 80px; width: 80px; float: left; margin-left: 1px; margin-top: 10px; margin-right: 10px; margin-bottom: 10px; border: 3px solid tomato" />
                            <address>
                                <br />
                                <strong>LUKE<br />
                                    VOIGT</strong><br />
                                DEVELOPER<br />
                                <hr />
                                University of Pretoria<br />
                                Hatfield, Pretoria<br />
                                <strong>
                                    <abbr title="Phone">P:</abbr></strong> (082) 559 2322<br />
                                <strong>
                                    <abbr title="E-mail">E:</abbr></strong> <a href="mailto:lukevoigt0407@gmail.com" class="fct">lukevoigt0407@gmail.com</a><br />
                            </address>
                        </div>
                        <div id="thumbRuco" class="col-lg-4 col-sm-4 col-md-4 col-xs-12" style="width: 255px/*; border: 1px solid darkslateblue*/">
                            <img src="Design Graphics/RCPThumbnail.jpg" style="border-radius: 100%; height: 80px; width: 80px; float: left; margin-left: 1px; margin-top: 10px; margin-right: 10px; margin-bottom: 10px; border: 3px solid tomato" />
                            <address>
                                <br />
                                <strong>RUCO<br />
                                    PRETORIUS</strong><br />
                                DEVELOPER<br />
                                <hr />
                                University of Pretoria<br />
                                Hatfield, Pretoria<br />
                                <strong>
                                    <abbr title="Phone">P:</abbr></strong> (084) 207 1214<br />
                                <strong>
                                    <abbr title="E-mail">E:</abbr></strong> <a href="mailto:ruco.facebook@gmail.com" class="fct">ruco.facebook@gmail.com</a><br />
                            </address>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading bkb">
                <h1 class="panel-title wHead">
                    <a data-toggle="collapse" data-parent="#accordion" href="#colpnlSendMessage"><span aria-hidden="true" style="font-size: 80%; margin-right: 5px" class="glyphicon glyphicon-envelope fct"></span>Contact us!</a>
                </h1>
            </div>
            <div id="colpnlSendMessage" class="panel-collapse collapse">
                <div class="panel-body">
                    <div class="col-lg-12 text-center col-md-12 topSpace" >
                        <h3 style="text-align: center; margin-top: -15px; width: 90%">Fill out the form below and we will try to get back to you as soon as possible!</h3>
                        <form name="sentMessage" id="contactForm" novalidate>
                            <div class="row control-group" style="align-content: center; text-align: left">
                                <div class="form-group col-xs-12 floating-label-form-group controls">
                                    <label style="text-align: left">Name</label>
                                    <input type="text" class="form-control" style="width: 100%" placeholder="Name" id="name" required data-validation-required-message="Please enter your name.">
                                    <p class="help-block text-danger"></p>
                                </div>
                            </div>
                            <div class="row control-group" style="text-align: left">
                                <div class="form-group col-xs-12 floating-label-form-group controls">
                                    <label style="text-align: left">Email Address</label>
                                    <input type="email" class="form-control" style="width: 100%" placeholder="Email Address" id="email" required data-validation-required-message="Please enter your email address.">
                                    <p class="help-block text-danger"></p>
                                </div>
                            </div>
                            <div class="row control-group" style="text-align: left">
                                <div class="form-group col-xs-12 floating-label-form-group controls">
                                    <label style="text-align: left">Phone Number</label>
                                    <input type="tel" class="form-control" style="width: 100%" placeholder="Phone Number" id="phone" required data-validation-required-message="Please enter your phone number.">
                                    <p class="help-block text-danger"></p>
                                </div>
                            </div>
                            <div class="row control-group" style="text-align: left">
                                <div class="form-group col-xs-12 floating-label-form-group controls">
                                    <label style="text-align: left">Message</label>
                                    <textarea rows="3" class="form-control" style="width: 100%" placeholder="Message" id="message" required data-validation-required-message="Please enter a message."></textarea>
                                    <p class="help-block text-danger"></p>
                                </div>
                            </div>
                            <br>
                            <div id="success"></div>
                            <div class="row" style="margin-top: -40px">
                                <div class="form-group col-xs-12">
                                    <button type="submit" class="btn btn-success btn-block btn-lg" style="">Send  <span aria-hidden="true" class="glyphicon glyphicon-send"></span></button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
