using System.Collections.Generic;
using MillionSteps.Core.Adventures;

namespace MillionSteps.Core.Events
{
  public class Origin : Event
  {
    public override string Category => "Overview";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["IntroductionFinished"];
    public override Speaker SpokenBy => Speaker.Narrator;
    public override bool Automatic => true;
  }

  public class OriginConfirmation : Event
  {
    public override string Category => "Overview";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["Origin"];
    public override Speaker SpokenBy => Speaker.Audience;
    public override bool Automatic => true;
  }

  public class CameOnFoot : Event
  {
    public override string Category => "Overview";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["OriginConfirmation"];
    public override Speaker SpokenBy => Speaker.Narrator;
    public override bool Automatic => true;
  }

  public class Counted : Event
  {
    public override string Category => "Overview";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["CameOnFoot"];
    public override Speaker SpokenBy => Speaker.Narrator;
    public override bool Automatic => true;
  }

  public class OverviewFinished : Event
  {
    public override string Category => "Overview";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["Counted"];
    public override Speaker SpokenBy => Speaker.Narrator;
    public override bool Automatic => true;
  }
}
