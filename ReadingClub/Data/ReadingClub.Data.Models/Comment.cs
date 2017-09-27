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

        [Required]
        public User Author { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(300)]
        public string Content { get; set; }

        [Required]
        public Discussion Discussion { get; set; }
                
        public bool IsDeleted { get; set; }

    }
}
