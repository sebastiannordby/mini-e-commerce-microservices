using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Models;

namespace UserService.DataAccess
{
    internal class UserDbContext : DbContext
    {
        internal DbSet<UserDao> Users { get; set; }

        public UserDbContext(
            DbContextOptions options) : base(options)
        {

        }
    }
}
