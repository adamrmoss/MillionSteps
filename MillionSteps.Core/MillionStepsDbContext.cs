using System.Data.Entity;
using MillionSteps.Core.Adventures;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Exercises;

namespace MillionSteps.Core
{
  [UnitWorker]
  public class MillionStepsDbContext : DbContext
  {
    public MillionStepsDbContext()
        : base("name=MillionSteps")
    {
    }

    public virtual DbSet<UserSession> UserSessions { get; set; }
    public virtual DbSet<ActivityLogEntry> ActivityLogEntries { get; set; }
    public virtual DbSet<Adventure> Adventures { get; set; }
    public virtual DbSet<Adventurer> Adventurers { get; set; }
  }
}
