<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Helpers.PaginatedTopQuotesList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Meistgesuchte Zitate - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Meistgesuchte Zitate:</h2>
    <hr />
    <br />
    <div class="main">
        <ol id="topQuotes" start="<%: (Model.PageIndex * Model.PageSize)+1 %>">
            <% foreach (var topQuote in Model)
               { %>
            <li>"<%: topQuote.Quote.QuoteString %>" - <span style="font-style: italic">
                <%: topQuote.Quote.Character %></span> (<%: topQuote.Quote.Invocations %>
                hits)
                <br />
                <%: Html.ActionLink(topQuote.Media.Title, "MediaDetails", new { mediaId = topQuote.Media.Id, mediaType = topQuote.Media.GetType().Name }, new { @class = "defaultLink" })%>
                (<%: topQuote.Media.GetType().Name %>) </li>
            <% } %>
        </ol>
    </div>
    <ul class="pageUI">
        <% if (Model.HasPreviousPage)
           { %>
        <li>
            <%: Html.ActionLink("Vorige", "TopQuotes", new { page = (Model.PageIndex - 1) })%></li>
        <% } %>
        <% for (int i = Model.PageIndex - 4; i < Model.PageIndex; i++)
           { %>
        <% if (i >= 0)
           { %>
        <li>
            <%: Html.ActionLink((i + 1).ToString(), "TopQuotes", new { page = i })%></li>
        <% } %>
        <% } %>
        <li>
            <%: Model.PageIndex + 1 %></li>
        <% for (int i = Model.PageIndex + 1; i < Model.PageIndex + 5; i++)
           { %>
        <% if (i < Model.TotalPages)
           { %>
        <li>
            <%: Html.ActionLink((i + 1).ToString(), "TopQuotes", new { page = i })%></li>
        <% } %>
        <% } %>
        <% if (Model.HasNextPage)
           { %>
        <li>
            <%: Html.ActionLink("Nächste", "TopQuotes", new { page = (Model.PageIndex + 1) })%></li>
        <% } %>
    </ul>
    <div id="result">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
