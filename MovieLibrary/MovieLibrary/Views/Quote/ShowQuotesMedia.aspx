<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.Media>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Zitate zu "<%: Model.Title %>" - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Zitate zu "<%: Model.Title %>":</h2>
    <hr />
    <% if (Request.IsAuthenticated)
       { %>
    <div>
        <a href="javascript:addQuote()">Zitat hinzufügen</a>
        <div id="addQuote" title="Zitat hinzufügen" style="display: none">
            <h3>
                <%: Model.Title%></h3>
            <% using (Html.BeginForm("AddQuote", "Quote", new { id = Model.Id }))
               { %>
            <fieldset>
                <label for="Language">
                    Sprache:</label>
                <%: Html.DropDownList("Language", ViewData["Language"] as SelectList)%>
                <label for="Character">
                    Rolle:</label>
                <%: Html.TextBox("Character")%>
                <% MovieLibrary.Models.Book book = MovieLibrary.Service.ServicesImpl.MediaService.Instance.GetBookById(Model.Id); if (book == null)
                   {%>
                <label for="Wann">
                    Wann:</label>
                <%: Html.TextBox("Wann")%>
                <%} %>
                <label for="QuoteString">
                    Zitat:</label>
                <br />
                <%: Html.TextArea("QuoteString")%>
            </fieldset>
            <% } %>
        </div>
    </div>
    <br />
    <%} %>
    <%  List<MovieLibrary.Models.Quote> list = Model.Quote.OrderByDescending(q => q.Invocations).ToList();
        foreach (var item in list)
        {
            if (item.Ranking > 0)
            {
    %>
    <ul style="list-style-type: none">
        <li><b>Zitat:</b>
            <%: item.QuoteString%></li>
        <li><b>Rolle:</b>
            <%: item.Character%></li>
        <li><b>Sprache:</b>
            <%: item.Language%></li>
        <li><b>Aufrufe:</b>
            <%: item.Invocations%></li>
    </ul>
    <hr />
    <% }
        } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
