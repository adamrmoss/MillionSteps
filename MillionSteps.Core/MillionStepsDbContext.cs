using System.Data.Entity;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Characters;
using MillionSteps.Core.Exercises;

namespace MillionSteps.Core
{
  public delegate MillionStepsDbContext MillionStepsDbContextFactory();

  public class MillionStepsDbContext : DbContext
  {
    public MillionStepsDbContext()
        : base("name=MillionSteps")
    {
    }

    public virtual DbSet<UserSession> UserSessions { get; set; }
    public virtual DbSet<ActivityLogEntry> ActivityLogEntries { get; set; }
    public virtual DbSet<Adventurer> Adventurer { get; set; }
  }
}
