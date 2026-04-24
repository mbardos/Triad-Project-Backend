using Microsoft.EntityFrameworkCore;

public class TriadDbContext : DbContext
{
    public TriadDbContext(DbContextOptions<TriadDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
}