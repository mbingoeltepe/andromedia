<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MovieLibrary.Models.ProfileMediaViewModel>" %>
<div class="userMediaList">
    <% if (Model.media != null)
       {
           if (Model.media.Count() > 0)
           { %>
    <% foreach (var item in Model.media)
       {
           MovieLibrary.Models.UserMovie movie = item as MovieLibrary.Models.UserMovie;
    %>
    <% if (item.User.Username.Equals(HttpContext.Current.User.Identity.Name))
       { %>
    <%: Html.ActionLink(movie.Movie.Title, "Movie", "Media", new { id = movie.Movie.Id }, new { @class = "defaultLink" })%>
    (<%: movie.StorageType%>)
    <% } %>
    <% else
        { %>
    <%: Html.ActionLink(movie.Movie.Title, "Movie", "Media", new { username = item.User.Username, id = movie.Movie.Id }, new { @class = "defaultLink" })%>
    (<%: movie.StorageType%>)
    <% } %>
    <br />
    <% }
           }
           else
           { %>
    Es befinden sich noch keine Filme in der Mediathek
    <% }
       }%>
    <% List<MovieLibrary.Models.UserMedia> borrowedList = new List<MovieLibrary.Models.UserMedia>();

       foreach (var borrowed in Model.borrowedMedia)
       {
           if (borrowed.UserMedia.GetType() == typeof(MovieLibrary.Models.UserMovie) && borrowed.DateOfReturn.CompareTo(DateTime.Now) > 0)
           {
               borrowedList.Add(borrowed.UserMedia);
           }
       }
    %>
    <% if (borrowedList.Count() > 0)
       { %>
    <h4>
        Ausgeborgte Filme:</h4>
    <%  string user = Model.username;
        foreach (var movie in borrowedList)
        { %>
    <%: Html.ActionLink(((MovieLibrary.Models.UserMovie)movie).Movie.Title, "Movie", "Media", new { username = ((MovieLibrary.Models.UserMovie)movie).User.Username, id = ((MovieLibrary.Models.UserMovie)movie).Movie.Id }, new { @class = "defaultLink" })%>
    (<%: ((MovieLibrary.Models.UserMovie)movie).StorageType%>)<br />
    <label style="font-size: smaller">(von '<%: ((MovieLibrary.Models.UserMovie)movie).User.Username%>')</label>
    <% }
       }
    %>
</div>
