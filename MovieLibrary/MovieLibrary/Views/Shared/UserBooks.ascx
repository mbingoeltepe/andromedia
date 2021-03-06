﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MovieLibrary.Models.BookDetailsViewModel>" %>
<% if (Model.UserMedia.Count<MovieLibrary.Models.UserBook>() != 0)
   {
       if (Model.User.Username.Equals(HttpContext.Current.User.Identity.Name))
       {
%>
<h3>
    In meiner Mediathek:</h3>
<% }
       else
       {
%>
<h3>
    In der Mediathek von '<%: Model.User.Username %>':</h3>
<% } %>
<ul>
    <% int i = 0;
       foreach (var userBook in Model.UserMedia)
       { %>
    <li>
        <div class="UserMedia">
            <h4>
                <%: userBook.Book.Title %></h4>
            <div class="info">
                <b>
                    Aufbewahrungsort:</b>
                <%: userBook.StoragePlace%></div>
            <div class="info">
                <b>
                    Status:</b>
                <% if (userBook.MediaStatus.Equals(MovieLibrary.Models.UserMediaStatusEnum.Verborgt.ToString()))
                   { %>
                <% if (Model.User.Username.Equals(HttpContext.Current.User.Identity.Name))
                   { %>
                <%: Ajax.ActionLink(userBook.MediaStatus, "BorrowDetails", "Mediathek", new { userMediaId = userBook.Id }, new AjaxOptions { OnComplete = "showBorrowDetails", InsertionMode = InsertionMode.Replace, UpdateTargetId = "borrowDetails" })%>
                <% }
                   else
                   {%>
                Verborgt
                <%   }
                   }  %>
                <% else
                    { %>
                Nicht Verborgt
                <% } %></div>
            <% if (HttpContext.Current.User.Identity.Name.Equals(Model.User.Username) && userBook.MediaStatus.Equals(MovieLibrary.Models.UserMediaStatusEnum.NichtVerborgt.ToString()))
               { %>
            <a class="defaultLink" href="javascript:deleteUserMediaDialog('userBook<%: i %>')">Aus
                Mediathek entfernen</a> <a class="defaultLink" href="javascript:showBorrowToDialog('borrowToPopUp<%: i %>')">
                    Verborgen</a>
            <% } %>
            <% else if (!HttpContext.Current.User.Identity.Name.Equals(Model.User.Username))
                { %>
            <% MovieLibrary.Models.User currentUser = MovieLibrary.Service.ServicesImpl.MembershipService.Instance.GetCurrentUser(HttpContext.Current.User.Identity.Name);
               bool borrowed = false;
               bool borrowRequest = false;
               bool giveBackRequest = false;
               foreach (var detail in Model.User.BorrowedDetails)
               {
                   if ((detail.UserMedia.Id == userBook.Id) && (detail.DateOfReturn.CompareTo(DateTime.Now) > 0) && (detail.NameTo.Equals(currentUser.Username)))
                   {
                       if (detail.TakeBackRequest == true)
                       {
                           giveBackRequest = true;
                           break;
                       }
                       else
                       {
                           borrowed = true;
                           break;
                       }
                   }
               }
               foreach (var request in userBook.BorrowRequest)
               {
                   if (request.UserTo.Equals(currentUser.Username))
                   {
                       borrowRequest = true;
                   }
               } %>
            <%if (borrowed)
              { %>
            <%: Html.ActionLink("Zurückgeben", "GiveBorrowedMediaBackRequest", "Mediathek", new { userMediaId = userBook.Id }, new { @class = "defaultLink" })%>
            <% }
              else if (borrowRequest)
              { %>
            <label style="color: Blue">
                Anfrage geschickt</label>
            <% }
              else if (giveBackRequest)
              {%>
            <label style="color: Blue">
                Auf Rücknahmebestätigung des Users wird gewartet</label>
            <% } %>
            <%else
                { %>
            <%: Html.ActionLink("Ausborge-Anfrage schicken", "SendBorrowRequest", "Mediathek", new { userMediaId = userBook.Id }, new { @class = "defaultLink" })%>
            <% } %>
            <% } %>
        </div>
        <div id="borrowToPopUp<%: i %>" style="display: none">
            <% using (Html.BeginForm("BorrowMediaToUser", "Mediathek", new { userMediaId = userBook.Id }))
               { %>
            <%  List<string> friends = new List<string>();
                foreach (var friend in Model.User.Friends)
                {
                    friends.Add(friend.Username);
                }
                friends.Insert(0, null);
                SelectList list = new SelectList(friends);
            %>
            <label>
                An:</label>
            <% if (list.Count() > 0)
               { %>
            <%: Html.DropDownListFor(m => m.User.Friends, (SelectList)list)%>
            <br />
            <br />
            <% } %>
            <label>
                Ist der Ausborger kein registrierter User? Dann kannst du hier seinen Namen eintragen:</label>
            <br />
            <br />
            <%: Html.TextBox("TextBoxNameTo", null, new { style ="width:200px" })%>
            <% } %>
        </div>
        <div id="userBook<%: i %>" class="UserMedia" title="Buch aus Mediathek löschen?"
            style="display: none">
            <h4>
                <%: userBook.Book.Title %></h4>
            <div class="info">
                <div class="UMAttribute">
                    Aufbewahrungsort:</div>
                <%: userBook.StoragePlace%></div>
            <div class="info">
                <div class="UMAttribute">
                    Status:</div>
                <%: userBook.MediaStatus%></div>
            <% using (Html.BeginForm("DeleteBook", "Mediathek"))
               { %>
            <input name="id" type="hidden" value="<%: userBook.Id %>" />
            <% }%>
        </div>
    </li>
    <% i++; %>
    <% } %>
</ul>
<% } %>
<br />
<br />
<% if (HttpContext.Current.User.Identity.Name.Equals(Model.User.Username))
   { %>
<a class="defaultLink" href="javascript:addUserMedia()">Buch zur Mediathek hinzufügen</a>
<% } %>
<div id="addUserMedia" title="Buch zur Mediathek hinzufügen" style="display: none">
    <h4>
        <%: Model.Media.Title %></h4>
    <% using (Html.BeginForm("AddBook", "Mediathek", new { id = Model.Media.Id }))
       { %>
    <fieldset>
        <label for="Storage">
            Aufbewahrungsort:</label>
        <%: Html.DropDownList("Storage", ViewData["Storage"] as SelectList )%>
        <%--  <label for="Status">
            Status:</label>
        <%: Html.DropDownList("Status", ViewData["Status"] as SelectList)%>--%>
    </fieldset>
    <% } %>
</div>
<div id="borrowDetails" style="display: none">
</div>
