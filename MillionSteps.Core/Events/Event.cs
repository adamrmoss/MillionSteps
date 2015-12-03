using System;
using System.Collections.Generic;

namespace MillionSteps.Core.Events
{
  public class Event
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Automatic { get; set; }
    public bool OneTime { get; set; }
    public int StepsConsumed { get; set; }
    public List<string> FlagsToSet { get; set; }
    public List<string> FlagsToClear { get; set; }
  }
}
