using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ReadingClub.Common;
using ReadingClub.Data.Models.Abstracts;

namespace ReadingClub.Data.Models
{
    public class Discussion: BaseDataModel
    {
        private ICollection<User> users;
        private ICollection<Comment> comments;

        public Discussion()
        {
            this.Users = new HashSet<User>();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        public virtual Book Book { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [MinLength(Constants.MinDiscussionSubjectLength)]
        [MaxLength(Constants.MaxDiscussionSubjectLength)]
        public string Subject { get; set; }

        [Required]
        [Range(Constants.MinNumberOfParticipants, Constants.MaxNumberOfParticipants)]
        public int MaximumNumberOfParticipants { get; set; }

        [InverseProperty("Discussions")]
        public virtual ICollection<User> Users
        {
            get
            {
                return this.users;
            }
            set
            {
                this.users = value;
            }
        }

        [Required]
        public string CreatorId { get; set; }
        
        [ForeignKey("CreatorId")]
        [InverseProperty("CreatedDiscussions")]
        public virtual User Creator { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }

        public bool IsApproved { get; set; }
    }
}
