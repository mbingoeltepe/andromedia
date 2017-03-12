using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Models
{
    [Bind(Include = "Title,OriginalTitle,Genre,Isbn")]
    [MetadataType(typeof(Book_Validation))]
    public partial class Book
    {
    }

    public class Book_Validation
    {
        [Required(ErrorMessage = "Pflichtfeld!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!")]
        public string OriginalTitle { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!")]
        [EnumDataType(typeof(GenreEnum), ErrorMessage = "ungültiges Genre!")]
        public string Genre { get; set; }

        [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$", ErrorMessage = "ungültige ISBN!")]
        public string Isbn { get; set; }
    }
}