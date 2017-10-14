using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;
using ReadingClub.Common;

namespace ReadingClub.Web.ViewModels.Discussions
{
    public class CreateDiscussionViewModel: IMapTo<Discussion>, IValidatableObject
    {
        [Required]
        [StringLength(StringLengthConstants.MaxDiscussionSubjectLength,
            MinimumLength = StringLengthConstants.MinDiscussionSubjectLength,
            ErrorMessage = ErrorMessageConstants.InvaliStringLengthErrorMessage)]
        public string Subject { get; set; }

        [Required]
        [DisplayName("Starting Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Maximum number of participants")]
        [Range(NumericConstants.MinNumberOfParticipants, 
            NumericConstants.MaxNumberOfParticipants, ErrorMessage = ErrorMessageConstants.InvaliRangeValueErrorMessage)]
        public int MaximumNumberOfParticipants { get; set; }

        public int TimezoneOffset { get; set; }

        public int BookId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.EndDate < this.StartDate)
            {
                yield return new ValidationResult(ErrorMessageConstants.InvalidEndDate);
            }
            else if (this.EndDate < this.StartDate.AddHours(1))
            {
                yield return new ValidationResult(ErrorMessageConstants.InvalidDifferenceBetweenStartAndEndTime);
            }
            else if(this.StartDate < DateTime.UtcNow.AddDays(2))
            {
                yield return new ValidationResult(ErrorMessageConstants.InvalidDiscussionStartDate);
            }

        }
    }
}