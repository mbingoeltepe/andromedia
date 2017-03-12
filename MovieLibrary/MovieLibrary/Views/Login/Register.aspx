<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Registrierung - Andromedia
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Registrierung</h2>
    <!--
    <a class="defaultLink" href="<%: ViewData["link"].ToString() %>">E-Mail bestätigen (Entwickler Stadium Placebo)</a>
    <br />
    <br />
    -->
    Eine E-Mail mit dem Bestätigungs - Link wurde Ihnen auf Ihre angegebene Adresse zugesandt.
    <br />
    Folgen Sie den Anweisungen in der E-Mail, um den Vorgang abzuschließen.
</asp:Content>