using System;

namespace MillionSteps.Core.Adventures
{
  public class Adventure
  {
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public DateTime DateCreated { get; set; }
    public int StepsTravelled { get; set; }
  }
}
