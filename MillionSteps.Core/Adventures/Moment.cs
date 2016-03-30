using System.Collections.Generic;
using System.Linq;

namespace MillionSteps.Core.Adventures
{
  public class Moment
  {
    public int Id { get; set; }
    public string EventName { get; set; }
    public int StepsConsumed { get; set; }
    public int Ordinal { get; set; }

    public virtual Adventure Adventure { get; set; }
    public virtual ICollection<MomentFlag> MomentFlags { get; set; }
    public IEnumerable<string> Flags => this.MomentFlags.Select(mf => mf.Flag);
  }
}
