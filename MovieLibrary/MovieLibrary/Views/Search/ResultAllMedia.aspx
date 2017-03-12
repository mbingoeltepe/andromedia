<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ViewMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<MovieLibrary.Models.Media>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Suchergebnisse - Andromedia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Suchergebnisse (Medien):
    </h2>
    <hr />
    <% 
        List<MovieLibrary.Models.TVShowDetailsViewModel> tvShows = new List<MovieLibrary.Models.TVShowDetailsViewModel>();
        List<MovieLibrary.Models.MovieDetailsViewModel> movies = new List<MovieLibrary.Models.MovieDetailsViewModel>();
        List<MovieLibrary.Models.BookDetailsViewModel> books = new List<MovieLibrary.Models.BookDetailsViewModel>();
        MovieLibrary.Service.IServices.IImageService imageService = new MovieLibrary.Service.ServicesImpl.AWSImageService();
        foreach (var item in Model)
        {
           if (item.GetType().Equals(typeof(MovieLibrary.Models.TV_Show)))
           {
               MovieLibrary.Service.Image image = imageService.GetImagesForVideo(((MovieLibrary.Models.TV_Show)item))[MovieLibrary.Service.ServicesImpl.AWSImageService.SWATCHIMAGE];
               if (image == null)
                   image = MovieLibrary.Service.ServicesImpl.AWSImageService.SWATCH_NOIMAGEFOUND;
               MovieLibrary.Models.TVShowDetailsViewModel model = new MovieLibrary.Models.TVShowDetailsViewModel(((MovieLibrary.Models.TV_Show)item), image);
                              
               tvShows.Add(model);
           }
           else if (item.GetType().Equals(typeof(MovieLibrary.Models.Movie)))
           {
               MovieLibrary.Service.Image image = imageService.GetImagesForVideo(((MovieLibrary.Models.Movie)item))[MovieLibrary.Service.ServicesImpl.AWSImageService.SWATCHIMAGE];
               if (image == null)
                   image = MovieLibrary.Service.ServicesImpl.AWSImageService.SWATCH_NOIMAGEFOUND;
               MovieLibrary.Models.MovieDetailsViewModel model = new MovieLibrary.Models.MovieDetailsViewModel(((MovieLibrary.Models.Movie)item), image);
               movies.Add(model);
           }
           else if (item.GetType().Equals(typeof(MovieLibrary.Models.Book)))
           {
               MovieLibrary.Service.Image image = imageService.GetImagesForBook(((MovieLibrary.Models.Book)item))[MovieLibrary.Service.ServicesImpl.AWSImageService.SWATCHIMAGE];
               if (image == null)
                   image = MovieLibrary.Service.ServicesImpl.AWSImageService.SWATCH_NOIMAGEFOUND;
               MovieLibrary.Models.BookDetailsViewModel model = new MovieLibrary.Models.BookDetailsViewModel(((MovieLibrary.Models.Book)item), image);
               books.Add(model);
           }
        }
        if (tvShows.Count != 0)
        {
            Html.RenderPartial("PartialTvShowResult", tvShows);
        }
        if (movies.Count != 0)
        {
            Html.RenderPartial("PartialMovieResult", movies);
        }
        if (books.Count != 0)
        {
            Html.RenderPartial("PartialBookResult", books);
        }
    %>
    
    <div id="result">
    </div>
</asp:Content>