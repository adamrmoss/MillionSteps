using System.Collections.Generic;
using System.Linq;
using Fasterflect;

namespace MillionSteps.Core.Events
{
  public class EventDriver
  {
    public EventDriver()
    {
      var eventTypeObject = typeof(Event);
      this.allEvents = eventTypeObject.Assembly.GetTypes()
                                      .Where(type => type.IsSubclassOf(eventTypeObject))
                                      .Select(type => type.CreateInstance())
                                      .Cast<Event>()
                                      .ToList();
    }

    private List<Event> allEvents;
  }
}
