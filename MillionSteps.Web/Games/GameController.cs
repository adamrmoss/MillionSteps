using System;
using System.Web.Mvc;
using GuardClaws;
using MillionSteps.Core;
using MillionSteps.Core.Adventures;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Configuration;
using MillionSteps.Core.Data;
using MillionSteps.Core.Events;
using MillionSteps.Web.Exercises;

namespace MillionSteps.Web.Games
{
  public class GameController : ControllerBase
  {
    public GameController(Settings settings, MillionStepsDbContext dbContext, UserSession userSession, UserProfileClient userProfileClient, ActivityLogUpdater activityLogUpdater, EventDriver eventDriver, AdventureDao adventureDao) : base(settings, dbContext)
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

      var existingAdventure = this.adventureDao.LookupAdventureByUserId(this.userSession.UserId);
      var currentMomentId = existingAdventure?.CurrentMomentId ?? this.adventureDao.CreateAdventure(this.userSession.UserId).CurrentMomentId ;

      return this.RedirectToRoute("Moment", new {momentId = currentMomentId});
    }

    [HttpGet]
    public ActionResult Moment(int momentId)
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

      var moment = this.dbContext.Moments.Find(momentId);
      if (moment == null)
        return this.RedirectToRoute("Game");

      var readOnly = adventure.CurrentMomentId != momentId;

      var flags = moment.Flags;
      if (moment.EventName != null)
        flags = flags.Append(moment.EventName);
      var flagDictionary = new FlagDictionary(flags);
      var events = this.eventDriver.GetValidEvents(flagDictionary);

      var viewModel = new MomentViewModel {
        DisplayName = userProfile.DisplayName,
        StrideLength = userProfile.StrideLengthWalking,
        MomentId = momentId,
        ReadOnly = readOnly,
        Flags = flagDictionary,
        Choices = events,
      };

      if (this.Request.IsAjaxRequest())
        return this.View("~/Games/Views/Choices.cshtml", viewModel);
      else
        return this.View("~/Games/Views/Moment.cshtml", viewModel);
    }

    [HttpPost]
    public ActionResult Choose(int momentId, string eventName)
    {
      if (this.userSession == null)
        return this.RedirectToRoutePermanent("Welcome");

      var priorMoment = this.dbContext.Moments.Find(momentId);
      var adventure = priorMoment.Adventure;

      if (adventure == null || adventure.CurrentMomentId != momentId)
        return this.RedirectToRoute("Game");

      var @event = this.eventDriver.LookupEvent(eventName);
      if (@event == null)
        throw new InvalidOperationException($"Can't find event named: {eventName}");

      var newMoment = this.adventureDao.BuildNextMoment(adventure, priorMoment, @event);

      return this.RedirectToRoutePermanent("Moment", new {momentId = newMoment.Id});
    }
  }
}
