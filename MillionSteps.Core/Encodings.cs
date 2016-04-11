using System;
using System.Text;

namespace MillionSteps.Core
{
  public static class Encodings
  {
    public static string Base64Encode(this string plainText)
    {
      var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
      return Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(this string base64)
    {
      var base64Bytes = Convert.FromBase64String(base64);
      return Encoding.UTF8.GetString(base64Bytes);
    }
  }
}
