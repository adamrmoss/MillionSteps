using System;
using System.Linq;
using System.Web;
using Fasterflect;
using MillionSteps.Core;
using MillionSteps.Core.Authentication;
using Raven.Client;
using Raven.Client.Document;
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
          .Use(context => BuildUserSession(context))
          .LifecycleIs<HttpContextLifecycle>();

      this.For<IDocumentStore>()
          .Use(() => new DocumentStore {ConnectionStringName = "MillionSteps"}.Initialize())
          .LifecycleIs<HttpContextLifecycle>();

      this.For<IDocumentSession>()
          .Use(context => context.GetInstance<IDocumentStore>().OpenSession())
          .LifecycleIs<HttpContextLifecycle>();

      this.Scan(scanner => {
        scanner.AssembliesFromApplicationBaseDirectory();
        scanner.Include(type => type.HasAttribute<UnitWorkerAttribute>());
        scanner.Convention<HttpContextLifecycleConvention>();
      });
    }

    private static UserSession BuildUserSession(IContext context)
    {
      var request = context.GetInstance<HttpRequest>();
      if (!request.Cookies.AllKeys.Contains(UserSession.CookieName))
        return null;
      // ReSharper disable once PossibleNullReferenceException
      var userSessionId = Guid.Parse(request.Cookies[UserSession.CookieName].Value);
      var authenticationDao = context.GetInstance<AuthenticationDao>();
      return authenticationDao.LookupUserSession(userSessionId);
    }
  }
}
