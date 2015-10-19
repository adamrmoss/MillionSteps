using System.Configuration;

namespace MillionSteps.Core.Configuration
{
  public abstract class SettingsBase
  {
    protected static string GetAppSetting(string settingName)
    {
      var appSettings = ConfigurationManager.AppSettings;
      return appSettings[settingName];
    }
  }
}
