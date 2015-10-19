using System;
using MillionSteps.Core.Configuration;

namespace MillionSteps.Web.Configuration
{
  public class Settings : SettingsBase
  {
    public string ConsumerKey => GetAppSetting("ConsumerKey");
    public string ConsumerSecret => GetAppSetting("ConsumerSecret");
    public Uri AppUrl => new Uri(GetAppSetting("AppUrl"));
    public Uri ApiUrl => new Uri(GetAppSetting("ApiUrl"));
    public Uri RequestTokenUrl => new Uri(GetAppSetting("RequestTokenUrl"));
    public Uri AccessTokenUrl => new Uri(GetAppSetting("AccessTokenUrl"));
    public Uri AuthorizeUrl => new Uri(GetAppSetting("AuthorizeUrl"));
  }
}
