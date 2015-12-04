using Raven.Client;

namespace MillionSteps.Core
{
  [UnitWorker]
  public abstract class Dao
  {
    protected readonly IDocumentSession DocumentSession;

    protected Dao(IDocumentSession documentSession)
    {
      this.DocumentSession = documentSession;
    }
  }
}
