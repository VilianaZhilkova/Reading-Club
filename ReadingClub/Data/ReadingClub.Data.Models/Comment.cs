﻿using System;
using System.ComponentModel.DataAnnotations;

using ReadingClub.Common;
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
        [MinLength(Constants.MinCommentContentLength)]
        [MaxLength(Constants.MaxCommentContentLength)]
        public string Content { get; set; }

        [Required]
        public Discussion Discussion { get; set; }
    }
}
