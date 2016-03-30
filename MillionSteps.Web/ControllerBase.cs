using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuardClaws;
using MillionSteps.Core;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Data;

namespace MillionSteps.Web
{
  [UnitWorker]
  public abstract class ControllerBase : Controller
  {
    protected ControllerBase(MillionStepsContext dbContext)
    {
      Claws.NotNull(() => dbContext);
      this.dbContext = dbContext;
    }

    protected readonly MillionStepsContext dbContext;

    protected void SetUserSessionCookie(Guid userSessionId)
    {
      var expires = DateTime.UtcNow + UserSession.Lifetime;
      this.SetUserSessionCookie(userSessionId, expires);
    }

    protected void ClearUserSessionCookie()
    {
      if (this.Request.Cookies.AllKeys.Contains(UserSession.CookieName)) {
        // ReSharper disable once PossibleNullReferenceException
        var userSessionId = Guid.Parse(this.Request.Cookies[UserSession.CookieName].Value);
        var expires = DateTime.UtcNow - TimeSpan.FromDays(1);
        this.SetUserSessionCookie(userSessionId, expires);
      }
    }

    private void SetUserSessionCookie(Guid userSessionId, DateTime expires)
    {
      var httpCookie = new HttpCookie(UserSession.CookieName, userSessionId.ToString()) {
        Expires = expires
      };
      this.Response.Cookies.Add(httpCookie);
    }

    protected ActionResult BuildJsonResult(object data)
    {
      return new JsonResult {
        Data = data,
      };
    }

    protected override void OnActionExecuted(ActionExecutedContext filterContext)
    {
      if (filterContext.Exception == null)
        this.dbContext.SaveChanges();
    }
  }
}
