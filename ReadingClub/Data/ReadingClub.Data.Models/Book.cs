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

        public string Title { get; set; }

        public string Author { get; set; }

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

        public bool IsDeleted { get; set; }
    }
}
