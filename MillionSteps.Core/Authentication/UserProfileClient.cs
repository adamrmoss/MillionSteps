using MillionSteps.Core.OAuth2;
using MillionSteps.Core.Work;

namespace MillionSteps.Core.Authentication
{
  [UnitWorker]
  public class UserProfileClient : AuthenticatedClientBase
  {
    public UserProfileClient(OAuth2Client oAuth2Client, UserSession userSession)
      : base(oAuth2Client, userSession)
    { }

    public UserProfile GetUserProfile()
    {
      if (this.userSession == null)
        return null;

      var url = "1/user/-/profile.json";
      var userProfile = this.oAuth2Client.MakeRequest(url, this.userSession, this.buildUserProfile);
      return userProfile;
    }

    private UserProfile buildUserProfile(dynamic json)
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
