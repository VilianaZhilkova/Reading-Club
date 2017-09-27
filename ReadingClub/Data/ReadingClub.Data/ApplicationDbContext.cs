using Microsoft.AspNet.Identity.EntityFramework;
using ReadingClub.Data.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ReadingClub.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("ReadingClubDb", throwIfV1Schema: false)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Discussion> Discussions { get; set; }

        public DbSet<Comment> Comments { get; set; }

        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            
            modelBuilder.Entity<User>()
                .HasMany<Discussion>(s => s.Discussions)
                .WithMany(c => c.Participants)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("DiscussionId");
                    cs.ToTable("ParticipantsDiscussions");
                });

         }
         */
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}
