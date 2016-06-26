using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillionSteps.Core.Authentication;
using MillionSteps.Core.OAuth2;
using MillionSteps.Core.Work;

namespace MillionSteps.Core.Exercises
{
  [UnitWorker]
  public class ActivityLogClient : AuthenticatedClientBase
  {
    public ActivityLogClient(OAuth2Client oAuth2Client, UserSession userSession)
      : base(oAuth2Client, userSession)
    { }

    public ActivityLogEntry GetActivityLogEntry(DateTime date)
    {
      if (this.userSession == null)
        return null;

      var url = "1/user/-/activities/date/{0}.json".FormatWith(date.ToString("yyyy-MM-dd"));
      var activityLogEntry = this.oAuth2Client.MakeRequest(url, this.userSession, json => this.BuildActivityLogEntry(json, date));
      return activityLogEntry;
    }

    public ActivityLogEntry BuildActivityLogEntry(dynamic json, DateTime date)
    {
      return new ActivityLogEntry {
        UserId = this.userSession.UserId,
        Date = date,
        Steps = json.summary.steps,
      };
    }
  }
}
