using Microsoft.AspNetCore.Components.Server.Circuits;

namespace Csla.Blazor
{

  /// <summary>
  /// Blazor Server Circuit Handler for use in identifying for other parts of 
  /// the system whether the current scope is running inside of Blazor Server
  /// </summary>
  public class IdentifyingCircuitHandler : CircuitHandler
  {
    private readonly BlazorIdentification _blazorIdentification;

    /// <summary>
    /// Constructor that accepts and stores the scoped instance of the identification DTO
    /// </summary>
    /// <param name="blazorIdentification">The scoped instance of the identification DTO</param>
    public IdentifyingCircuitHandler(BlazorIdentification blazorIdentification)
    {
      _blazorIdentification = blazorIdentification;
    }

    /// <summary>
    /// Handler for the OnCircuitOpenedAsync call
    /// </summary>
    /// <param name="circuit">The circuit in which we are running</param>
    /// <param name="cancellationToken">The cancellation token provided by the runtime</param>
    public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
      _blazorIdentification.IsServerSideBlazor = true;
      return base.OnCircuitOpenedAsync(circuit, cancellationToken);
    }
  }
}
