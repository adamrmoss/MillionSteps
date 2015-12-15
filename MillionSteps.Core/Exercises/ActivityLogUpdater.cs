using System;
using System.Linq;
using Fitbit.Api;
using GuardClaws;
using MillionSteps.Core.Authentication;

namespace MillionSteps.Core.Exercises
{
  [UnitWorker]
  public class ActivityLogUpdater
  {
    public ActivityLogUpdater(UserProfileClient userProfileClient, ActivityLogDao activityLogDao, UserSession userSession, FitbitClient fitbitClient)
    {
      Claws.NotNull(() => userProfileClient);
      Claws.NotNull(() => activityLogDao);

      this.userProfileClient = userProfileClient;
      this.activityLogDao = activityLogDao;
      this.userSession = userSession;
      this.fitbitClient = fitbitClient;
    }

    private readonly UserProfileClient userProfileClient;
    private readonly ActivityLogDao activityLogDao;
    private readonly UserSession userSession;
    private readonly FitbitClient fitbitClient;

    public void UpdateTodayAndYesterday()
    {
      Claws.NotNull(() => this.userSession);
      Claws.NotNull(() => this.fitbitClient);

      var userProfile = this.userProfileClient.GetUserProfile();
      var usersOffsetFromUtc = TimeSpan.FromMilliseconds(userProfile.OffsetFromUTCMillis);
      var today = (DateTime.UtcNow + usersOffsetFromUtc).Date;
      var yesterday = today - TimeSpan.FromDays(1);

      this.UpdateActivityLog(yesterday, today, false);
    }

    public void UpdateActivityLog(DateTime startDate, DateTime endDate, bool skipExisting)
    {
      Claws.NotNull(() => this.userSession);
      Claws.NotNull(() => this.fitbitClient);

      var numberOfDaysPassed = (endDate - startDate).Days + 1;

      var existingActivityLogEntries = this.activityLogDao.GetExistingActivityLogEntries(this.userSession.UserId, startDate, endDate);

      var allDatesInRange = Enumerable.Range(0, numberOfDaysPassed).Select(i => startDate + TimeSpan.FromDays(i));
      var datesToUpdate = skipExisting ? allDatesInRange.Except(existingActivityLogEntries.Keys) : allDatesInRange;

      foreach (var dateToUpdate in datesToUpdate) {
        var steps = this.fitbitClient.GetDayActivitySummary(dateToUpdate).Steps;
        if (existingActivityLogEntries.ContainsKey(dateToUpdate))
          existingActivityLogEntries[dateToUpdate].Steps = steps;
        else
          this.activityLogDao.AddActivityLogEntry(this.userSession.UserId, dateToUpdate, steps);
      }
    }
  }
}
