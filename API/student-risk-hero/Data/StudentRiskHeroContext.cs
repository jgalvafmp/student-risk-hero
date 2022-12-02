namespace student_risk_hero.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using student_risk_hero.Contracts;
    using student_risk_hero.Data.Models;
    using student_risk_hero.Data.Models.RiskProfiles;

    public class StudentRiskHeroContext : DbContext
    {
        private readonly ICurrentUserService currentUser;

        public StudentRiskHeroContext(DbContextOptions<StudentRiskHeroContext> options, ICurrentUserService currentUser) : base(options)
        {
            this.currentUser = currentUser;
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
                        if(currentUser.UserId.HasValue) auditableEntity.Entity.CreatedBy = currentUser.UserId.Value;
                    }

                    if (auditableEntity.State == EntityState.Modified)
                    {
                        auditableEntity.Entity.UpdatedAt = DateTime.Now;
                        auditableEntity.Entity.UpdatedBy = currentUser.UserId.HasValue ? currentUser.UserId.Value : null;
                    }

                    if (auditableEntity.State == EntityState.Deleted)
                    {
                        auditableEntity.Entity.DeletedAt = DateTime.Now;
                        auditableEntity.Entity.DeletedBy = currentUser.UserId.HasValue ? currentUser.UserId.Value : null;
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
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentStudent> AssignmentStudents { get; set; }

        // Main Process
        public DbSet<RiskProfile> RiskProfiles { get; set; }
        public DbSet<RiskProfileEntries> RiskProfileEntries { get; set; }
        public DbSet<RiskProfileEvidence> RiskProfileEvidences { get; set; }
    }
}
