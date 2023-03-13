using Microsoft.EntityFrameworkCore;
using Poc.Ecare.Infrastructure.Models.Classes;
using System.Reflection;

namespace Poc.Ecare.Infrastructure.Models.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AccessTemp> AccessTemps { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
