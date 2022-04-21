using Glitch.Helpers;
using Glitch.Helpers.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Glitch.Models
{
    public class GlitchContext : IdentityDbContext<User, AppRole, int>
    {
        public GlitchContext(DbContextOptions<GlitchContext> options)
            : base(options)
        {

        }

        public override DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Table> Tables { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Place>(entity =>
            {
                entity.HasOne(pl => pl.User).WithMany(u => u.Places).HasForeignKey(pl => pl.UserId);
            });
            modelBuilder.Entity<Table>(entity =>
            {
                entity.HasOne(t => t.Place).WithMany(pl => pl.Tables).HasForeignKey(t => t.PlaceId);
            });
        }
    }
}
