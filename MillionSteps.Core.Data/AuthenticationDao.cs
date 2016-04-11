using System;
using System.Linq;
using MillionSteps.Core.Authentication;

namespace MillionSteps.Core.Data
{
  public class AuthenticationDao : Dao
  {
    public AuthenticationDao(MillionStepsDbContext dbContext)
      : base(dbContext)
    {}

    public UserSession CreateSession(string accessToken, string refreshToken)
    {
      var userSession = new UserSession {
        Id = Guid.NewGuid(),
        DateCreated = DateTime.UtcNow,
        AccessToken = accessToken,
        RefreshToken = refreshToken
      };
      this.dbContext.UserSessions.Add(userSession);
      return userSession;
    }

    public UserSession LoadUserSession(Guid userSessionId)
    {
      return this.dbContext.UserSessions.Find(userSessionId);
    }
  }
}
