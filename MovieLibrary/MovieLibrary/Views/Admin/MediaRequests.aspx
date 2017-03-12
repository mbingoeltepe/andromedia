<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Helpers.PaginatedList<MovieLibrary.Models.InsertRequest>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Admin - Medien Anfragen
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Neue Medien Anfragen:</h2>
    <hr />
    <br />
        <div id="accordion">
            <% foreach (var mediaRequest in Model)
               { %>
	            <h3><a href="#"><%: mediaRequest.Media.Title %></a></h3>
	            <div class="insertRequest">
                    <h4>Info</h4>
                    <fieldset class="info">
                        <%: Html.Label("Datum:")%><span class="infoText"><%: mediaRequest.RequestDate %></span>
                        <%: Html.Label("User:")%><span class="infoText"><%: mediaRequest.User.Username %></span>
                    </fieldset>
                    <% if (mediaRequest.Media.GetType().Equals(typeof(MovieLibrary.Models.Book)))
                       { %>
                        <h4>Buch</h4>
                            <% using (Html.BeginForm("ConfirmRequestBook/" + mediaRequest.Id, "Admin", FormMethod.Post))
                               { %>
                                <% Html.RenderPartial("BookRequestForm", mediaRequest.Media); %>
                                <input type="submit" value="Bestätigen"/>
                                <%: Html.ActionLink("Ablehnen", "DeclineRequest", new { id = mediaRequest.Id }, new { @class = "declineButton" }) %>
                            <% } %>
                    <% } %>
                    <% if (mediaRequest.Media.GetType().Equals(typeof(MovieLibrary.Models.Movie)))
                       { %>
                        <h4>Film</h4>
                        <% using (Html.BeginForm("ConfirmRequestMovie/" + mediaRequest.Id, "Admin", FormMethod.Post))
                           { %>
                            <% Html.RenderPartial("MovieRequestForm", mediaRequest.Media); %>
                            <input type="submit" value="Bestätigen"/>
                            <%: Html.ActionLink("Ablehnen", "DeclineRequest", new { id = mediaRequest.Id }, new { @class = "declineButton" }) %>
                        <% } %>
                    <% } %>
                    <% if (mediaRequest.Media.GetType().Equals(typeof(MovieLibrary.Models.TV_Show)))
                       { %>
                        <h4>Serie</h4>
                        <% using (Html.BeginForm("ConfirmRequestTVShow/" + mediaRequest.Id, "Admin", FormMethod.Post))
                           { %>
                            <% Html.RenderPartial("TV_ShowRequestForm", mediaRequest.Media); %>
                            <input type="submit" value="Bestätigen"/>
                            <%: Html.ActionLink("Ablehnen", "DeclineRequest", new { id = mediaRequest.Id }, new { @class = "declineButton" }) %>
                        <% } %>
                    <% } %>
                </div>
            <% } %>
        </div>
        
       <% if (ViewData.ModelState.IsValid)
          { %>
            <ul class="pageUI">
                <% if (Model.HasPreviousPage)
               { %>
                        <li><%: Html.ActionLink("Vorige", "MediaRequests", new { page = (Model.PageIndex - 1) })%></li>
                <% } %>

                <% for (int i = Model.PageIndex - 4; i < Model.PageIndex; i++)
               { %>
                        <% if (i >= 0)
                       { %>
                                <li><%: Html.ActionLink((i + 1).ToString(), "MediaRequests", new { page = i })%></li>
                        <% } %>
                <% } %>

               <% if (Model.TotalPages > 0)
               { %>
                <li><%: Model.PageIndex + 1 %></li>
            <% } %>

                <% for (int i = Model.PageIndex + 1; i < Model.PageIndex + 5; i++)
               { %>
                    <% if (i < Model.TotalPages)
                   { %>
                               <li><%: Html.ActionLink((i + 1).ToString(), "MediaRequests", new { page = i })%></li>
                    <% } %>
                <% } %>

                <% if (Model.HasNextPage)
               { %>
                        <li><%: Html.ActionLink("Nächste", "MediaRequests", new { page = (Model.PageIndex + 1) })%></li>
                <% } %>
            </ul>
        <% }
          else
          { %>
                <%: Html.ActionLink("Alle anzeigen", "MediaRequests", new { }, new { @id = "requests_AlleAnzeigen" })%>
        <% } %>
        
        <% if (Model.TotalPages == 0)
           { %>
           <h3>Zurzeit sind keine Anfragen offen!</h3>
        <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(function () {
            $("#accordion").accordion({
                autoHeight: false
            });
        });

        $(function () {
            $(".insertRequest input:submit, .declineButton").button();
        });
    </script>
</asp:Content>
