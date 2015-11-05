using System;
using System.Linq;
using GuardClaws;

namespace MillionSteps.Core.Authentication
{
  public class AuthenticationDao
  {
    public AuthenticationDao(MillionStepsDbContext dbContext)
    {
      Claws.NotNull(() => dbContext);
      this.dbContext = dbContext;
    }

    private readonly MillionStepsDbContext dbContext;

    public void CreateSession(string tempToken, string tempSecret)
    {
      var userSession = new UserSession {
        Id = Guid.NewGuid(),
        DateCreated = DateTime.UtcNow,
        TempToken = tempToken,
        TempSecret = tempSecret,
      };
      this.dbContext.UserSessions.Add(userSession);
    }

    public UserSession LookupSessionByTempToken(string tempToken)
    {
      var userSession = this.dbContext.UserSessions
        .SingleOrDefault(us => us.TempToken == tempToken);
      return userSession;
    }
  }
}
