using System;
using System.ComponentModel.DataAnnotations;

using ReadingClub.Data.Models.Abstracts;

namespace ReadingClub.Data.Models
{
    public class Comment: BaseDataModel
    {
        public Comment()
        {
        }

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
    }
}
