using RestSharp;
using RestSharp.Authenticators;
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
          .Use(context => BuildRestClient(context))
          .LifecycleIs<UniquePerRequestLifecycle>();

      this.ConfigureFitbitApi();
    }

    private static RestClient BuildRestClient(IContext context)
    {
      var settings = context.GetInstance<Settings>();
      return new RestClient(settings.ApiUrl.OriginalString);
    }

    private void ConfigureFitbitApi()
    {
    }
  }
}
