using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MillionSteps.Core.Events
{
  [ComplexType]
  public class EventSetFlag
  {
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string Name { get; set; }
  }
}
