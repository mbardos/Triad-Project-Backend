using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TriadInterviewBackend.DataLayer.Entities;

namespace TriadInterviewBackend.DataLayer.Contexts
{
    public class UserDbContext : IdentityDbContext<IdentityUserEntity>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Additional configuration if needed
        }
    }
}