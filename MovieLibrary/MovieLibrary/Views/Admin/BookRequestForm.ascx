<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MovieLibrary.Models.Book>" %>

<fieldset>
    <label for="Title">Titel: </label>
    <%: Html.TextBoxFor(m => m.Title)%> 
    <%: Html.ValidationMessageFor(m => m.Title) %><br />
    <label for="OriginalTitle">Original Titel: </label>
    <%: Html.TextBoxFor(m => m.OriginalTitle) %> 
    <%: Html.ValidationMessageFor(m => m.OriginalTitle) %><br />
    <label for="Genre">Genre: </label>
    <% if (ViewData.ModelState["Genre"] != null && 
           ViewData.ModelState["Genre"].Errors.Count > 0)
       { %>
        <select id="Genre" name="Genre" class="input-validation-error">
    <% }
       else
       { %>
       <select id="Genre" name="Genre">
    <% } %>
        <% foreach(SelectListItem item in ViewData["Genre"] as SelectList) 
            { %>
            <% if (item.Text == Model.Genre)
                { %>
                <option selected="selected"><%: item.Text%></option>
            <% }
                else
                { %>
                <option><%: item.Text%></option>
            <% } %>
        <% } %>
    </select>
    <%: Html.ValidationMessageFor(m => m.Genre) %> <br />
    <label for="Isbn">ISBN: </label>
    <%: Html.TextBoxFor(m => m.Isbn) %>
    <%: Html.ValidationMessageFor(m => m.Isbn) %>
</fieldset>