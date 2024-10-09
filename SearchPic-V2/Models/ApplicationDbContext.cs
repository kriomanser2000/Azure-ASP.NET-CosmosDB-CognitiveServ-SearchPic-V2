using Microsoft.EntityFrameworkCore;

namespace SearchPic_V2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Image> Images { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Image>()
                .HasMany(i => i.Tags)
                .WithOne(t => t.Image)
                .HasForeignKey(t => t.ImageId);
        }
    }
}
