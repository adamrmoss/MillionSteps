namespace MillionSteps.Core.Authentication
{
  public class UserProfile
  {
    public string UserId { get; set; }
    public string DisplayName { get; set; }
    public int OffsetFromUtcMillis { get; set; }
    public double StrideLengthWalking { get; set; }
  }
}
