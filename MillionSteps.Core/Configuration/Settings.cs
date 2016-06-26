using System;
using System.Configuration;

namespace MillionSteps.Core.Configuration
{
  public class Settings : SettingsBase
  {
    public Uri AppUrl => new Uri(getAppSetting("AppUrl"));
    public string ClientId => getAppSetting("ClientId");
    public string ClientSecret => getAppSetting("ClientSecret");
    public Uri AuthorizationUrl => new Uri(getAppSetting("AuthorizationUrl"));
    public Uri TokenUrl => new Uri(getAppSetting("TokenUrl"));
    public Uri ApiUrl => new Uri(getAppSetting("ApiUrl"));

    private static string getAppSetting(string settingName)
    {
      return ConfigurationManager.AppSettings[settingName];
    }
  }
}
