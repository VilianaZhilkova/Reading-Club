using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ReadingClub.Common;
using ReadingClub.Data.Models.Abstracts;

namespace ReadingClub.Data.Models
{
    public class Book: BaseDataModel
    {
        private ICollection<Discussion> discussions;
        public Book()
        {
            this.Discussions = new HashSet<Discussion>();
        }

        [Required]
        [MinLength(Constants.MinBookTitleLength)]
        [MaxLength(Constants.MaxBookTitleLength)]
        public string Title { get; set; }

        [Required]
        public Author Author { get; set; }

        [Required]
        [MinLength(Constants.MinBookDescriptionLength)]
        [MaxLength(Constants.MaxBookDescriptionLength)]
        public string Description { get; set; }

        public virtual ICollection<Discussion> Discussions
        {
            get
            {
                return this.discussions;
            }
            set
            {
                this.discussions = value;
            }
        }
    }
}
