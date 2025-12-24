using case1.Models;
using Microsoft.EntityFrameworkCore;

namespace case1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<User> Users => Set<User>();
        public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Patient - MedicalRecord ilişkisi
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.Records)
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}