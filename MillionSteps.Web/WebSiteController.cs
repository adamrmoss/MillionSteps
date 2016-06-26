using System.Web.Mvc;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Configuration;
using MillionSteps.Core.Data;

namespace MillionSteps.Web
{
  public class WebSiteController : ControllerBase
  {
    public WebSiteController(Settings settings, MillionStepsDbContext dbContext, UserProfileClient userProfileClient)
      : base(settings, dbContext)
    {
      this.userProfileClient = userProfileClient;
    }

    private readonly UserProfileClient userProfileClient;

    [HttpGet]
    public ActionResult Index()
    {
      var userProfile = this.userProfileClient.GetUserProfile();

      if (userProfile == null)
        return this.RedirectToRoute("Welcome");
      else
        return this.RedirectToRoute("Game");
    }

    [HttpGet]
    public ActionResult Welcome()
    {
      this.clearUserSessionCookie();

      return this.View("~/Authentication/Views/Welcome.cshtml");
    }

    [HttpGet]
    public ActionResult Initialize()
    {
      // TODO: Still need this?

      return this.RedirectToRoute("Index");
    }

    [HttpGet]
    public ActionResult Reset()
    {
      // TODO: Delete everything

      return this.RedirectToRoute("Index");
    }
  }
}
