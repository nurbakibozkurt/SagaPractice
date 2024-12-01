using Microsoft.EntityFrameworkCore;
using Stock.Saga.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Saga.Service.Data
{
    public class ServiceDbContext: DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options) { }

        public DbSet<StockModel> Stocks { get; set; }
    }
}
