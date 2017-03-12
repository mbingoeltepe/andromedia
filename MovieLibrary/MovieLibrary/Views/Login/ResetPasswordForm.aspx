<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Passwort zurücksetzen - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Passwort ändern</h2>
    <% using (Html.BeginForm("DoResetPassword", "Login"))
       { %>
    <div class="formLayoutRegister">
        <label>
            E-Mail:
        </label>
        <label>
            <%: ViewData["mailAddress"] %>
        </label>
        <%: Html.TextBox("TextBoxEMail", ViewData["mailAddress"], new { style = "display: none;" })%>
        <br />
        <label>
            Neues Passwort:
        </label>
        <%: Html.Password("TextBoxPassword")%>
        <br />
        <label>
            Neues Passwort wiederholen:
        </label>
        <%: Html.Password("TextBoxPasswordConfirm")%>
        <br />
        <br />
        <input type="submit" value="Passwort ändern" style="float: right" />
        <br />
        <br />
        <br />
        <br />
    </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
