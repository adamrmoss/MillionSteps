using System.Linq;
using Raven.Client.Indexes;

namespace MillionSteps.Core.Exercises
{
  public class ActivityLogIndex : AbstractIndexCreationTask<ActivityLogEntry>
  {
    public ActivityLogIndex()
    {
      this.Map = activityLogEntries => from activityLogEntry in activityLogEntries
                                       select new {
                                         activityLogEntry.UserId,
                                         activityLogEntry.Date,
                                       };
    }
  }
}
