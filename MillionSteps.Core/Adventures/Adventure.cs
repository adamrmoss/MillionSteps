﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MillionSteps.Core.Adventures
{
  public class Adventure
  {
    [Key]
    public int Id { get; set; }

    [MaxLength(16)]
    public string UserId { get; set; }

    public DateTime DateCreated { get; set; }
    public int CurrentMomentId { get; set; }

    private ICollection<Moment> moments; 
    public virtual ICollection<Moment> Moments {
      get { return this.moments; }
      set { this.moments = value; }
    }

    public Adventure()
    {
      this.moments = new List<Moment>();
    }

    public AdventureSummary GetSummary()
    {
      return new AdventureSummary {
        AdventureId = this.Id,
        UserId = this.UserId,
        DateCreated = this.DateCreated,
        CurrentMomentId = this.CurrentMomentId,
        TotalStepsConsumed = this.Moments.Sum(moment => moment.StepsConsumed),
      };
    }
  }
}
