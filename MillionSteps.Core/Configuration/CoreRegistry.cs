using RestSharp;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace MillionSteps.Core.Configuration
{
  public class CoreRegistry : Registry
  {
    public CoreRegistry()
    {
      this.For<Settings>()
          .LifecycleIs<SingletonLifecycle>();

      this.For<RestClient>()
          .Use(context => buildRestClient(context))
          .LifecycleIs<UniquePerRequestLifecycle>();

      this.configureFitbitApi();
    }

    private static RestClient buildRestClient(IContext context)
    {
      var settings = context.GetInstance<Settings>();
      return new RestClient(settings.ApiUrl.OriginalString);
    }

    private void configureFitbitApi()
    {
    }
  }
}
