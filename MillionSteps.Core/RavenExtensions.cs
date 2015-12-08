using System;
using Raven.Abstractions.Data;
using Raven.Client;

namespace MillionSteps.Core
{
  public static class RavenExtensions
  {
    public static void CreateIfNew<TDocument, TId>(this IDocumentSession documentSession, TDocument document)
      where TDocument : RavenDocument<TId>
    {
      var existingDocument = documentSession.Load<TDocument>(document.Id);
      if (existingDocument != null)
        documentSession.Advanced.Evict(existingDocument);
      else
        documentSession.Store(document);
    }

    public static void DeleteAll<TDocument>(this IDocumentStore documentStore)
    {
      var collectionName = typeof(TDocument).GetCollectionName();

      documentStore.DatabaseCommands
        .DeleteByIndex("Raven/DocumentsByEntityName",
                       new IndexQuery { Query = "Tag:" + collectionName },
                       new BulkOperationOptions { AllowStale = false, StaleTimeout = TimeSpan.FromMinutes(1) });
    }

    public static string GetCollectionName(this Type type)
    {
      return type.Name
                 .InflectTo().Pluralized;
    }
  }
}
