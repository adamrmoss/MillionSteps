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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace MillionSteps.Web.Authentication
{
  public class AuthenticationController : ControllerBase
  {
    public AuthenticationController(Settings settings, MillionStepsDbContext dbContext, AuthenticationDao authenticationDao, RestClient restClient)
      : base(settings, dbContext)
    {
      this.authenticationDao = authenticationDao;
      this.restClient = restClient;
    }

    private readonly AuthenticationDao authenticationDao;
    private readonly RestClient restClient;

    [HttpGet]
    public ActionResult Authenticate()
    {
      var userSession = this.authenticationDao.CreateUserSession();

      var completeAuthorizationUrl = new Uri(this.settings.AppUrl, this.Url.RouteUrl("CompleteAuthentication")).ToString();

      var nvc = new NameValueCollection {
        { "response_type", "code" },
        { "scope", "profile activity" },
        { "client_id", this.settings.ClientId },
        { "redirect_uri", completeAuthorizationUrl },
        { "state", userSession.Verifier.ToString() },
      };
      var queryString = nvc.ToQueryString();

      var redirectUrl = "{0}?{1}".FormatWith(this.settings.AuthorizationUrl, queryString);
      return this.Redirect(redirectUrl);
    }

    [HttpGet]
    public ActionResult Complete(string code, string state)
    {
      var userSession = this.authenticationDao.LoadUserSessionByVerifier(Guid.Parse(state));

      if (userSession == null)
        throw new ArgumentException("User Session not found for verifier", nameof(state));

      var basicAuthHeader = "Basic " + "{0}:{1}".FormatWith(this.settings.ClientId, this.settings.ClientSecret).Base64Encode();

      var completeAuthorizationUrl = new Uri(this.settings.AppUrl, this.Url.RouteUrl("CompleteAuthentication")).ToString();

      var restRequest = new RestRequest(this.settings.TokenUrl, Method.POST);
      restRequest.AddHeader("Authorization", basicAuthHeader);
      restRequest.AddParameter("grant_type", "authorization_code");
      restRequest.AddParameter("code", code);
      restRequest.AddParameter("client_id", this.settings.ClientId);
      restRequest.AddParameter("redirect_uri", completeAuthorizationUrl);
      restRequest.AddParameter("state", userSession.Verifier.ToString());

      var response = this.restClient.Execute(restRequest);

      if (response.StatusCode != HttpStatusCode.OK)
        // TODO: Better handling of token failure
        return this.RedirectToRoute("Index");

      var combinedToken = JsonConvert.DeserializeObject<dynamic>(response.Content);
      var accessToken = (string) combinedToken.access_token;
      var refreshToken = (string) combinedToken.refresh_token;
      var userId = (string) combinedToken.user_id;

      userSession.AccessToken = accessToken;
      userSession.RefreshToken = refreshToken;
      userSession.UserId = userId;

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
