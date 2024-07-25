using api.models;
using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models;

namespace api.data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring relationships if necessary
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Subject)
                .WithMany()
                .HasForeignKey(s => s.SubjectId);
        }

        public DbSet<Subject> Subject { get; set; }

        public DbSet<Session> Session { get; set; }

        public DbSet<Break> Break { get; set; }
    }
}