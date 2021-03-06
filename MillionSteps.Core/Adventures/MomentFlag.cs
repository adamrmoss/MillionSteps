﻿using System.ComponentModel.DataAnnotations;

namespace MillionSteps.Core.Adventures
{
  public class MomentFlag
  {
    [Key]
    public int Id { get; set; }

    [MaxLength(64)]
    public string Flag { get; set; }

    public virtual Moment Moment { get; set; }
  }
}
