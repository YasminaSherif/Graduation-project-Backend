using Microsoft.EntityFrameworkCore;

namespace Graduation_project.Models
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ImageOfPastWork> imageOfPastWorks { get; set; }
        public DbSet<CustomerRequest> CustomerRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-ELNCRIE\\SQLEXPRESS;Database=GraduationProject;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasDiscriminator(u => u.Role)
                .HasValue<Customer>(Roles.Customer)
                .HasValue<Worker>(Roles.Worker);

        }


    }
}
