using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web.Mvc;
using GuardClaws;
using MillionSteps.Core;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Configuration;
using MillionSteps.Core.Data;
using MillionSteps.Core.Work;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace MillionSteps.Web.Authentication
{
  public class AuthenticationController : ControllerBase
  {
    public AuthenticationController(Settings settings, ISaveChanges unitOfWork, AuthenticationDao authenticationDao, OAuth2Client oAuth2Client)
      : base(settings, unitOfWork)
    {
      this.authenticationDao = authenticationDao;
      this.oAuth2Client = oAuth2Client;
    }

    private readonly AuthenticationDao authenticationDao;
    private readonly OAuth2Client oAuth2Client;

    [HttpGet]
    public ActionResult Authenticate()
    {
      var completeAuthorizationUrl = new Uri(this.settings.AppUrl, this.Url.RouteUrl("CompleteAuthentication")).ToString();
      var userSession = this.authenticationDao.CreateUserSession(completeAuthorizationUrl);

      var redirectUrl = this.oAuth2Client.GetAuthorizationUrl(userSession);
      return this.Redirect(redirectUrl);
    }

    [HttpGet]
    public ActionResult Complete(string code, string state)
    {
      var userSession = this.authenticationDao.LoadUserSessionByVerifier(Guid.Parse(state));

      if (userSession == null)
        throw new ArgumentException("User Session not found for verifier", nameof(state));

      this.oAuth2Client.RequestTokens(userSession, code);

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
