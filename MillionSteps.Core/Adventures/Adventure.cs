using System;

namespace MillionSteps.Core.Adventures
{
  public class Adventure
  {
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid AdventurerId { get; set; }
  }
}
