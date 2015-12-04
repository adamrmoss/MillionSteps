using System;
using System.Linq;
using Raven.Client;

namespace MillionSteps.Core.Authentication
{
  public class AuthenticationDao : Dao
  {
    public AuthenticationDao(IDocumentSession documentSession)
      : base(documentSession)
    { }

    public void CreateSession(string tempToken, string tempSecret)
    {
      var userSession = new UserSession(Guid.NewGuid()) {
        DateCreated = DateTime.UtcNow,
        TempToken = tempToken,
        TempSecret = tempSecret,
      };
      this.DocumentSession.Store(userSession);
    }

    public UserSession LookupUserSession(Guid userSessionId)
    {
      return this.DocumentSession.Load<UserSession>(userSessionId);
    }

    public UserSession LookupSessionByTempToken(string tempToken)
    {
      var userSession = this.DocumentSession.Query<UserSession, UserSessionIndex>()
                            .SingleOrDefault(us => us.TempToken == tempToken);
      return userSession;
    }
  }
}
