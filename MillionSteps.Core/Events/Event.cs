using System;
using System.Collections.Generic;

namespace MillionSteps.Core.Events
{
  public class Event : GuidRavenDocument<Event>
  {
    public Event(Guid documentId)
      : base(documentId)
    {
      this.FlagsToSet = new HashSet<string>();
      this.FlagsToClear = new HashSet<string>();
    }

    public string Name { get; set; }
    public string Text { get; set; }
    public bool Automatic { get; set; }
    public bool Repeatable { get; set; }
    public int Steps { get; set; }
    public HashSet<string> FlagsToSet { get; set; }
    public HashSet<string> FlagsToClear { get; set; }
  }
}
