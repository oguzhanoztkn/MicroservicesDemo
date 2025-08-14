using Microsoft.EntityFrameworkCore;
using OrderingService.Models;

namespace OrderingService.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
    }
}
