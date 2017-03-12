<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MovieLibrary.Models.User>>" %>

<div class="userList">
<ul>
    <% int i = 1;
        foreach (var item in Model)
        { %>
    <li>
            <h4> <%: item.Username %></h4>
            <a class="defaultLink" href="javascript:userLoeschen('userId<%: item.Id %>')">User Löschen</a>
            <div id="userId<%:item.Id %>" title="User Löschen" style="display: none">
            <h4>Wollen Sie wirklich den User löschen? </h4>
            <% using (Html.BeginForm("UserLoeschen", "Admin"))
            { %>
            <%: item.Username %>    
            <input name="id" type="hidden" value="<%: item.Id %>" />       
            <% } %>
            </div>
    </li>
    <% i++;
        } %>                
</ul>
</div>