using System;
using System.Linq;
using MillionSteps.Core.Events;
using Raven.Client;

namespace MillionSteps.Core.Adventures
{
  public class AdventureDao : Dao
  {
    public AdventureDao(IDocumentSession documentSession)
      : base(documentSession)
    {}

    public Adventure LookupAdventureByUserId(string userId)
    {
      return this.DocumentSession.Query<Adventure, AdventureIndex>()
                 .SingleOrDefault(a => a.UserId == userId);
    }

    public Adventure CreateAdventure(string userId)
    {
      var adventureId = Guid.NewGuid();
      var initialMomentId = Guid.NewGuid();

      var adventure = new Adventure(adventureId) {
        UserId = userId,
        DateCreated = DateTime.UtcNow,
        CurrentMomentId = initialMomentId,
      };
      this.DocumentSession.Store(adventure);

      var initialMoment = new Moment(initialMomentId) {
        UserId = userId,
        AdventureId = adventure.DocumentId,
        Ordinal = 0,
      };
      this.DocumentSession.Store(initialMoment);

      return adventure;
    }

    public Moment BuildNextMoment(Adventure adventure, Moment priorMoment, Event @event)
    {
      var newMoment = new Moment(Guid.NewGuid()) {
        UserId = adventure.UserId,
        AdventureId = adventure.DocumentId,
        EventName = @event.Name,
        StepsConsumed = @event.StepsConsumed,
        Ordinal = priorMoment.Ordinal + 1,
        Flags = priorMoment.Flags.Append(@event.Name).Concat(@event.FlagsToSet).Except(@event.FlagsToClear).Distinct().ToArray(),
      };
      this.DocumentSession.Store(newMoment);

      adventure.CurrentMomentId = newMoment.DocumentId;

      return newMoment;
    }
  }
}
