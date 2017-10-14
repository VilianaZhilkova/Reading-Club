using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ReadingClub.Common;

using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.ViewModels.Books
{
    public class AddBookViewModel : IMapTo<Book>
    {
        [Required]
        [StringLength(StringLengthConstants.MaxBookTitleLength,
            MinimumLength = StringLengthConstants.MinBookTitleLength,
            ErrorMessage = ErrorMessageConstants.InvaliStringLengthErrorMessage)]
        public string Title { get; set; }

        [Required]
        [StringLength(StringLengthConstants.MaxAuthorNameLength, 
            MinimumLength = StringLengthConstants.MinAuthorNameLength,
            ErrorMessage = ErrorMessageConstants.InvaliStringLengthErrorMessage)]
        public string AuthorName { get; set; }

        [Required]
        [StringLength(StringLengthConstants.MaxBookDescriptionLength, 
          MinimumLength = StringLengthConstants.MinBookDescriptionLength,
          ErrorMessage = ErrorMessageConstants.InvaliStringLengthErrorMessage)]
        public string Description { get; set; }
    }
}