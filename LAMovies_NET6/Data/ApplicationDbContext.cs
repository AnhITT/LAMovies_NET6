using LAMovies_NET6.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace LAMovies_NET6.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Genre>? Genres { get; set; }

        public DbSet<MovieGenre>? MovieGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>()
                    .HasKey(pc => new { pc.idMovie, pc.idGenre });
            modelBuilder.Entity<MovieGenre>()
                    .HasOne(p => p.Movie)
                    .WithMany(pc => pc.MovieGenre)
                    .HasForeignKey(p => p.idMovie);
            modelBuilder.Entity<MovieGenre>()
                    .HasOne(p => p.Genre)
                    .WithMany(pc => pc.MovieGenre)
                    .HasForeignKey(c => c.idGenre);

            modelBuilder.Entity<IdentityUserLogin<string>>()
        .HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>()
        .HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>()
        .HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
        }
    }
}
