using Microsoft.EntityFrameworkCore;
using ChatApp.Models;
namespace ChatApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ChatMessage> Messages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
