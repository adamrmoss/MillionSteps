using System;
using System.Collections.Generic;
using MillionSteps.Core.Events;

namespace MillionSteps.Web.Games
{
  public class GameViewModel
  {
    public string DisplayName { get; set; }
    public Guid MomentId { get; set; }
    public List<Event> Events { get; set; }
  }
}
