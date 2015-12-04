using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace MillionSteps.Web.Configuration
{
  public class StructureMapControllerFactory : DefaultControllerFactory
  {
    public StructureMapControllerFactory(IContainer container)
    {
      this.container = container;
    }

    private readonly IContainer container;

    protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
    {
      if (controllerType == null)
        return null;

      var controller = this.container.GetInstance(controllerType);

      return controller as Controller;
    }
  }
}
