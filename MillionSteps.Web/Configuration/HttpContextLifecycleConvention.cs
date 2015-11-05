using System;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Web.Pipeline;

namespace MillionSteps.Web.Configuration
{
  public class HttpContextLifecycleConvention : IRegistrationConvention
  {
    public void Process(Type type, Registry registry)
    {
      registry.For(type, new HttpContextLifecycle());
    }
  }
}
