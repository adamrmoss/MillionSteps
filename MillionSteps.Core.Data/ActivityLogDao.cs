using System;
using System.Linq;
using System.Collections.Generic;
using MillionSteps.Core.Exercises;

namespace MillionSteps.Core.Data
{
  public class ActivityLogDao : Dao
  {
    public ActivityLogDao(MillionStepsDbContext dbContext)
      : base(dbContext)
    {}

    public Dictionary<DateTime, ActivityLogEntry> GetExistingActivityLogEntries(string userId, DateTime startDate, DateTime endDate)
    {
      var existingActivityLogEntries = this.dbContext.ActivityLogEntries
        .Where(ale => ale.UserId == userId && ale.Date >= startDate && ale.Date <= endDate);
      return existingActivityLogEntries.ToDictionary(ale => ale.Date);
    }

    public ActivityLogSummary GetActivityLogSummary(string userId)
    {
      var totalSteps = this.dbContext.ActivityLogEntries
        .Where(ale => ale.UserId == userId)
        .Sum(ale => ale.Steps);

      return new ActivityLogSummary
      {
        UserId = userId,
        TotalSteps = totalSteps,
      };
    }

    public void AddActivityLogEntry(string userId, DateTime date, int steps)
    {
      var activityLogEntry = new ActivityLogEntry
      {
        UserId = userId,
        Date = date,
        Steps = steps
      };
      this.dbContext.ActivityLogEntries.Add(activityLogEntry);
    }
  }
}
