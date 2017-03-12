<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MovieLibrary.Models.BorrowedDetails>>" %>
<% foreach (var item in Model)
   { %>
<%if (item.UserMedia.GetType() == typeof(MovieLibrary.Models.UserBook))
  { %>
Titel:
<%: ((MovieLibrary.Models.UserBook)item.UserMedia).Book.Title %><br />
<%} %>
<%else if (item.UserMedia.GetType() == typeof(MovieLibrary.Models.UserMovie))
    { %>
Titel:
<%: ((MovieLibrary.Models.UserMovie)item.UserMedia).Movie.Title %><br />
Format: 
<%: ((MovieLibrary.Models.UserMovie)item.UserMedia).StorageType%><br />
<%} %>
<%if (item.UserMedia.GetType() == typeof(MovieLibrary.Models.UserTV_Show))
  { %>
Titel:
<%: ((MovieLibrary.Models.UserTV_Show)item.UserMedia).Season.TV_Show.Title %><br />
Staffel:
<%: ((MovieLibrary.Models.UserTV_Show)item.UserMedia).Season.Number %><br />
Format: 
<%: ((MovieLibrary.Models.UserTV_Show)item.UserMedia).StorageType%><br />
<%} %>
Hergeborgt am:
<%: String.Format("{0:g}", item.Date) %><br />
Zurückgegeben am:
<% if (item.DateOfReturn.Date.ToShortDateString().Equals(DateTime.MaxValue.Date.ToShortDateString()))
   { %>
   Noch nicht zurückgegeben<br />
<% }
   else
   {%>
<%: String.Format("{0:g}", item.DateOfReturn)%><br />
<% } %>
Geborgt von:
<%: item.UserFrom.Username %><br />
<br />
<br />
<% } %>

   

