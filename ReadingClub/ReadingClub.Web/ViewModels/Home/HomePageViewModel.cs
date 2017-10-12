using ReadingClub.Web.ViewModels.Books;
using ReadingClub.Web.ViewModels.Discussions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadingClub.Web.ViewModels.Home
{
    public class HomePageViewModel
    {
        public IEnumerable<DiscussionViewModel> UpcomingDiscussions { get; set; }

        public IEnumerable<DiscussionViewModel> CurrentDiscussions { get; set; }

        public IEnumerable<BookViewModel> Books { get; set; }
    }
}