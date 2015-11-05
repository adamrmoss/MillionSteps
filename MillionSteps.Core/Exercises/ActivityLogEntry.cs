using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MillionSteps.Core.Exercises
{
  [Table("ActivityLogEntry")]
  public class ActivityLogEntry
  {
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public DateTime Date { get; set; }
    public int Steps { get; set; }
  }
}
