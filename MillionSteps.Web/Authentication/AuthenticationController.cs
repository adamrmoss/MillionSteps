using System;
using System.Web.Mvc;
using Fitbit.Api;
using Fitbit.Models;
using GuardClaws;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Configuration;
using Raven.Client;

namespace MillionSteps.Web.Authentication
{
  public class AuthenticationController : ControllerBase
  {
    public AuthenticationController(IDocumentSession documentSession, Settings settings, Authenticator authenticator, AuthenticationDao authenticationDao)
      : base(documentSession)
    {
      Claws.NotNull(() => settings);
      Claws.NotNull(() => authenticator);

      this.settings = settings;
      this.authenticator = authenticator;
      this.authenticationDao = authenticationDao;
    }

    private readonly Settings settings;
    private readonly Authenticator authenticator;
    private readonly AuthenticationDao authenticationDao;

    [HttpGet]
    public ActionResult Authenticate(AuthenticationRequest authenticationRequest)
    {
      var completeAuthorizationUrl = new Uri(this.settings.AppUrl, this.Url.RouteUrl("CompleteAuthentication")).ToString();
      var requestToken = this.authenticator.GetRequestToken(completeAuthorizationUrl);

      this.authenticationDao.CreateSession(requestToken.Token, requestToken.Secret);

      var redirectUrl = this.authenticator.GenerateAuthUrlFromRequestToken(requestToken, false);
      return this.Redirect(redirectUrl);
    }

    [HttpGet]
    // ReSharper disable InconsistentNaming
    public ActionResult Complete(string oauth_token, string oauth_verifier)
    {
      var userSession = this.authenticationDao.LookupSessionByTempToken(oauth_token);

      if (userSession == null)
        throw new ArgumentException("Session not found", nameof(oauth_token));

      userSession.Verifier = oauth_verifier;

      var authCredential = this.authenticator.ProcessApprovedAuthCallback(BuildRequestToken(userSession));
      userSession.Token = authCredential.AuthToken;
      userSession.Secret = authCredential.AuthTokenSecret;
      userSession.UserId = authCredential.UserId;

      this.SetUserSessionCookie(userSession.DocumentId);

      return this.RedirectToRoute("Index");
    }
    // ReSharper restore InconsistentNaming

    private static RequestToken BuildRequestToken(UserSession userSession)
    {
      return new RequestToken {
        Token = userSession.TempToken,
        Secret = userSession.TempSecret,
        Verifier = userSession.Verifier,
      };
    }

    [HttpGet]
    public ActionResult Logout()
    {
      this.ClearUserSessionCookie();
      return this.RedirectToRoute("Index");
    }
  }
}
