<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.Book>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AddBookForMovieView
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h4>
        Buch verknüpfen</h4>
    <div>
        <% if ((ViewData["MoviesForBook"] as SelectList).Count() != 0)
           { %>
        <label>
            Titel des Buches:
        </label>
        <% using (Html.BeginForm("AddMovieToBook", "Media", new {id = Model.Id}, FormMethod.Post, null))
           { %>
        <%: Html.DropDownList("MovieForBook", ViewData["MoviesForBook"] as SelectList)%>
        <input type="submit" value="Hinzufügen" />
        <% } %>
        <% } %>
        <% else
            { %>
        <% using (Html.BeginForm("FindMoviesByTitle", "Media", new {id = Model.Id}, FormMethod.Post, null))
           { %>
        Es wurden keine Filme in der DB gefunden, welche dem Buch im Titel ähneln.
        <br />
        <br />
        Selbstständig nach einem Titel suchen:
        <input type="text" name="SearchMovieBar" id="SearchMovieBar" style="width:200px" />
        <input type="submit" value="Suchen" />
        <% } %>
    </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
