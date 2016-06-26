using System;
using System.Linq;
using System.Web;
using Fasterflect;
using MillionSteps.Core;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Configuration;
using MillionSteps.Core.Data;
using MillionSteps.Core.Work;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
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
          .Use(context => buildUserSession(context))
          .LifecycleIs<HttpContextLifecycle>();

      this.For<MillionStepsDbContext>()
          .LifecycleIs<HttpContextLifecycle>();

      this.For<ISaveChanges>()
          .Use(context => context.GetInstance<MillionStepsDbContext>())
          .LifecycleIs<HttpContextLifecycle>();

      this.Scan(performScan);
    }

    private static UserSession buildUserSession(IContext context)
    {
      var request = context.GetInstance<HttpRequest>();
      if (!request.Cookies.AllKeys.Contains(UserSession.CookieName))
        return null;
      // ReSharper disable once PossibleNullReferenceException
      var userSessionId = Guid.Parse(request.Cookies[UserSession.CookieName].Value);
      var authenticationDao = context.GetInstance<AuthenticationDao>();
      return authenticationDao.LoadUserSession(userSessionId);
    }

    private static void performScan(IAssemblyScanner scanner)
    {
      scanner.AssembliesFromApplicationBaseDirectory();
      scanner.Include(type => type.HasAttribute<UnitWorkerAttribute>());
      scanner.Convention<HttpContextLifecycleConvention>();
    }
  }
}
