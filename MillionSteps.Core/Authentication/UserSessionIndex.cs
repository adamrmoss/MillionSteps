using System.Linq;
using Raven.Client.Indexes;

namespace MillionSteps.Core.Authentication
{
  public class UserSessionIndex : AbstractIndexCreationTask<UserSession>
  {
    public UserSessionIndex()
    {
      this.Map = userSessions => from userSession in userSessions
                                 select new {
                                   userSession.TempToken,
                                 };
    }
  }
}
