<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.Book>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AddAuthorToBook
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Autor zu Buch hinzufügen</h2>

        <% using (Ajax.BeginForm("RefreshAuthorList", new AjaxOptions { UpdateTargetId = "authors", OnSuccess = "showAddPersonButton" }))
           { %>
        <label>Name: </label><%: Html.TextBox("Name")%>
        
        <input type="submit" value="Liste aktualisieren" />
        <% } %>
        <%--<%: Ajax.ActionLink("Liste refreshen", "RefreshAuthorList", new AjaxOptions{ UpdateTargetId="authors" }) %>--%>

        <br />
        <br />
        <% using (Html.BeginForm("AddAuthor", "Media", new { id = Model.Id }))
           { %>
          <div id="authors" style="margin-left:auto; margin-right:auto;">
            
            </div>
        <% } %>
        <br />

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
