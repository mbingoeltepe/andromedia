<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MovieLibrary.Models.Quote>>" %>
<div class="quoteList">
    <ul>
        <% int i = 1;
           foreach (var item in Model)
           { %>
        <li>
            <h4>
                Zitat:
                "<%: item.QuoteString %>"</h4>
            <a class="defaultLink" href="javascript:zitatRanken('zitatId<%: item.Id %>')">Ranken</a>
            <div id="zitatId<%:item.Id %>" title="Zitat Anfrage überprüfen" style="display: none">
                <% using (Html.BeginForm("ZitatRanken", "Quote"))
                   { %>
                <fieldset>
                    User:
                    <%: item.User.Username %>
                    <br />
                    Titel:
                    <%: item.Media.Title %>
                    <br />
                </fieldset>
                <fieldset class="formLayout2">
                    <label for="Rolle:">
                        Rolle:(*)
                    </label>
                    <%: Html.TextBox("Rolle", item.Character) %>
                    <br />
                    <label for="Wann:">
                        Wann:
                    </label>
                    <%: Html.TextBox("Wann", item.OccurenceTime) %>
                    <br />
                    <label for="Sprache:">
                        Sprache:
                    </label>
                    <%: Html.DropDownList("Language",ViewData["Language"] as SelectList)%>
                    <br clear="left" />
                    <label for="Ranken:">
                        Ranken:
                    </label>
                    <%: Html.DropDownList("RankenWert", ViewData["RankenWert"] as SelectList)%>
                    <br />
                </fieldset>
                <br />
                <label for="Zitat:">
                    Zitat:(*)
                </label>
                <%: Html.TextArea("QuoteString", item.QuoteString, 5, 84, null)%>
                <br />
                <input name="id" type="hidden" value="<%: item.Id %>" />
                <% } %>
            </div>
            | <a class="defaultLink" href="javascript:zitatLoeschen('zitatLoeschenId<%: item.Id %>')">
                Ablehnen</a>
            <div id="zitatLoeschenId<%:item.Id %>" title="Zitat Anfrage ablehnen" style="display: none">
                <h4>
                    Wollen Sie die Anfrage ablehnen?
                </h4>
                <% using (Html.BeginForm("ZitatLoeschen", "Quote"))
                   { %>
                <fieldset>
                    User:
                    <%: item.User.Username %>
                    <br />
                    Titel:
                    <%: item.Media.Title %>
                    <br />
                </fieldset>
                <fieldset>
                    Zitat:
                    <%: item.QuoteString %>
                    <br />
                </fieldset>
                <input name="id" type="hidden" value="<%: item.Id %>" />
                <% } %>
            </div>
        </li>
        <% i++;
           } %>
    </ul>
</div>
