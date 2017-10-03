using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.ViewModels.Discussions
{
    public class CreateDiscussionViewModel: IMapTo<Discussion>
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        [DisplayName("Starting Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        public int TimezoneOffset { get; set; }
    }
}