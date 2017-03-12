<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Anmeldung - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        Bitte loggen Sie sich ein:</h3>
    <div class="formLayoutRegister">
        <% using (Html.BeginForm("Login", "Login"))
           { %>
        <label>
            E-Mail:
        </label>
        <%: Html.TextBoxFor(m => m.Username)%>
        <br />
        <label>
            Passwort:
        </label>
        <%: Html.PasswordFor(m => m.Password)%>
        <div style="font-size: smaller">
            <label for="RememberCheckBox">
                Angemeldet bleiben</label><%: Html.CheckBox("RememberCheckBox")%>
        </div>
        <input type="submit" value="Anmelden" style="float: right" />
        <br />
        <br />
        <br />
        <br />
        <%: Html.ActionLink("Passwort vergessen?", "ForgottenPassword", "Login", null, new { @class = "defaultLink" })%>
        <br />
        <br />
        <%} %>
    </div>
</asp:Content>
