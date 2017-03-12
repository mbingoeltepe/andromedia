<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Profil - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <span class="alignLeft">Profil von <b>
            <%: Model.Username %></b> </span>
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
    <% if (Request.IsAuthenticated && User.Identity.Name.Equals(Model.Username))
       {
    %>
    <%: Html.ActionLink("Passwort ändern", "ResetPasswordNow", "Login", new { username = Page.User.Identity.Name }, new { @class = "defaultLink", style="float: left; font-size: 11px"})%>
    <%
        }
    %>
    <br />
    <br />
    <hr />
    <br />
    <%= Model.UserMedien.Count() == 1 ? "Es befindet sich" : "Es befinden sich "%>
    <b>
        <%: Html.ActionLink(Model.UserMedien.Count() + " " + 
    (Model.UserMedien.Count() == 1 ? "Medium" : "Medien"), "ShowUserMediathek", "User", new { username = HttpContext.Current.User.Identity.Name }, new { @class = "defaultLink" })%></b>
    
    <%= Model.Username.Equals(HttpContext.Current.User.Identity.Name) ? " in Ihrer persönlichen " : " in seiner persönlichen "%>
    <b>
        <%: Html.ActionLink("Mediathek", "ShowUserMediathek", "User", new { username = HttpContext.Current.User.Identity.Name }, new { @class = "defaultLink" })%></b>
    <br />
    <% if (Model.Username.Equals(HttpContext.Current.User.Identity.Name) && Model.Requestlist.Count > 0)
       { %>
    <br />
    Sie haben <b>
        <%: Html.ActionLink(Model.Requestlist.Count + (Model.Requestlist.Count == 1 ? " Freund-Anfrage" : " Freund-Anfragen"), "ShowFriends", "User", null, new { @class = "defaultLink" })%></b>
    <br />
    <% } %>
    <% if (Model.Username.Equals(HttpContext.Current.User.Identity.Name) && Model.BorrowRequest.Count > 0)
       {%>
    <br />
    Sie haben <b><a href="javascript:showBorrowRequests()" class="defaultLink">
        <%= Model.BorrowRequest.Count + (Model.BorrowRequest.Count == 1 ? " Ausborge-Anfrage" : " Ausborge-Anfragen") %></a></b>
    <br />
    <% } %>
    <% if (Model.Username.Equals(HttpContext.Current.User.Identity.Name))
       { %>
    <% foreach (var details in MovieLibrary.Service.ServicesImpl.UserMediaService.Instance.GetAllBorrowedAwayMediaByUser(Model.Username))
       {
           if (details.TakeBackRequest == true)
           { %>
    <br />
    Sie haben <b><a href="javascript:showTakeBackRequests()" class="defaultLink">Rückgabe-Anfragen</a></b>
    <% break;
           }
       }
    %>
    <br />
    <% } %>
    <% if (MovieLibrary.Service.ServicesImpl.MembershipService.Instance.IsAdmin(Model.Username))
       {
           int rankingCount = MovieLibrary.Service.ServicesImpl.AllQuotesService.Instance.GetAllNotRankingQuotes("", "", "").Count();
           if (rankingCount > 0)
           {
    %>
    <hr />
    <br />
    Sie haben
        <b><%:Html.ActionLink(rankingCount.ToString() + (rankingCount == 1 ? " Zitat" : " Zitate"), "ShowNotRankingZitate", "Quote", null, new { @class = "defaultLink" })%></b>
    zu ranken
    <% }
           int medienRequestCount = MovieLibrary.Service.ServicesImpl.InsertRequestService.Instance.GetAllOrderedByRequestDate().Count();
    %>
    <br />
    <br />
    <%if (medienRequestCount > 0)
      {
    %>
    Sie haben <b>
        <%:Html.ActionLink(medienRequestCount.ToString() + (medienRequestCount == 1 ? " Medien-Anfrage" : " Medien-Anfragen"), "MediaRequests", "Admin", null, new { @class = "defaultLink" })%></b>
    zu bearbeiten
    <br />
    <%} %>
    <br />
    <% } %>
    <hr />
    <div id="recommendedMedia">
        <% Html.RenderPartial("ShowRecommendedMedia", MovieLibrary.Service.ServicesImpl.UserMediaService.Instance.GetRecommendedMedia(Model.Username)); %>
    </div>
    <div id="borrowRequests" style="display: none">
        <% foreach (var request in Model.BorrowRequest)
           { %>
        <ul style="list-style-type: none">
            <% if (request.UserMedia.GetType() == typeof(MovieLibrary.Models.UserMovie))
               {%>
            <li><b>
                <%: ((MovieLibrary.Models.UserMovie)request.UserMedia).Movie.Title %> (<%: ((MovieLibrary.Models.UserMovie)request.UserMedia).StorageType %>)</b></li>
            <%} %>
            <% if (request.UserMedia.GetType() == typeof(MovieLibrary.Models.UserBook))
               {%>
            <li>
                <%: ((MovieLibrary.Models.UserBook)request.UserMedia).Book.Title %></li>
            <%} %>
            <% if (request.UserMedia.GetType() == typeof(MovieLibrary.Models.UserTV_Show))
               {%>
            <li>
                <%: ((MovieLibrary.Models.UserTV_Show)request.UserMedia).Season.TV_Show.Title %> (<%: ((MovieLibrary.Models.UserTV_Show)request.UserMedia).StorageType %>)</li>
            <li>Staffel:
                <%: ((MovieLibrary.Models.UserTV_Show)request.UserMedia).Season.Number %></li>
            <%} %>
            <li>
                <%: request.UserTo %></li>
            <li>
                <%: request.UserMedia.StoragePlace %></li>
            <% if (request.UserMedia.MediaStatus.Equals(MovieLibrary.Models.UserMediaStatusEnum.NichtVerborgt.ToString()))
               { %>
            <li>
                <%: Html.ActionLink("Herborgen", "BorrowMediaToUser", "Mediathek", new { name = request.UserTo, userMediaId = request.UserMedia.Id }, new { @class = "defaultLink" })%> -
                <%: Html.ActionLink("Ablehnen", "DiscardBorrowMediaToUser", "Mediathek", new { name = request.UserTo, userMediaId = request.UserMedia.Id }, new { @class = "defaultLink" })%></li>
            <% }
               else
               {%>
            <li>Schon Verborgt</li>
            <% } %>
        </ul>
        <% } %>
    </div>
    <div id="takeBackRequests" style="display: none">
        <% foreach (var details in MovieLibrary.Service.ServicesImpl.UserMediaService.Instance.GetAllBorrowedAwayMediaByUser(Model.Username))
           {
               if (details.TakeBackRequest == true)
               {
        %>
        <ul style="list-style-type: none">
            <% if (details.UserMedia.GetType() == typeof(MovieLibrary.Models.UserMovie))
               {%>
            <li><b>
                <%: ((MovieLibrary.Models.UserMovie)details.UserMedia).Movie.Title%></b></li>
            <%} %>
            <% if (details.UserMedia.GetType() == typeof(MovieLibrary.Models.UserBook))
               {%>
            <li>
                <%: ((MovieLibrary.Models.UserBook)details.UserMedia).Book.Title%></li>
            <%} %>
            <% if (details.UserMedia.GetType() == typeof(MovieLibrary.Models.UserTV_Show))
               {%>
            <li>
                <%: ((MovieLibrary.Models.UserTV_Show)details.UserMedia).Season.TV_Show.Title%></li>
            <li>Staffel:
                <%: ((MovieLibrary.Models.UserTV_Show)details.UserMedia).Season.Number%></li>
            <%} %>
            <li>Ausborger:
                <%: details.NameTo%></li>
            <li>
                <%: details.UserMedia.StoragePlace%></li>
            <li>
                <%: Html.ActionLink("Zurücknehmen", "TakeBorrowedMediaBack", "Mediathek", new { userMediaId = details.UserMedia.Id }, new { @class = "defaultLink" })%></li>
        </ul>
        <%}
           }%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
