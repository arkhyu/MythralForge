using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MythralForge.Infrastructure.Persistence
{
    public class MythralForgeDbContext : DbContext
    {
        public MythralForgeDbContext(DbContextOptions<MythralForgeDbContext> options) : base(options) {}
    }
}
