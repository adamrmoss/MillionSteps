using System.Web;
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
    }
  }
}
