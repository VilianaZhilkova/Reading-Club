using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ReadingClub.Common.Constants;
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
        [MinLength(StringLengthConstants.MinBookTitleLength)]
        [MaxLength(StringLengthConstants.MaxBookTitleLength)]
        public string Title { get; set; }

        [Required]
        public virtual Author Author { get; set; }

        [Required]
        [MinLength(StringLengthConstants.MinBookDescriptionLength)]
        [MaxLength(StringLengthConstants.MaxBookDescriptionLength)]
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

        public bool IsApproved { get; set; }
    }
}
