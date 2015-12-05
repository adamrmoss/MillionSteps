using System.Collections.Generic;
using MillionSteps.Core.Adventures;

namespace MillionSteps.Core.Events
{
  public class FemalePlayerChosen : Event
  {
    public override string Text => "Mommy, tell me a story!";
    public override bool CanExecute(FlagDictionary flagDictionary) => !flagDictionary["PlayerGenderChosen"];
    public override HashSet<string> FlagsToSet => new HashSet<string> {"PlayerGenderChosen", "PlayerIsFemale"};
  }

  public class MalePlayerChosen : Event
  {
    public override string Text => "Daddy, tell me a story!";
    public override bool CanExecute(FlagDictionary flagDictionary) => !flagDictionary["PlayerGenderChosen"];
    public override HashSet<string> FlagsToSet => new HashSet<string> {"PlayerGenderChosen", "PlayerIsMale" };
  }
}
