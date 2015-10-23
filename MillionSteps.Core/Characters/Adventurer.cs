using System;
using Fitbit.Models;

namespace MillionSteps.Core.Characters
{
  public class Adventurer
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Wisdom { get; set; }
    public int Charisma { get; set; }
    public string UserId { get; set; }
  }
}
