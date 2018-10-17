<%@ Page Title="Wizard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Wizard.aspx.cs" Inherits="Wizard" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Content/agency.css" <%--type="text/css"--%> />
    <style type="text/css">
        .modal {position: fixed;top: 0;left: 0;background-color: rgba(72, 61, 139, 0.70);z-index: 99;opacity: 0.1; filter: alpha(opacity=10);-moz-opacity: 0.1;min-height: 100%;width: 100%;}
        .loading {width: 100px;height: 100px;border: 5px solid;border-top-color: darkslateblue;border-bottom-color: darkslateblue;border-right-color: tomato;border-left-color: tomato;border-radius: 100%;position: fixed; left: 0;right: 0;top: 0;bottom: 0;margin: auto;z-index: 9999;<%-- Spinning/Rotation: --%> animation: Spin 2s linear infinite;}@keyframes Spin {<%-- Spinning/Rotation: --%> from {transform: rotate(0deg);}to {transform: rotate(360deg);}}
    </style>
    <script type="text/javascript">function ShowProgress() {setTimeout(function () {var modal = $('<div />');modal.addClass("modal");$('body').append(modal);var loading = $(".loading");loading.show();try {var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0); }catch (err) { }}, 200);}function loadscreen() {ShowProgress();}</script>
<%-- Start of Page: "--%>
    <div class="container body-content topSpace"></div>
    <div class="jumbotron">
        <div class="container container-fluid">
            <div class="row" style="margin-top: -25px">
                <%--                <div class="col-md-2" <%--style="margin-top: 10px"--%>
                <%--<img src="Design Graphics/TTATempLogoWhiteBg2.png" alt="TTAv4Logo" style="width: 150px; border: 0"/>--%>
                <%--                </div>--%>
                <div class="col-md-12 col-xs-12" style="width: 100%">
                    <h1 style="font-family: 'Segoe UI'; font-size: 40px; color: black" class="text-center">TimeTable Wizard</h1>
                    <p class="lead text-center" style="line-height: 18px">Use the sections below to select your <strong>campus/es</strong>, <strong>modules</strong>, <strong>time period</strong> and <strong>language</strong>:</p>
                    <div style="margin-top: 30px" id="sharebuttons" class="text-center">
                        <p class="outlineblack" style="margin-bottom: 15px; color: black;">Share this page</p>
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
    </div>
    <div class="container container-fluid">
        <div id="rowDisclaimer" class="row" style="text-align: center">
            <div class="col-lg-12 col-sm-12 col-md-12 col-xs-12" style="font-weight: bold; background-color: tomato; color: white; border-color: red; border-radius: 5px; margin-top: -25px; line-height: 20px">
                <p>THIS IS THE INITIAL WEB RELEASE OF THE TimeTable ASSISTANT. PLEASE ASSIST US IN IMPROVING THE QUALITY OF OUR SERVICE BY PROMPTLY REPORTING ANY BUGS, ISSUES OR ERRORS USING THE TOOLS PROVIDED ON THE <span class="bkb" style="display: inline-block; text-align: center; border: 1px solid darkslateblue; color: white; width: 80px; height: 25px"><span aria-hidden="true" class="glyphicon glyphicon-envelope" style="color: deepskyblue"></span>Contact</span> PAGE ABOVE.</p>
            </div>
        </div>
    </div>
    <br />
    <asp:UpdatePanel ID="WizardUpdatePanel" runat="server">
        <ContentTemplate>
            <div class="container container-fluid">
                <div class="row">
                    <div id="pnlInputs" class="panel panel-default" style="padding: 10px">
                        <div class="row" style="min-height: 380px;">
                            <div id="divCampus" class="col-md-3 col-sm-12" style="/*border: 1px solid darkslateblue; */ /*margin: auto; */ min-width: 200px; min-height: 335px">
                                <h2 style="color: darkslateblue"><span style="color: tomato; font-size: 70%" class="glyphicon glyphicon-map-marker"></span>Campus</h2>
                                <p style="margin-top: 20px; margin-bottom: 25px; text-align: left">
                                    Select your <strong style="text-transform: uppercase">
                                        <abbr title="Campus refers to a complete TimeTable followed by a specific geographical campus.">campus</abbr></strong> or <strong style="text-transform: uppercase">
                                            <abbr title="Faculty refers to certain faculties that follow specialised TimeTables.">faculty:</abbr><br />
                                        </strong>
                                </p>
                                <div>
                                    <span style="font-style: normal; text-indent: 5px; font-size: 14px; font-family: 'Segoe UI', Helvetica, sans-serif; text-align: left">
                                        <p>
                                            <asp:CheckBox ID="cbxHatfield" runat="server" Text="Hatfield Campus" />
                                        </p>
                                        <p>
                                            <asp:CheckBox ID="cbxMamelodi" runat="server" Text="Mamelodi Campus" />
                                        </p>
                                        <p>
                                            <asp:CheckBox ID="cbxGroenkloof" runat="server" Text="Groenkloof Campus" />
                                        </p>
                                        <p>
                                            <asp:CheckBox ID="cbxTheology" runat="server" Text="Theology Faculty" />
                                        </p>
                                        <p>
                                            <asp:CheckBox ID="cbxEngineering" runat="server" Text="Engineering Faculty" />
                                        </p>
                                    </span>
                                </div>
                            </div>
                            <div id="divMod" class="col-md-6 col-sm-12" style="/*margin: auto; */ /*border: 1px solid darkslateblue; */ min-width: 200px; min-height: 335px">

                                <h2 class="fcb">
                                    <span style="color: tomato; font-size: 80%" class="glyphicon glyphicon-tags "></span>Module</h2>
                                <p style="margin-top: 20px; margin-bottom: 25px; text-align: left">
                                    Select your <strong style="text-transform: uppercase">
                                        <abbr title="A module is represented by a 6-digit code in the format 'AAA 999', e.g. 'ODT 200'.">modules:</abbr><br />
                                    </strong>
                                </p>
                                <div class="row">
                                    <div class="col-lg-7 col-md-7 col-sm-6 col-xs-6">
                                        <asp:TextBox ID="txtInput" Style="width: 110%" runat="server" AutoPostBack="true" OnTextChanged="btnSearch_Click" class="form-control" placeholder="E.g. EKN..."></asp:TextBox>
                                    </div>
                                    <div class="col-lg-5 col-md-5 col-sm-6 col-xs-6" <%--style="border: 1px solid black"--%>>
                                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search" OnClick="btnSearch_Click" OnClientClick="loadscreen()"> Search  <span aria-hidden="true" class="glyphicon glyphicon-search"></span></asp:LinkButton>
                                    </div><br /><br />
                                    <style type="text/css">
                                        .lbx {margin-left: 0;height: 125px;}
                                        .topBtn {margin-top: 10px;}
                                    </style>
                                    <div class="col-lg-5 col-sm-5 col-md-5 col-xs-5">
                                        <asp:ListBox ID="lbxSource" runat="server" Height="125px" CssClass="form-control lbx" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                    <div class="col-lg-2 col-sm-2 col-md-2 col-xs-2">
                                        <asp:LinkButton ID="btnTransfer" runat="server" CssClass="btn btn-success form-control btn-block topBtn" OnClick="btnTransfer_Click" OnClientClick="loadscreen()">
                                                <span aria-hidden="true" class="glyphicon glyphicon-chevron-right"></span>
                                        </asp:LinkButton>
                                        <br />
                                        <asp:LinkButton ID="btnRemove" runat="server" CssClass="btn btn-success form-control btn-block" OnClick="btnRemove_Click" OnClientClick="loadscreen()">
                                                <span aria-hidden="true" class="glyphicon glyphicon-chevron-left"></span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-lg-5 col-sm-5 col-md-5 col-xs-5">
                                        <asp:ListBox ID="lbxDestination" runat="server" Height="125px" CssClass="form-control lbx" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <div id="divTime" class="col-md-3 col-sm-12" style="/*border: 1px solid darkslateblue; */ /*margin-right: auto; */ min-width: 200px; min-height: 335px">
                                <h2 style="color: darkslateblue"><span style="color: tomato; font-size: 80%" class="glyphicon glyphicon-time"></span>Period</h2>
                                <div>
                                    <p style="margin-top: 20px; margin-bottom: 25px; text-align: left">
                                        Select the <strong style="text-transform: uppercase">
                                            <abbr title="Time period refers to the section of the academic year for which the TimeTable will be generated.">time period:</abbr></strong>
                                    </p>
                                    <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="btn btn-default btn-block" Style="width: 75%" placeholder="Select your campus...">
                                        <asp:ListItem>Semester 1</asp:ListItem>
                                        <asp:ListItem>Semester 2</asp:ListItem>
                                        <asp:ListItem>Quarter 1</asp:ListItem>
                                        <asp:ListItem>Quarter 2</asp:ListItem>
                                        <asp:ListItem>Quarter 3</asp:ListItem>
                                        <asp:ListItem>Quarter 4</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div>
                                    <p style="margin-top: 20px; margin-bottom: 25px; text-align: left">
                                        Select your preferred <strong style="text-transform: uppercase">
                                            <abbr title="Language refers to the language of instruction.">language:</abbr></strong>
                                    </p>
                                    <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="btn btn-default btn-block" Style="width: 75%" placeholder="Select your language...">
                                        <asp:ListItem>English</asp:ListItem>
                                        <asp:ListItem>Afrikaans</asp:ListItem>
                                        <asp:ListItem>Either</asp:ListItem>
                                    </asp:DropDownList>
                                    </p>
                                    <br />
                                    <p>
                                        <asp:LinkButton ID="btnTime" runat="server" CssClass="btn btn-success" type="submit" Style="margin-left: -25%; width: 75%" OnClick="btnTime_Click" OnClientClick="loadscreen()"> Generate  <span aria-hidden="true" class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                                    </p>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="WizardProgressPanel" runat="server" AssociatedUpdatePanelID="WizardUpdatePanel">
        <ProgressTemplate>
            <div class="loading" id="loadmodal" style="display: normal"></div>
            <div class="modal-backdrop" id="modal" style="background-color: rgba(72, 61, 139, 0.70);">
                <img src="Design Graphics/TTATempLogoWhiteBg.png" alt="LOADING" style="height: 100px; width: 100px; position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto" />
            </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script>$('#txtInput').keypress(function (event) {if (event.keyCode == 13) {$('#btnSearch').click();}})</script>
</asp:Content>
