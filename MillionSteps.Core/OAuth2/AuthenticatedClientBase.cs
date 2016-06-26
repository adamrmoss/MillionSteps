using MillionSteps.Core.Authentication;

namespace MillionSteps.Core.OAuth2
{
  public class AuthenticatedClientBase
  {
    protected readonly OAuth2Client oAuth2Client;
    protected readonly UserSession userSession;

    protected AuthenticatedClientBase(OAuth2Client oAuth2Client, UserSession userSession)
    {
      this.oAuth2Client = oAuth2Client;
      this.userSession = userSession;
    }
  }
}