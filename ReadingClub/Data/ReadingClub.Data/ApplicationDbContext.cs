using Microsoft.AspNet.Identity.EntityFramework;
using ReadingClub.Data.Models;
using System.Data.Entity;

namespace ReadingClub.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("ReadingClubDb", throwIfV1Schema: false)
        {
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}
