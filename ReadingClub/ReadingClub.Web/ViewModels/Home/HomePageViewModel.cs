using System.Collections.Generic;

using ReadingClub.Web.ViewModels.Books;
using ReadingClub.Web.ViewModels.Discussions;

namespace ReadingClub.Web.ViewModels.Home
{
    public class HomePageViewModel
    {
        public IEnumerable<DiscussionViewModel> UpcomingDiscussions { get; set; }

        public IEnumerable<DiscussionViewModel> CurrentDiscussions { get; set; }

        public IEnumerable<BookViewModel> Books { get; set; }
    }
}