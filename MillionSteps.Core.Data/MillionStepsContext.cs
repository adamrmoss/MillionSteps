using System.Data.Entity;
using MillionSteps.Core.Adventures;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Configuration;
using MillionSteps.Core.Exercises;

namespace MillionSteps.Core.Data
{
  public class MillionStepsContext : DbContext
  {
    public MillionStepsContext(Settings settings)
      : base(settings.ConnectionString)
    { }

    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<ActivityLogEntry> ActivityLogEntries { get; set; }

    public DbSet<Adventure> Adventures { get; set; }
    public DbSet<Moment> Moments { get; set; }
    public DbSet<MomentFlag> MomentFlags { get; set; }
  }
}
