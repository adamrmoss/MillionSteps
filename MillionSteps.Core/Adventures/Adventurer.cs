using System;

namespace MillionSteps.Core.Adventures
{
  public enum Gender
  {
    Male,
    Female,
  }

  public class Adventurer
  {
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public DateTime DateCreated { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Wisdom { get; set; }
    public int Charisma { get; set; }
  }
}
