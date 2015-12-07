using System;

namespace MillionSteps.Core
{
  public abstract class RavenDocument<TId>
  {
    protected RavenDocument(TId documentId)
    {
      this.DocumentId = documentId;
    }

    public string Id => this.BuildId(this.DocumentId);

    private string BuildId(TId documentId)
    {
      var collectionName = this.GetType().Name
        .InflectTo().Pluralized;
      return "{0}/{1}".FormatWith(collectionName, documentId);
    }

    public TId DocumentId { get; }
  }

  public abstract class RavenDocument : RavenDocument<string>
  {
    protected RavenDocument(string documentId)
      : base(documentId)
    { }
  }

  public abstract class GuidRavenDocument : RavenDocument<Guid>
  {
    protected GuidRavenDocument(Guid documentId)
      : base(documentId)
    { }
  }
}
