<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Passwort vergessen - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Passwort vergessen?</h2>
    <hr />
    <br />
    <br />
    <% using (Html.BeginForm("SendForgottenPasswordLink", "Login"))
       { %>
    <%: Html.Label("E-Mail Adresse:") %>
    <br />
    <%= Html.TextBox("TextBoxEMail", null, new { style = "width: 200px" } )%>
    <br />
    <br />
    <input type="submit" value="Passwort zurücksetzen" />
    <% } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
