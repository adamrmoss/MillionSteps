using System;
using System.ComponentModel.DataAnnotations;

namespace MillionSteps.Core.Authentication
{
  public class UserSession
  {
    public const string CookieName = "UserSessionId";
    public static readonly TimeSpan Lifetime = TimeSpan.FromDays(30);

    [Key]
    public Guid Id { get; set; }

    public Guid Verifier { get; set; }

    [MaxLength(128)]
    public string AccessToken { get; set; }

    [MaxLength(128)]
    public string RefreshToken { get; set; }

    [MaxLength(16)]
    public string UserId { get; set; }

    public DateTime DateCreated { get; set; }
    public int OffsetFromUtcMillis { get; set; }
  }
}
