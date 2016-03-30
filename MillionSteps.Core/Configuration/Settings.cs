using System;
using System.Configuration;

namespace MillionSteps.Core.Configuration
{
  public class Settings : SettingsBase
  {
    public string ConnectionString => GetConnectionString("MillionSteps");
    public string ConsumerKey => GetAppSetting("ConsumerKey");
    public string ConsumerSecret => GetAppSetting("ConsumerSecret");
    public Uri AppUrl => new Uri(GetAppSetting("AppUrl"));
    public Uri ApiUrl => new Uri(GetAppSetting("ApiUrl"));
    public Uri RequestTokenUrl => new Uri(GetAppSetting("RequestTokenUrl"));
    public Uri AccessTokenUrl => new Uri(GetAppSetting("AccessTokenUrl"));
    public Uri AuthorizeUrl => new Uri(GetAppSetting("AuthorizeUrl"));

    private static string GetConnectionString(string name)
    {
      return ConfigurationManager.ConnectionStrings[name].ConnectionString;
    }

    private static string GetAppSetting(string settingName)
    {
      return ConfigurationManager.AppSettings[settingName];
    }
  }
}
