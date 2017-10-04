using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.ViewModels.Discussions
{
    public class CreateDiscussionViewModel: IMapTo<Discussion>, IValidatableObject
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        [DisplayName("Starting Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Maximum number of participants")]
        public int MaximumNumberOfParticipants { get; set; }

        public int TimezoneOffset { get; set; }

        public int BookId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.EndDate < this.StartDate)
            {
                yield return new ValidationResult("End Date must be greater than Starting Date");
            }
            else if (this.EndDate < this.StartDate.AddHours(1))
            {
                yield return new ValidationResult("The difference between the start and the end time must be at least 1 hour");
            }
        }
    }
}