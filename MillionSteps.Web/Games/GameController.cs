using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GuardClaws;
using MillionSteps.Core.Adventures;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Events;
using MillionSteps.Core.Exercises;
using Raven.Client;

namespace MillionSteps.Web.Games
{
  public class GameController : ControllerBase
  {
    public GameController(IDocumentSession documentSession, UserSession userSession, UserProfileClient userProfileClient, ActivityLogUpdater activityLogUpdater) : base(documentSession)
    {
      this.userSession = userSession;
      this.userProfileClient = userProfileClient;
      this.activityLogUpdater = activityLogUpdater;
    }

    private readonly UserSession userSession;
    private readonly UserProfileClient userProfileClient;
    private readonly ActivityLogUpdater activityLogUpdater;

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

      var adventure = this.DocumentSession.Query<Adventure, AdventureIndex>()
                          .SingleOrDefault(a => a.UserId == this.userSession.UserId);
      if (adventure == null) {
        var adventureId = Guid.NewGuid();
        var momentId = Guid.NewGuid();

        adventure = new Adventure(adventureId) {
          UserId = this.userSession.UserId,
          DateCreated = DateTime.UtcNow,
          CurrentMomentId = momentId,
        };
        this.DocumentSession.Store(adventure);

        var initialMoment = new Moment(momentId) {
          UserId = this.userSession.UserId,
          AdventureId = adventure.DocumentId,
          EventName = typeof(StoryStarted).Name,
          Ordinal = 0,
        };
        this.DocumentSession.Store(initialMoment);
      }

      return this.RedirectToRoute("Moment", new { momentId = adventure.CurrentMomentId });
    }

    [HttpGet]
    public ActionResult Moment(Guid momentId)
    {
      Claws.NotNull(() => this.userProfileClient);
      var userProfile = this.userProfileClient.GetUserProfile();
      if (userProfile == null)
        return this.RedirectToRoute("Welcome");

      var moment = this.DocumentSession.Load<Moment>(momentId);
      var flagDictionary = new FlagDictionary(moment.Flags);

      var events = new List<Event>();

      var viewModel = new GameViewModel {
        DisplayName = userProfile.DisplayName,
        MomentId = momentId,
        Flags = flagDictionary,
        Choices = events,
      };

      return this.View("~/Games/Views/Game.cshtml", viewModel);
    }
  }
}
