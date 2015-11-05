using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionSteps.Core.Exercises
{
  public class ActivityLogDao : Dao
  {
    public ActivityLogDao(MillionStepsDbContext dbContext) : base(dbContext)
    {
    }

    public Dictionary<DateTime, ActivityLogEntry> GetExistingActivityLogEntries(string userId, DateTime startDate, DateTime endDate)
    {
      var existingActivityLogEntries = this.DbContext.ActivityLogEntries
                                           .Where(ale => ale.UserId == userId && ale.Date >= startDate && ale.Date <= endDate)
                                           .ToDictionary(ale => ale.Date);
      return existingActivityLogEntries;
    }

    public void AddActivityLogEntry(string userId, DateTime date, int steps)
    {
      this.DbContext.ActivityLogEntries.Add(new ActivityLogEntry {
        Id = Guid.NewGuid(),
        UserId = userId,
        Date = date,
        Steps = steps,
      });
    }
  }
}
