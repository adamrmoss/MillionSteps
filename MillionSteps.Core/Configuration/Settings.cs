using System;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace MillionSteps.Core.Configuration
{
  public class Settings : SettingsBase
  {
    public Uri AppUrl => new Uri(this.getAppSetting("AppUrl"));
    public string ClientId => this.getAppSetting("ClientId");
    public string ClientSecret => this.getAppSetting("ClientSecret");
    public Uri AuthorizationUrl => new Uri(this.getAppSetting("AuthorizationUrl"));
    public Uri TokenUrl => new Uri(this.getAppSetting("TokenUrl"));
    public Uri ApiUrl => new Uri(this.getAppSetting("ApiUrl"));

    private string getAppSetting(string settingName)
    {
      return this[settingName] as string;
    }
  }
}
