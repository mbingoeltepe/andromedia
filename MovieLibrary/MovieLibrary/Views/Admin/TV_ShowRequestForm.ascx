<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MovieLibrary.Models.TV_Show>" %>

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
    <label for="Seasons">Staffeln: </label>
    <% if (ViewData.ModelState["Seasons"] == null)
       { %>
        <select id="Seasons" name="Seasons">
    <% }
       else
       { %>
       <select id="Seasons" name="Seasons" class="input-validation-error">
    <% } %>
        <% foreach (SelectListItem item in ViewData["Seasons"] as SelectList) 
            { %>
            <% if (item.Text == Model.Season.Count.ToString())
                { %>
                <option selected="selected"><%: item.Text %></option>
            <% }
                else
                { %>
                <option><%: item.Text %></option>
            <% } %>
        <% } %>
    </select>
    <%: Html.ValidationMessage("Seasons", "muss zwischen 1 und 100 liegen!") %> <br />

    <label for="ShowBeginning">Beginn der Serie: </label>
    <%: Html.TextBox("ShowBeginning", Model.ShowBeginning.ToString("dd.MM.yyyy"), new { @id = "datepicker_begin" + Model.Id}) %>

    <script type="text/javascript">
        $(function () {
            $("#datepicker_begin<%: Model.Id %>").datepicker({
                changeMonth: true,
                changeYear: true,
                defaultDate: new Date(<%: Model.ShowBeginning.Year %>, 
                                      <%: Model.ShowBeginning.Month %>, 
                                      <%: Model.ShowBeginning.Day %>),
                dateFormat: 'dd.mm.yy',
                yearRange: 'c-30:c+10'
            });
        });
    </script>

    <%: Html.ValidationMessageFor(m => m.ShowBeginning, "Format: \"TT.MM.YYYY\"!")%> <br />

    <label for="ShowEnding">Ende der Serie: </label>
    <% if (Model.ShowEnding != null)
       { %>
        <%: Html.TextBox("ShowEnding", ((DateTime)Model.ShowEnding).ToString("dd.MM.yyyy"), new { @id = "datepicker_end" + Model.Id }) %>
    <% }
       else
       { %>
            <%: Html.TextBox("ShowEnding", "", new { @id = "datepicker_end" + Model.Id }) %>
    <% } %>

    <script type="text/javascript">
        $(function () {
            $("#datepicker_end<%: Model.Id %>").datepicker({
                changeMonth: true,
                changeYear: true,
                <% if (Model.ShowEnding != null) 
                { %>
                    defaultDate: new Date(<%: ((DateTime)Model.ShowEnding).Year %>, 
                                            <%: ((DateTime)Model.ShowEnding).Month %>, 
                                            <%: ((DateTime)Model.ShowEnding).Day %>),
             <% } %>
                dateFormat: 'dd.mm.yy',
                yearRange: 'c-30:c+10'
            });
        });
    </script>

    <%: Html.ValidationMessageFor(m => m.ShowEnding, "Format: \"TT.MM.YYYY\"!")%>
</fieldset>