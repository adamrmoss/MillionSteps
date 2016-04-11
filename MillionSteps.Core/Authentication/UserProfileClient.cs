using System.Net;

namespace MillionSteps.Core.Authentication
{
  [UnitWorker]
  public class UserProfileClient
  {
    public UserProfileClient(UserSession userSession)
    {
      this.userSession = userSession;
    }

    private readonly UserSession userSession;

    public UserProfile GetUserProfile()
    {
      if (this.userSession == null)
        return null;

      // TODO: Lookup UserProfile
      return null;
    }
  }
}
