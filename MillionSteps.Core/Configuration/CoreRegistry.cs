using Fitbit.Api;
using MillionSteps.Core.Authentication;
using RestSharp;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;
using StructureMap.Web.Pipeline;

namespace MillionSteps.Core.Configuration
{
  public class CoreRegistry : Registry
  {
    public CoreRegistry()
    {
      this.For<Settings>()
        .LifecycleIs<SingletonLifecycle>();

      this.For<MillionStepsDbContextFactory>()
        .Use(context => BuildMillionStepsDbContextFactory())
        .LifecycleIs<SingletonLifecycle>();

      this.For<MillionStepsDbContext>()
        .Use(context => context.GetInstance<MillionStepsDbContextFactory>()())
        .LifecycleIs<HttpContextLifecycle>();

      this.For<RestClient>()
        .Use(context => BuildRestClient(context))
        .LifecycleIs<TransientLifecycle>();

      this.For<Authenticator>()
        .Use(context => BuildAuthenticator(context))
        .LifecycleIs<TransientLifecycle>();

      this.For<UserProfileClient>()
        .LifecycleIs<TransientLifecycle>();

      this.For<FitbitClient>()
        .Use(context => BuildFitbitClient(context))
        .LifecycleIs<TransientLifecycle>();
    }

    private static MillionStepsDbContextFactory BuildMillionStepsDbContextFactory()
    {
      return () => new MillionStepsDbContext();
    }

    private static RestClient BuildRestClient(IContext context)
    {
      var settings = context.GetInstance<Settings>();
      return new RestClient(settings.ApiUrl.OriginalString);
    }

    private static Authenticator BuildAuthenticator(IContext context)
    {
      var settings = context.GetInstance<Settings>();
      var restClient = context.GetInstance<RestClient>();

      return new Authenticator(settings.ConsumerKey, settings.ConsumerSecret, settings.RequestTokenUrl.OriginalString, settings.AccessTokenUrl.OriginalString, settings.AuthorizeUrl.OriginalString, restClient);
    }

    private static FitbitClient BuildFitbitClient(IContext context)
    {
      var settings = context.GetInstance<Settings>();
      var userSession = context.TryGetInstance<UserSession>();
      var restClient = context.GetInstance<RestClient>();

      if (userSession == null || restClient == null)
        return null;

      var accessToken = userSession.Token;
      return new FitbitClient(settings.ConsumerKey, settings.ConsumerSecret, accessToken, userSession.Secret, restClient);
    }
  }
}
