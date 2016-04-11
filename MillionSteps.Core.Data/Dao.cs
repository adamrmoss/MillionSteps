using MillionSteps.Core.Work;

namespace MillionSteps.Core.Data
{
  [UnitWorker]
  public abstract class Dao
  {
    protected Dao(MillionStepsDbContext dbContext)
    {
      this.DbContext = dbContext;
    }

    protected readonly MillionStepsDbContext DbContext;
  }
}
