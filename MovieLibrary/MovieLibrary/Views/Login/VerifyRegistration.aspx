<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Registrierung bestätigt - Andromedia
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Registrierung bestätigt!</h2>

    Herzlichen Glückwunsch!
    <br />
    Sie haben sich erfolgreich bei Andromedia registriert. =)
    <br />
    <br />
    Hier gehts zu Ihrem <%: Html.ActionLink("Profil", "ShowProfile", "User", new { @class="defaultLink", @style = "font-weight:bold" })%>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>