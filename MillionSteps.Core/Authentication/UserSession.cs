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

    [MaxLength(128)]
    public string TempToken { get; set; }

    [MaxLength(128)]
    public string TempSecret { get; set; }

    [MaxLength(128)]
    public string Verifier { get; set; }

    [MaxLength(128)]
    public string Token { get; set; }

    [MaxLength(128)]
    public string Secret { get; set; }

    [MaxLength(16)]
    public string UserId { get; set; }

    public DateTime DateCreated { get; set; }
    public int OffsetFromUtcMillis { get; set; }
  }
}
