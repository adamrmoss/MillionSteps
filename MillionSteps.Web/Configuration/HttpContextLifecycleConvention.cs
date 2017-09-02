using System;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using StructureMap.Web.Pipeline;

namespace MillionSteps.Web.Configuration
{
    public class HttpContextLifecycleConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            registry.For(type, new HttpContextLifecycle());
        }

        public void ScanTypes(TypeSet types, Registry registry)
        {
            throw new NotImplementedException();
        }
    }
}
