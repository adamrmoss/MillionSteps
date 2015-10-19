namespace MillionSteps.Core.Adventures
{
  public class AdventureInitialization
  {
    public bool HasAssignedGender { get; set; }
    public bool HasAssignedName { get; set; }
    public bool HasAssignedStrength { get; set; }
    public bool HasAssignedDexterity { get; set; }
    public bool HasAssignedConstitution { get; set; }
    public bool HasAssignedIntelligence { get; set; }
    public bool HasAssignedWisdom { get; set; }
    public bool HasAssignedCharisma { get; set; }

    public bool HasFinishedInitializing { 
      get {
        return this.HasAssignedGender && this.HasAssignedName && this.HasAssignedStrength &&
               this.HasAssignedDexterity && this.HasAssignedConstitution && this.HasAssignedIntelligence &&
               this.HasAssignedWisdom && this.HasAssignedCharisma;
      }
    }
  }
}
