using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ReadingClub.Data.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {

        private ICollection<Discussion> discussions;
        private ICollection<Discussion> createdDiscussions;
        private ICollection<Comment> comments;

        public User()
        {
            this.Discussions = new HashSet<Discussion>();
            this.Comments = new HashSet<Comment>();
            this.CreatedDiscussions = new HashSet<Discussion>();
        }

        public virtual ICollection<Discussion> Discussions
        {
            get
            {
                return this.discussions;
            }

            set
            {
                this.discussions = value;
            }
        }

        public virtual ICollection<Discussion> CreatedDiscussions
        {
            get
            {
                return this.createdDiscussions;
            }

            set
            {
                this.createdDiscussions = value;
            }
        }

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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
