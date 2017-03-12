<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.Actor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ActorAlreadyThere
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    Dieser Schauspieler scheint schon in der Datenbank zu existieren. Falls dies nicht der Fall ist, wenden Sie sich bitte an den Systemadministrator.

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
