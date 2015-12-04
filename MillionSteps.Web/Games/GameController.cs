using System;
using System.Web.Mvc;
using GuardClaws;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Exercises;
using Raven.Client;

namespace MillionSteps.Web.Games
{
  public class GameController : ControllerBase
  {
    private readonly UserSession userSession;
    private readonly UserProfileClient userProfileClient;
    private readonly ActivityLogUpdater activityLogUpdater;

    public GameController(IDocumentSession documentSession, UserSession userSession, UserProfileClient userProfileClient, ActivityLogUpdater activityLogUpdater) : base(documentSession)
    {
      this.userSession = userSession;
      this.userProfileClient = userProfileClient;
      this.activityLogUpdater = activityLogUpdater;
    }

    [HttpGet]
    public ActionResult Index()
    {
      if (this.userSession == null)
        return this.RedirectToRoute("Welcome");

      Claws.NotNull(() => this.userProfileClient);
      var userProfile = this.userProfileClient.GetUserProfile();

      if (userProfile == null)
        return this.RedirectToRoute("Welcome");

      Claws.NotNull(() => this.activityLogUpdater);
      this.activityLogUpdater.UpdateTodayAndYesterday();
      this.userSession.OffsetFromUtcMillis = userProfile.OffsetFromUTCMillis;

      var viewModel = new GameViewModel {
        DisplayName = userProfile.DisplayName,
      };

      return this.View("~/Games/Views/Game.cshtml", viewModel);
    }

    [HttpGet]
    public ActionResult Moment(Guid momentId)
    {
      throw new NotImplementedException();
    }
  }
}
