<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.Book>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Andromedia erweitern - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Buch zu Andromedia hinzufügen</h2>
    <div id="addBook" class="addMedia">
        <form id="Form1" runat="server" class="formLayout" action="AddBookToDb">
        <label>
            Titel:*
        </label>
        <asp:TextBox ID="BookTitle" runat="server" />
        <label>
            Original-Titel:*
        </label>
        <asp:TextBox ID="BookTitleOriginal" runat="server" />
        <label>
            Genre:*
        </label>
        <%: Html.DropDownList("GenreList", ViewData["Genre"] as SelectList) %>
        <label>
            ISBN:
        </label>
        <asp:TextBox ID="Isbn" runat="server" />
        <label style="width: auto; float: none; text-align: center">
            Felder mit * sind Pflichtfelder.
        </label>
        <hr />
        <asp:Button ID="btnSubmit" runat="server" Text="Hinzufügen" Style="float: right" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="BookTitle"
            ErrorMessage="Titel erforderlich" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="BookTitleOriginal"
            ErrorMessage="Original-Titel erforderlich" Display="Dynamic" SetFocusOnError="true"
            ForeColor="Red" />
        <!--<br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Isbn"
            ErrorMessage="ISBN erforderlich" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />-->
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
