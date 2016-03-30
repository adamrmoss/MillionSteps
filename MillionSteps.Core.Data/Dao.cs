namespace MillionSteps.Core.Data
{
  [UnitWorker]
  public abstract class Dao
  {
    protected Dao(MillionStepsContext dbContext)
    {
      this.dbContext = dbContext;
    }

    protected readonly MillionStepsContext dbContext;
  }
}
