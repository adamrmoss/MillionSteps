namespace MillionSteps.Core
{
  public abstract class Dao
  {
    protected readonly MillionStepsDbContext DbContext;

    protected Dao(MillionStepsDbContext dbContext)
    {
      this.DbContext = dbContext;
    }
  }
}
