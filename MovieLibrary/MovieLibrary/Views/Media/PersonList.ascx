<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MovieLibrary.Models.Person>>" %>
<%   List<string> names = new List<string>();
     foreach (var author in Model)
     {
         string name = string.Format("{0} {1}", author.FirstName, author.LastName);
         names.Add(name);
     }
%>
<% if (names.Count == 0)
   { %>
   <b>Es wurden leider keine Personen gefunden. Wählen Sie <a class="defaultLink" href="javascript:chooseMediaType()">Andromedia Erweitern</a>, um neue Personen zu unserer Datenbank hinzuzufügen!</b>
<% } %>
<% else
    { %>
    Gefundene Personen: 
    <br />
    <br />
<%: Html.DropDownList("Persons", new SelectList(names), new { style = "width:400px;" })%>
<br />
<br />
<input id="submitButton" type="submit" value="Hinzufügen" style="display:none" />
<% } %>
<br />
