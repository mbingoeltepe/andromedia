<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MovieLibrary.Models.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ShowUsers
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="userSearchbox">
    <% using (Ajax.BeginForm("FilternUsers", "Admin", new AjaxOptions { UpdateTargetId = "ResultUsers" }))
       { %>
    <label>User Name: </label> <%: Html.TextBox("NameBox", "", new { style="float:right;" })%>
    <br /><br />
    <input type="submit" value="Filtern" style="float:right"/>
    <% } %>
    </div>

    <h2>Users List</h2>
    <div id="ResultUsers">
        <% Html.RenderPartial("../Admin/UserView", Model);%>
    </div>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

