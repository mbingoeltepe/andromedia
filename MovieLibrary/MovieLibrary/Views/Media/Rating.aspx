<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.Media>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Bewertung - Andromedia
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Bewertung</h2>
    <br />
    <br />
    <%= Html.Encode(ViewData["Message"])%>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <a href="<%= Request.UrlReferrer %>">Zurück</a>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
