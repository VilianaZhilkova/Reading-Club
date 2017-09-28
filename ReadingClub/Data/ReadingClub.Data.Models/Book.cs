using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [MinLength(2)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public Author Author { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(300)]
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
