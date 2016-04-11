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

    public UserSession CreateUserSession()
    {
      var userSession = new UserSession {
        Id = Guid.NewGuid(),
        Verifier = Guid.NewGuid(),
        DateCreated = DateTime.UtcNow,
      };
      this.DbContext.UserSessions.Add(userSession);
      return userSession;
    }

    public UserSession LoadUserSession(Guid userSessionId)
    {
      return this.DbContext.UserSessions.Find(userSessionId);
    }

    public UserSession LoadUserSessionByVerifier(Guid verifier)
    {
      var userSession = this.DbContext.UserSessions.SingleOrDefault(us => us.Verifier == verifier);
      this.DbContext.UserSessions.Attach(userSession);
      return userSession;
    }
  }
}
