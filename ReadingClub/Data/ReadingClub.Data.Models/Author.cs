using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ReadingClub.Common;
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
        [MinLength(Constants.MinAuthorNameLength)]
        [MaxLength(Constants.MaxAuthorNameLength)]
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
