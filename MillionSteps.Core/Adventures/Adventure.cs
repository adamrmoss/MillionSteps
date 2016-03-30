using System;
using System.Collections.Generic;
using System.Linq;

namespace MillionSteps.Core.Adventures
{
  public class Adventure
  {
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime DateCreated { get; set; }

    public virtual Moment CurrentMoment { get; set; }
    public virtual ICollection<Moment> Moments { get; set; }

    public AdventureSummary GetSummary()
    {
      return new AdventureSummary {
        AdventureId = this.Id,
        UserId = this.UserId,
        DateCreated = this.DateCreated,
        CurrentMomentId = this.CurrentMoment.Id,
        TotalStepsConsumed = this.Moments.Sum(moment => moment.StepsConsumed),
      };
    }
  }
}
