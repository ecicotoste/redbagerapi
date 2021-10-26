using Microsoft.EntityFrameworkCore;
using RedBagerApi.Models;

namespace RedBagerApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options): base (options)
        {
        }

         public DbSet<PlacaRB> PlacaRBs {get; set;}
         public DbSet<Consumer> Consumers {get; set;}        
    }
}