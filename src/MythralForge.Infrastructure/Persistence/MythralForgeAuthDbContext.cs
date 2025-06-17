using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MythralForge.Infrastructure.Persistence
{
    public class MythralForgeAuthDbContext : IdentityDbContext
    {
        public MythralForgeAuthDbContext(DbContextOptions<MythralForgeAuthDbContext> options) : base(options) {}
    }
}
