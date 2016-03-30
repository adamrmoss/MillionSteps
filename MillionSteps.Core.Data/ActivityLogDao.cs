using System;
using System.Linq;
using System.Collections.Generic;
using MillionSteps.Core.Exercises;

namespace MillionSteps.Core.Data
{
  public class ActivityLogDao : Dao
  {
    public ActivityLogDao(MillionStepsContext dbContext)
      : base(dbContext)
    {}

    public Dictionary<DateTime, ActivityLogEntry> GetExistingActivityLogEntries(string userId, DateTime startDate, DateTime endDate)
    {
      var existingActivityLogEntries = this.dbContext.ActivityLogEntries
        .Where(ale => ale.UserId == userId && ale.Date >= startDate && ale.Date <= endDate);
      return existingActivityLogEntries.ToDictionary(ale => ale.Date);
    }

    public void AddActivityLogEntry(string userId, DateTime date, int steps)
    {
      var activityLogEntry = this.dbContext.ActivityLogEntries.Create();
      activityLogEntry.UserId = userId;
      activityLogEntry.Date = date;
      activityLogEntry.Steps = steps;
    }
  }
}
