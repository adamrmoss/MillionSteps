using System.Collections.Generic;
using MillionSteps.Core.Adventures;

namespace MillionSteps.Core.Events
{
  public class StoryStarted : Event
  {
    public override string Category => "Prelude";
    public override bool CanExecute(FlagDictionary flagDictionary) => true;
    public override bool Automatic => true;
    public override Speaker SpokenBy => Speaker.Narrator;
  }

  public class FemalePlayerChosen : Event
  {
    public override string Category => "Prelude";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["StoryStarted"] && !flagDictionary["PlayerGenderChosen"];
    public override HashSet<string> FlagsToSet => new HashSet<string> {"PlayerGenderChosen"};
    public override Speaker SpokenBy => Speaker.Audience;
  }

  public class MalePlayerChosen : Event
  {
    public override string Category => "Prelude";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["StoryStarted"] && !flagDictionary["PlayerGenderChosen"];
    public override HashSet<string> FlagsToSet => new HashSet<string> {"PlayerGenderChosen"};
    public override Speaker SpokenBy => Speaker.Audience;
  }

  public class YeahStory : Event
  {
    public override string Category => "Prelude";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["PlayerGenderChosen"];
    public override Speaker SpokenBy => Speaker.Audience;
    public override bool Automatic => true;
  }

  public class AllCapsStory : Event
  {
    public override string Category => "Prelude";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["YeahStory"];
    public override Speaker SpokenBy => Speaker.Audience;
    public override bool Automatic => true;
  }

  public class AgreedToTellStory : Event
  {
    public override string Category => "Prelude";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["AllCapsStory"];
    public override HashSet<string> FlagsToSet => new HashSet<string> {"IntroductionFinished"};
    public override Speaker SpokenBy => Speaker.Narrator;
    public override bool Automatic => true;
  }
}
