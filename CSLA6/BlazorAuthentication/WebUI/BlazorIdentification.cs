namespace Csla.Blazor
{
  /// <summary>
  /// DTO that is used to retain and expose whether we are running inside of SSB
  /// </summary>
  public class BlazorIdentification
  {

    /// <summary>
    /// Gets and sets whether we are running inside of Server-side Blazor
    /// </summary>
    public bool IsServerSideBlazor { get; set; } = false;
  }
}
