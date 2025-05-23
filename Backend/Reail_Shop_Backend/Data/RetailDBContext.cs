using Microsoft.EntityFrameworkCore;
using Reail_Shop_Backend.Models;

namespace Reail_Shop_Backend.Data
{
    public class RetailDBContext: DbContext
    {
        public RetailDBContext(DbContextOptions<RetailDBContext> options) : base(options) { }

        public DbSet <CustomerInvoice> CustomerInvoices { get; set; }
        public DbSet <ItemInvoice> ItemInvoices { get; set; }
        public DbSet <Product> Products { get; set; }
    }
}
