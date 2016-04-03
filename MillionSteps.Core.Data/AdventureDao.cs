using System;
using System.Linq;
using MillionSteps.Core.Adventures;
using MillionSteps.Core.Events;

namespace MillionSteps.Core.Data
{
  public class AdventureDao : Dao
  {
    public AdventureDao(MillionStepsDbContext dbContext)
      : base(dbContext)
    {}

    public Adventure CreateAdventure(string userId)
    {
      var adventure = this.dbContext.Adventures.Create();
      var initialMoment = this.dbContext.Moments.Create();
      adventure.UserId = userId;
      adventure.DateCreated = DateTime.UtcNow;
      adventure.Moments.Add(initialMoment);
      adventure.CurrentMoment = initialMoment;

      initialMoment.Ordinal = 0;
      initialMoment.Adventure = adventure;

      return adventure;
    }

    public AdventureSummary LookupAdventureByUserId(string userId)
    {
      var adventure = this.dbContext.Adventures
        .OrderByDescending(a => a.DateCreated)
        .FirstOrDefault(a => a.UserId == userId);
      return adventure?.GetSummary();
    }

    public Moment BuildNextMoment(Adventure adventure, Moment priorMoment, Event @event)
    {
      var flags = priorMoment.Flags.Append(@event.Name).Concat(@event.FlagsToSet).Except(@event.FlagsToClear).Distinct().ToArray();
      var newMoment = this.dbContext.Moments.Create();
      newMoment.Adventure = adventure;
      newMoment.EventName = @event.Name;
      newMoment.StepsConsumed = @event.StepsConsumed;
      newMoment.Ordinal = priorMoment.Ordinal + 1;
      foreach (var flag in flags) {
        var momentFlag = this.dbContext.MomentFlags.Create();
        momentFlag.Flag = flag;
        newMoment.MomentFlags.Add(momentFlag);
      };

      adventure.Moments.Add(newMoment);
      adventure.CurrentMoment = newMoment;

      return newMoment;
    }
  }
}
