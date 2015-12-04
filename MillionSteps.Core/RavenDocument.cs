using System;

namespace MillionSteps.Core
{
  public abstract class RavenDocument<TDocument, TId>
    where TDocument : RavenDocument<TDocument, TId>
  {
    protected RavenDocument(TId documentId)
    {
      this.DocumentId = documentId;
    }

    public string Id => BuildId(this.DocumentId);

    public static string BuildId(TId documentId)
    {
      var collectionName = typeof(TDocument).Name
        .InflectTo().Uncapitalized
        .InflectTo().Pluralized;
      return "{0}/{1}".FormatWith(collectionName, documentId);
    }

    public TId DocumentId { get; }
  }

  public abstract class RavenDocument<TDocument> : RavenDocument<TDocument, string>
    where TDocument : RavenDocument<TDocument, string>
  {
    protected RavenDocument(string documentId)
      : base(documentId)
    { }
  }

  public abstract class GuidRavenDocument<TDocument> : RavenDocument<TDocument, Guid>
    where TDocument : RavenDocument<TDocument, Guid>
  {
    protected GuidRavenDocument(Guid documentId)
      : base(documentId)
    { }
  }
}
