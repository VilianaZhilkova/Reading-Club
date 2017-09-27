using System;
using System.ComponentModel.DataAnnotations;

namespace ReadingClub.Data.Models
{
    public class Comment
    {
        public Comment()
        {
        }

        [Key]
        public int Id { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public string Content { get; set; }

        public Discussion Discussion { get; set; }
    }
}
