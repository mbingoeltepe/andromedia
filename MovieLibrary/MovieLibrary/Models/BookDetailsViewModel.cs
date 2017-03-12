using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Service;

namespace MovieLibrary.Models
{
    public class BookDetailsViewModel : AbstractMediaDetailsViewModel<Book, UserBook>
    {
        public BookDetailsViewModel(Book book, Image image, IQueryable<UserBook> userBooks, User user) : base(book, image, userBooks, user)
        {
        }

        public BookDetailsViewModel(Book book, Image image) : base(book, image)
        {
        }
    }
}