using System.Collections.Generic;

using ReadingClub.Web.ViewModels.Books;
using ReadingClub.Web.ViewModels.Discussions;

namespace ReadingClub.Web.ViewModels.Home
{
    public class SearchViewModel
    {
        public IEnumerable<DiscussionViewModel> Discussions { get; set; }

        public IEnumerable<BookViewModel> Books { get; set; }
    }
}