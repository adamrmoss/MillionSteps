using System;

namespace MillionSteps.Core.Adventures
{
  public class Moment : GuidRavenDocument
  {
    public Moment(Guid documentId)
      : base(documentId)
    {
      this.Flags = new string[0];
    }

    public string UserId { get; set; }
    public Guid AdventureId { get; set; }
    public string EventName { get; set; }
    public int StepsConsumed { get; set; }
    public int Ordinal { get; set; }
    public string[] Flags { get; set; }
  }
}
