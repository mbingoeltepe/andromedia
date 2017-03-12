using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieLibrary.Service
{
    public class Image
    {
        public string Url { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Image(string url, int height, int width)
        {
            Url = url;
            Height = height;
            Width = width;
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this,obj)) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            Image i = (Image)obj;
            return (Url.Equals(i.Url)) && (Height == i.Height) && (Width == i.Width);
        }

        public override int GetHashCode()
        {
            string toHash = Url + Height + Width;
            return toHash.GetHashCode();
        }
    }
}
