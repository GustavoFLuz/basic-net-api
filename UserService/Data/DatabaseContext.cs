using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data;

public class DatabaseContext : IdentityDbContext<User, IdentityRole, string>
{
  public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
  {

  }
  public DbSet<Address> Addresses { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.Entity<User>()
      .HasMany(u => u.Addresses)
      .WithOne()
      .HasForeignKey(a => a.UserId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
