using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ReadingClub.Data.Models.Abstracts;

namespace ReadingClub.Data.Models
{
    public class Author: BaseDataModel
    {
        private ICollection<Book> books;
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Book> Books
        {
            get
            {
                return this.books;
            }
            set
            {
                this.books = value;
            }
        }
    }
}
