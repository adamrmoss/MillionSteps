using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MillionSteps.Core.Authentication;

namespace MillionSteps.Web
{
  public abstract class ControllerBase : Controller
  {
    protected ActionResult BuildJsonResult(object data)
    {
      return new JsonResult {
        Data = data,
      };
    }

    protected void ClearUserSessionCookie()
    {
      if (this.Request.Cookies.AllKeys.Contains(UserSession.CookieName)) {
        // ReSharper disable once PossibleNullReferenceException
        var userSessionId = Guid.Parse(this.Request.Cookies[UserSession.CookieName].Value);
        var httpCookie = new HttpCookie(UserSession.CookieName, userSessionId.ToString()) {
          Expires = DateTime.UtcNow - TimeSpan.FromDays(1)
        };
        this.Response.Cookies.Add(httpCookie);
      }
    }
  }
}
