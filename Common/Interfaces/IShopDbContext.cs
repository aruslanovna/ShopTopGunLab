using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Interfaces
{
   public interface IShopDbContext
    {
        DbSet<Product> products { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
