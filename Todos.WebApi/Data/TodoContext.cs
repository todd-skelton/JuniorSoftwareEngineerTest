using Microsoft.EntityFrameworkCore;
using Todos.WebApi.Domain;

namespace Todos.WebApi.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
