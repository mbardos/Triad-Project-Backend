using Microsoft.EntityFrameworkCore;
using TriadInterviewBackend.DataLayer.Entities;

namespace TriadInterviewBackend.DataLayer.Contexts
{
    public class TriadDbContext : DbContext
    {   
        public TriadDbContext(DbContextOptions<TriadDbContext> options) : base(options)
        {
        }   
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
    } 
}
