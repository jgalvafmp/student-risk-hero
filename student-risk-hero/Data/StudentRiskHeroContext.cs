namespace student_risk_hero.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using student_risk_hero.Contracts;
    using student_risk_hero.Data.Models;

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

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
