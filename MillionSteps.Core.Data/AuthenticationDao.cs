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

    public UserSession CreateSession(string tempToken, string tempSecret)
    {
      var userSession = new UserSession
      {
        Id = Guid.NewGuid(),
        DateCreated = DateTime.UtcNow,
        TempToken = tempToken,
        TempSecret = tempSecret
      };
      this.dbContext.UserSessions.Add(userSession);
      return userSession;
    }

    public UserSession LoadUserSession(Guid userSessionId)
    {
      return this.dbContext.UserSessions.Find(userSessionId);
    }

    public UserSession LookupSessionByTempToken(string tempToken)
    {
      return this.dbContext.UserSessions.SingleOrDefault(userSession => userSession.TempToken == tempToken);
    }
  }
}
