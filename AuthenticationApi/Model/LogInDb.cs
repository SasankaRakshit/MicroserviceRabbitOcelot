using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi.Model
{
    public class LogInDb : DbContext
    {
        public LogInDb(DbContextOptions<LogInDb> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
