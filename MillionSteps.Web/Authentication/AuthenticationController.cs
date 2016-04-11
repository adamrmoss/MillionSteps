using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using GuardClaws;
using MillionSteps.Core;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Configuration;
using MillionSteps.Core.Data;
using RestSharp.Authenticators;

namespace MillionSteps.Web.Authentication
{
  public class AuthenticationController : ControllerBase
  {
    public AuthenticationController(Settings settings, MillionStepsDbContext dbContext, AuthenticationDao authenticationDao)
      : base(settings, dbContext)
    {
      this.authenticationDao = authenticationDao;
    }

    private readonly AuthenticationDao authenticationDao;

    [HttpGet]
    public ActionResult Authenticate()
    {
      var userSession = this.authenticationDao.CreateUserSession();

      var completeAuthorizationUrl = new Uri(this.settings.AppUrl, this.Url.RouteUrl("CompleteAuthentication")).ToString();

      var nvc = new NameValueCollection {
        { "response_type", "code" },
        { "client_id", this.settings.ClientId },
        { "redirect_uri", completeAuthorizationUrl },
        { "scope", "profile activity" },
        { "state", userSession.Id.ToString() },
      };
      var queryString = nvc.ToQueryString();

      var redirectUrl = "{0}?{1}".FormatWith(this.settings.AuthorizationUrl, queryString);
      return this.Redirect(redirectUrl);
    }

    [HttpGet]
    public ActionResult Complete(string code, string state)
    {
      var userSession = this.authenticationDao.LookupUserSessionByVerifier(Guid.Parse(state));

      if (userSession == null)
        throw new ArgumentException("User Session not found for verifier", nameof(state));

      var nvc = new NameValueCollection {
        { "code", code },
        { "grant_type", "authorization_code" },
      };
      var queryString = nvc.ToQueryString();


      //userSession.Verifier = oauth_verifier;

      //var authCredential = this.authenticator.ProcessApprovedAuthCallback(BuildRequestToken(userSession));
      //userSession.Token = authCredential.AuthToken;
      //userSession.Secret = authCredential.AuthTokenSecret;
      //userSession.UserId = authCredential.UserId;

      //this.SetUserSessionCookie(userSession.Id);

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
