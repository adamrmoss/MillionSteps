using System;
using GuardClaws;

namespace MillionSteps.Core
{
  public class GuidEntityBase
  {
    public GuidEntityBase(Guid id)
    {
      Claws.NotDefault(() => id);

      this.Id = id;
    }

    public Guid Id { get; private set; }
  }
}
