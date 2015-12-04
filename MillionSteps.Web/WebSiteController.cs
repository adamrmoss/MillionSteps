using System.Web.Mvc;
using MillionSteps.Core;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Events;
using Raven.Client;
using Raven.Client.Indexes;

namespace MillionSteps.Web
{
  public class WebSiteController : ControllerBase
  {
    public WebSiteController(IDocumentSession documentSession, UserProfileClient userProfileClient) 
      : base(documentSession)
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
      this.ClearUserSessionCookie();

      return this.View("~/Authentication/Views/Welcome.cshtml");
    }

    [HttpGet]
    public ActionResult Initialize()
    {
      IndexCreation.CreateIndexes(typeof(UserSessionIndex).Assembly, this.DocumentSession.Advanced.DocumentStore);
      foreach (var @event in IntroductionEvents.All) {
        this.DocumentSession.CreateIfNew(@event);
      }

      return this.RedirectToRoute("Index");
    }
  }
}
