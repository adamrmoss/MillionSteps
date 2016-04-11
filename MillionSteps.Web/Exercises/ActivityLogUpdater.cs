using System;
using System.Linq;
using GuardClaws;
using MillionSteps.Core;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Data;
using MillionSteps.Core.Exercises;
using MillionSteps.Core.OAuth2;
using MillionSteps.Core.Work;

namespace MillionSteps.Web.Exercises
{
  [UnitWorker]
  public class ActivityLogUpdater
  {
    public ActivityLogUpdater(ActivityLogDao activityLogDao, UserSession userSession, ActivityLogClient activityLogClient)
    {
      Claws.NotNull(() => activityLogDao);

      this.activityLogDao = activityLogDao;
      this.userSession = userSession;
      this.activityLogClient = activityLogClient;
    }

    private readonly ActivityLogDao activityLogDao;
    private readonly UserSession userSession;
    private readonly ActivityLogClient activityLogClient;

    public void UpdateTodayAndYesterday()
    {
      Claws.NotNull(() => this.userSession);
      Claws.NotNull(() => this.activityLogClient);

      var usersOffsetFromUtc = TimeSpan.FromMilliseconds(this.userSession.OffsetFromUtcMillis);
      var today = (DateTime.UtcNow + usersOffsetFromUtc).Date;
      var yesterday = today - TimeSpan.FromDays(1);

      this.UpdateActivityLog(yesterday, today, false);
    }

    public void UpdateActivityLog(DateTime startDate, DateTime endDate, bool skipExisting)
    {
      Claws.NotNull(() => this.userSession);
      Claws.NotNull(() => this.activityLogClient);

      var numberOfDaysPassed = (endDate - startDate).Days + 1;

      var existingActivityLogEntries = this.activityLogDao.GetExistingActivityLogEntries(this.userSession.UserId, startDate, endDate);

      var allDatesInRange = Enumerable.Range(0, numberOfDaysPassed).Select(i => startDate + TimeSpan.FromDays(i));
      var datesToUpdate = skipExisting ? allDatesInRange.Except(existingActivityLogEntries.Keys) : allDatesInRange;

      foreach (var dateToUpdate in datesToUpdate) {
        var steps = this.activityLogClient.GetActivityLogEntry(dateToUpdate).Steps;
        if (existingActivityLogEntries.ContainsKey(dateToUpdate))
          existingActivityLogEntries[dateToUpdate].Steps = steps;
        else
          this.activityLogDao.AddActivityLogEntry(this.userSession.UserId, dateToUpdate, steps);
      }
    }
  }
}
