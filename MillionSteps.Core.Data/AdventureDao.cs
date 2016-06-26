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
    { }

    public Adventure CreateAdventure(string userId)
    {
      var adventure = new Adventure {
        UserId = userId,
        DateCreated = DateTime.UtcNow
      };
      this.dbContext.Adventures.Add(adventure);

      var initialMoment = new Moment {
        Ordinal = 0,
        Adventure = adventure
      };
      this.dbContext.Moments.Add(initialMoment);

      adventure.Moments.Add(initialMoment);

      this.dbContext.SaveChanges();

      adventure.CurrentMomentId = initialMoment.Id;

      return adventure;
    }

    public AdventureSummary LookupAdventureByUserId(string userId)
    {
      var adventure = this.dbContext.Adventures
        .OrderByDescending(a => a.DateCreated)
        .FirstOrDefault(a => a.UserId == userId);
      return adventure?.GetSummary();
    }

    public Moment LoadMoment(int momentId)
    {
      return this.dbContext.Moments.Find(momentId);
    }

    public Moment BuildNextMoment(Adventure adventure, Moment priorMoment, Event @event)
    {
      var flags = priorMoment.Flags.Concat(@event.FlagsToSet).Except(@event.FlagsToClear).Distinct().ToArray();
      var newMoment = new Moment {
        Adventure = adventure,
        EventName = @event.Name,
        StepsConsumed = @event.StepsConsumed,
        Ordinal = priorMoment.Ordinal + 1
      };
      this.dbContext.Moments.Add(newMoment);

      foreach (var flag in flags) {
        var momentFlag = new MomentFlag {
          Flag = flag
        };
        this.dbContext.MomentFlags.Add(momentFlag);
        newMoment.MomentFlags.Add(momentFlag);
      };

      adventure.Moments.Add(newMoment);

      this.dbContext.SaveChanges();

      adventure.CurrentMomentId = newMoment.Id;

      return newMoment;
    }
  }
}
