using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Customer> Customers { get; set; }  
        public DbSet<Payment> Payments { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries())
            {
                if(entry.Entity is BaseEntity entity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                            entity.DeletedDate = DateTime.UtcNow;
                            entry.State = EntityState.Modified;
                            entity.IsDeleted = true;
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
