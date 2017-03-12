<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<MovieLibrary.Models.Quote>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Suchergebnisse - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Suchergebnisse (Zitate):
    </h2>
    <hr />
    <br />
    <% foreach (var item in Model)
       { %>
    <div class="main">
        <br />
        <b>Titel:</b>
        <% if (item.Media.GetType() == typeof(MovieLibrary.Models.Book))
           {%>
        <%:Html.ActionLink(item.Media.Title, "../Media/Book", new { id = item.MediaId }, new { @class = "defaultLink" })%>
        <%}%>
        <%else if (item.Media.GetType() == typeof(MovieLibrary.Models.Movie))
            {%>
        <%:Html.ActionLink(item.Media.Title, "../Media/Movie", new { id = item.MediaId }, new { @class = "defaultLink" })%>
        <%}%>
        <%else if (item.Media.GetType() == typeof(MovieLibrary.Models.TV_Show))
            {%>
        <%:Html.ActionLink(item.Media.Title, "../Media/TVShow", new { id = item.MediaId }, new { @class = "defaultLink" })%>
        <%}%>
        <br />
        <b>Zitat:</b>
        <%: item.QuoteString %>
        <br />
        <b>Rolle:</b>
        <%: item.Character %>
        <br />
        <b>Aufrufe:</b>
        <%: item.Invocations %>
        <br />
        <%if (item.OccurenceTime != null)
          {
              if (!item.OccurenceTime.Equals(string.Empty))
              {%>
        <b>Wann:</b>
        <%: item.OccurenceTime%>
        <br />
        <%}
          }%>
        <br />
    </div>
    <br />
    <% } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
