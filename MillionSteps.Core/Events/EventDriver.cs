using System.Collections.Generic;
using System.Linq;
using Fasterflect;
using MillionSteps.Core.Adventures;

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

    private readonly List<Event> allEvents;

    public List<Event> GetValidEvents(FlagDictionary flagDictionary)
    {
      return this.allEvents.Where(@event => EventCanExecute(@event, flagDictionary)).ToList();
    }

    private static bool EventCanExecute(Event @event, FlagDictionary flagDictionary)
    {
      return (@event.Repeatable || !flagDictionary[@event.Name]) && @event.CanExecute(flagDictionary);
    }

    public Event LookupEvent(string eventName)
    {
      return this.allEvents.SingleOrDefault(e => e.Name == eventName);
    }
  }
}
