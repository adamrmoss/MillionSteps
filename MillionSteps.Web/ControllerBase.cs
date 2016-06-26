using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GuardClaws;
using MillionSteps.Core;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.Configuration;
using MillionSteps.Core.Data;
using MillionSteps.Core.Work;

namespace MillionSteps.Web
{
  [UnitWorker]
  public abstract class ControllerBase : Controller
  {
    protected ControllerBase(Settings settings, ISaveChanges unitOfWork)
    {
      Claws.NotNull(() => settings);
      Claws.NotNull(() => unitOfWork);

      this.settings = settings;
      this.unitOfWork = unitOfWork;
    }

    protected readonly Settings settings;
    protected readonly ISaveChanges unitOfWork;

    protected void setUserSessionCookie(Guid userSessionId)
    {
      var expires = DateTime.UtcNow + UserSession.Lifetime;
      this.setUserSessionCookie(userSessionId, expires);
    }

    protected void clearUserSessionCookie()
    {
      if (this.Request.Cookies.AllKeys.Contains(UserSession.CookieName)) {
        // ReSharper disable once PossibleNullReferenceException
        var userSessionId = Guid.Parse(this.Request.Cookies[UserSession.CookieName].Value);
        var expires = DateTime.UtcNow - TimeSpan.FromDays(1);
        this.setUserSessionCookie(userSessionId, expires);
      }
    }

    private void setUserSessionCookie(Guid userSessionId, DateTime expires)
    {
      var httpCookie = new HttpCookie(UserSession.CookieName, userSessionId.ToString()) {
        Expires = expires
      };
      this.Response.Cookies.Add(httpCookie);
    }

    protected JsonResult buildJsonResult(object data)
    {
      return new JsonResult {
        Data = data,
      };
    }

    protected HttpStatusCodeResult forbiddenResult()
    {
      return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
    }

    protected HttpStatusCodeResult preconditionFailedResult()
    {
      return new HttpStatusCodeResult(HttpStatusCode.PreconditionFailed);
    }

    protected override void OnActionExecuted(ActionExecutedContext filterContext)
    {
      if (filterContext.Exception == null)
        this.unitOfWork.SaveChanges();
    }
  }
}
