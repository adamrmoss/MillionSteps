using System;
using System.Configuration;

namespace MillionSteps.Core.Configuration
{
  public class Settings : SettingsBase
  {
    public Uri AppUrl => new Uri(GetAppSetting("AppUrl"));
    public string ClientId => GetAppSetting("ClientId");
    public string ClientSecret => GetAppSetting("ClientSecret");
    public Uri AuthorizationUrl => new Uri(GetAppSetting("AuthorizationUrl"));
    public Uri TokenUrl => new Uri(GetAppSetting("TokenUrl"));
    public Uri ApiUrl => new Uri(GetAppSetting("ApiUrl"));

    private static string GetAppSetting(string settingName)
    {
      return ConfigurationManager.AppSettings[settingName];
    }
  }
}
