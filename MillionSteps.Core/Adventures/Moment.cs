using System;

namespace MillionSteps.Core.Adventures
{
  public class Moment : GuidRavenDocument<Moment>
  {
    public Moment(Guid documentId) 
      : base(documentId)
    { }

    public string UserId { get; set; }
    public Guid AdventureId { get; set; }
    public Guid EventId { get; set; }
    public int Ordinal { get; set; }
  }
}
