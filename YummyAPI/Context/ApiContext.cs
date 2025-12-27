using Microsoft.EntityFrameworkCore;
using YummyAPI.Entities;

namespace YummyAPI.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=YummyDbContext;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;");
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Chef>? Chefs { get; set; }
        public DbSet<Contact>? Contacts {get;set;}
        public DbSet<Feature>? Features { get; set; }
        public DbSet<Gallery>? Galleries { get; set; }
        public DbSet<Footer>?  Footers { get; set; }
        public DbSet<Organization>? Organizations { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Service>? Services { get; set; }
        public DbSet<Rezervation>? Rezervations { get; set; }
        public DbSet<Testimonial>? Testimonials { get; set; }
        public DbSet<About>? Abouts { get; set; }
        public DbSet<Notification>? Notifications { get; set; }
    }
}