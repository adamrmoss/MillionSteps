using System;
using System.Collections.Generic;

namespace MillionSteps.Core.Adventures
{
  public class Moment : GuidRavenDocument<Moment>
  {
    public Moment(Guid documentId) 
      : base(documentId)
    {
      this.Flags = new List<string>();
    }

    public string UserId { get; set; }
    public Guid AdventureId { get; set; }
    public string EventName { get; set; }
    public int Ordinal { get; set; }
    public List<string> Flags { get; }
  }
}
