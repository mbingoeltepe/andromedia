using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieLibrary.Models
{
    public class TopQuotesViewModel
    {
        public Quote Quote { get; private set; }
        public Media Media { get; private set; }

        public TopQuotesViewModel(Quote quote, Media media)
        {
            Quote = quote;
            Media = media;
        }
    }
}