using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GuardClaws;
using MillionSteps.Core;
using MillionSteps.Core.Adventures;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Events;
using MillionSteps.Core.Exercises;
using Raven.Client;

namespace MillionSteps.Web.Games
{
  public class GameController : ControllerBase
  {
    public GameController(IDocumentSession documentSession, UserSession userSession, UserProfileClient userProfileClient, ActivityLogUpdater activityLogUpdater, EventDriver eventDriver) : base(documentSession)
    {
      Claws.NotNull(() => eventDriver);

      this.userSession = userSession;
      this.userProfileClient = userProfileClient;
      this.activityLogUpdater = activityLogUpdater;
      this.eventDriver = eventDriver;
    }

    private readonly UserSession userSession;
    private readonly UserProfileClient userProfileClient;
    private readonly ActivityLogUpdater activityLogUpdater;
    private readonly EventDriver eventDriver;

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
        var initialMomentId = Guid.NewGuid();

        adventure = new Adventure(adventureId) {
          UserId = this.userSession.UserId,
          DateCreated = DateTime.UtcNow,
          CurrentMomentId = initialMomentId,
        };
        this.DocumentSession.Store(adventure);

        var initialMoment = new Moment(initialMomentId) {
          UserId = this.userSession.UserId,
          AdventureId = adventure.DocumentId,
          Ordinal = 0,
        };
        this.DocumentSession.Store(initialMoment);
      }

      return this.RedirectToRoute("Moment", new { momentId = adventure.CurrentMomentId });
    }

    [HttpGet]
    public ActionResult Moment(Guid momentId)
    {
      if (this.userSession == null)
        return this.RedirectToRoute("Welcome");

      Claws.NotNull(() => this.userProfileClient);
      var userProfile = this.userProfileClient.GetUserProfile();
      if (userProfile == null)
        return this.RedirectToRoute("Welcome");

      var adventure = this.DocumentSession.Query<Adventure, AdventureIndex>()
                          .SingleOrDefault(a => a.UserId == this.userSession.UserId);
      if (adventure == null)
        return this.RedirectToRoute("Game");

      var moment = this.DocumentSession.Load<Moment>(momentId);
      if (moment == null)
        return this.RedirectToRoute("Game");

      var readOnly = adventure.CurrentMomentId != momentId;

      var flagDictionary = new FlagDictionary(moment.Flags);
      var events = this.eventDriver.GetValidEvents(flagDictionary);

      var viewModel = new MomentViewModel {
        MomentId = momentId,
        ReadOnly = readOnly,
        DisplayName = userProfile.DisplayName,
        Flags = flagDictionary,
        Choices = events,
      };

      return this.View("~/Games/Views/Moment.cshtml", viewModel);
    }

    [HttpPost]
    public ActionResult Choose(Guid momentId, string eventName)
    {
      if (this.userSession == null)
        return this.RedirectToRoutePermanent("Welcome");

      var priorMoment = this.DocumentSession.Load<Moment>(momentId);
      var adventure = this.DocumentSession.Load<Adventure>(priorMoment.AdventureId);

      if (adventure == null || adventure.CurrentMomentId != momentId)
        return this.RedirectToRoute("Game");

      var @event = this.eventDriver.LookupEvent(eventName);
      if (@event == null)
        throw new InvalidOperationException($"Can't find event named: {eventName}");

      var newMoment = new Moment(Guid.NewGuid()) {
        UserId = this.userSession.UserId,
        AdventureId = adventure.DocumentId,
        Ordinal = priorMoment.Ordinal + 1,
        Flags = priorMoment.Flags.Append(eventName).Concat(@event.FlagsToSet).Except(@event.FlagsToClear).Distinct().ToArray(),
      };
      this.DocumentSession.Store(newMoment);

      adventure.CurrentMomentId = newMoment.DocumentId;

      return this.RedirectToRoutePermanent("Moment", new { momentId = newMoment.DocumentId });
    }
  }
}
