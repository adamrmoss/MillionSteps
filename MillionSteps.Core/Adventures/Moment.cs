using System;

namespace MillionSteps.Core.Adventures
{
  public class Moment
  {
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid AdventureId { get; set; }
    public Guid EventId { get; set; }
    public int Ordinal { get; set; }
  }
}
