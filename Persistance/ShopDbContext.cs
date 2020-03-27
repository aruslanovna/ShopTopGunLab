using Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Persistance
{
   public class ShopDbContext : DbContext, IShopDbContext
    {

        // private readonly ICurrentUserService _currentUserService;
        // private readonly IDateTime _dateTime;

        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {
        }

        

        public DbSet<Product> products { get; set; }

      

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{
        //    foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreatedBy = _currentUserService.UserId;
        //                entry.Entity.Created = _dateTime.Now;
        //                break;
        //            case EntityState.Modified:
        //                entry.Entity.LastModifiedBy = _currentUserService.UserId;
        //                entry.Entity.LastModified = _dateTime.Now;
        //                break;
        //        }
        //    }

        //    return base.SaveChangesAsync(cancellationToken);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopDbContext).Assembly);
          //  modelBuilder.Entity<Partner>().HasQueryFilter(c => c.UserId == _currentUserService.UserId);

            base.OnModelCreating(modelBuilder);

        }
    }
}

 