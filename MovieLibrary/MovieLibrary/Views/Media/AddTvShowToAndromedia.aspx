<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.TV_Show>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Andromedia erweitern - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        TV-Serie zu Andromedia hinzufügen</h2>
    <div class="addMedia">
        <form runat="server" class="formLayout" action="AddTvShowToDb">
        <label>
            Titel:*
        </label>
        <asp:TextBox ID="TvShowTitle" runat="server" />
        <label>
            Original-Titel:*
        </label>
        <asp:TextBox ID="TvShowTitleOriginal" runat="server" />
        <label>
            Genre:*
        </label>
        <%: Html.DropDownList("GenreList", ViewData["Genre"] as SelectList) %>
        <label>
            Anzahl Staffeln:*
        </label>
        <%: Html.DropDownList("SeasonList", ViewData["Seasons"] as SelectList) %>
        <label>
            Beginn der Serie:*
        </label>
        <asp:TextBox ID="DateTextBoxBeginning" runat="server" />
        <label>
            Ende der Serie:
        </label>
        <asp:TextBox ID="DateTextBoxEnding" runat="server" />
        <label style="width: auto; float: none; text-align: center">
            Felder mit * sind Pflichtfelder.
        </label>
        <hr />
        <asp:Button ID="btnSubmit" runat="server" Text="Hinzufügen" Style="float: right" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TvShowTitle"
            ErrorMessage="Titel erforderlich" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TvShowTitleOriginal"
            ErrorMessage="Original-Titel erforderlich" Display="Dynamic" SetFocusOnError="true"
            ForeColor="Red" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DateTextBoxBeginning"
            ErrorMessage="Beginn Datum erforderlich" Display="Dynamic" SetFocusOnError="true"
            ForeColor="Red" />
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(function () {
            $("#<%= DateTextBoxBeginning.ClientID %>").datepicker({ 
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd.mm.yy',
                yearRange: 'c-30:c+10'
            });
        });
        $(function () {
            $("#<%= DateTextBoxEnding.ClientID %>").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd.mm.yy',
                yearRange: 'c-30:c+10'
            });
        });</script>
</asp:Content>
