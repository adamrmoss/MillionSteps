using System;
using System.Linq;
using Raven.Client.Indexes;

namespace MillionSteps.Core.Adventures
{
  public class AdventureSummaryIndex : AbstractMultiMapIndexCreationTask<AdventureSummary>
  {
    public AdventureSummaryIndex()
    {
      this.AddMap<Adventure>(adventures => from adventure in adventures
                                           select new {
                                             AdventureId = adventure.DocumentId,
                                             adventure.UserId,
                                             adventure.DateCreated,
                                             adventure.CurrentMomentId,
                                             TotalStepsConsumed = 0,
                                           });

      this.AddMap<Moment>(moments => from moment in moments
                                     select new {
                                       moment.AdventureId,
                                       moment.UserId,
                                       DateCreated = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                                       CurrentMomentId = (Guid?) null,
                                       TotalStepsConsumed = moment.StepsConsumed,
                                     });

      this.Reduce = adventureSummaries => from adventureSummary in adventureSummaries
                                          group adventureSummary by adventureSummary.AdventureId into g
                                          select new {
                                            AdventureId = g.Key,
                                            g.First().UserId,
                                            DateCreated = g.Max(adventureSummary => adventureSummary.DateCreated),
                                            g.First(adventureSummary => adventureSummary.CurrentMomentId != null).CurrentMomentId,
                                            TotalStepsConsumed = g.Sum(adventureSummary => adventureSummary.TotalStepsConsumed),
                                          };
    }
  }
}
