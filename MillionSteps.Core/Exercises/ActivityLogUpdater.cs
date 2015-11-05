using System;
using System.Collections.Generic;
using System.Linq;
using Fitbit.Api;
using Fitbit.Models;
using GuardClaws;
using MillionSteps.Core.Authentication;

namespace MillionSteps.Core.Exercises
{
  [UnitWorker]
  public class ActivityLogUpdater
  {
    private readonly UserProfileClient userProfileClient;
    private readonly ActivityLogDao activityLogDao;
    private readonly UserSession userSession;
    private readonly FitbitClient fitbitClient;

    public ActivityLogUpdater(UserProfileClient userProfileClient, ActivityLogDao activityLogDao, UserSession userSession, FitbitClient fitbitClient)
    {
      Claws.NotNull(() => userProfileClient);
      Claws.NotNull(() => activityLogDao);

      this.userProfileClient = userProfileClient;
      this.activityLogDao = activityLogDao;
      this.userSession = userSession;
      this.fitbitClient = fitbitClient;
    }

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

    public ActivityLogEntry[] UpdateActivityLog(DateTime startDate, DateTime endDate, bool skipExisting) {
      Claws.NotNull(() => this.userSession);
      Claws.NotNull(() => this.fitbitClient);

      var numberOfDaysPassed = (endDate - startDate).Days;

      var existingActivityLogEntries = this.activityLogDao.GetExistingActivityLogEntries(this.userSession.UserId, startDate, endDate);

      var allDatesInRange = Enumerable.Range(0, numberOfDaysPassed).Select(i => startDate + TimeSpan.FromDays(i));
      var datesToUpdate = skipExisting ? allDatesInRange.Except(existingActivityLogEntries.Keys) : allDatesInRange;

      var updatedActivityLogEntries = new List<ActivityLogEntry>();
      foreach (var dateToUpdate in datesToUpdate) {
        var activityLogEntry = existingActivityLogEntries.ContainsKey(dateToUpdate) ? existingActivityLogEntries[dateToUpdate] :
          new ActivityLogEntry {
            Id = Guid.NewGuid(),
            UserId = this.userSession.UserId,
            Date = dateToUpdate,
          };
        this.UpdateActivityLogEntry(activityLogEntry);
        updatedActivityLogEntries.Add(activityLogEntry);
      }
      return updatedActivityLogEntries.ToArray();
    }

    private void UpdateActivityLogEntry(ActivityLogEntry activityLogEntry)
    {
      try {
        var activityDate = activityLogEntry.Date;
        var steps = this.fitbitClient.GetDayActivitySummary(activityDate).Steps;

        activityLogEntry.Steps = steps;
      } catch (Exception e) {
        Console.WriteLine(e.Message);
      }
    }
  }
}
