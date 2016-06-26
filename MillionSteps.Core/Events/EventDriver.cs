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
      var automaticEvents = this.allEvents.Where(@event => @event.Automatic && eventCanExecute(@event, flagDictionary)).ToList();
      if (automaticEvents.Any())
        return new List<Event> { automaticEvents.First() };

      return this.allEvents.Where(@event => eventCanExecute(@event, flagDictionary)).ToList();
    }

    private static bool eventCanExecute(Event @event, FlagDictionary flagDictionary)
    {
      return (@event.Repeatable || !flagDictionary[@event.Name]) && @event.CanExecute(flagDictionary);
    }

    public Event LookupEvent(string eventName)
    {
      return this.allEvents.SingleOrDefault(e => e.Name == eventName);
    }
  }
}
