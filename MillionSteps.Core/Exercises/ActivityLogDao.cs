using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;

namespace MillionSteps.Core.Exercises
{
  public class ActivityLogDao : Dao
  {
    public ActivityLogDao(IDocumentSession documentSession)
      : base(documentSession)
    { }

    public Dictionary<DateTime, ActivityLogEntry> GetExistingActivityLogEntries(string userId, DateTime startDate, DateTime endDate)
    {
      var existingActivityLogEntries = this.DocumentSession.Query<ActivityLogEntry, ActivityLogIndex>()
                                           .Where(ale => ale.UserId == userId && ale.Date >= startDate && ale.Date <= endDate)
                                           .ToDictionary(ale => ale.Date);
      return existingActivityLogEntries;
    }

    public void AddActivityLogEntry(string userId, DateTime date, int steps)
    {
      var activityLogEntry = new ActivityLogEntry(Guid.NewGuid()) {
        UserId = userId,
        Date = date,
        Steps = steps,
      };
      this.DocumentSession.Store(activityLogEntry);
    }
  }
}
