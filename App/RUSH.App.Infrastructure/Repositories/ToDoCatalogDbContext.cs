using RUSH.App.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace RUSH.App.Infrastructure.Repositories
{
    public class ToDoCatalogDbContext : DbContext
    {
        public ToDoCatalogDbContext(DbContextOptions<ToDoCatalogDbContext> options)
                                                              : base(options)
        {
        }

        public DbSet<ToDo> ToDos { get; set; }
    }
}
