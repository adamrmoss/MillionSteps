using MillionSteps.Core.Adventures;

namespace MillionSteps.Core.Events
{
  public class VillageDestroyed : Event
  {
    public override string Category => "Departure";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["OverviewFinished"];
    public override Speaker SpokenBy => Speaker.Narrator;
    public override bool Automatic => true;
  }

  public class StayOffRoads : Event
  {
    public override string Category => "Departure";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["VillageDestroyed"];
    public override Speaker SpokenBy => Speaker.Narrator;
    public override bool Automatic => true;
  }
}
