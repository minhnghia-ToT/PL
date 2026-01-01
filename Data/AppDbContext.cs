using Microsoft.EntityFrameworkCore;
using PFL_API.Models;

namespace PFL_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =======================
            // User
            // =======================
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(u => u.Email)
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

                entity.HasOne(u => u.Profile)
                      .WithOne(p => p.User)
                      .HasForeignKey<Profile>(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // =======================
            // Profile
            // =======================
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profiles");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(p => p.FullName)
                      .HasMaxLength(255);

                entity.Property(p => p.Phone)
                      .HasMaxLength(50);

                entity.Property(p => p.CareerObjective)
                      .HasMaxLength(500);

                entity.Property(p => p.Summary)
                      .HasMaxLength(1000);
            });

            // =======================
            // Education
            // =======================
            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("Educations");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.SchoolName)
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.Major)
                      .HasMaxLength(255);

                entity.Property(e => e.Degree)
                      .HasMaxLength(100);

                entity.HasOne(e => e.Profile)
                      .WithMany(p => p.Educations)
                      .HasForeignKey(e => e.ProfileId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // =======================
            // Project
            // =======================
            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Projects");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(p => p.Name)
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(p => p.Technologies)
                      .HasMaxLength(255);

                entity.Property(p => p.Role)
                      .HasMaxLength(100);

                entity.Property(p => p.ProjectUrl)
                      .HasMaxLength(500);

                entity.HasOne(p => p.Profile)
                      .WithMany(pr => pr.Projects)
                      .HasForeignKey(p => p.ProfileId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
