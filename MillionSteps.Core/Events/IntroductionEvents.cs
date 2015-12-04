using System;
using System.Collections.Generic;

namespace MillionSteps.Core.Events
{
  public static class IntroductionEvents
  {
    public static List<Event> GetAll() => new List<Event> {FemalePlayerChosen(), MalePlayerChosen()}; 

    public static Event FemalePlayerChosen() => new Event(new Guid("a74fe02a-29ba-4a05-8976-20b708be29ce")) {
      Name = "FemalePlayerChosen",
      Text = "Mommy, tell me a story!",
      FlagsToSet = new HashSet<string> { "PlayerGenderChosen" },
    };

    public static Event MalePlayerChosen() => new Event(new Guid("a74fe02a-29ba-4a05-8976-20b708be29ce")) {
      Name = "MalePlayerChosen",
      Text = "Daddy, tell me a story!",
      FlagsToSet = new HashSet<string> { "PlayerGenderChosen" },
    };
  }
}
