using System.Data.Entity;
using Basket.Models;

namespace Basket.DataAccess
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(string connectionString)
            :base(connectionString)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Order> Order { get; set; }

        public DbSet<GameDetail> gameDetails { get; set; }

    }
}
