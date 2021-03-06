﻿using System.ComponentModel.DataAnnotations;

using ReadingClub.Common.Constants;
using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.ViewModels.Comments
{
    public class CreateCommentViewModel : IMapFrom<Book>
    {
        [Required]
        [StringLength(StringLengthConstants.MaxCommentContentLength,
            MinimumLength = StringLengthConstants.MinCommentContentLength,
            ErrorMessage = ErrorMessageConstants.InvaliStringLengthErrorMessage)]
        public string Content { get; set; }
    }
}