<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= (HttpContext.Current.User.Identity.Name.Equals(Model.Username) ? "Meine TV Serien" : "TV Serien von '" + Model.Username + "'")%>
    - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <span class="alignLeft">
        <%: Html.ActionLink("Mediathek", "ShowUserMediathek", "User", new { username = Model.Username }, new { @class = "defaultLink" })%> /
            <%= (HttpContext.Current.User.Identity.Name.Equals(Model.Username) ? "Meine TV Serien" : "TV Serien von '" + Model.Username + "'")%>:</span>
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
    <div id="ResultUserTvShow">
        <% Html.RenderAction("RenderTVShowsPartial", "Mediathek"); %>
        <%--<% Html.RenderPartial("../User/UserTvShowView", Model);%>--%>
    </div>
    <div class="userMediaSearchbox">
        <% using (Ajax.BeginForm("FilterUserTvShow", "Mediathek", new AjaxOptions { UpdateTargetId = "ResultUserTvShow" }))
           { %>
        <label>
            Titel:
        </label>
        <%: Html.TextBox("TitleBox", "", new { style="float:right;" })%>
        <br />
        <br />
        <label>
            Aufbewahrungsort:
        </label>
        <%: Html.DropDownList("StoragePlaceList", ViewData["Storage"] as SelectList, "", new { style = "float:right;" })%>
        <br />
        <br />
        <label>
            Nur ausgeborgte anzeigen:
        </label>
        <%: Html.CheckBox("BorrowedFromCheckBox", new { style = "float:right;" })%>
        <br />
        <br />
        <label>
            Staffel:
        </label>
        <%: Html.DropDownList("SeasonList", ViewData["Seasons"] as SelectList, "", new { style = "float:right;" })%>
        <br />
        <br />
        <%--<label>Nur nicht hergeborgte anzeigen: </label> <%: Html.CheckBox("NotBorrowedToCheckBox", new { style = "float:right;" })%>
    <br /><br />--%>
        <label>
            Nur hergeborgte anzeigen:
        </label>
        <%: Html.CheckBox("BorrowedToCheckBox", new { style = "float:right;" })%>
        <br />
        <br />
        <label>
            Format:
        </label>
        <%: Html.DropDownList("TypeList", ViewData["Device"] as SelectList, "", new { style = "float:right;" })%>
        <br />
        <br />
        <input type="submit" value="Filtern" style="float: right" />
        <% } %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
