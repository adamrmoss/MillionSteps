using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MillionSteps.Core.Configuration;
using MillionSteps.Web.Configuration;
using StructureMap;

namespace MillionSteps.Web
{
  public class MvcApplication : HttpApplication
  {
    protected void Application_Start()
    {
      RegisterRoutes();
      BuildControllerFactory();
    }

    public static void RegisterRoutes()
    {
      var routes = RouteTable.Routes;
      routes.RouteExistingFiles = true;
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute("Index", "", new { controller = "WebSite", action = "Index" });
      routes.MapRoute("Welcome", "Welcome", new { controller = "WebSite", action = "Welcome" });
      routes.MapRoute("Initialize", "Initialize", new { controller = "WebSite", action = "Initialize" });

      routes.MapRoute("Authenticate", "Authenticate", new { controller = "Authentication", action = "Authenticate" });
      routes.MapRoute("CompleteAuthentication", "Authenticate/Complete", new { controller = "Authentication", action = "Complete" });
      routes.MapRoute("Logout", "Logout", new { controller = "Authentication", action = "Logout" });

      routes.MapRoute("Game", "Game", new { controller = "Game", action = "Index" });
      routes.MapRoute("Moment", "Moment/{momentId}", new { controller = "Game", action = "Moment" });
      routes.MapRoute("Choose", "Choose/{momentId}", new { controller = "Game", action = "Choose" });
    }

    private static void BuildControllerFactory()
    {
      var container = new Container(config => {
        config.AddRegistry<CoreRegistry>();
        config.AddRegistry<WebRegistry>();
      });
      var structureMapControllerFactory = new StructureMapControllerFactory(container);
      ControllerBuilder.Current.SetControllerFactory(structureMapControllerFactory);
    }
  }
}
