using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MillionSteps.Core.Adventures
{
  public class Moment
  {
    [Key]
    public int Id { get; set; }

    [MaxLength(128)]
    public string EventName { get; set; }

    public int StepsConsumed { get; set; }
    public int Ordinal { get; set; }

    public virtual Adventure Adventure { get; set; }

    private ICollection<MomentFlag> momentFlags;
    public virtual ICollection<MomentFlag> MomentFlags {
      get { return this.momentFlags; }
      set { this.momentFlags = value; }
    }

    public IEnumerable<string> Flags => this.MomentFlags.Select(mf => mf.Flag);

    public Moment()
    {
      this.momentFlags = new List<MomentFlag>();
    }
  }
}
