<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<MovieLibrary.Models.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Freund Suche - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h4>
        Gefundene User:</h4>
    <br />
    <br />
    <% int i = 0;  %>
    <% foreach (var user in Model)
       { %>
    <a class="defaultLink" href="javascript:addFriendToFriendList('user<%: i %>')">
        <%: user.Username%></a>
    <%--<%: Ajax.ActionLink(user.Username, "SendFriendRequest", new { username = user.Username }, new AjaxOptions { Confirm = "Sind Sie sicher, dass Sie '" + user.Username + "' als Freund hinzufügen wollen?" }, new { @class = "defaultLink" })%>--%>
    <br />
    <div id="user<%: i %>" title="Freund hinzufügen?" style="display: none">
        <% using (Html.BeginForm("SendFriendRequest", "User", new { username = user.Username }))
           { %>
        Sind Sie sicher, dass Sie '<%: user.Username  %>' als Freund hinzufügen wollen?
        <input name="id" type="hidden" value="<%: user.Username %>" />
        <% }%>
    </div>
    <% i++;
       } %>
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
