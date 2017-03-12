<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<MovieLibrary.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Borge-Verlauf - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Verlauf - Herborgen / Ausborgen:</h2>
    <hr />
    <div style="display: inline; width: 30%; float: left; margin-right: 50px">
        <h3>
            Herborg-Verlauf:
        </h3>
        <%  IQueryable<MovieLibrary.Models.BorrowedDetails> borrowedAway = MovieLibrary.Service.ServicesImpl.UserMediaService.Instance.GetAllBorrowedAwayMediaByUser(User.Identity.Name);

            Html.RenderPartial("../User/BorrowedAwayStatistics", borrowedAway); %>
    </div>
    <div style="display: inline; width: 30%; float: left">
        <h3>
            Ausborg-Verlauf:
        </h3>
        <%  IQueryable<MovieLibrary.Models.BorrowedDetails> borrowedFrom = MovieLibrary.Service.ServicesImpl.UserMediaService.Instance.GetAllBorrowedFromMediaByUser(User.Identity.Name);

            Html.RenderPartial("../User/BorrowedFromStatistics", borrowedFrom); %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
