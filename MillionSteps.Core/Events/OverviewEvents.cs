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
    public override Speaker SpokenBy => Speaker.Narrator;
    public override bool Automatic => true;
  }
}
