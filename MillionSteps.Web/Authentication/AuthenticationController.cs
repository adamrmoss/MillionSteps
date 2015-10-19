using System;
using System.Web.Mvc;
using GuardClaws;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Configuration;
using HttpCookie = System.Web.HttpCookie;

namespace MillionSteps.Web.Authentication
{
  public class AuthenticationController : ControllerBase
  {
    private readonly Settings settings;
    //private readonly Authenticator authenticator;
    //private readonly IDocumentSession documentSession;

    //public AuthenticationController(Settings settings, Authenticator authenticator, IDocumentSession documentSession)
    //{
    //  Claws.NotNull(() => settings);
    //  Claws.NotNull(() => authenticator);
    //  Claws.NotNull(() => documentSession);

    //  this.documentSession = documentSession;
    //  this.settings = settings;
    //  this.authenticator = authenticator;
    //}

    [HttpGet]
    public ActionResult Authenticate(AuthenticationRequest authenticationRequest)
    {
      var completeAuthorizationUrl = new Uri(this.settings.AppUrl, this.Url.RouteUrl("CompleteAuthentication")).ToString();
      //var requestToken = this.authenticator.GetRequestToken(completeAuthorizationUrl);

      //var userSession = new UserSession(Guid.NewGuid()) {
      //  CreateDate = DateTime.UtcNow,
      //  TempToken = requestToken.Token,
      //  TempSecret = requestToken.Secret,
      //};
      //this.documentSession.Store(userSession);
      //this.documentSession.SaveChanges();

      //var redirectUrl = this.authenticator.GenerateAuthUrlFromRequestToken(requestToken, false);
      var redirectUrl = "www.fitbit.com";
      return this.Redirect(redirectUrl);
    }

    [HttpGet]
    // ReSharper disable InconsistentNaming
    public ActionResult Complete(string oauth_token, string oauth_verifier)
    // ReSharper restore InconsistentNaming
    {
      //var userSession = this.documentSession.Query<UserSession, UserSessionIndex>()
      //  .SingleOrDefault(us => us.TempToken == oauth_token);

      //if (userSession == null)
      //  throw new ArgumentException("Session not found", "oauth_token");

      //var requestToken = new RequestToken {
      //  Token = userSession.TempToken,
      //  Secret = userSession.TempSecret,
      //  Verifier = oauth_verifier,
      //};
      //var authenticationCallback = this.authenticator.ProcessApprovedAuthCallback(requestToken);

      //userSession.Verifier = oauth_verifier;
      //userSession.Token = authenticationCallback.AuthToken;
      //userSession.Secret = authenticationCallback.AuthTokenSecret;
      //userSession.UserId = authenticationCallback.UserId;
      //this.documentSession.Store(userSession);
      //this.documentSession.SaveChanges();

      //var httpCookie = new HttpCookie(UserSession.CookieName, userSession.Id.ToString()) {
      //  Expires = DateTime.UtcNow + UserSession.Lifetime
      //};
      //this.Response.Cookies.Add(httpCookie);

      return this.RedirectToAction("Index", "WebSite");
    }
  }
}
