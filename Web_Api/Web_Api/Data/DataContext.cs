using Microsoft.EntityFrameworkCore;
using Web_Api.Entities;

namespace Web_Api.Data
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<KeyValue> KeyValues { get; set; }
    }
}
