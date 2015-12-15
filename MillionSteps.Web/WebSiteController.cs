using System.Web.Mvc;
using MillionSteps.Core;
using MillionSteps.Core.Adventures;
using MillionSteps.Core.Authentication;
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
      var documentStore = this.DocumentSession.Advanced.DocumentStore;
      IndexCreation.CreateIndexes(typeof(UserSessionIndex).Assembly, documentStore);

      return this.RedirectToRoute("Index");
    }

    [HttpGet]
    public ActionResult Reset()
    {
      var documentStore = this.DocumentSession.Advanced.DocumentStore;
      documentStore.DeleteAll<UserSession>();
      documentStore.DeleteAll<Adventure>();
      documentStore.DeleteAll<Moment>();

      return this.RedirectToRoute("Index");
    }
  }
}
