using System;

namespace MillionSteps.Core.Exercises
{
  public class ActivityLogEntry : GuidEntityBase
  {
    public string UserId { get; set; }
    public DateTime Date { get; set; }
    public int Steps { get; set; }
    public decimal SleepHours { get; set; }

    public ActivityLogEntry(Guid documentId)
      : base(documentId) 
    { }
  }
}
