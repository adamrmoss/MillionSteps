using System;

namespace MillionSteps.Core.Exercises
{
  public class ActivityLogEntry
  {
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime Date { get; set; }
    public int Steps { get; set; }
  }
}
