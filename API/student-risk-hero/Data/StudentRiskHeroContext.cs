namespace student_risk_hero.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using student_risk_hero.Contracts;
    using student_risk_hero.Data.Models;
    using student_risk_hero.Data.Models.RiskProfiles;

    public class StudentRiskHeroContext : DbContext
    {
        public StudentRiskHeroContext(DbContextOptions<StudentRiskHeroContext> options) : base(options)
        {
        }

        #region Save Changes
        public override int SaveChanges()
        {
            var auditableEntitySet = ChangeTracker.Entries<IAuditable>();

            if (auditableEntitySet != null)
            {
                foreach (var auditableEntity in auditableEntitySet.Where(c => c.State == EntityState.Added || 
                                                                              c.State == EntityState.Modified || 
                                                                              c.State == EntityState.Deleted))
                {
                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.CreatedAt = DateTime.Now;
                        auditableEntity.Entity.CreatedBy = 0;
                    }

                    if (auditableEntity.State == EntityState.Modified)
                    {
                        auditableEntity.Entity.UpdatedAt = DateTime.Now;
                        auditableEntity.Entity.UpdatedBy = 0;
                    }

                    if (auditableEntity.State == EntityState.Deleted)
                    {
                        auditableEntity.Entity.DeletedAt = DateTime.Now;
                        auditableEntity.Entity.DeletedBy = 0;
                        auditableEntity.Entity.IsDeleted = true;
                    }
                }
            }

            return base.SaveChanges();
        }

        #endregion

        // Security
        public DbSet<User> Users { get; set; }

        // Main Actors
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Counselor> Counselors { get; set; }

        // School related
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseGrades> CourseGrades { get; set; }

        // Main Process
        public DbSet<RiskProfile> RiskProfiles { get; set; }
        public DbSet<RiskProfileEntries> RiskProfileEntries { get; set; }
        public DbSet<RiskProfileEvidence> RiskProfileEvidences { get; set; }
    }
}
