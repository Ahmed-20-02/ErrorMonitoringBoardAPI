namespace DevelopmentProjectErrorBoardAPI.Data
{
    using Microsoft.EntityFrameworkCore;
    using Entities;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
          ChangeTracker.LazyLoadingEnabled = false;
        }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Status> Statuses { get; set; }
        
        public DbSet<Error> Errors { get; set; }
        
        public DbSet<ErrorLogPath> ErrorLogPaths { get; set; }
        
    }
}