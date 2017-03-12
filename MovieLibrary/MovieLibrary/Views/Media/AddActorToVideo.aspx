<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.Media>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Schauspieler zu Video hinzufügen - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Schauspieler zu Video hinzufügen:</h2>
    <hr />
    <br />
    <% using (Ajax.BeginForm("RefreshActorList", new AjaxOptions { UpdateTargetId = "actors", OnSuccess = "showAddPersonButton" }))
       { %>
    <label>
        Name:
    </label>
    <%: Html.TextBox("Name", null, new { style = "width: 200px" } )%>
    <input type="submit" value="Liste aktualisieren" />
    <% } %>
    <%--<%: Ajax.ActionLink("Liste refreshen", "RefreshAuthorList", new AjaxOptions{ UpdateTargetId="authors" }) %>--%>
    <br />
    <br />
    <% string method = string.Empty;
       if (Model.GetType() == typeof(MovieLibrary.Models.TV_Show))
       {
           method = "AddActorTvShow";
       }
       else if (Model.GetType() == typeof(MovieLibrary.Models.Movie))
       {
           method = "AddActorMovie";
       }
    %>
    <% using (Html.BeginForm(method, "Media", new { id = Model.Id }))
       { %>
    <div id="actors" style="margin-left: auto; margin-right: auto;">
    </div>
    <% } %>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
