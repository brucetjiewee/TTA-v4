<%@ Page Title="Wizard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Results.aspx.cs" Inherits="Results" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="Scripts/html2canvas.js"></script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
                                            <asp:LinkButton ID="btnPreviousOutcome" runat="server" CssClass="btn btn-primary pull-right bkb" OnClick="btnPreviousOutcome_Click"><span aria-hidden="true" class="glyphicon glyphicon-chevron-left"></span></asp:LinkButton>
                                        </div>
                                        <div class="col-lg-2 col-sm-4 col-md-2 col-xs-4">
                                            <asp:Label ID="lblOutcomes" runat="server" CssClass="text-center" Text="Solution <#> of <#>" OnLoad="lblOutcomes_Load"></asp:Label>
                                        </div>
                                        <div class="col-lg-5 col-sm-4 col-md-5 col-xs-4">
                                            <asp:LinkButton ID="btnNextOutcome" runat="server" CssClass="btn btn-primary bkb" OnClick="btnNextOutcome_Click"><span aria-hidden="true" class="glyphicon glyphicon-chevron-right"></span></asp:LinkButton>
                                            <asp:LinkButton ID="btnPreviewImg" runat="server" CssClass="btn btn-primary pull-right" OnClientClick="PrintDiv(divTimeTable)">Download <span aria-hidden="true" class="glyphicon glyphicon-download"></span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Panel ID="pnlTimeTable" runat="server">
                                        <div id="divTimeTable" style="margin-top: 15px; margin-bottom: auto" class="table-responsive">
                                            <asp:Table ID="tblOutput" CssClass="table table-bordered table-inverse" runat="server">
                                                <asp:TableHeaderRow runat="server" Height="40px" Font-Bold="true">
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

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="ResultsProgressPanel" runat="server" AssociatedUpdatePanelID="ResultsUpdatePanel">
        <ProgressTemplate>
            <div class="loading" id="loadmodal" style="display: normal"></div>
            <div class="modal-backdrop" id="modal" style="background-color: rgba(72, 61, 139, 0.70);">
                <img src="LoadScreen.png" alt="LOADING" style="height: 100px; width: 100px; position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script>
        function PrintDiv(n) { html2canvas(n, { onrendered: function (n) { downloadURI(n.toDataURL(), "My TimeTable.png") } }) } function downloadURI(n, o) { var d = document.createElement("a"); d.download = o, d.href = n, document.body.appendChild(d), d.click() }
    </script>
</asp:Content>
