using Microsoft.EntityFrameworkCore;
using ETicaret.Entities;

namespace ETicaret.Data
{
    public class DatabaseContext: DbContext 
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=OGKAYA\MSSQLSERVER1;Database=ETicaretProje; User Id=sa; Password=1234567890Aa;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 1,
                Name = "Admin"
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id=1,
                Name = "Admin",
                Surname = "admin",
                Email = "Admin.ticaret@tc.com",
                Phone = "0850",
                Password = "Adminp",
                CreatedAt = DateTime.Now,
                RoleId = 1,
                
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
