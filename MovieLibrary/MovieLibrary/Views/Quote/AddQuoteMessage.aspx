<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Zitat Anfrage - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Zitat Anfrage</h2>
    Vielen Dank für Ihren Beitrag. Ihre Eingaben werden in Kürze online sein.
    <br />
    <br />
    <br />
    <a href="<%= Request.UrlReferrer %>">Zurück</a>
</asp:Content>
