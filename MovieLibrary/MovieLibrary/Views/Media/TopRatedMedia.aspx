<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<MovieLibrary.Models.Media>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    TopRatedMedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="topRated">
        <h2>
            Die Medien mit den besten Ratings:
        </h2>
        <hr />
        <br />
        <% foreach (var item in Model)
           { %>
        <% if (item.GetType() == typeof(MovieLibrary.Models.Movie))
           { %>
        Film :
        <%: Html.ActionLink(item.Title, "Movie", "Media", new { id = item.Id}, new { @class = "defaultLink" }) %>
        (Rating:
        <%: item.AverageRating %>)
        <% } %>
        <% if (item.GetType() == typeof(MovieLibrary.Models.Book))
           { %>
        Buch:
        <%: Html.ActionLink(item.Title, "Book", "Media", new { id = item.Id}, new { @class = "defaultLink" }) %>
        (Rating:
        <%: item.AverageRating %>)
        <% } %>
        <% if (item.GetType() == typeof(MovieLibrary.Models.TV_Show))
           { %>
        Serie:
        <%: Html.ActionLink(item.Title, "TVShow", "Media", new { id = item.Id}, new { @class = "defaultLink" }) %>
        (Rating:
        <%: item.AverageRating %>)
        <% } %>
        <br />
        <br />
        <% } %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
