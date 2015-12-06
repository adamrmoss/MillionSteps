using System;
using System.Collections.Generic;
using MillionSteps.Core.Adventures;
using MillionSteps.Core.Events;

namespace MillionSteps.Web.Games
{
  public class MomentViewModel
  {
    public Guid MomentId { get; set; }
    public bool ReadOnly { get; set; }
    public string DisplayName { get; set; }
    public FlagDictionary Flags { get; set; }
    public List<Event> Choices { get; set; }
    public string ChosenChoice { get; set; }
  }
}
