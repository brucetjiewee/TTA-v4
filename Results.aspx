<%@ Page Title="Wizard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Results.aspx.cs" Inherits="Results" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--// Themes: "--%>
    <link rel="stylesheet" href="Content/agency.css" <%--type="text/css"--%> />
    <%--// Link to downloaded Agency.css file as per Bruce's link "--%>
    <%--// Scripts & Styles: "--%>
    <script id="jsHTML2Canvas" src="Scripts/html2canvas.js"></script>
    <script id="jsSetupCanvas" type="text/javascript">
        //$("#divPreviewImage").hide();
        function PreviewImg() {
            var $element = $("#divTimeTable"); // global variable
            //$element.prependTo("#accordion2");
            var getCanvas; // global variable
            html2canvas($element, {
                onrendered: function (canvas) {
                    <%--//$element.prependTo("#accordion2");
                    //document.body.appendChild(canvas);--%>
                    $(canvas).prependTo("#divPreviewImage");

                    getCanvas = canvas;
                    alert('Image generated');
                }, width: 1920, height: 1080
            });
            <%--//var imgageData = getCanvas.toDataURL("image/png");
            // Now browser starts downloading it instead of just showing it
            //var newData = imgageData.replace(/^data:image\/png/, "data:application/octet-stream");
            //$("#<%:btnSaveImg.ClientID%>").attr("download", "TimeTable.png").attr("href", newData);--%>
        }

        <%--//function ConvertToImage() {
        //    alert("Image save started");
        //var imgageData = getCanvas.toDataURL("image/png");
        // Now browser starts downloading it instead of just showing it
        //var newData = imgageData.replace(/^data:image\/png/, "data:application/octet-stream");
        //$("#<%:btnSaveImg.ClientID%>").attr("download", "TimeTable.png").attr("href", newData);
        //    alert("Image saved");
        //}

        //function save2() {
        //    window.open(canvas.toDataURL('image/png'));
        //    var gh = canvas.toDataURL('png');

        //    var a = document.createElement('a');
        //    a.href = gh;
        //    a.download = 'image.png';

        //    a.click()
        //}--%>
    </script>
    <style id="cssLoadingScreen" type="text/css">
        .loading, .modal {
            position: fixed;
            left: 0;
            top: 0;
        }

        .modal {
            background-color: rgba(72,61,139,.7);
            z-index: 99;
            opacity: .1;
            filter: alpha(opacity=10);
            -moz-opacity: .1;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            width: 100px;
            height: 100px;
            border: 5px solid;
            border-radius: 100%;
            right: 0;
            bottom: 0;
            margin: auto;
            z-index: 9999;
            animation: Spin 2s linear infinite;
            border-color: #483d8b tomato;
        }

        @keyframes Spin {
            from {
                transform: rotate(0);
            }

            to {
                transform: rotate(360deg);
            }
        }
    </style>
    <script id="jsLoadingScreen" type="text/javascript">//function ShowProgress() {
        //            setTimeout(function () {
        //                var modal = $('<div />');
        //                modal.addClass("modal");
        //                $('body').append(modal);
        //                var loading = $(".loading");
        //                loading.show();
        //                try {
        //                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        //                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        //                }
        //                catch (err) { }
        //            }, 200);
        //        }

        //        function loadscreen() {
        //            ShowProgress();
        function ShowProgress() { setTimeout(function () { var o = $("<div />"); o.addClass("modal"), $("body").append(o); var t = $(".loading"); t.show(); try { Math.max($(window).height() / 2 - t[0].offsetHeight / 2, 0), Math.max($(window).width() / 2 - t[0].offsetWidth / 2, 0) } catch (a) { } }, 200) } function loadscreen() { ShowProgress() }</script>
    <div style="margin-top: 95px"></div>
    <div class="container body-content topSpace"></div>
    <asp:UpdatePanel ID="ResultsUpdatePanel" runat="server">
        <ContentTemplate>
            <div class="container">
                <div id="rowDisclaimer" class="row" style="text-align: center">
                    <div class="alert alert-warning alert-dismissible" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <p>
                            This is our initial version of the Chylls TimeTable System on the web. If there is any comments that you would like to give us, please hop onto our 
                            <a href="https://web.facebook.com/TimeTableAssistant">Facebook page</a>
                        </p>
                    </div>
                </div>
            </div>
            <div class="container">
                <div id="rowDisclaimer2" class="row" style="text-align: center">
                    <div class="alert alert-warning alert-dismissible" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <p>
                            Please take note that this website is owned and operated independently from the University of Pretoria. 
                                        It is a tool intended to aid in the creation of your timetable and as such it is your own
                                        responsibility to ensure the accuracy of the timetable provided. For more information please feel free to contact us. 
                        </p>
                    </div>
                </div>
            </div>
            <div class="container container-fluid">
                <div id="rowDisclaimer3" class="row" style="text-align: center; margin-bottom: 10px; margin-bottom: 10px">

                    <%--<br /><br />IN THE EVENT OF DISCREPANCIES/ERRORS, PLEASE ASSIST US IN IMPROVING THE QUALITY OF OUR SERVICE BY PROMPTLY REPORTING ANY BUGS, ISSUES OR ERRORS USING THE TOOLS PROVIDED ON THE <span style="display: inline-block; text-align:center; border: 1px solid darkslateblue; color: white; background-color: darkslateblue; width: 80px; height: 20px"><span aria-hidden="true" class="glyphicon glyphicon-envelope" style="color: deepskyblue"></span> Contact</span> PAGE ABOVE.--%>
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
                <div class="panel-group" id="accordion">
                    <div class="panel panel-primary">
                        <div class="panel-heading bkb">

                            <h1 class="panel-title">
                                <h4><a style="color: white" data-toggle="collapse" data-parent="#accordion" href="#colpnlYourTimeTable">
                                    <span aria-hidden="true" class="glyphicon glyphicon-th fct"></span>Your TimeTable</a>
                                    <asp:LinkButton ID="btnBack" runat="server" CssClass="pull-right" Style="color: white" href="Wizard.aspx">Restart Wizard 
                                    <span aria-hidden="true" class="glyphicon glyphicon-repeat" ></span></asp:LinkButton></h4>
                            </h1>
                        </div>
                        <div id="colpnlYourTimeTable" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <div class="panel-group" id="accordion2">
                                    <div id="OutcomeNavigator">
                                        <div class="col-lg-5 col-sm-4 col-md-5 col-xs-4">
                                            <asp:LinkButton ID="btnPreviousOutcome" runat="server" CssClass="btn btn-primary pull-right bkb" OnClick="btnPreviousOutcome_Click"><span aria-hidden="true" class="glyphicon glyphicon-chevron-left" style="color: deepskyblue"></span></asp:LinkButton>
                                        </div>
                                        <div class="col-lg-2 col-sm-4 col-md-2 col-xs-4">
                                            <asp:Label ID="lblOutcomes" runat="server" CssClass="text-center" Text="<#> of <#>" OnLoad="lblOutcomes_Load"></asp:Label>
                                        </div>
                                        <div class="col-lg-5 col-sm-4 col-md-5 col-xs-4">
                                            <asp:LinkButton ID="btnNextOutcome" runat="server" CssClass="btn btn-primary bkb" OnClick="btnNextOutcome_Click"><span aria-hidden="true" class="glyphicon glyphicon-chevron-right" style="color: deepskyblue"></span></asp:LinkButton>
                                            <asp:LinkButton ID="btnPreviewImg" runat="server" CssClass="btn btn-primary pull-right" OnClick="btnPreviewImg_Click">Preview Image <span aria-hidden="true" class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Panel ID="pnlTimeTable" runat="server">
                                        <div id="divTimeTable" style="margin-top: 15px; margin-bottom: auto" class="table-responsive">
                                            <asp:Table ID="tblOutput" CssClass="table table-bordered table-inverse" runat="server">
                                                <asp:TableHeaderRow runat="server" Height="40px" Font-Bold="true">
                                                    <%--<asp:TableHeaderCell runat="server" HorizontalAlign="Center"></asp:TableHeaderCell>--%>
                                                    <asp:TableCell runat="server" CssClass="tableHeader" Text="Time"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableHeader" Text="Mon"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableHeader" Text="Tue"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableHeader" Text="Wed"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableHeader" Text="Thu"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableHeader" Text="Fri"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableHeader" Text="Sat"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableHeader" Text="Sun"></asp:TableCell>
                                                </asp:TableHeaderRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="07:30-08:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="08:30-09:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="09:30-10:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="10:30-11:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="11:30-12:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="12:30-13:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="13:30-14:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="14:30-15:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="15:30-16:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="16:30-17:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="17:30-18:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="18:30-19:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="19:30-20:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableHeaderCell runat="server" CssClass="tableTimeHeader" Text="20:30-21:20"></asp:TableHeaderCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                    <asp:TableCell runat="server" CssClass="tableCell"></asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </div>
                                    </asp:Panel>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
                <div class="container container-fluid">
                    <%--<div class="panel-heading" style="background-color: darkslateblue">--%>
                    <%--<h1 class="panel-title wHead">--%>
                    <%--<a data-toggle="collapse" data-parent="#accordion" href="#colpnlImagePreview"><span aria-hidden="true" style="color: tomato; font-size: 80%" class="glyphicon glyphicon-picture"></span> Image Preview</a>--%>
                    <%--</h1>--%>
                    <%--</div>--%>
                    <%--<div id="colpnlImagePreview" class="panel-collapse collapse">--%>
                    <%--<div class="panel-body" >--%>
                    <div id="divPreviewImage"></div>
                    <%--</div>--%>
                    <%--</div>--%>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="ResultsProgressPanel" runat="server" AssociatedUpdatePanelID="ResultsUpdatePanel">
        <ProgressTemplate>
            <div class="loading" id="loadmodal" style="display: normal"></div>
            <div class="modal-backdrop" id="modal" style="background-color: rgba(72, 61, 139, 0.70);">
                <img src="Design Graphics/TTATempLogoWhiteBg.png" alt="LOADING" class="img img-responsive" <%--style="height: 100px; width: 100px; position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto"--%> />
            </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
