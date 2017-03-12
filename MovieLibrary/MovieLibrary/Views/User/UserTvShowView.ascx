<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MovieLibrary.Models.ProfileMediaViewModel>" %>
<div class="userMediaList">
    <% if (Model.media != null)
       {
           if (Model.media.Count() > 0)
           { %>
    <% foreach (var item in Model.media)
       {
           MovieLibrary.Models.UserTV_Show show = item as MovieLibrary.Models.UserTV_Show;
    %>
    <% if (item.User.Username.Equals(HttpContext.Current.User.Identity.Name))
       { %>
    <%: Html.ActionLink(show.Season.TV_Show.Title, "TVShow", "Media", new { id = show.Season.TV_Show.Id }, new { @class="defaultLink" } )%>
    (<%: show.StorageType%>)
    <% } %>
    <% else
        { %>
    <%: Html.ActionLink(show.Season.TV_Show.Title, "TVShow", "Media", new { username = item.User.Username, id = show.Season.TV_Show.Id }, new { @class = "defaultLink" })%>
    (<%: show.StorageType%>)
    <% } %>
    <br />
    Staffel:
    <%: show.Season.Number%>
    <br />
    <% }
           }
           else
           { %>
    Es befinden sich noch keine TV Serien in der Mediathek
    <% }
       }%>
    <% List<MovieLibrary.Models.UserMedia> borrowedList = new List<MovieLibrary.Models.UserMedia>();

       foreach (var borrowed in Model.borrowedMedia)
       {
           if (borrowed.UserMedia.GetType() == typeof(MovieLibrary.Models.UserTV_Show) && borrowed.DateOfReturn.CompareTo(DateTime.Now) > 0)
           {
               borrowedList.Add(borrowed.UserMedia);
           }
       }
    %>
    <% if (borrowedList.Count() > 0)
       { %>
    <h4>
        Ausgeborgte TV Serien:</h4>
    <%  string user = Model.username;
        foreach (var tvShow in borrowedList)
        {
    %>
    <%: Html.ActionLink(((MovieLibrary.Models.UserTV_Show)tvShow).Season.TV_Show.Title, "TVShow", "Media", new { username = ((MovieLibrary.Models.UserTV_Show)tvShow).User.Username, id = ((MovieLibrary.Models.UserTV_Show)tvShow).Season.TV_Show.Id }, new { @class = "defaultLink" })%>
    (<%: ((MovieLibrary.Models.UserTV_Show)tvShow).StorageType%>)<br />
    <label style="font-size: smaller">(von '<%: ((MovieLibrary.Models.UserTV_Show)tvShow).User.Username %>')</label>
    <% }
       }
    %>
</div>
