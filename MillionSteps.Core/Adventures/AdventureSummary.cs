using System;

namespace MillionSteps.Core.Adventures
{
  public class AdventureSummary
  {
    public int AdventureId { get; set; }
    public string UserId { get; set; }
    public DateTime DateCreated { get; set; }
    public int CurrentMomentId { get; set; }
    public int TotalStepsConsumed { get; set; }
  }
}
