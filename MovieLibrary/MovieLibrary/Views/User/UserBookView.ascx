<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MovieLibrary.Models.ProfileMediaViewModel>" %>
<div class="userMediaList">
    <% if (Model.media != null)
       {
           if (Model.media.Count() > 0)
           { %>
    <% foreach (var item in Model.media)
       {
           MovieLibrary.Models.UserBook book = item as MovieLibrary.Models.UserBook;
    %>
    <% if (item.User.Username.Equals(HttpContext.Current.User.Identity.Name))
       { %>
    <%: Html.ActionLink(book.Book.Title, "Book", "Media", new { id = book.Book.Id }, new { @class = "defaultLink" })%>
    (<%: ((MovieLibrary.Models.Book)book.Book).Isbn%>)
    <% } %>
    <% else
        { %>
    <%: Html.ActionLink(book.Book.Title, "Book", "Media", new { username = item.User.Username, id = book.Book.Id }, new { @class = "defaultLink" })%>
    (<%: ((MovieLibrary.Models.Book)book.Book).Isbn%>)
    <% } %>
    <br />
    <% }
           }
           else
           { %>
    Es befinden sich noch keine Bücher in der Mediathek
    <% }
       }%>
    <% List<MovieLibrary.Models.UserMedia> borrowedList = new List<MovieLibrary.Models.UserMedia>();

       foreach (var borrowed in Model.borrowedMedia)
       {
           if (borrowed.UserMedia.GetType() == typeof(MovieLibrary.Models.UserBook) && borrowed.DateOfReturn.CompareTo(DateTime.Now) > 0)
           {
               borrowedList.Add(borrowed.UserMedia);
           }
       }
    %>
    <% if (borrowedList.Count() > 0)
       { %>
    <h4>
        Ausgeborgte Bücher:</h4>
    <%  string user = Model.username;
        foreach (var book in borrowedList)
        { %>
    <%: Html.ActionLink(((MovieLibrary.Models.UserBook)book).Book.Title, "Book", "Media", new { username = ((MovieLibrary.Models.UserBook)book).User.Username, id = ((MovieLibrary.Models.UserBook)book).Book.Id }, new { @class = "defaultLink" })%>
    (<%: ((MovieLibrary.Models.UserBook)book).Book.Isbn%>)<br />
    <label style="font-size: smaller">(von '<%: ((MovieLibrary.Models.UserBook)book).User.Username%>')</label>
    <% }
       }
    %>
</div>
