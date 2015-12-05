using System;
using System.Collections.Generic;
using MillionSteps.Core.Adventures;
using MillionSteps.Core.Events;

namespace MillionSteps.Web.Games
{
  public class GameViewModel
  {
    public string DisplayName { get; set; }
    public Guid MomentId { get; set; }
    public FlagDictionary Flags { get; set; }
    public List<Event> Choices { get; set; } 
  }
}
