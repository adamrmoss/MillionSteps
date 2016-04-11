using MillionSteps.Core.Work;

namespace MillionSteps.Core.Authentication
{
  [UnitWorker]
  public class UserProfileClient
  {
    public UserProfileClient(OAuth2Client oAuth2Client, UserSession userSession)
    {
      this.oAuth2Client = oAuth2Client;
      this.userSession = userSession;
    }

    private readonly OAuth2Client oAuth2Client;
    private readonly UserSession userSession;

    public UserProfile GetUserProfile()
    {
      if (this.userSession == null)
        return null;

      var url = "1/user/-/profile.json";
      var userProfile = this.oAuth2Client.MakeRequest(url, this.userSession, this.BuildUserProfile);
      return userProfile;
    }

    private UserProfile BuildUserProfile(dynamic json)
    {
      return new UserProfile {
        UserId = this.userSession.UserId,
        DisplayName = json.user.fullName,
        OffsetFromUtcMillis = json.user.offsetFromUTCMillis,
        StrideLengthWalking = json.user.strideLengthWalking,
      };
    }
  }
}
