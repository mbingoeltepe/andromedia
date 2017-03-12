<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MovieLibrary.Models.Quote>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Zitate Ranking - Andromedia
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%if (Model.Count() != 0)
      { %>
   <div class="userSearchbox" style="border-width: 0">
    <% using (Ajax.BeginForm("FilternQuotes", "Quote", new AjaxOptions { UpdateTargetId = "ResultQuotes" }))
       { %>
        <fieldset class="formLayout">
            <label>User:</label><%: Html.DropDownList("NotRankingUserList", ViewData["NotRankingUserList"] as SelectList)%>
            <label>Sprache:</label><%: Html.DropDownList("NotRankingLanguage", ViewData["NotRankingLanguage"] as SelectList)%>
            <label>Titel:</label><%: Html.DropDownList("NotRankingMediumTitle", ViewData["NotRankingMediumTitle"] as SelectList)%>
            <input type="submit" value="Filtern" style="float:right"/>
        </fieldset>
    <% } %>
    </div>
        <%} %>
    <h2>Neue Zitate Anfragen:</h2>
    <hr />
    <%if (Model.Count() == 0)
      { %>
           <h3>Zur Zeit keine neuen Anfragen</h3>
    <%}
      else
      { %>
    <div id="ResultQuotes">
        <% Html.RenderPartial("../Quote/NotRankingQuotesViews", Model);%>
    </div>
    <%} %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

