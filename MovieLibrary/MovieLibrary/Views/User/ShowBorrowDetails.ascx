<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MovieLibrary.Models.BorrowedMediaDetail>" %>
<%if (Model != null)
  { %>
<% using (Html.BeginForm("TakeBorrowedMediaBack", "Mediathek", new { userMediaId = Model.media.Id }))
   { %>
<div>
    Hergeborgt am:
    <%: Model.details.Date%>
    <br />
    <br />
    Verborgt an:
    <%: Model.details.NameTo%>
</div>
<% } %>
<% }
  else
  {%>
Keine Informationen über diesen Verborgt-Status verfügbar.
<% } %>