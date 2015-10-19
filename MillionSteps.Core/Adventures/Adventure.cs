using System;

namespace MillionSteps.Core.Adventures
{
  public class Adventure
  {
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string AdventurerId { get; set; }

    public AdventureInitialization AdventureInitialization { get; set; }
  }
}
