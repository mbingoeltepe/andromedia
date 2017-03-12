using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Models
{
    [Bind(Include = "Title,OriginalTitle,Genre,ShowBeginning,ShowEnding")]
    [MetadataType(typeof(TV_Show_Validation))]
    public partial class TV_Show
    {
    }

    public class TV_Show_Validation
    {
        [Required(ErrorMessage = "Pflichtfeld!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!")]
        public string OriginalTitle { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!")]
        [EnumDataType(typeof(GenreEnum), ErrorMessage = "ungültiges Genre!")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!")]
        public DateTime ShowBeginning { get; set; }

        public DateTime ShowEnding { get; set; }
    }
}