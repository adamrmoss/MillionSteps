using System;
using MillionSteps.Core.Adventures;

namespace MillionSteps.Web.Games
{
  public class GameViewModel
  {
    public string DisplayName { get; set; }
    public Guid MomentId { get; set; }
    public FlagDictionary Flags { get; set; }
  }
}
