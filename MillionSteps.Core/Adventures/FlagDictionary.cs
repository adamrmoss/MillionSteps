using System.Collections.Generic;

namespace MillionSteps.Core.Adventures
{
  public class FlagDictionary
  {
    private readonly HashSet<string> flags;

    public FlagDictionary(IEnumerable<string> flags)
    {
      this.flags = new HashSet<string>(flags);
    }

    public bool this[string flag] => this.flags.Contains(flag);
  }
}
