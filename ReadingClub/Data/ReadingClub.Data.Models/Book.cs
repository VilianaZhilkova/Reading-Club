using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReadingClub.Data.Models
{
    public class Book
    {
        private ICollection<Discussion> discussions;
        public Book()
        {
            this.Discussions = new HashSet<Discussion>();
        }

        [Key]
        public int Id { get; set; }

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

        [Required]
        public bool IsDeleted { get; set; }
    }
}
