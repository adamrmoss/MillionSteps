using Raven.Client;

namespace MillionSteps.Core
{
  [UnitWorker]
  public abstract class Dao
  {
    protected Dao(IDocumentSession documentSession)
    {
      this.DocumentSession = documentSession;
    }

    protected readonly IDocumentSession DocumentSession;
  }
}
