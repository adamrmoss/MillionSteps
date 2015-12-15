using System;
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
    public GameController(IDocumentSession documentSession, UserSession userSession, UserProfileClient userProfileClient, ActivityLogUpdater activityLogUpdater, EventDriver eventDriver, AdventureDao adventureDao) : base(documentSession)
    {
      Claws.NotNull(() => eventDriver);

      this.userSession = userSession;
      this.userProfileClient = userProfileClient;
      this.activityLogUpdater = activityLogUpdater;
      this.eventDriver = eventDriver;
      this.adventureDao = adventureDao;
    }

    private readonly UserSession userSession;
    private readonly UserProfileClient userProfileClient;
    private readonly ActivityLogUpdater activityLogUpdater;
    private readonly EventDriver eventDriver;
    private readonly AdventureDao adventureDao;

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

      var adventure = this.adventureDao.LookupAdventureByUserId(this.userSession.UserId) ??
                      this.adventureDao.CreateAdventure(this.userSession.UserId);

      return this.RedirectToRoute("Moment", new {momentId = adventure.CurrentMomentId});
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

      var adventure = this.adventureDao.LookupAdventureByUserId(this.userSession.UserId);
      if (adventure == null)
        return this.RedirectToRoute("Game");

      var moment = this.DocumentSession.Load<Moment>(momentId);
      if (moment == null)
        return this.RedirectToRoute("Game");

      var readOnly = adventure.CurrentMomentId != momentId;

      var flagDictionary = new FlagDictionary(moment.Flags);
      var events = this.eventDriver.GetValidEvents(flagDictionary);

      var viewModel = new MomentViewModel {
        DisplayName = userProfile.DisplayName,
        StrideLength = userProfile.StrideLengthWalking,
        MomentId = momentId,
        ReadOnly = readOnly,
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

      var newMoment = this.adventureDao.BuildNextMoment(adventure, priorMoment, @event);

      return this.RedirectToRoutePermanent("Moment", new {momentId = newMoment.DocumentId});
    }
  }
}
