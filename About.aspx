<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content topSpace"></div>
    <div class="panel-group" id="accordion" style="margin-top: 25px; margin-bottom: 20px">
        <div class="panel panel-default">
            <div class="panel-heading bkb">
                <h1 class="panel-title wHead">
                    <a data-toggle="collapse" data-parent="#accordion" href="#Disclaimer">
                        <img src="Design Graphics/TTATempLogoWhiteBg2.png" alt="TTAv4Logo" style="width: 38px; border: 0; float: center; margin-right: 10px" />
                        Disclaimer
                    </a>
                </h1>
            </div>
            <div id="Disclaimer" class="panel-collapse collapse in">
                <div class="panel-body" style="font-weight: bold; font-size: 120%; text-align: justify; color: black">
                    The <strong>TUKS TimeTable ASSISTANT</strong> is only <strong>intended to simplify the process of creating your TimeTable.</strong> It is a useful tool for generating TimeTables for any UP campus (or combinations campuses). However, note that <strong>care should be taken to confirm lecture times and groups</strong> with the official faculty TimeTable, available from UP. This tool is not officially supported by the University of Pretoria, it is your own responsibility to ensure that any timetable suggested by the tool is accurate. The University shall not be responsible for any inaccurate information provided.
                    <div class="col-lg-8 col-lg-offset-2 col-md-10 col-md-offset-1">
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
        <div class="panel panel-default">
            <div class="panel-heading bkb">
                <h1 class="panel-title wHead">
                    <a data-toggle="collapse" data-parent="#accordion" href="#colpnlUsageInstructions"><span aria-hidden="true" style="color: tomato; font-size: 80%; margin-right: 5px" class="glyphicon glyphicon-info-sign"></span>Instructions</a>
                </h1>
            </div>
            <div id="colpnlUsageInstructions" class="panel-collapse collapse">
                <div class="panel-body">
                    <ol style="text-align: justify; font-weight: bold; font-style: normal">
                        <li style="color: black">Navigate to the
                            <div class="bkb text-center" style="display: inline-block; border: 1px solid; color: white; width: 75px; height: 30px"><span aria-hidden="true" class="glyphicon glyphicon-th bkb"></span>Wizard</div>
                            page using the menu above.</li>
                        <li>Select a campus (or a combination campuses) from the " <span aria-hidden="true" style="color: tomato" class="glyphicon glyphicon-map-marker"></span><strong class="bkb">CAMPUS"</strong> section.</li>
                        <li style="color: black">Using the " <span aria-hidden="true" style="color: tomato" class="glyphicon glyphicon-tags"></span><strong class="bkb">MODULE"</strong> section, search for modules by typing the module code (or part of it) into the search box, and click
                            <div style="display: inline-block; border-radius: 2px; text-align: center; border: 1px solid red; color: white; background-color: tomato; width: 75px; height: 30px"><strong>Search <span aria-hidden="true" class="glyphicon glyphicon-search" style="margin-left: 3px"></span></strong></div>
                            .</li>
                        <li>To add a module to your TimeTable, select it from the resulting list on the left, and click
                            <div style="display: inline-block; border-radius: 2px; text-align: center; border: 1px solid red; color: white; background-color: tomato; width: 35px; height: 30px"><strong><span aria-hidden="true" class="glyphicon glyphicon-arrow-right"></span></strong></div>
                            .</li>
                        <li style="color: black">To remove a previously selected module from your TimeTable, select it from the list on the right, and click
                            <div style="display: inline-block; border-radius: 2px; text-align: center; border: 1px solid red; color: white; background-color: tomato; width: 35px; height: 30px"><strong><span aria-hidden="true" class="glyphicon glyphicon-arrow-left"></span></strong></div>
                            .</li>
                        <li>Using the " <span aria-hidden="true" style="color: tomato" class="glyphicon glyphicon-time"></span><strong class="bkb">PERIOD"</strong> section, select the relevant time period (i.e. quarter, semester or year).</li>
                        <li style="color: black">Again using the " <span aria-hidden="true" style="color: tomato" class="glyphicon glyphicon-time"></span><strong class="bkb">PERIOD"</strong> section, select the relevant language of instruction (i.e. English or Afrikaans).</li>
                        <li>Once all your desired settings have been provided, click
                            <div style="display: inline-block; border-radius: 2px; text-align: center; border: 1px solid red; color: white; background-color: tomato; width: 90px; height: 30px"><strong>Generate <span aria-hidden="true" class="glyphicon glyphicon-ok" style="margin-left: 3px"></span></strong></div>
                            .</li>
                        <li style="color: black">Your generated TimeTable will be presented on a new page.</li>
                        <li>Click
                            <div style="display: inline-block; border-radius: 2px; text-align: center; border: 1px solid red; color: white; background-color: tomato; width: 120px; height: 30px"><strong>Reset Wizard <span aria-hidden="true" class="glyphicon glyphicon-repeat" style="margin-left: 3px"></span></strong></div>
                            to return to the wizard and start a new TimeTable.</li>
                        <li style="color: black">Click
                            <div style="display: inline-block; border-radius: 2px; text-align: center; border: 1px solid red; color: white; background-color: tomato; width: 140px; height: 30px"><strong>Send Via E-mail <span aria-hidden="true" class="glyphicon glyphicon-envelope" style="margin-left: 3px"></span></strong></div>
                            to send the generated TimeTable via e-mail.</li>
                        <li>Click
                            <div style="display: inline-block; border-radius: 2px; text-align: center; border: 1px solid red; color: white; background-color: tomato; width: 135px; height: 30px"><strong>Save As Image <span aria-hidden="true" class="glyphicon glyphicon-picture" style="margin-left: 3px"></span></strong></div>
                            to save the generated TimeTable to the current device.</li>
                    </ol>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading bkb">
                <h1 class="panel-title wHead">
                    <a data-toggle="collapse" data-parent="#accordion" href="#colpnlDevelopers"><span aria-hidden="true" style="color: tomato; font-size: 80%; margin-right: 5px" class="glyphicon glyphicon-user"></span>Developers</a>
                </h1>
            </div>
            <div id="colpnlDevelopers" class="panel-collapse collapse">
                <div class="panel-body">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #bbbbbb">
                            <h1 class="panel-title wHead">
                                <a data-toggle="collapse" data-parent="#accordion" href="#colpnlASPDevs"><span aria-hidden="true" style="font-size: 80%" class="glyphicon glyphicon-globe bkb"></span>ASP Migration Concept</a>
                            </h1>
                        </div>
                        <div id="colpnlASPDevs" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <h3 style="text-align: left">v4.0.0 TO CURRENT DEVELOPED BY:</h3>
                                <ul style="text-align: left">
                                    <li><span style="font-weight: bold">Yi-Yu (Bruce) Liu</span> - Founder</li>
                                    <li><span style="font-weight: bold">Luke Voigt</span> - Lead Developer</li>
                                    <li><span style="font-weight: bold">Ruco Pretorius</span> - Developer</li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #bbbbbb">
                            <h1 class="panel-title wHead">
                                <a data-toggle="collapse" data-parent="#accordion" href="#colpnlInitialDevs"><span aria-hidden="true" style="font-size: 80%" class="glyphicon glyphicon-globe bkb"></span>Initial Concept</a>
                            </h1>
                        </div>
                        <div id="colpnlInitialDevs" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <h3 style="text-align: left">v1.0.0 TO v3.0.0 DEVELOPED BY:</h3>
                                <ul style="text-align: left">
                                    <li><span style="font-weight: bold">Yi-Yu (Bruce) Liu</span> - Founder</li>
                                    <li><span style="font-weight: bold">Christopher Alexander Park</span> - Founder</li>
                                    <li><span style="font-weight: bold">Duran Nial Cole</span> - Joined: Jan 2014</li>
                                    <li><span style="font-weight: bold">Kristina Jovanovic</span> - Joined: Jun 2014</li>
                                    <li><span style="font-weight: bold">Nonde Masondo</span> - Joined: Jun 2014</li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading bkb">
                <h1 class="panel-title wHead">
                    <a data-toggle="collapse" data-parent="#accordion" href="#colpnlChangelog"><span aria-hidden="true" style="color: tomato; font-size: 80%; margin-right: 5px" class="glyphicon glyphicon-pencil"></span>Changelog</a>
                </h1>
            </div>
            <div id="colpnlChangelog" class="panel-collapse collapse">
                <div class="panel-body">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #bbbbbb">
                            <h1 class="panel-title wHead">
                                <a data-toggle="collapse" data-parent="#accordion" href="#colpnlV4Changelog"><span aria-hidden="true" style="font-size: 80%" class="glyphicon glyphicon-asterisk bkb"></span>Version 4.0.0</a>
                            </h1>
                        </div>
                        <div id="colpnlV4Changelog" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <ol style="text-align: left; font-weight: bold">
                                    <li style="color: black">Migrated to a web-based ASP WebForms platform!</li>
                                    <li>Temporarily limited input functionality to selection of campus/es, modules, TimeTable period and language of instruction in order to enable initial release as close as possible to registration for the 2017 academic year.</li>
                                    <li style="color: black">Temporarily limited output functionality to displaying, e-mailing and saving the generated TimeTable as an image in order to enable basic TimeTable sharing.</li>
                                    <li>Implemented Bootstrap 3 to enable a more accurate and smoother cross-platform application and display of the TUKS TimeTable ASSISTANT.</li>
                                    <li style="color: black">Implemented and adapted the free Agency v3.3.7+1 Bootstrap theme, source available from <a href="https://startbootstrap.com/template-overviews/agency/" style="color: tomato">StartBootstrap.com</a>.</li>
                                    <%--            <li>Implemented and adapted the free AZMIND Bootstrap Social Icons, source available from <a href="http://azmind.com/bootstrap-social-icons/" style="color: tomato">Azmind.com</a>.</li>--%>
                                </ol>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #bbbbbb">
                            <h1 class="panel-title wHead">
                                <a data-toggle="collapse" data-parent="#accordion" href="#colpnlV3Changelog"><span aria-hidden="true" style="font-size: 80%" class="glyphicon glyphicon-time bkb"></span>Version 3.0.0</a>
                            </h1>
                        </div>
                        <div id="colpnlV3Changelog" class="panel-collapse collapse">
                            <div class="panel-body">
                                <ol style="text-align: left; font-weight: bold">
                                    <li style="color: black">New overall design with a completely new look and feel.</li>
                                    <li>Implemented "Export to Image" functionality.</li>
                                    <li style="color: black">Implemented sorting of the generated options for more choice.</li>
                                    <li>Implemented integration of test schedules with a user's Google Account.</li>
                                    <li style="color: black">Implemented and enabled viewing of UP campus maps.</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #bbbbbb">
                            <h1 class="panel-title wHead">
                                <a data-toggle="collapse" data-parent="#accordion" href="#colpnlV2Changelog"><span aria-hidden="true" style="font-size: 80%" class="glyphicon glyphicon-time bkb"></span>Version 2.0.0</a>
                            </h1>
                        </div>
                        <div id="colpnlV2Changelog" class="panel-collapse collapse">
                            <div class="panel-body">
                                <ol style="text-align: left; font-weight: bold">
                                    <li style="color: black">Implemented a new wizard that automatically compiles a TimeTable based on pre-defined criteria.</li>
                                    <li>Wizard implemented as a multi-outcome generator - a variety of TimeTable options is generated</li>
                                    <li style="color: black">Lots of miscellaneous bug fixes.</li>
                                    <li>Improved program performance.</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading bkb">
                <h1 class="panel-title wHead">
                    <a data-toggle="collapse" data-parent="#accordion" href="#colpnlContributors"><span aria-hidden="true" style="color: tomato; font-size: 80%; margin-right: 5px" class="glyphicon glyphicon-thumbs-up"></span>Contributors</a>
                </h1>
            </div>
            <div id="colpnlContributors" class="panel-collapse collapse">
                <div class="panel-body">
                    <h1 class="panel-title" style="font-family: 'Segoe UI'; font-size: 30px; text-align: center; color: black">Main Concept Contributor: Jeanne-Michael du Plessis</h1>
                    <ul style="list-style-type: none; text-align: left">
                        <li>
                            <h3>Version 4:</h3>
                        </li>
                        <li>
                            <ul style="list-style-type: none">
                                <li>
                                    <img src="Design Graphics/UPlogo.png" style="width: 40px" /><strong>  University of Pretoria</strong> - (URLs for Updated CSV Files).<br />
                                    <br />
                                </li>
                                <li>
                                    <img src="Design Graphics/DeptINFlogo.png" style="width: 40px" /><strong>  Department of Informatics</strong> - (Marketing, Administration, Inspiration).</li>
                            </ul>
                        </li>
                        <li>
                            <h3>Version 3:</h3>
                        </li>
                        <li>
                            <ul>
                                <li><strong>Henk Pretorius</strong> - (Obtaining the Test TimeTable).</li>
                            </ul>
                        </li>
                        <li>
                            <h3>Version 2:</h3>
                        </li>
                        <li>
                            <ul>
                                <li><strong>Sophia Liu</strong> - (Additional ideas and areas for improvement).</li>
                            </ul>
                        </li>
                        <li>
                            <h3>Version 1:</h3>
                        </li>
                        <li>
                            <ul>
                                <li><strong>Henk Pretoruis</strong> - (Support, Database Guidance).</li>
                                <li><strong>Chris Kirkwood</strong> - (Database Guidance).</li>
                                <li><strong>Duran Nial Cole</strong> - (Idea, Coding and Debugging with Memento Design Pattern).</li>
                                <li><strong>Daniel Christiphor Alves Araujo</strong> - (Debugging with References).</li>
                                <li><strong>Tebogo Precious Makopo</strong> - (General Ideas).</li>
                                <li><strong>Tebogo Christopher Seshibe</strong> - (Debugging regarding Redo, Undo - Deep Copy Error).</li>
                                <li><strong>Anrich van Schalkwyk</strong> - (Idea for the Generator).</li>
                                <li><strong>Sophia Liu</strong> - (A veriety of ideas and areas for improvement)</li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading bkb">
                <h1 class="panel-title wHead">
                    <a data-toggle="collapse" data-parent="#accordion" href="#colpnlMotivation"><span aria-hidden="true" style="color: tomato; font-size: 80%; margin-right: 5px" class="glyphicon glyphicon-bullhorn"></span>Motivation</a>
                </h1>
            </div>
            <div id="colpnlMotivation" class="panel-collapse collapse">
                <div class="panel-body" style="font-weight: bold; font-size: 120%; text-align: justify; color: tomato">
                    This project was built under the innovation of INF 164 Lectures. Our first motivation to push for completion is the reward of our course coordinator allowing us to demonstrate this concept. We <strong>intend to release and keep updating this web application to all students</strong> of the University of Pretoria.
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading bkb">
                <h1 class="panel-title wHead">
                    <a data-toggle="collapse" data-parent="#accordion" href="#colpnlTesters"><span aria-hidden="true" style="color: tomato; font-size: 80%; margin-right: 5px" class="glyphicon glyphicon-cog"></span>Testers</a>
                </h1>
            </div>
            <div id="colpnlTesters" class="panel-collapse collapse">
                <div class="panel-body">
                    <h3>The developers would like to thank the following persons for their contributions during the testing phase/s:</h3>
                    <ul style="text-align: left">
                        <li><strong>Matthew White</strong></li>
                        <li><strong>Lindsay Norman</strong></li>
                        <li><strong>Johannes Pretorius</strong></li>
                        <li><strong>James Mann</strong></li>
                        <li><strong>Werner Mostert</strong></li>
                        <li><strong>Kent Smith</strong></li>
                        <li><strong>Sophia Liu</strong></li>
                    </ul>
                </div>
            </div>
        </div>        
    </div>
</asp:Content>
