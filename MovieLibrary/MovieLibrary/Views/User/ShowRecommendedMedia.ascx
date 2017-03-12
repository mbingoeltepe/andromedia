<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MovieLibrary.Models.MediaImageUserViewModel>>" %>
<div>
    <% if (Model.Count() > 0)
       { %>
    <h4>
        Medien, welche Sie vielleicht interessieren könnten:
    </h4>
    <% foreach (var recommendedMedia in Model)
       { %>
    <% if (recommendedMedia.Media.GetType() == typeof(MovieLibrary.Models.Movie))
       { %>
    <a href='<%: Url.Action("Movie", "Media", new { id = recommendedMedia.Media.Id }) %>'>
        <img src="<%: recommendedMedia.Image.Url %>" height="<%: recommendedMedia.Image.Height %>"
            width="<%: recommendedMedia.Image.Width %>" alt="<%: recommendedMedia.Media.Title %>"
            title="<%: recommendedMedia.Media.Title %>" style="float: left; margin-right: 10px" />
    </a>
    <% } %>
    <% else if (recommendedMedia.Media.GetType() == typeof(MovieLibrary.Models.TV_Show))
        { %>
    <a href='<%: Url.Action("TVShow", "Media", new { id = recommendedMedia.Media.Id }) %>'>
        <img src="<%: recommendedMedia.Image.Url %>" height="<%: recommendedMedia.Image.Height %>"
            width="<%: recommendedMedia.Image.Width %>" alt="<%: recommendedMedia.Media.Title %>"
            title="<%: recommendedMedia.Media.Title %>" style="float: left; margin-right: 10px" />
    </a>
    <% } %>
    <% else if (recommendedMedia.Media.GetType() == typeof(MovieLibrary.Models.Book))
        { %>
    <a href='<%: Url.Action("Book", "Media", new { id = recommendedMedia.Media.Id }) %>'>
        <img src="<%: recommendedMedia.Image.Url %>" height="<%: recommendedMedia.Image.Height %>"
            width="<%: recommendedMedia.Image.Width %>" alt="<%: recommendedMedia.Media.Title %>"
            title="<%: recommendedMedia.Media.Title %>" style="float: left; margin-right: 10px" />
    </a>
    <% } %>
    <% } %>
    <% } %>
</div>
