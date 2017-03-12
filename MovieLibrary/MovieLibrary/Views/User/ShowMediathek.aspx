<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Mediathek - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <span class="alignLeft">Mediathek <%= (!HttpContext.Current.User.Identity.Name.Equals(Model.Username) ? " von '" + Model.Username + "'" : "") %></span>
    </h2>
    <span class="alignRight">
        <% if (Model.Username.Equals(HttpContext.Current.User.Identity.Name))
           { %>
        <%:  Html.ActionLink("TV Serien", "ShowUserTVShows", "Mediathek", null, new { @class = "defaultLink" })%>
        |
        <%: Html.ActionLink("Filme", "ShowUserMovies", "Mediathek", null, new { @class = "defaultLink" })%>
        |
        <%: Html.ActionLink("Bücher", "ShowUserBooks", "Mediathek", null, new { @class = "defaultLink" })%>
        <% } %>
        <% else
            { %>
        <%: Html.ActionLink("TV Serien", "ShowUserTVShows", "Mediathek", new { username = Model.Username }, new { @class = "defaultLink" })%>
        |
        <%: Html.ActionLink("Filme", "ShowUserMovies", "Mediathek", new { username = Model.Username }, new { @class = "defaultLink" })%>
        |
        <%: Html.ActionLink("Bücher", "ShowUserBooks", "Mediathek", new { username = Model.Username }, new { @class = "defaultLink" })%>
        <% } %>
    </span>
    <br />
    <br />
    <hr />
    <h4>
        Zuletzt hinzugefügte Medien:
    </h4>
    <br />
    <span class="userMediaBox" style="width: 150px"><b style="text-align: center">TV Serien:</b>
        <br />
        <br />
        <% int j = 0;

           List<MovieLibrary.Models.UserMedia> tvShows = new List<MovieLibrary.Models.UserMedia>();
           foreach (var i in Model.UserMedien)
           { %>
        <% if (i.GetType() == typeof(MovieLibrary.Models.UserTV_Show) && j < 5)
           {
               MovieLibrary.Models.UserTV_Show tvShow = ((MovieLibrary.Models.UserTV_Show)i);
               tvShows.Add(tvShow);

               j++;
           }
           if (j >= 5)
           {
               break;
           }
        %>
        <% }
           MovieLibrary.Models.ProfileMediaViewModel model = new MovieLibrary.Models.ProfileMediaViewModel(tvShows, Model.Username,
               MovieLibrary.Service.ServicesImpl.UserMediaService.Instance.GetAllBorrowedFromMediaByUser(Model.Username).ToList());
           Html.RenderPartial("UserTvShowView", model); %>
    </span><span class="userMediaBox" style="width: 150px"><b style="text-align: center">
        Filme:</b>
        <br />
        <br />
        <% int x = 0;
           List<MovieLibrary.Models.UserMedia> movies = new List<MovieLibrary.Models.UserMedia>();
           foreach (var i in Model.UserMedien)
           { %>
        <% if (i.GetType() == typeof(MovieLibrary.Models.UserMovie) && x < 5)
           {
               MovieLibrary.Models.UserMovie mov = ((MovieLibrary.Models.UserMovie)i);
               movies.Add(mov);

               x++;
           }
           if (x >= 5)
           {
               break;
           }
        %>
        <% }
           MovieLibrary.Models.ProfileMediaViewModel modelMovie = new MovieLibrary.Models.ProfileMediaViewModel(movies, Model.Username,
              MovieLibrary.Service.ServicesImpl.UserMediaService.Instance.GetAllBorrowedFromMediaByUser(Model.Username).ToList());
           Html.RenderPartial("UserMovieView", modelMovie); %>
    </span><span class="userMediaBox" style="width: 150px"><b style="text-align: center">
        Bücher:</b>
        <br />
        <br />
        <% int y = 0;
           List<MovieLibrary.Models.UserMedia> books = new List<MovieLibrary.Models.UserMedia>();
           foreach (var i in Model.UserMedien)
           { %>
        <% if (i.GetType() == typeof(MovieLibrary.Models.UserBook) && y < 5)
           {
               MovieLibrary.Models.UserBook book = ((MovieLibrary.Models.UserBook)i);
               books.Add(book);

               y++;
           }
           if (y >= 5)
           {
               break;
           }
        %>
        <% }
           MovieLibrary.Models.ProfileMediaViewModel modelBook = new MovieLibrary.Models.ProfileMediaViewModel(books, Model.Username,
              MovieLibrary.Service.ServicesImpl.UserMediaService.Instance.GetAllBorrowedFromMediaByUser(Model.Username).ToList());
           Html.RenderPartial("UserBookView", modelBook); %>
    </span>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
