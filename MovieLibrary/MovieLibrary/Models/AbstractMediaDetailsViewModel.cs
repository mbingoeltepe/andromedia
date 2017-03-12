using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Service;

namespace MovieLibrary.Models
{
    public abstract class AbstractMediaDetailsViewModel<T,V>
        where T : Media
        where V : UserMedia
    {
        public T Media { get; protected set; }
        public Image Image { get; protected set; }
        public IQueryable<V> UserMedia { get; protected set; }
        public User User { get; protected set; }

        public AbstractMediaDetailsViewModel(T media, Image image, IQueryable<V> userMedia, User user)
        {
            Media = media;
            Image = image;
            UserMedia = userMedia;
            User = user;
        }

        public AbstractMediaDetailsViewModel(T media, Image image)
        {
            Media = media;
            Image = image;
        }
    }
}