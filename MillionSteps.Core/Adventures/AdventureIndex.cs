using System.Linq;
using Raven.Client.Indexes;

namespace MillionSteps.Core.Adventures
{
  public class AdventureIndex : AbstractIndexCreationTask<Adventure>
  {
    public AdventureIndex()
    {
      this.Map = adventures => from adventure in adventures
                               select new {
                                 adventure.UserId,
                               };
    }
  }
}
