<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.Author>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Andromedia erweitern - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Autor zu Andromedia hinzufügen</h2>
    <div class="addMedia">
        <form id="Form1" runat="server" class="formLayout" action="AddAuthorToDb">
        <label>
            Vorname:*
        </label>
        <asp:TextBox ID="AuthorFirstName" runat="server" />
        <label>
            Nachname:*
        </label>
        <asp:TextBox ID="AuthorLastName" runat="server" />
        <label style="width: auto; float: none; text-align: center">
            Felder mit * sind Pflichtfelder.
        </label>
        <hr />
        <asp:Button ID="btnSubmit" runat="server" Text="Hinzufügen" Style="float: right" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="AuthorFirstName"
            ErrorMessage="Vorname erforderlich" Display="Dynamic" SetFocusOnError="true"
            ForeColor="Red" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="AuthorLastName"
            ErrorMessage="Nachname erforderlich" Display="Dynamic" SetFocusOnError="true"
            ForeColor="Red" />
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
