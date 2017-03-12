using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Service.IServices
{
    public interface IImageService
    {
        IDictionary<string, Image> GetImagesForVideo(Video video);
        IDictionary<string, Image> GetImagesForBook(Book book);
    }
}
