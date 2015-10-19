﻿using RestSharp;
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

      this.For<MillionStepsDbContext>()
        .LifecycleIs<UniquePerRequestLifecycle>();

      this.For<RestClient>()
        .Use(context => BuildRestClient(context))
        .LifecycleIs<TransientLifecycle>();
    }

    private static RestClient BuildRestClient(IContext context)
    {
      var settings = context.GetInstance<Settings>();
      return new RestClient(settings.ApiUrl.OriginalString);
    }
  }
}