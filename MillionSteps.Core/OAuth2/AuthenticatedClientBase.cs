using MillionSteps.Core.Authentication;

namespace MillionSteps.Core.OAuth2
{
  public class AuthenticatedClientBase
  {
    protected readonly OAuth2Client OAuth2Client;
    protected readonly UserSession UserSession;

    protected AuthenticatedClientBase(OAuth2Client oAuth2Client, UserSession userSession)
    {
      this.OAuth2Client = oAuth2Client;
      this.UserSession = userSession;
    }
  }
}