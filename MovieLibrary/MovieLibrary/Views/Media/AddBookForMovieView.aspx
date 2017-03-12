<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.Movie>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AddBookForMovieView
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h4>
        Buch verknüpfen</h4>
    <div>
        <% if ((ViewData["BooksForMovie"] as SelectList).Count() != 0)
           { %>
        <label>
            Titel des Buches:
        </label>
        <% using (Html.BeginForm("AddBookToMovie", "Media", new {id = Model.Id}, FormMethod.Post, null))
           { %>
        <%: Html.DropDownList("BooksForMovie", ViewData["BooksForMovie"] as SelectList)%>
        <input type="submit" value="Hinzufügen" />
        <% } %>
        <% } %>
        <% else
            { %>
        <% using (Html.BeginForm("FindBooksByTitle", "Media", new {id = Model.Id}, FormMethod.Post, null))
           { %>
        Es wurden keine Bücher in des DB gefunden, welche dem Film im Titel ähneln.
        <br />
        <br />
        Selbstständig nach einem Titel suchen:
        <input type="text" name="SearchBookBar" id="SearchBookBar" style="width:200px" />
        <input type="submit" value="Suchen" />
        <% } %>
    </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
