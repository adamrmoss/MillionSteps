using System;

namespace MillionSteps.Core.Adventures
{
  public class AdventureSummary
  {
    public Guid AdventureId { get; set; }
    public string UserId { get; set; }
    public DateTime DateCreated { get; set; }
    public Guid? CurrentMomentId { get; set; }
    public int TotalStepsConsumed { get; set; }
  }
}
