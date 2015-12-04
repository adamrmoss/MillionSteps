using System;
using System.Collections.Generic;

namespace MillionSteps.Core.Adventures
{
  public class Adventure : GuidRavenDocument<Adventure>
  {
    public Adventure(Guid documentId)
      : base(documentId)
    {
      this.Flags = new List<string>();
    }

    public string UserId { get; set; }
    public DateTime DateCreated { get; set; }
    public int StepsTravelled { get; set; }
    public List<string> Flags { get; }
  }
}
