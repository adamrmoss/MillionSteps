using System.Linq;
using Raven.Client.Indexes;

namespace MillionSteps.Core.Exercises
{
  public class ActivityLogSummaryIndex : AbstractIndexCreationTask<ActivityLogEntry, ActivityLogSummary>
  {
    public ActivityLogSummaryIndex()
    {
      this.Map = activityLogEntries => from activityLogEntry in activityLogEntries
                                       select new {
                                         activityLogEntry.UserId,
                                         TotalSteps = activityLogEntry.Steps,
                                       };

      this.Reduce = activityLogSummaries => from activityLogSummary in activityLogSummaries
                                            group activityLogSummary by activityLogSummary.UserId into g
                                            select new {
                                              UserId = g.Key,
                                              TotalSteps = g.Sum(activityLogSummary => activityLogSummary.TotalSteps),
                                            };
    }
  }
}
