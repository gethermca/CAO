using System.Data.Entity;
using CaseManagement.Models;

namespace CaseManagement.Data
{
    public class CaseManagementContext : DbContext
    {
        public CaseManagementContext() : base("name=CaseManagementContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Case> Cases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Case entity
            modelBuilder.Entity<Case>()
                .HasRequired(c => c.MakerUser)
                .WithMany()
                .HasForeignKey(c => c.MakerUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Case>()
                .HasOptional(c => c.CheckerUser)
                .WithMany()
                .HasForeignKey(c => c.CheckerUserId)
                .WillCascadeOnDelete(false);

            // Configure indexes
            modelBuilder.Entity<Case()>
                .Property(c => c.CaseNumber)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired();
        }
    }
}
