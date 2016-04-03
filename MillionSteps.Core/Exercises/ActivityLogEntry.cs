using System;
using System.ComponentModel.DataAnnotations;

namespace MillionSteps.Core.Exercises
{
  public class ActivityLogEntry
  {
    public int Id { get; set; }

    [MaxLength(16)]
    public string UserId { get; set; }

    public DateTime Date { get; set; }
    public int Steps { get; set; }
  }
}
