using dll.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dll.Data
{
    public interface IDataContext : IDisposable
    {
        DbSet<Article> Articles { get; set; }
        DbSet<Purchase> Purchases { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
