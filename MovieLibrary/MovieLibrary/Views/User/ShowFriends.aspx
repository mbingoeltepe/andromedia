<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Freunde - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Meine Freunde:</h2>
    <hr />
    <div id="NewFriend" style="float: right">
        <h3>
            Neuen Freund hinzufügen:</h3>
        <label>
            E-Mail Adresse des Freundes:
        </label>
        <% using (Html.BeginForm("SearchUser", "User"))
           { %>
        <%: Html.TextBox("UsernameBox")%>
        <br />
        <br />
        <input value="Freund suchen" type="submit" />
        <% } %>
    </div>
    <br />
    <br />
    <% if (Model.Friends.Count() <= 0)
       { %>
    <label>
        Sie haben leider noch keine Freunde
    </label>
    <% }
       else
       {
    %>
    <label>
        Du hast
        <%= Model.Friends.Count()%>
        <%= (Model.Friends.Count() == 1 ? "Freund" : "Freunde") %>:
    </label>
    <% } %>
    <br />
    <br />
    <div>
        <% foreach (var friend in Model.Friends)
           { %>
        <ul style="list-style-type: none">
            <li>
                <%: Html.ActionLink(friend.Username, "ShowUserMediathek", new { username = friend.Username }, new { @class = "defaultLink" })%>
                - (
                <%: Html.ActionLink("Löschen", "RemoveFriend", new { username = friend.Username }, new { @class = "defaultLink", style="font-size: smaller" })%>
                ) </li>
        </ul>
        <% } %>
        <br />
        <br />
        <% if (Model.Requestlist.Count > 0)
           { %>
        <label>
            Anfragen:
        </label>
        <% foreach (var friend in Model.Requestlist)
           { %>
        <ul style="list-style-type: none">
            <li>
                <label>
                    <%: friend.Username%></label></li>
            <%: Html.ActionLink(" Bestätigen   ", "ConfirmFriendRequest", new { username = friend.Username }, new { @class = "defaultLink" })%>
            -
            <%: Html.ActionLink("   Ablehnen ", "DiscardFriendRequest", new { username = friend.Username }, new { @class = "defaultLink" })%>
        </ul>
        <% }
           } %>
        <br />
        <br />
        <br />
        <%-- <a href="javascript:showFindUser()">Neuen Freund hinzufügen</a>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
