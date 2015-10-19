using System.Web.Mvc;

namespace MillionSteps.Web
{
  public class WebSiteController : ControllerBase
  {
    //private readonly UserProfileClient userProfileClient;

    //public WebSiteController(UserProfileClient userProfileClient)
    //{
    //  this.userProfileClient = userProfileClient;
    //}

    [HttpGet]
    public ActionResult Index()
    {
      //var userProfile = this.userProfileClient.GetUserProfile();

      //if (userProfile == null) 
        return this.RedirectToRoute("Welcome");
      //else
      //  return this.RedirectToRoute("Game");
    }

    [HttpGet]
    public ActionResult Welcome()
    {
      this.ClearUserSessionCookie();

      return this.View("~/Authentication/Views/Welcome.cshtml");
    }
  }
}
