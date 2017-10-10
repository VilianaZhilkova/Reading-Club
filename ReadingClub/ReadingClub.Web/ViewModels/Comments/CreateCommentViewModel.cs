using System.ComponentModel.DataAnnotations;

using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.ViewModels.Comments
{
    public class CreateCommentViewModel: IMapFrom<Book>
    {
        [Required]
        public string Content { get; set; }
    }
}