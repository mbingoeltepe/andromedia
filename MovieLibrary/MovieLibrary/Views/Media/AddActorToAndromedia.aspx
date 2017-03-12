<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.Actor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Andromedia erweitern - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Schauspieler zu Andromedia hinzufügen</h2>
    <div class="addMedia">
        <form id="Form1" runat="server" class="formLayout" action="AddActorToDb">
        <label>
            Vorname:*
        </label>
        <asp:TextBox ID="ActorFirstName" runat="server" />
        <label>
            Nachname:*
        </label>
        <asp:TextBox ID="ActorLastName" runat="server" />
        <label style="width: auto; float: none; text-align: center">
            Felder mit * sind Pflichtfelder.
        </label>
        <hr />
        <asp:Button ID="btnSubmit" runat="server" Text="Hinzufügen" Style="float: right" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ActorFirstName"
            ErrorMessage="Vorname erforderlich" Display="Dynamic" SetFocusOnError="true"
            ForeColor="Red" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ActorLastName"
            ErrorMessage="Nachname erforderlich" Display="Dynamic" SetFocusOnError="true"
            ForeColor="Red" />
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
