using Microsoft.EntityFrameworkCore;

namespace Todo.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {

        }

        public DbSet<TodoModel> TodoItems { get; set; }
    }
}