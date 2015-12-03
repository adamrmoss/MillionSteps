using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MillionSteps.Core.Events
{
  [Table("Event")]
  public class Event
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public bool Automatic { get; set; }
    public bool OneTime { get; set; }
    public int Steps { get; set; }
    public HashSet<string> FlagsToSet { get; set; }
    public HashSet<string> FlagsToClear { get; set; }
  }
}
