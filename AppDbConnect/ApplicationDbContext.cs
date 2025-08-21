using CarKilometerTrack.Model;
using Microsoft.EntityFrameworkCore;

namespace CarKilometerTrack.AppDbConnect
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Car> cars { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Car>().HasKey(c => c.Id);

            modelBuilder.Entity<Log>().HasKey(l => l.Id);

            modelBuilder.Entity<Note>().HasKey(l => l.Id);

            modelBuilder.Entity<User>().HasMany(e => e.Notes)
                .WithOne(e => e.user).HasForeignKey(e => e.userId);

            modelBuilder.Entity<Car>().HasMany(n=>n.Notes)
                .WithOne(n=>n.car).HasForeignKey(e => e.carId);


            modelBuilder.Entity<Car>().HasMany(n => n.Logs)
                .WithOne(n => n.car).HasForeignKey(e => e.carId);

            modelBuilder.Entity<User>().HasMany(e => e.Logs)
                .WithOne(e => e.user).HasForeignKey(e => e.userId);


        }
    }
}
