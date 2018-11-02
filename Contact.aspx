<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top: 95px" class="topSpace"></div>
    <div class="container">
        <div class="panel-group topSpace" id="accordion">
            <div class="panel panel-primary">
                <div class="panel-heading bkb">
                    <h1 class="panel-title wHead">
                        <a data-toggle="collapse" data-parent="#accordion" href="#colpnlDevContactInfo"><span aria-hidden="true" style="font-size: 80%; margin-right: 5px" class="glyphicon glyphicon-user fct"></span>Developers</a>
                    </h1>
                </div>
                <div id="colpnlDevContactInfo" class="panel-collapse collapse topSpace in">
                    <div class="panel-body">
                        <div class="row">
                            <div id="thumbBruce" class="col-lg-4 col-sm-4 col-md-4 col-xs-12">
                                <img src="img/Bruce.jpg" class="img img-circle img-profile" />
                                <address>
                                    <br />
                                    <strong>Yi-Yu (Bruce)<br />
                                        Liu</strong><br />
                                    Founder<br />
                                    <hr />
                                    <strong>
                                        <abbr title="Phone">Cell:</abbr></strong> (072) 379 9000<br />
                                    <strong>
                                        <abbr title="E-mail">E-Mail:</abbr></strong> <a href="mailto:yiyubruceliu@gmail.com" class="fct">yiyubruceliu@gmail.com</a><br />
                                </address>
                            </div>
                            <div id="thumbLuke" class="col-lg-4 col-sm-4 col-md-4 col-xs-12">
                                <img src="img/Luke.jpg" class="img img-circle img-profile" />
                                <address>
                                    <br />
                                    <strong>Luke<br />
                                        Voigt</strong><br />
                                    Developer<br />
                                    <hr />
                                    <strong>
                                        <abbr title="Phone">Cell:</abbr></strong> (082) 559 2322<br />
                                    <strong>
                                        <abbr title="E-mail">E-mail:</abbr></strong> <a href="mailto:lukevoigt0407@gmail.com" class="fct">lukevoigt0407@gmail.com</a><br />
                                </address>
                            </div>
                            <div id="thumbRuco" class="col-lg-4 col-sm-4 col-md-4 col-xs-12">
                                <img src="img/Ruco.jpg" class="img img-circle img-profile" />
                                <address>
                                    <br />
                                    <strong>Ruco<br />
                                        Pretorius</strong><br />
                                    Developer<br />
                                    <hr />
                                    <strong>
                                        <abbr title="Phone">Cell:</abbr></strong> (084) 207 1214<br />
                                    <strong>
                                        <abbr title="E-mail">E-Mail:</abbr></strong> <a href="mailto:ruco.facebook@gmail.com" class="fct">ruco.facebook@gmail.com</a><br />
                                </address>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading bkb">
                    <h1 class="panel-title wHead">
                        <a data-toggle="collapse" data-parent="#accordion" href="#colpnlSendMessage"><span aria-hidden="true" style="font-size: 80%; margin-right: 5px" class="glyphicon glyphicon-envelope fct"></span>Contact us!</a>
                    </h1>
                </div>
                <div id="colpnlSendMessage" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="container">
                            <div class="col-lg-12 text-center col-md-12 topSpace">
                                <h3>Fill out the form below and we will try to get back to you as soon as possible!</h3>
                                <form name="sentMessage" id="contactForm" novalidate>
                                    <div class="row control-group">
                                        <div class="form-group col-xs-12 floating-label-form-group controls">
                                            <label class="text-left">Name</label>
                                            <input type="text" class="form-control" placeholder="Name" id="name" required data-validation-required-message="Please enter your name.">
                                            <p class="help-block text-danger"></p>
                                        </div>
                                    </div>
                                    <div class="row control-group">
                                        <div class="form-group col-xs-12 floating-label-form-group controls">
                                            <label class="text-left">Email Address</label>
                                            <input type="email" class="form-control" placeholder="Email Address" id="email" required data-validation-required-message="Please enter your email address.">
                                            <p class="help-block text-danger"></p>
                                        </div>
                                    </div>
                                    <div class="row control-group">
                                        <div class="form-group col-xs-12 floating-label-form-group controls">
                                            <label class="text-left">Phone Number</label>
                                            <input type="tel" class="form-control" placeholder="Phone Number" id="phone" required data-validation-required-message="Please enter your phone number.">
                                            <p class="help-block text-danger"></p>
                                        </div>
                                    </div>
                                    <div class="row control-group">
                                        <div class="form-group col-xs-12 floating-label-form-group controls">
                                            <label class="text-left">Message</label>
                                            <textarea rows="5" class="form-control" placeholder="Message" id="message" required data-validation-required-message="Please enter a message."></textarea>
                                            <p class="help-block text-danger"></p>
                                        </div>
                                    </div>
                                    <br>
                                    <div id="success"></div>
                                    <div class="row">
                                        <div class="form-group col-xs-12">
                                            <button type="submit" class="btn btn-success btn-lg pull-right">Send  <span aria-hidden="true" class="glyphicon glyphicon-send"></span></button>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
