﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.TVShowDetailsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.Media.Title%>
    - Details - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <span id="titleSpace"><b>
        <%: Model.Media.Title%></b>
        <br />
        (Original: "<%: Model.Media.OriginalTitle%>") </span>
    <br />
    <br />
    <br />
    <span id="quoteLink">
        <%: Html.ActionLink("Zitate anzeigen", "ShowQuotesMedia", "Quote", new { mediaId = Model.Media.Id }, new { @class = "quoteLink" })%></span>
    <br />
    <br />
    <br />
    <br />
    <script src="/Scripts/jquery.MetaData.js" type="text/javascript"> </script>
    <script src="/Scripts/jquery.rating.js" type="text/javascript"> </script>
    <link href="/Scripts/jquery.rating.css" rel="Stylesheet" type="text/css" />
    <form action="../Rating" method="post">
    <% double selectedRating = Model.Media.AverageRating;
       for (int i = 1; i <= 5; i++)
       { %>
    <input name="rating" type="radio" class="star" value="<%=i%>" <% if (i <= selectedRating) { %>
        checked="checked" <% } %> <% if (!Request.IsAuthenticated)
                { %> disabled="disabled" <% } %> />
    <%}%>
    <input type="hidden" name="id" value="<%=Model.Media.Id %>" />
    </form>
    <%  HttpCookie voteCookie = Request.Cookies["Votes"];

        if (voteCookie != null)
        {
            if (voteCookie[Model.Media.Id.ToString()] != null)
            {
    %>
    &nbsp;&nbsp;&nbsp;&nbsp;(Ihre Bewertung:<b>
        <%: String.Format("{0:F}", voteCookie[Model.Media.Id.ToString()])%></b>)
    <%
}
            else
            {
    %>
    &nbsp;&nbsp;&nbsp;&nbsp;(Noch nicht bewertet)
    <%
}
        }
        else
        {%>
    &nbsp;&nbsp;&nbsp;&nbsp;(Noch nicht bewertet)
    <%
}
    %>
    <br />
    <br />
    <% if (Model.Media.TotalRaters > 0)
       { %>
    Durchschnittliche Bewertung: <b>
        <%: String.Format("{0:F}", Model.Media.AverageRating)%></b> von <b>
            <%: Model.Media.TotalRaters%>
            <%= Model.Media.TotalRaters == 1 ? "User" : "Usern"%></b>.
    <% }
       else
       {%>
    Es liegen noch keine User - Bewertungen vor.
    <% } %>
    <div id="infoBox">
        Genre:
        <%: Model.Media.Genre%>
        <br />
        Gestartet:
        <%: Model.Media.ShowBeginning.Year%>
    </div>
    <div class="additionalInfoTable">
        <h4>
            Schauspieler
        </h4>
        <ul style="padding-left: 20px">
            <% foreach (var actor in Model.Media.Actor)
               { %>
            <li>
                <%: string.Format("{0} {1}", actor.FirstName, actor.LastName)%></li>
            <% } %>
            <% if (Model.UserMedia != null)
               { %>
            <li>
                <%: Html.ActionLink("Schauspieler hinzufügen", "AddActorToVideo", "Media", new { id = Model.Media.Id }, new { @class = "defaultLink" })%>
            </li>
            <% } %>
        </ul>
    </div>
    <br />
    <br />
    <% if (User.Identity.IsAuthenticated && Model.UserMedia != null)
       { %>
    <div class="additionalInfoTable" style="clear: right">
        <% Boolean request = false;
           foreach (var m in Model.UserMedia)
           {
               if (m.BorrowRequest != null && m.Season.TV_Show.Id == Model.Media.Id)
               {
                   foreach (var req in m.BorrowRequest)
                   {
                       if (req.UserTo != User.Identity.Name) request = true;
                   }
               }
           }
           if (Model.UserMedia != null && request)
           { %>
        <h4>
            Folgende Leute wollen sich diese Serie ausborgen:
        </h4>
        <% foreach (var media in Model.UserMedia)
           {
               if (media.BorrowRequest != null && media.Season.TV_Show.Id == Model.Media.Id)
               {
                   foreach (var req in media.BorrowRequest)
                   {%>
        <ul>
            <li>
                <%: req.UserTo %>
                (<%: media.Season.Number %>,
                <%: media.StoragePlace %>)<br />
                <% if (Model.User.Username.Equals(User.Identity.Name) && media.MediaStatus.Equals(MovieLibrary.Models.UserMediaStatusEnum.NichtVerborgt.ToString()))
                   { %>
                <%: Html.ActionLink("Herborgen", "BorrowMediaToUser", "Mediathek", new { name = req.UserTo, userMediaId = media.Id }, null)%>
                -
                <% } %>
                <%: Html.ActionLink("Ablehnen", "DiscardBorrowMediaToUser", "Mediathek", new { name = req.UserTo, userMediaId = media.Id }, null)%>
            </li>
        </ul>
        <%}
               }
           }
        %>
        <% } %>
    </div>
    <% } %>
    <img src="<%: Model.Image.Url %>" height="<%: Model.Image.Height %>" width="<%: Model.Image.Width %>"
        alt="<%: Model.Media.Title %>" title="<%: Model.Media.Title %>" />
    <%
        if (Model.UserMedia != null)
        {
            Html.RenderPartial("UserTVShows", Model);
        }
    %>
</asp:Content>
