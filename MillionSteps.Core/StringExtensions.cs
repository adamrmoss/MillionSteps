using JetBrains.Annotations;

namespace MillionSteps.Core
{
  public static class StringExtensions
  {
    [StringFormatMethod("me")]
    public static string FormatWith(this string me, params object[] args)
    {
      if (me == null)
        return null;
      return string.Format(me, args);
    }
  }
}
