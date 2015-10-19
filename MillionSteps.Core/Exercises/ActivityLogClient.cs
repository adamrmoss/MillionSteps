using System;
using System.Collections.Generic;
using System.Linq;
using Fitbit.Api;
using GuardClaws;
using MillionSteps.Core.Authentication;

namespace MillionSteps.Core.Exercises
{
  public class ActivityLogClient
  {
    private readonly MillionStepsDbContextFactory dbContextFactory;
    private readonly UserSession userSession;
    private readonly FitbitClient fitbitClient;
    private readonly UserProfileClient userProfileClient;

    public ActivityLogClient(MillionStepsDbContextFactory dbContextFactory, UserSession userSession, FitbitClient fitbitClient, UserProfileClient userProfileClient)
    {
      Claws.NotNull(() => dbContextFactory);
      Claws.NotNull(() => userProfileClient);

      this.dbContextFactory = dbContextFactory;
      this.userSession = userSession;
      this.fitbitClient = fitbitClient;
      this.userProfileClient = userProfileClient;
    }

    public ActivityLogEntry[] UpdateActivityLog(DateTime startDate, DateTime endDate, bool skipExisting) {
      var numberOfDaysPassed = (endDate - startDate).Days;
      var dbContext = this.dbContextFactory();

      var existingActivityLogEntries = dbContext.ActivityLogEntries
        .Where(ale => ale.UserId == this.userSession.UserId && ale.Date >= startDate && ale.Date <= endDate)
        .ToDictionary(ale => ale.Date);

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
        var sleep = this.fitbitClient.GetSleep(activityDate).Summary;
        var steps = this.fitbitClient.GetDayActivitySummary(activityDate).Steps;

        activityLogEntry.Steps = steps;
        activityLogEntry.SleepHours = sleep.TotalMinutesAsleep/60m;
      } catch (Exception e) {
        Console.WriteLine(e.Message);
      }
    }

    public void UpdateTodayAndYesterday()
    {
      Claws.NotNull(() => this.userSession);
      Claws.NotNull(() => this.fitbitClient);

      var usersOffsetFromUtc = TimeSpan.FromMilliseconds(this.userProfileClient.GetUserProfile().OffsetFromUTCMillis);
      var today = (DateTime.UtcNow + usersOffsetFromUtc).Date;
      var yesterday = today - TimeSpan.FromDays(1);

      this.UpdateActivityLog(yesterday, today, false);
    }
  }
}
