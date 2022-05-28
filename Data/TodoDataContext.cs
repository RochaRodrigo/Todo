using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data
{
    public class TodoDataContext : DbContext
    {
        public TodoDataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}