using System;
using System.Linq;
using System.Web.Mvc;
using Fitbit.Api;
using Fitbit.Models;
using GuardClaws;
using MillionSteps.Core;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Configuration;

namespace MillionSteps.Web.Authentication
{
  public class AuthenticationController : ControllerBase
  {
    private readonly Settings settings;
    private readonly Authenticator authenticator;

    public AuthenticationController(MillionStepsDbContext dbContext, Settings settings, Authenticator authenticator)
      : base(dbContext)
    {
      Claws.NotNull(() => settings);
      Claws.NotNull(() => authenticator);

      this.settings = settings;
      this.authenticator = authenticator;
    }

    [HttpGet]
    public ActionResult Authenticate(AuthenticationRequest authenticationRequest)
    {
      var completeAuthorizationUrl = new Uri(this.settings.AppUrl, this.Url.RouteUrl("CompleteAuthentication")).ToString();
      var requestToken = this.authenticator.GetRequestToken(completeAuthorizationUrl);

      var userSession = new UserSession {
        Id = Guid.NewGuid(),
        CreateDate = DateTime.UtcNow,
        TempToken = requestToken.Token,
        TempSecret = requestToken.Secret,
      };
      this.DbContext.UserSessions.Add(userSession);

      var redirectUrl = this.authenticator.GenerateAuthUrlFromRequestToken(requestToken, false);
      return this.Redirect(redirectUrl);
    }

    [HttpGet]
    // ReSharper disable InconsistentNaming
    public ActionResult Complete(string oauth_token, string oauth_verifier)
    // ReSharper restore InconsistentNaming
    {
      var userSession = this.DbContext.UserSessions
        .SingleOrDefault(us => us.TempToken == oauth_token);

      if (userSession == null)
        throw new ArgumentException("Session not found", nameof(oauth_token));

      var requestToken = new RequestToken {
        Token = userSession.TempToken,
        Secret = userSession.TempSecret,
        Verifier = oauth_verifier,
      };
      var authenticationCallback = this.authenticator.ProcessApprovedAuthCallback(requestToken);

      userSession.Verifier = oauth_verifier;
      userSession.Token = authenticationCallback.AuthToken;
      userSession.Secret = authenticationCallback.AuthTokenSecret;
      userSession.UserId = authenticationCallback.UserId;

      this.SetUserSessionCookie(userSession.Id);

      return this.RedirectToRoute("Index");
    }

    [HttpGet]
    public ActionResult Logout()
    {
      this.ClearUserSessionCookie();
      return this.RedirectToRoute("Index");
    }
  }
}
