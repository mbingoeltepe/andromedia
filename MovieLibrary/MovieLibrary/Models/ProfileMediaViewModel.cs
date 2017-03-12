using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieLibrary.Models
{
    public class ProfileMediaViewModel
    {
        public List<UserMedia> media { private set; get; }
        public string username { private set; get; }
        public List<BorrowedDetails> borrowedMedia { private set; get; }

        public ProfileMediaViewModel(List<UserMedia> media, string username, List<BorrowedDetails> borrowedMedia)
        {
            this.media = media;
            this.username = username;
            this.borrowedMedia = borrowedMedia;
        }
    }
}