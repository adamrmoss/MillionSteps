using System.Collections.Generic;
using MillionSteps.Core.Adventures;

namespace MillionSteps.Core.Events
{
  public enum Speaker
  {
    Meta,
    Narrator,
    Audience,
  }

  public abstract class Event
  {
    public string Name => this.GetType().Name;
    public abstract string Category { get; }

    public abstract bool CanExecute(FlagDictionary flagDictionary);

    public virtual bool Automatic => false;
    public virtual bool Repeatable => false;
    public virtual Speaker SpokenBy => Speaker.Meta;
    public virtual EventPriority Priority => EventPriority.Medium;
    public virtual int StepsConsumed => 0;

    public virtual HashSet<string> FlagsToSet => new HashSet<string>();
    public virtual HashSet<string> FlagsToClear => new HashSet<string>();
  }

  public enum EventPriority
  {
    Low,
    Medium,
    High,
  }
}
