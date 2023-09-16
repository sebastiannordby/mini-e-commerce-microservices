using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.API.Data
{
    public class IdentitySeviceDbContext : IdentityDbContext
    {
        public IdentitySeviceDbContext(
            DbContextOptions<IdentitySeviceDbContext> options) : base(options)
        {

        }
    }
}
