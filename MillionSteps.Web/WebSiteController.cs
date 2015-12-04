using System.Web.Mvc;
using MillionSteps.Core.Authentication;
using Raven.Client;

namespace MillionSteps.Web
{
  public class WebSiteController : ControllerBase
  {
    private readonly UserProfileClient userProfileClient;

    public WebSiteController(IDocumentSession documentSession, UserProfileClient userProfileClient) 
      : base(documentSession)
    {
      this.userProfileClient = userProfileClient;
    }

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
      this.ClearUserSessionCookie();

      return this.View("~/Authentication/Views/Welcome.cshtml");
    }
  }
}
