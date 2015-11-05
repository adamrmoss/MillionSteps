using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
