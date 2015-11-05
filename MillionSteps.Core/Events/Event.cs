using System;
using System.Collections.Generic;

namespace MillionSteps.Core.Events
{
  public class Event
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool OneTime { get; set; }
    public List<string> FlagsToSet { get; set; } 
  }
}
