﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Book Book { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(150)]
        public string Subject { get; set; }

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
