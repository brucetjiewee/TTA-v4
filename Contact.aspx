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
                                        <abbr title="Phone">Cell:</abbr></strong> -<br />
                                    <strong>
                                        <abbr title="E-mail">E-mail:</abbr></strong> <a class="fct">-</a><br />
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
                                        <abbr title="Phone">Cell:</abbr></strong> -<br />
                                    <strong>
                                        <abbr title="E-mail">E-Mail:</abbr></strong> <a class="fct"></a><br />
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
                            <div class="col-lg-12  col-md-12 topSpace">
                                <h3 class="text-center">Fill out the form below and we will try to get back to you as soon as possible!</h3>
                                <form>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1"><span class="glyphicon glyphicon-user"></span> Full Name</label>
                                        <input type="email" class="form-control" id="inputName"  placeholder="Enter full name">
                                        
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1"><span class="glyphicon glyphicon-envelope"></span> Email address</label>
                                        <input type="email" class="form-control" id="inputEmail" placeholder="Enter email">
                                        
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1"><span class="glyphicon glyphicon-earphone"></span> Cellphone Number</label>
                                        <input type="number" class="form-control" maxlength="10" id="inputCell" placeholder="012 555 5555" pattern="/^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im" title="Must be a valid cell number">
                                        
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleFormControlTextarea1"><span class="glyphicon glyphicon-comment"></span> Message</label>
                                        <textarea class="form-control" id="exampleFormControlTextarea1" rows="5"></textarea>
                                    </div>
                                    <button type="submit" class="btn btn-primary btn-lg pull-right"><span class="glyphicon glyphicon-send"></span> Send</button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
