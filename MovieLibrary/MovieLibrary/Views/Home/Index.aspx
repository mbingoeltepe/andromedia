<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<MovieLibrary.Models.MediaImageUserViewModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2 style="text-align: center">
            Willkommen zu Andromedia</h2>
        <hr />
        Hier finden Sie alles, was Ihr Sammlerherz begehrt!
        <br />
        <br />
        Bei uns dreht sich alles rund um Filme, Serien & Bücher!<br />
        Wenn Sie <b><a class="defaultLink" href="javascript:disableDropDownBox()">
            Zitate</a></b> suchen, oder eine <b>
                <% if (Request.IsAuthenticated)
                   { %>
                <%: Html.ActionLink("Medienverwaltungs-Software", "ShowUserMediathek",
                "User", new { username = HttpContext.Current.User.Identity.Name }, new { @class = "defaultLink" })%>
                <% }
                   else
                   { %>
                Medienverwaltungs-Software
                <% } %>
            </b>, dann sind Sie bei uns genau richtig!
    </div>
    <div style="height: 100%">
        <br />
        <hr />
        <h4>
            Zuletzt hinzugefügte Medien zu Andromedia:
        </h4>
        <% foreach (var recommendedMedia in Model)
           { %>
        <% if (recommendedMedia.Media.GetType() == typeof(MovieLibrary.Models.Movie))
           { %>
        <a href='<%: Url.Action("Movie", "Media", new { id = recommendedMedia.Media.Id }) %>'>
            <img src="<%: recommendedMedia.Image.Url %>" height="<%: recommendedMedia.Image.Height %>"
                width="<%: recommendedMedia.Image.Width %>" alt="<%: recommendedMedia.Media.Title %>"
                title="<%: recommendedMedia.Media.Title %>" style="float: left; margin-right: 10px;
                margin-top: 10px" />
        </a>
        <% } %>
        <% else if (recommendedMedia.Media.GetType() == typeof(MovieLibrary.Models.TV_Show))
            { %>
        <a href='<%: Url.Action("TVShow", "Media", new { id = recommendedMedia.Media.Id }) %>'>
            <img src="<%: recommendedMedia.Image.Url %>" height="<%: recommendedMedia.Image.Height %>"
                width="<%: recommendedMedia.Image.Width %>" alt="<%: recommendedMedia.Media.Title %>"
                title="<%: recommendedMedia.Media.Title %>" style="float: left; margin-right: 10px;
                margin-top: 10px" />
        </a>
        <% } %>
        <% else if (recommendedMedia.Media.GetType() == typeof(MovieLibrary.Models.Book))
            { %>
        <a href='<%: Url.Action("Book", "Media", new { id = recommendedMedia.Media.Id }) %>'>
            <img src="<%: recommendedMedia.Image.Url %>" height="<%: recommendedMedia.Image.Height %>"
                width="<%: recommendedMedia.Image.Width %>" alt="<%: recommendedMedia.Media.Title %>"
                title="<%: recommendedMedia.Media.Title %>" style="float: left; margin-right: 10px;
                margin-top: 10px" />
        </a>
        <% } %>
        <% } %>
    </div>
</asp:Content>
