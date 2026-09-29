using Microsoft.EntityFrameworkCore;
using URLShortnerAPI.Models;

namespace URLShortnerAPI.Database;

public class UrlDbContext : DbContext
{
  public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options)
  {

  }
  public DbSet<URLModel> shortUrls { get; set; }
}