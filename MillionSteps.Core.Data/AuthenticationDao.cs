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

    public void CreateSession(string tempToken, string tempSecret)
    {
      var userSession = this.dbContext.UserSessions.Create();
      userSession.DateCreated = DateTime.UtcNow;
      userSession.TempToken = tempToken;
      userSession.TempSecret = tempSecret;
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
