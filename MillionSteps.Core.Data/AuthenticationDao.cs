using System;
using System.Linq;
using MillionSteps.Core.Authentication;

namespace MillionSteps.Core.Data
{
  public class AuthenticationDao : Dao
  {
    public AuthenticationDao(MillionStepsDbContext dbContext)
      : base(dbContext)
    { }

    public UserSession CreateUserSession(string redirectUrl)
    {
      var userSession = new UserSession {
        Id = Guid.NewGuid(),
        Verifier = Guid.NewGuid(),
        DateCreated = DateTime.UtcNow,
        RedirectUrl = redirectUrl,
      };
      this.dbContext.UserSessions.Add(userSession);
      return userSession;
    }

    public UserSession LoadUserSession(Guid userSessionId)
    {
      return this.dbContext.UserSessions.Find(userSessionId);
    }

    public UserSession LoadUserSessionByVerifier(Guid verifier)
    {
      var userSession = this.dbContext.UserSessions.SingleOrDefault(us => us.Verifier == verifier);
      this.dbContext.UserSessions.Attach(userSession);
      return userSession;
    }
  }
}
