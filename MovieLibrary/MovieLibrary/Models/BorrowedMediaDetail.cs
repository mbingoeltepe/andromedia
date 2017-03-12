using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieLibrary.Models
{
    public class BorrowedMediaDetail
    {
        public BorrowedDetails details { private set; get; }
        public UserMedia media { private set; get; }

        public BorrowedMediaDetail(BorrowedDetails details, UserMedia media)
        {
            this.details = details;
            this.media = media;
        }
    }
}