<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewData["title"] %>
    - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%: ViewData["heading"]%></h2>
    <!--
    <% if (ViewData["link"] != null)
       { %>
    <a class="defaultLink" href="<%: ViewData["link"].ToString() %>">Passwort zurücksetzen
        (Entwickler Stadium Placebo)</a>
    <% } %>
    <br />
    <br />
    -->
    <%: ViewData["message"] %>
    <%--Eine E-Mail mit weiteren Anweisungen wurde Ihnen auf Ihre angegebene Adresse zugesandt.--%>
</asp:Content>
