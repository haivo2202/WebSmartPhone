using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_PhoneStore.Models;

namespace Project_PhoneStore.Repository
{
    public class DataContext : IdentityDbContext<Account>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DataContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
        public DbSet<BrandModel> Brands { get; set; }

        public DbSet<ProductModel> Products { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }

        public DbSet<Model> Models { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<AdminModel> Admins { get; set; }

		public DbSet<Order> orders { get; set; }
	}
}
