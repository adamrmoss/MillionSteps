using System.Web.Mvc;
using GuardClaws;
using MillionSteps.Core;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Exercises;

namespace MillionSteps.Web.Games
{
  public class GameController : ControllerBase
  {
    private readonly MillionStepsDbContext dbContext;
    private readonly UserSession userSession;
    private readonly UserProfileClient userProfileClient;
    private readonly ActivityLogClient activityLogClient;

    public GameController(MillionStepsDbContext dbContext, UserSession userSession, UserProfileClient userProfileClient, ActivityLogClient activityLogClient)
    {
      Claws.NotNull(() => dbContext);

      this.dbContext = dbContext;
      this.userSession = userSession;
      this.userProfileClient = userProfileClient;
      this.activityLogClient = activityLogClient;
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

      Claws.NotNull(() => this.activityLogClient);
      this.activityLogClient.UpdateTodayAndYesterday();
      this.userSession.OffsetFromUtcMillis = userProfile.OffsetFromUTCMillis;
      this.dbContext.SaveChanges();

      var viewModel = new GameViewModel {
        DisplayName = userProfile.DisplayName,
      };

      return this.View("~/Games/Views/CreateAdventurer.cshtml", viewModel);
    }
  }
}
