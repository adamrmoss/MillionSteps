using System.ComponentModel.DataAnnotations;

namespace MillionSteps.Core.Exercises
{
  public class ActivityLogSummary
  {
    [MaxLength(16)]
    public string UserId { get; set; }

    public int TotalSteps { get; set; }
  }
}
