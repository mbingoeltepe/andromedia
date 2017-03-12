<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MovieLibrary.Models.Movie>" %>

<fieldset>
    <label for="Title">Titel: </label>
    <%: Html.TextBoxFor(m => m.Title)%> 
    <%: Html.ValidationMessageFor(m => m.Title) %>
    <label for="OriginalTitle">Original Titel: </label>
    <%: Html.TextBoxFor(m => m.OriginalTitle) %> 
    <%: Html.ValidationMessageFor(m => m.OriginalTitle) %>
    <label for="Genre">Genre: </label>
    <% if (ViewData.ModelState["Genre"] != null && 
           ViewData.ModelState["Genre"].Errors.Count > 0)
       { %>
        <select id="Select1" name="Genre" class="input-validation-error">
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
    <%: Html.ValidationMessageFor(m => m.Genre) %>
    <label for="ReleaseDate">Erscheinungsdatum: </label>
    <%: Html.TextBox("ReleaseDate", Model.ReleaseDate.ToString("dd.MM.yyyy"), new { @id = "datepicker" + Model.Id })%>

    <script type="text/javascript">
        $(function () {
            $("#datepicker<%: Model.Id %>").datepicker({
                changeMonth: true,
                changeYear: true,
                defaultDate: new Date(<%: Model.ReleaseDate.Year %>, 
                                      <%: Model.ReleaseDate.Month %>, 
                                      <%: Model.ReleaseDate.Day %>),
                dateFormat: 'dd.mm.yy',
                yearRange: 'c-30:c+10'
            });
        });
    </script>

    <%: Html.ValidationMessageFor(m => m.ReleaseDate, "Format: \"TT.MM.YYYY\"!")%>
</fieldset>