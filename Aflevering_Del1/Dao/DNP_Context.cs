using Microsoft.EntityFrameworkCore;
using Shared;

namespace Aflevering_Del1.Dao
{
    public class DNP_Context : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = dnp.db");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasKey(post => post.Id);
            modelBuilder.Entity<User>().HasKey(user => user.Username);

            
             Example of how to set constraint here
             modelBuilder.Entity<Todo>().Property(todo => todo.Title).HasMaxLength(50);
             
        }
    */
    }
}