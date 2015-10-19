using System;
using System.Linq;
using System.Web;
using MillionSteps.Core;
using MillionSteps.Core.Authentication;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Web.Pipeline;

namespace MillionSteps.Web.Configuration
{
  public class WebRegistry : Registry
  {
    public WebRegistry()
    {
      this.For<HttpRequest>()
        .Use(() => HttpContext.Current.Request)
        .LifecycleIs<HttpContextLifecycle>();

      this.For<UserSession>()
        .Use(context => BuildUserSession(context))
        .LifecycleIs<HttpContextLifecycle>();
    }

    private static UserSession BuildUserSession(IContext context)
    {
      var request = context.GetInstance<HttpRequest>();
      if (!request.Cookies.AllKeys.Contains(UserSession.CookieName))
        return null;
      // ReSharper disable once PossibleNullReferenceException
      var userSessionId = Guid.Parse(request.Cookies[UserSession.CookieName].Value);
      var dbContext = context.GetInstance<MillionStepsDbContext>();
      return dbContext.UserSessions.Find(userSessionId);
    }
  }
}
