using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Service;

namespace MovieLibrary.Models
{
    public class MediaImageUserViewModel
    {
        public Media Media { get; protected set; }
        public Image Image { get; protected set; }
        public User User { get; protected set; }

        public MediaImageUserViewModel(Media media, Image image, User user)
        {
            Media = media;
            Image = image;
            User = user;
        }
    }
}