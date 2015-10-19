using System.Data.Entity;
using MillionSteps.Core.Authentication;

namespace MillionSteps.Core
{
  public class MillionStepsDbContext : DbContext
  {
    public MillionStepsDbContext()
        : base("name=MillionSteps")
    {
    }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
    }
  }
}
