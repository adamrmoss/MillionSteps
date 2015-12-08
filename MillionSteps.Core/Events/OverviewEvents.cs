using MillionSteps.Core.Adventures;

namespace MillionSteps.Core.Events
{
  public class Origin : Event
  {
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["IntroductionFinished"];
    public override bool Automatic => true;
  }

  public class OriginConfirmation : Event
  {
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["Origin"];
    public override bool SpokenByNarrator => false;
    public override bool Automatic => true;
  }
}
