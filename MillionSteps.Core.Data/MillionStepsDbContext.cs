using System.Data.Entity;
using MillionSteps.Core.Adventures;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Exercises;

namespace MillionSteps.Core.Data
{
  public class MillionStepsDbContext : DbContext, ISaveChanges
  {
    public MillionStepsDbContext()
      : base("MillionSteps")
    { }

    public DbSet<UserSession> UserSessions { get; set; }
    //public DbSet<ActivityLogEntry> ActivityLogEntries { get; set; }

    //public DbSet<Adventure> Adventures { get; set; }
    //public DbSet<Moment> Moments { get; set; }
    //public DbSet<MomentFlag> MomentFlags { get; set; }
  }
}
