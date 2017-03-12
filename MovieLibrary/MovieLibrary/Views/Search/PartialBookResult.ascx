<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MovieLibrary.Models.BookDetailsViewModel>>" %>
<h2 class="indent20">
    Bücher:</h2>
<div class="main">
    <br />
    <% foreach (var item in Model)
       { %>
    <img src="<%: item.Image.Url %>" height="<%: item.Image.Height %>" width="<%: item.Image.Width %>"
        alt="<%: item.Media.Title %>" title="<%: item.Media.Title %>" style="float: left;
        margin-right: 10px" />
    <b>
        <%: Html.ActionLink(item.Media.Title, "../Media/Book", new { id = item.Media.Id }, new { @class="defaultLink" })%>
        <span>(ISBN:
            <%: item.Media.Isbn %>)</span></b>
    <br />
    (Original: "<%: item.Media.OriginalTitle%>")
    <br />
    <br />
    <% } %>
</div>
