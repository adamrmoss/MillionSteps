using System;

namespace MillionSteps.Core.Exercises
{
  public class ActivityLogEntry : GuidRavenDocument
  {
    public ActivityLogEntry(Guid documentId)
      : base(documentId)
    {}

    public string UserId { get; set; }
    public DateTime Date { get; set; }
    public int Steps { get; set; }
  }
}
