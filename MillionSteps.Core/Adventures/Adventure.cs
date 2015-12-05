using System;

namespace MillionSteps.Core.Adventures
{
  public class Adventure : GuidRavenDocument<Adventure>
  {
    public Adventure(Guid documentId)
      : base(documentId)
    { }

    public string UserId { get; set; }
    public DateTime DateCreated { get; set; }
    public Guid CurrentMomentId { get; set; }
  }
}
