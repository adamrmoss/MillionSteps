using System.Collections.Generic;

namespace MillionSteps.Core
{
  public static class EnumerableExtensions
  {
    public static IEnumerable<TElement> Append<TElement>(this IEnumerable<TElement> sequence, TElement element)
    {
      foreach (var sequenceElement in sequence)
        yield return sequenceElement;

      yield return element;
    } 
  }
}
