using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Entities.Identity;
using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Customer> Customers { get; set; }  
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<CarCategory> CarCatagories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<CarDetails> CarDetails { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountCustomer> DiscountsCustomer { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries())
            {
                if(entry.Entity is BaseEntity entity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.CreatedDate = DateTime.Now;
                            break;
                        case EntityState.Modified:
                            entity.UpdatedDate = DateTime.Now;
                            break;

                    }
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }


    }
}
