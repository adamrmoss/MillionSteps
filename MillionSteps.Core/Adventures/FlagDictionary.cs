using System.Collections.Generic;
using Raven.Abstractions.Extensions;

namespace MillionSteps.Core.Adventures
{
  public class FlagDictionary
  {
    private readonly HashSet<string> flags;

    public FlagDictionary(IEnumerable<string> flags)
    {
      this.flags = flags.ToHashSet();
    }

    public bool this[string flag] => this.flags.Contains(flag);
  }
}
