using System;
using GuardClaws;
using MillionSteps.Core;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Data;
using MillionSteps.Core.Work;

namespace MillionSteps.Web.Exercises
{
  [UnitWorker]
  public class ActivityLogUpdater
  {
    public ActivityLogUpdater(UserProfileClient userProfileClient, ActivityLogDao activityLogDao, UserSession userSession)
    {
      Claws.NotNull(() => userProfileClient);
      Claws.NotNull(() => activityLogDao);

      this.userProfileClient = userProfileClient;
      this.activityLogDao = activityLogDao;
      this.userSession = userSession;
    }

    private readonly UserProfileClient userProfileClient;
    private readonly ActivityLogDao activityLogDao;
    private readonly UserSession userSession;

    public void UpdateTodayAndYesterday()
    {
      //Claws.NotNull(() => this.userSession);

      //var userProfile = this.userProfileClient.GetUserProfile();
      //var usersOffsetFromUtc = TimeSpan.FromMilliseconds(userProfile.OffsetFromUTCMillis);
      //var today = (DateTime.UtcNow + usersOffsetFromUtc).Date;
      //var yesterday = today - TimeSpan.FromDays(1);

      //this.UpdateActivityLog(yesterday, today, false);
    }

    public void UpdateActivityLog(DateTime startDate, DateTime endDate, bool skipExisting)
    {
      //Claws.NotNull(() => this.userSession);

      //var numberOfDaysPassed = (endDate - startDate).Days + 1;

      //var existingActivityLogEntries = this.activityLogDao.GetExistingActivityLogEntries(this.userSession.UserId, startDate, endDate);

      //var allDatesInRange = Enumerable.Range(0, numberOfDaysPassed).Select(i => startDate + TimeSpan.FromDays(i));
      //var datesToUpdate = skipExisting ? allDatesInRange.Except(existingActivityLogEntries.Keys) : allDatesInRange;

      //foreach (var dateToUpdate in datesToUpdate) {
      //  var steps = this.fitbitClient.GetDayActivitySummary(dateToUpdate).Steps;
      //  if (existingActivityLogEntries.ContainsKey(dateToUpdate))
      //    existingActivityLogEntries[dateToUpdate].Steps = steps;
      //  else
      //    this.activityLogDao.AddActivityLogEntry(this.userSession.UserId, dateToUpdate, steps);
      //}
    }
  }
}
