<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MovieLibrary.Models.TVShowDetailsViewModel>>" %>
<h2 class="indent20">
    TV Serien:</h2>
<div class="main">
    <br />
    <% foreach (var item in Model)
       { %>
    <img src="<%: item.Image.Url %>" height="<%: item.Image.Height %>" width="<%: item.Image.Width %>"
        alt="<%: item.Media.Title %>" title="<%: item.Media.Title %>" style="float: left;
        margin-right: 10px" />
    <b>
        <%: Html.ActionLink(item.Media.Title, "../Media/TVShow", new { id = item.Media.Id }, new { @class = "defaultLink" })%>
        <span>(<%: item.Media.ShowBeginning.Year.ToString()%>)</span></b>
    <br />
    (Original: "<%: item.Media.OriginalTitle%>")
    <br />
    <br />
    <% } %>
</div>
