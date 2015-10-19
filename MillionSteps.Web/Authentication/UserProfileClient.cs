using System.Net;
using Fitbit.Api;
using Fitbit.Models;
using MillionSteps.Core.Authentication;

namespace MillionSteps.Web.Authentication
{
  public class UserProfileClient
  {
    private readonly UserSession userSession;
    private readonly FitbitClient fitbitClient;

    public UserProfileClient(UserSession userSession, FitbitClient fitbitClient)
    {
      this.userSession = userSession;
      this.fitbitClient = fitbitClient;
    }

    public UserProfile GetUserProfile()
    {
      if (this.userSession == null || this.fitbitClient == null)
        return null;

      try {
        return this.fitbitClient.GetUserProfile(this.userSession.UserId);
      } catch (FitbitException fitbitException) {
        if (fitbitException.HttpStatusCode == HttpStatusCode.Unauthorized)
          return null;
        throw;
      }
    }
  }
}
