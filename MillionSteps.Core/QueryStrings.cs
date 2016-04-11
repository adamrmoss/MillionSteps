using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using RestSharp.Extensions.MonoHttp;

namespace MillionSteps.Core
{
  public static class QueryStrings
  {
    public static string ToQueryString(this NameValueCollection nvc)
    {
      var array = (from key in nvc.AllKeys
                   from value in nvc.GetValues(key)
                   select "{0}={1}".FormatWith(HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                   .ToArray();
      return string.Join("&", array);
    }
  }
}
