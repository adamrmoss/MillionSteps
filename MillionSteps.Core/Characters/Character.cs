using System;

namespace MillionSteps.Core.Characters
{
  public class Character
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Wisdom { get; set; }
    public int Charisma { get; set; }
  }
}
