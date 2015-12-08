using System.Collections.Generic;
using MillionSteps.Core.Adventures;

namespace MillionSteps.Core.Events
{
  public class StoryStarted : Event
  {
    public override string Text => "Hello, my little ones!";
    public override bool CanExecute(FlagDictionary flagDictionary) => true;
    public override bool Automatic => true;
  }

  public class FemalePlayerChosen : Event
  {
    public override string Text => "Mommy, tell us a story!";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["StoryStarted"] && !flagDictionary["PlayerGenderChosen"];
    public override HashSet<string> FlagsToSet => new HashSet<string> {"PlayerGenderChosen"};
    public override bool SpokenByNarrator => false;
  }

  public class MalePlayerChosen : Event
  {
    public override string Text => "Daddy, tell us a story!";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["StoryStarted"] && !flagDictionary["PlayerGenderChosen"];
    public override HashSet<string> FlagsToSet => new HashSet<string> {"PlayerGenderChosen"};
    public override bool SpokenByNarrator => false;
  }

  public class YeahStory : Event
  {
    public override string Text => "Yeah, story!";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["PlayerGenderChosen"];
    public override bool SpokenByNarrator => false;
    public override bool Automatic => true;
  }

  public class AllCapsStory : Event
  {
    public override string Text => "STORY!!!!";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["YeahStory"];
    public override bool SpokenByNarrator => false;
    public override bool Automatic => true;
  }

  public class AgreedToTellStory : Event
  {
    public override string Text => "Okay, kids.  I'll tell you the story of my great adventure!";
    public override bool CanExecute(FlagDictionary flagDictionary) => flagDictionary["AllCapsStory"];
    public override bool Automatic => true;
  }
}
