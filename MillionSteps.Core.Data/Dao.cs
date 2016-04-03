namespace MillionSteps.Core.Data
{
  [UnitWorker]
  public abstract class Dao
  {
    protected Dao(MillionStepsDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    protected readonly MillionStepsDbContext dbContext;
  }
}
