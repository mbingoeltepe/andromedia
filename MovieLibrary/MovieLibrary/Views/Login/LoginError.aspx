<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Login Fehler - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Login Fehler =/</h2>
    <% if (ViewData["errorMessage"] != null)
       { %>
    <%= ViewData["errorMessage"]%>
    <% }
       else
       { %>
    Leider stimmt das Passwort nicht.
    <br />
    <br />
    <!--<a class="defaultLink" href="javascript:login()">Versuchen Sie es erneut!</a>-->
    <%: Html.ActionLink("Versuchen Sie es erneut!", "SecureLogin", "Login", null, new { @class = "whiteFont" })%>
    <br />
    <br />
    <%: Html.ActionLink("Passwort vergessen?", "ForgottenPassword", "Login", null, new { @class = "defaultLink" })%>
    <% } %>
</asp:Content>
