using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MovieLibrary.Models
{
    [Bind(Include = "Title,OriginalTitle,Genre,ReleaseDate")]
    [MetadataType(typeof(Movie_Validation))]
    public partial class Movie
    {
    }

    public class Movie_Validation
    {
        [Required(ErrorMessage = "Pflichtfeld!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!")]
        public string OriginalTitle { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!")]
        [EnumDataType(typeof(GenreEnum), ErrorMessage = "ungültiges Genre!")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!")]
        public DateTime ReleaseDate { get; set; }
    }
}