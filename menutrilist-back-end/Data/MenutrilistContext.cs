using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Menutrilist.Data
{
    public partial class MenutrilistContext : IdentityDbContext<IdentityUser>
    {
        public MenutrilistContext(DbContextOptions<MenutrilistContext> options)
        : base(options)
        {
        }
    }
}