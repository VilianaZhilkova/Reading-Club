using System;
using System.ComponentModel.DataAnnotations;

using ReadingClub.Common.Constants;
using ReadingClub.Data.Models.Abstracts;

namespace ReadingClub.Data.Models
{
    public class Comment : BaseDataModel
    {
        public Comment()
        {
        }

        [Required]
        public virtual User Author { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MinLength(StringLengthConstants.MinCommentContentLength)]
        [MaxLength(StringLengthConstants.MaxCommentContentLength)]
        public string Content { get; set; }

        [Required]
        public virtual Discussion Discussion { get; set; }
    }
}
