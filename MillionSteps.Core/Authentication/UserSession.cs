using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MillionSteps.Core.Authentication
{
  [Table("UserSession")]
  public class UserSession
  {
    public const string CookieName = "UserSessionId";
    public static readonly TimeSpan Lifetime = TimeSpan.FromDays(30);

    public Guid Id { get; set; }
    public string TempToken { get; set; }
    public string TempSecret { get; set; }
    public string Verifier { get; set; }
    public string Token { get; set; }
    public string Secret { get; set; }
    public string UserId { get; set; }
    public DateTime DateCreated { get; set; }
    public int OffsetFromUtcMillis { get; set; }
  }
}
