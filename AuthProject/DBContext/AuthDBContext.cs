using AuthProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthProject.DBContext
{
    public class AuthDBContext : IdentityDbContext<AppUser>
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(
                x =>
                {
                    x.ToTable("User");
                });
        }
    }
}
