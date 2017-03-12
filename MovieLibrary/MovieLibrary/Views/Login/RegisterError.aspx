<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Registierungsfehler - Andromedia
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Registrierungsfehler   =/</h2>

    Bei der Registrierung ist einer der folgenden Fehler aufgetreten: 
    <br />
    <br />
    - Die angegebene E-Mail Adresse ist bereits registriert.
    <br />
    - Das Passwort hat nicht mindestens 5 Zeichen.
    <br />
    - Die Passwörter stimmen nicht überein.
    <br />
    <br />
    <!--<a class="defaultLink" href="javascript:register()">Versuchen Sie es erneut!</a>-->
    <%: Html.ActionLink("Versuchen Sie es erneut!", "SecureRegister", "Login", null, new { @class = "whiteFont" })%>

</asp:Content>