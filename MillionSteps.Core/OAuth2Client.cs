using System;
using System.Collections.Specialized;
using System.Net;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Configuration;
using MillionSteps.Core.Work;
using Newtonsoft.Json;
using RestSharp;

namespace MillionSteps.Core
{
  [UnitWorker]
  public class OAuth2Client
  {
    private readonly Settings settings;
    private readonly RestClient restClient;

    public OAuth2Client(Settings settings, RestClient restClient)
    {
      this.settings = settings;
      this.restClient = restClient;
    }

    public string GetAuthorizationUrl(UserSession userSession) {
      var nvc = new NameValueCollection {
        { "response_type", "code" },
        { "scope", "profile activity" },
        { "client_id", this.settings.ClientId },
        { "redirect_uri", userSession.RedirectUrl },
        { "state", userSession.Verifier.ToString() },
      };
      var queryString = nvc.ToQueryString();

      var redirectUrl = "{0}?{1}".FormatWith(this.settings.AuthorizationUrl, queryString);
      return redirectUrl;
    }

    public void RequestTokens(UserSession userSession, string code, Action<IRestResponse> errorAction=null)
    {
      var restRequest = new RestRequest(this.settings.TokenUrl, Method.POST);
      restRequest.AddHeader("Authorization", this.GetBasicAuthHeader());
      restRequest.AddParameter("grant_type", "authorization_code");
      restRequest.AddParameter("code", code);
      restRequest.AddParameter("client_id", this.settings.ClientId);
      restRequest.AddParameter("redirect_uri", userSession.RedirectUrl);
      restRequest.AddParameter("state", userSession.Verifier.ToString());

      var response = this.restClient.Execute(restRequest);

      if (response.StatusCode != HttpStatusCode.OK) {
        if (errorAction != null)
          errorAction(response);
        return;
      }

      var combinedToken = JsonConvert.DeserializeObject<dynamic>(response.Content);
      userSession.AccessToken = (string) combinedToken.access_token;
      userSession.RefreshToken = (string) combinedToken.refresh_token;
      userSession.UserId = (string) combinedToken.user_id;
    }

    public void RefreshToken(UserSession userSession, Action<IRestResponse> errorAction=null)
    {
      var restRequest = new RestRequest(this.settings.TokenUrl, Method.POST);
      restRequest.AddHeader("Authorization", this.GetBasicAuthHeader());
      restRequest.AddParameter("grant_type", "refresh_token");
      restRequest.AddParameter("refresh_token", userSession.RefreshToken);

      var response = this.restClient.Execute(restRequest);

      if (response.StatusCode != HttpStatusCode.OK) {
        if (errorAction != null)
          errorAction(response);
        return;
      }

      var combinedToken = JsonConvert.DeserializeObject<dynamic>(response.Content);
      userSession.AccessToken = (string) combinedToken.access_token;
      userSession.RefreshToken = (string) combinedToken.refresh_token;
    }

    private string GetBasicAuthHeader()
    {
      return "Basic " + "{0}:{1}".FormatWith(this.settings.ClientId, this.settings.ClientSecret).Base64Encode();
    }

    public TResponse MakeRequest<TResponse>(string relativeUrl, UserSession userSession, Func<dynamic, TResponse> buildResponse, Action<IRestResponse> errorAction = null)
    {
      var fullUri = new Uri(this.settings.ApiUrl, relativeUrl);
      var restRequest = new RestRequest(fullUri, Method.GET);
      var bearerAuthHeader = "Bearer " + userSession.AccessToken;
      restRequest.AddHeader("Authorization", bearerAuthHeader);

      var response = this.restClient.Execute(restRequest);

      if (response.StatusCode == HttpStatusCode.Unauthorized) {
        this.RefreshToken(userSession);
        return this.MakeRequest(relativeUrl, userSession, buildResponse, errorAction);
      } else if (response.StatusCode != HttpStatusCode.OK) {
        if (errorAction != null)
          errorAction(response);
        return default(TResponse);
      }

      var responseObject = JsonConvert.DeserializeObject<dynamic>(response.Content);
      return buildResponse(responseObject);
    }
  }
}
