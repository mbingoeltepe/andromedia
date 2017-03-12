<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.Movie>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Andromedia erweitern - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Film zu Andromedia hinzufügen</h2>
    <div id="addMovie" class="addMedia">
        <form runat="server" class="formLayout" action="AddMovieToDb">
        <label>
            Titel:*
        </label>
        <asp:TextBox ID="MovieTitle" runat="server" />
        <label>
            Original-Titel:*
        </label>
        <asp:TextBox ID="MovieTitleOriginal" runat="server" />
        <label>
            Genre:*
        </label>
        <%: Html.DropDownList("GenreList", ViewData["Genre"] as SelectList) %>
        <label>
            Release-Datum:*
        </label>
        <asp:TextBox ID="DatePicker" runat="server" />
        <label style="width: auto; float: none; text-align: center">
            Felder mit * sind Pflichtfelder.
        </label>
        <hr />
        <asp:Button ID="btnSubmit" runat="server" Text="Hinzufügen" Style="float: right" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="MovieTitle"
            ErrorMessage="Titel erforderlich" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="MovieTitleOriginal"
            ErrorMessage="Original-Titel erforderlich" Display="Dynamic" SetFocusOnError="true"
            ForeColor="Red" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DatePicker"
            ErrorMessage="Release-Datum erforderlich" Display="Dynamic" SetFocusOnError="true"
            ForeColor="Red" />
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(function () {
            $("#<%= DatePicker.ClientID %>").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd.mm.yy',
                yearRange: 'c-30:c+10'
            });
        });</script>
</asp:Content>
