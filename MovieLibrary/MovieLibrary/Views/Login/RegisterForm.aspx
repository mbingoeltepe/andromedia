<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Registrierung - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        Geben Sie E-Mail und Passwort an:</h3>
    <% using (Html.BeginForm("SendVerificationEMail", "Login"))
       { %>
    <div class="formLayoutRegister">
        <label>
            E-Mail:
        </label>
        <%: Html.TextBox("TextBoxEMail") %>
        <label>
            Passwort:
        </label>
        <%: Html.Password("TextBoxPassword")%>
        <label>
            Passwort wiederholen:
        </label>
        <%: Html.Password("TextBoxPasswordConfirm")%>
        <input type="submit" value="Registrieren" style="float: right" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
    <%} %>
</asp:Content>
