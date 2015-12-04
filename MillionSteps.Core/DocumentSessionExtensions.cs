using Raven.Client;

namespace MillionSteps.Core
{
  public static class DocumentSessionExtensions
  {
    public static void CreateIfNew<TDocument, TId>(this IDocumentSession documentSession, RavenDocument<TDocument, TId> document)
      where TDocument : RavenDocument<TDocument, TId>
    {
      var existingDocument = documentSession.Load<TDocument>(document.Id);
      if (existingDocument != null)
        documentSession.Advanced.Evict(existingDocument);
      else
        documentSession.Store(document);
    }
  }
}
