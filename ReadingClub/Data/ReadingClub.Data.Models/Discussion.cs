using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReadingClub.Data.Models
{
    public class Discussion
    {
        private ICollection<User> participants;
        private ICollection<Comment> comments;

        public Discussion()
        {
            this.Participants = new HashSet<User>();
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        public Book Book { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Subject { get; set; }

        public virtual ICollection<User> Participants
        {
            get
            {
                return this.participants;
            }
            set
            {
                this.participants = value;
            }
        }

        public User Creator { get; set; }

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

        public bool IsDeleted { get; set; }

        public bool IsApproved { get; set; }
    }
}
