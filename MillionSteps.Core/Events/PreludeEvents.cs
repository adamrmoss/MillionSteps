using System.Collections.Generic;
using MillionSteps.Core.Adventures;

namespace MillionSteps.Core.Events
{
  public class StoryStarted : Event
  {
    public override bool CanExecute(FlagDictionary flagDictionary) => true;
    public override bool Automatic => true;
  }

  public class FemalePlayerChosen : Event
  {
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["StoryStarted"] && !flagDictionary["PlayerGenderChosen"];
    public override HashSet<string> FlagsToSet => new HashSet<string> {"PlayerGenderChosen"};
    public override bool SpokenByNarrator => false;
  }

  public class MalePlayerChosen : Event
  {
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["StoryStarted"] && !flagDictionary["PlayerGenderChosen"];
    public override HashSet<string> FlagsToSet => new HashSet<string> {"PlayerGenderChosen"};
    public override bool SpokenByNarrator => false;
  }

  public class YeahStory : Event
  {
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["PlayerGenderChosen"];
    public override bool SpokenByNarrator => false;
    public override bool Automatic => true;
  }

  public class AllCapsStory : Event
  {
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["YeahStory"];
    public override bool SpokenByNarrator => false;
    public override bool Automatic => true;
  }

  public class AgreedToTellStory : Event
  {
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["AllCapsStory"];
    public override HashSet<string> FlagsToSet => new HashSet<string> {"IntroductionFinished"};
    public override bool Automatic => true;
  }
}
