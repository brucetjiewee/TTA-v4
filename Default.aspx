<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <header id="top" class="header">
        <div class="text-vertical-center container">
            <h1>Chylls TimeTable Assistant</h1>
            <p class="subtitle">A minimalistic, free TimeTable generator tool for the University of Pretoria.</p>
            <br />
            <a href="/../Wizard.aspx" class="btn btn-dark btn-lg">Get Started! <span class="glyphicon glyphicon-chevron-right"></span></a>
        </div>
    </header>
    <script>alert('Please take note that this website is owned and operated independently from the University of Pretoria. It is a tool intended to aid in the creation of your timetable and as such it is YOUR OWN responsibility to ensure the accuracy of the timetable provided. For more information please feel free to contact us.');</script>
</asp:Content>
