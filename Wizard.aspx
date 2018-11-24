<%@ Page Title="Wizard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Wizard.aspx.cs" Inherits="Wizard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Start of Page: "--%>
    <%--<div style="margin-top: 95px"></div>--%>
    <%--<div class="container container-fluid">
        <div class="row" style="margin-top: -25px">
            <div class="col-md-12 col-xs-12" style="width: 100%">
                <h1 class="text-center">TimeTable Wizard</h1>
                <p class="lead text-center">Use the sections below to select your <strong>campus/es</strong>, <strong>modules</strong>, <strong>time period</strong> and <strong>language</strong>:</p>
                <div id="sharebuttons" class="text-center">
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
    </div>--%>
    <%--<div class="container">
        <div id="rowDisclaimer" class="row" style="text-align: center">
            <div class="alert alert-warning alert-dismissible" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <p>
                    This is the initial version of Chylls TimeTable Assistant on the Web.
                    <br />
                    Please assist us in improving the quality of our services by reporting any bugs, issues or errors by using the 
                     "<span class="bkb">
                         <span aria-hidden="true" class="glyphicon glyphicon-envelope" style="color: deepskyblue"></span>Contact"
                     </span>page above.
                </p>
            </div>
        </div>
    </div>--%>

    <asp:UpdatePanel ID="WizardUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="wizard-content" class="container"> 
                <h1>Let&apos;s build your timetable!</h1>
                <div id="CampusWizard" class="panel-collapse collapse <%= Session["ActiveOne"]%>" role="tabpanel" aria-labelledby="headingOne">
                    <h2>First, let&apos;s select your campus</h2>
                    <h4>
                        Select your 
                        <strong>
                            <abbr title="Campus refers to a complete TimeTable followed by a specific geographical campus."> campus</abbr>
                        </strong> or
                        <strong>
                            <abbr title="Faculty refers to certain faculties that follow specialised TimeTables."> faculty:</abbr>
                        </strong>
                    </h4>
                    <div class="list-group">
                        <asp:CheckBox class="list-group-item " ID="cbxHatfield" runat="server" Text=" Hatfield campus" />
                        <asp:CheckBox class="list-group-item" ID="cbxMamelodi" runat="server" Text=" Mamelodi Campus" />
                        <asp:CheckBox class="list-group-item" ID="cbxGroenkloof" runat="server" Text=" Groenkloof Campus" />
                        <asp:CheckBox class="list-group-item" ID="cbxTheology" runat="server" Text=" Theology Faculty" />
                        <asp:CheckBox class="list-group-item" ID="cbxEngineering" runat="server" Text=" Engineering Faculty" />
                    </div>
                     <asp:LinkButton ID="btnCampusNext" runat="server" CssClass="btn btn-primary btn-lg pull-right" OnClientClick="loadscreen()" OnClick="btnCampusNext_Click">Next <span class="glyphicon glyphicon-chevron-down"></span></asp:LinkButton>
                </div>
                <div id="ModuleWizard" class="panel-collapse collapse <%= Session["ActiveTwo"]%>" role="tabpanel" aria-labelledby="headingTwo">
                    <h2 class="fcb">Next, let&apos;s select your modules</h2>
                    <h4>
                        Select your 
                        <strong>
                            <abbr title="A module is represented by a 6-digit code in the format 'AAA 999', e.g. 'ODT 200'.">modules:</abbr><br />
                        </strong>
                    </h4>
                    <div class="row">
                        <div class="col-sm-6">
                            <h2>Existing modules:</h2>
                            <asp:LinkButton ID="btnTransfer" runat="server" CssClass="btn btn-default form-control btn-block btnSharp" OnClick="btnTransfer_Click" OnClientClick="loadscreen()">
                        Add <span aria-hidden="true" class="glyphicon glyphicon-chevron-right"></span>
                            </asp:LinkButton>
                            <div class="input-group">
                                <asp:TextBox ID="txtInput" type="text" runat="server" OnTextChanged="btnSearch_Click" class="form-control" placeholder="E.g. EKN..."></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:Button ID="btnSearch" type="button" runat="server" CssClass="btn btn-default" Text="Search" OnClick="btnSearch_Click" OnClientClick="loadscreen()" />
                                </span>
                            </div>
                            <asp:ListBox ID="lbxSource" runat="server" CssClass="form-control" SelectionMode="Multiple" Rows="12"></asp:ListBox>
                        </div>
                        <div class="col-sm-6">
                            <h2>My Selected Modules</h2>
                            <asp:LinkButton ID="btnRemove" runat="server" CssClass="btn btn-default form-control btn-block btnSharp" OnClick="btnRemove_Click" OnClientClick="loadscreen()">
                        <span aria-hidden="true" class="glyphicon glyphicon-chevron-left"> Remove</span>
                            </asp:LinkButton>
                            <asp:ListBox ID="lbxDestination" runat="server" CssClass="form-control" SelectionMode="Multiple" Rows="14"></asp:ListBox>
                        </div>

                    </div>
                    
                </div>
                <div id="PeriodWizard" class="panel-collapse collapse <%= Session["ActiveThree"]%>" role="tabpanel" aria-labelledby="headingThree">
                    <h2>Lastly, let's select the period</h2>
                    <h4>
                        Select the <strong>
                            <abbr title="Time period refers to the section of the academic year for which the TimeTable will be generated.">time period:</abbr></strong>
                    </h4>
                    <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="btn btn-default btn-block" placeholder="Select your campus...">
                        <asp:ListItem>Semester 1</asp:ListItem>
                        <asp:ListItem>Semester 2</asp:ListItem>
                        <asp:ListItem>Quarter 1</asp:ListItem>
                        <asp:ListItem>Quarter 2</asp:ListItem>
                        <asp:ListItem>Quarter 3</asp:ListItem>
                        <asp:ListItem>Quarter 4</asp:ListItem>
                    </asp:DropDownList>
                            
                    <h4>
                        Select your preferred <strong><abbr title="Language refers to the language of instruction.">language:</abbr></strong>
                    </h4>
                    <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="btn btn-default form-control" placeholder="Select your language...">
                        <asp:ListItem>English</asp:ListItem>
                        <asp:ListItem>Afrikaans</asp:ListItem>
                        <asp:ListItem>Either</asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton ID="btnTime" runat="server" CssClass="btn btn-success btn-lg pull-right" type="submit" OnClick="btnTime_Click" OnClientClick="loadscreen()"> Generate  <span aria-hidden="true" class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                          
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="WizardProgressPanel" runat="server" AssociatedUpdatePanelID="WizardUpdatePanel">
        <ProgressTemplate>
            <div class="loading" id="loadmodal" style="display: normal"></div>
            <div class="modal-backdrop" id="modal" style="background-color: rgba(72, 61, 139, 0.70);">
                <img src="LoadScreen.png" alt="LOADING" style="height: 100px; width: 100px; position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <script>$('#txtInput').keypress(function (event) { if (event.keyCode == 13) { $('#btnSearch').click(); } })</script>
</asp:Content>
